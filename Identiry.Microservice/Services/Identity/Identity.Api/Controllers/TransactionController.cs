
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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        
        public TransactionController(ITransactionService transactionService)
        {
           _transactionService=transactionService;
        }

     
        [HttpPost("addTransaction")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddTransaction([FromBody] TransactionRequest TransactionRequest)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Transactiondto = TransactionRequest.Adapt<TransactionDto>();
            Transactiondto.UserId =Guid.Parse(userid);
            var Transaction =await _transactionService.AddTransactionAsync(Transactiondto);
            var TransactionResponse = Transaction.Adapt<TransactionResponse>();
            return Ok(TransactionResponse);
        }
      
        [HttpGet("getTransactionById")]
        [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetTransactionById([FromQuery] Guid Id)
        {
            var Transaction = await _transactionService.GetTransactionByIdAsync(Id);
            var TransactionResponse=Transaction.Adapt<TransactionResponse>();
            return Transaction == null ? NotFound() : Ok(TransactionResponse);
        }

        
        [HttpGet("getAllTransactions")]
        [ProducesResponseType(typeof(TransactionResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllTransactions()
        {
            var userid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var TransactionList = await _transactionService.GetAllTransactionsAsync(userid);
            var TransactionResponse = TransactionList.Adapt<TransactionResponse[]>();
            return TransactionList == null ? NotFound() : Ok(TransactionResponse);
        }
      
        [HttpDelete("deleteTransaction")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTransaction([FromQuery] Guid Id)
        {
            await _transactionService.DeleteTransactionAsync(Id);
            return Ok("Transaction deleted");
        }
        [AllowAnonymous]
        [HttpGet("searchTransactions")]
        [ProducesResponseType(typeof(TransactionResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SearchTransactions(string key)
        {
            var userid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var TransactionList = await _transactionService.SearchTransactionsAsync(userid,key);
            var TransactionResponse = TransactionList.Adapt<TransactionResponse[]>();
            return TransactionList == null ? NotFound() : Ok(TransactionResponse);
        }

    }
}
