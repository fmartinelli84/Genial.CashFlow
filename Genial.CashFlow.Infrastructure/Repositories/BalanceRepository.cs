using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        public BalanceRepository()
        {
        }

        public async Task<GetBalanceQueryResult> GetAsync(GetBalanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
