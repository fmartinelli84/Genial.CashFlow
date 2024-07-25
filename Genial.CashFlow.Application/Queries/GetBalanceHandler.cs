using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Domain.Services;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using MediatR;

namespace Genial.CashFlow.Application.Queries
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, GetBalanceQueryResult>
    {
        private readonly ITransactionService transactionService;

        public GetBalanceHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<GetBalanceQueryResult> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            return await this.transactionService.GetBalanceAsync(request);
        }
    }
}
