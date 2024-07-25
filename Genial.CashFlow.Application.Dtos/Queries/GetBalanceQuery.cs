using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos.Queries
{
    public class GetBalanceQuery : IRequest<GetBalanceQueryResult>
    {
        public string CustomerDocument { get; set; } = null!;

        public string AgencyNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
    }

    public class GetBalanceQueryResult
    {
        public CustomerDto Customer { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;

        public decimal BalanceValue { get; set; }
    }
}
