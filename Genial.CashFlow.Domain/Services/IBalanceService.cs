using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Domain.Services
{
    public interface IBalanceService
    {
        Task<GetBalanceQueryResult> GetAsync(GetBalanceQuery query);
        Task UpdateFromTransactionAsync(TransactionDto transaction);
    }
}