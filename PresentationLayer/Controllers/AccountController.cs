using ApplicationLayer.DTOs;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly MoneyTransferService _moneyTransferService;

        public AccountController(AccountService accountService, MoneyTransferService moneyTransferService)
        {
            _accountService = accountService;
            _moneyTransferService = moneyTransferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_accountService.GetAllAccounts(userId));
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] CreateUserAccountDTO request)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _accountService.CreateAccount(UserId, request);
            return Ok();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _moneyTransferService.TransferAsync(request, UserId);
                return Ok("Transfer successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
