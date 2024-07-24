using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Domain.Services
{
    public interface ITransactionService
    {
        Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery query);
        Task<TransactionDto> CreateAsync(CreateTransactionCommand command);
    }
}