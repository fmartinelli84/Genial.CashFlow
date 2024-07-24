using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public interface IBalanceRepository
    {
        Task<GetBalanceQueryResult> GetAsync(GetBalanceQuery query);
    }
}