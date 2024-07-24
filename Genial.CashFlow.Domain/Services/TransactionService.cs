using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery query)
        {
            return await this.transactionRepository.GetStatementAsync(query);
        }

        public async Task<TransactionDto> CreateAsync(CreateTransactionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
