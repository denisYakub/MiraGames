using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraGames.Server.Entities.DTOs;
using MiraGames.Server.Entities.Enums;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController(IUserService user, IRateService rate, IPaymentService payment) 
        : ControllerBase
    {
        [Authorize]
        [HttpGet("ping/auth")]
        public IActionResult GetPing()
        {

            if (User.FindFirst("userId") == null)
                return new UnauthorizedObjectResult("First you need to login");

            return new OkResult();
        }

        [HttpPost("auth/login")]
        public IActionResult Login([FromBody] NewUserRequest request)
        {
            var result = user.CreateUserAndToken(request.Email, request.Password);

            return new OkObjectResult(new { result });
        }

        [Authorize]
        [HttpGet("clients")]
        public IActionResult GetClients()
        {
            var result = user.GetAll();

            return new OkObjectResult(new { result });
        }

        [Authorize]
        [HttpGet("payments")]
        public IActionResult GetNPayments([FromQuery] int take = 5)
        {
            var userId = User.FindFirst("userId");
            
            if(userId == null)
                return new UnauthorizedObjectResult("First you need to login");

            var result = payment.GetPayments(Guid.Parse(userId.Value), take);

            return new OkObjectResult( new { result });
        }

        [Authorize]
        [HttpGet("rate")]
        public IActionResult GetRate([FromQuery] Currencies currency = Currencies.Ruble)
        {
            var result = rate.Get(currency);

            return new OkObjectResult(new { result });
        }

        [Authorize]
        [HttpPost("rate")]
        public IActionResult UpdateRate([FromBody] CurrencyUpdateRequest request)
        {
            rate.Update(request.Currency, request.NewValue);

            return new OkResult();
        }
    }
}
