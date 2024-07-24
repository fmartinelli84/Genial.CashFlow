
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Genial.CashFlow.Api.Controllers
{
    [ApiController]
    [Route("cashflow")]
    public class CashFlowController : ControllerBase
    {
        [HttpGet("statement")]
        public async Task<ActionResult<GetStatementQueryResult>> GetStatementAsync(
            [FromRoute] GetStatementQuery query, 
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(query);
        }

        [HttpGet("balance")]
        public async Task<ActionResult<GetBalanceQueryResult>> GetBalanceAsync(
            [FromRoute] GetBalanceQuery query,
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(query);
        }

        [HttpPost("transactions")]
        public async Task<ActionResult<TransactionDto?>> CreateTransactionAsync(
            [FromBody] CreateTransactionCommand command,
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
        }
    }
}
