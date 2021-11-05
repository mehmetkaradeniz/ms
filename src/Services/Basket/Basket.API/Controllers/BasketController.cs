using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _repository;
        private readonly IDiscountService _discountService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, IDiscountService discountService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{username}", Name = "Get")]
        [ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketEntity>> Get(string username)
        {
            var basket = await _repository.Get(username);
            return Ok(basket ?? new BasketEntity(username));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketEntity>> Update(BasketEntity basket)
        {
            foreach (var item in basket.Items)
            {
                // Performance is important here (for each item we communicate with discount.grpc service)
                // So it is important that we use postgres with dapper
                var coupon = await _discountService.Get(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.Update(basket));
        }

        [HttpDelete("{username}", Name = "Delete")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string username)
        {
            await _repository.Delete(username);
            return Ok();
        }


        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckoutEntity basketCheckout)
        {
            // get existing basket by userame
            var basket = await _repository.Get(basketCheckout.Username);
            if (basket == null)
                return BadRequest();

            // create basketcheckoutevent and  set total price of checkout event
            var basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

            // send checkout event to rabbitmq
            basketCheckoutEvent.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(basketCheckoutEvent);

            // delete basket
            await _repository.Delete(basketCheckout.Username);

            return Accepted();
        }

    }
}
