using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "Get")]
        [ProducesResponseType(typeof(CouponEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CouponEntity>> Get(string productName)
        {
            var coupon = await _repository.Get(productName);
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CouponEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CouponEntity>> Create([FromBody] CouponEntity coupon)
        {
            await _repository.Create(coupon);
            return CreatedAtRoute("Get", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CouponEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CouponEntity>> Update([FromBody] CouponEntity coupon)
        {
            return Ok(await _repository.Update(coupon));
        }

        [HttpDelete("{productName}", Name = "Delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Delete(string productName)
        {
            return Ok(await _repository.Delete(productName));
        }
    }
}
