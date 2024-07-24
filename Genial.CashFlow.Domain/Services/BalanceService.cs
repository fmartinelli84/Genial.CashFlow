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
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            this.balanceRepository = balanceRepository;
        }

        public async Task<GetBalanceQueryResult> GetAsync(GetBalanceQuery query)
        {
            return await this.balanceRepository.GetAsync(query);
        }

        public async Task UpdateFromTransactionAsync(TransactionDto transaction)
        {

        }
    }
}
