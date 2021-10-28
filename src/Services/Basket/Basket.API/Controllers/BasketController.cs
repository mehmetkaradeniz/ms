using Basket.API.Entities;
using Basket.API.Repositories;
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

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
            return Ok(await _repository.Update(basket));
        }

        [HttpDelete("{username}", Name = "Delete")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string username)
        {
            await _repository.Delete(username);
            return Ok();
        }

    }
}
