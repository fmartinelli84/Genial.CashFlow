using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public TransactionRepository()
        {
        }

        public async Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
