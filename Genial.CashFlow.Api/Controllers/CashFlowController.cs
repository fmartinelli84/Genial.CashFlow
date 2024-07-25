
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
            [FromQuery] GetStatementQuery request, 
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(request);
        }

        [HttpGet("balance")]
        public async Task<ActionResult<GetBalanceQueryResult>> GetBalanceAsync(
            [FromQuery] GetBalanceQuery request,
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(request);
        }

        [HttpPost("transactions")]
        public async Task<ActionResult<TransactionDto?>> CreateTransactionAsync(
            [FromBody] CreateTransactionCommand request,
            [FromServices] IMediator mediator)
        {
            return this.Created((string?)null, await mediator.Send(request));
        }
    }
}
