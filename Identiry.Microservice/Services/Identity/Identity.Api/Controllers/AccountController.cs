
using ManageMoney.Api.ViewModels.Requests;
using ManageMoney.Api.ViewModels.Responses;
using ManageMoney.Application.Services;
using ManageMoney.Data.Dtos;
using ManageMoney.Data.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace ManageMoney.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        
        public AccountController(IAccountService accountService)
        {
           _accountService=accountService;
        }

     
        [HttpPost("addAccount")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAccount([FromBody] AccountRequest accountRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountdto = accountRequest.Adapt<AccountDto>();
            accountdto.UserId =Guid.Parse(userid);
            var account =await _accountService.AddAccountAsync(accountdto);
            var accountResponse = account.Adapt<AccountResponse>();
           // return Ok(accountResponse);
            return Ok(accountResponse);
        }
      
        [HttpGet("getAccountById")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAccountById([FromQuery] Guid Id)
        {
            var account = await _accountService.GetAccountByIdAsync(Id);
            if (account == null)
            {
                return NotFound();
            }
            var accountResponse=account.Adapt<AccountResponse>();
            return Ok(accountResponse);
        }

        
        [HttpGet("getAllAccounts")]
        [ProducesResponseType(typeof(AccountResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllAccounts()
        {
            var accountList = await _accountService.GetAllAccountsAsync();
            var accountResponse = accountList.Adapt<AccountResponse[]>();
            return Ok(accountResponse);
        }

     
        [HttpPut("updateAccount")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAccount([FromBody] AccountUpdateDto accountUpdate)
        {
            await _accountService.UpdateAccountAsync(accountUpdate);
            return Ok("Account updated successfully"); 
        }

       
        [HttpDelete("deleteAccount")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAccount([FromQuery] Guid Id)
        {
            var existingItem =_accountService.GetAccountByIdAsync(Id);
            if (existingItem.Result == null)
            {
                return NotFound();
            }
            await _accountService.DeleteAccountAsync(Id);
            return NoContent();
        }

    }
}
