using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Domain.Services;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using MediatR;

namespace Genial.CashFlow.Application.Queries
{
    public class GetStatementHandler : IRequestHandler<GetStatementQuery, GetStatementQueryResult>
    {
        private readonly ITransactionService transactionService;

        public GetStatementHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<GetStatementQueryResult> Handle(GetStatementQuery request, CancellationToken cancellationToken)
        {
            return await this.transactionService.GetStatementAsync(request);
        }
    }
}
