using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos.Queries
{
    public class GetStatementQuery : IRequest<GetStatementQueryResult>
    {
        public string CustomerDocument { get; set; } = null!;

        public string AgencyNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class GetStatementQueryResult
    {
        public CustomerDto Customer { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;

        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
