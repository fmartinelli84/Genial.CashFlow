using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task<TransactionDto?> GetByIdAsync(long id);
        Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery request);
        Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request);
        Task<TransactionDto> CreateAsync(CreateTransactionCommand request);
    }
}