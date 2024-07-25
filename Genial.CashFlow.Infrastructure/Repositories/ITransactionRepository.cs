using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery request);
        Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request);

    }
}