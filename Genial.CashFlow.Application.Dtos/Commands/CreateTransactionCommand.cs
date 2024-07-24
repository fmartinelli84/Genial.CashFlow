using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos.Commands
{
    public class CreateTransactionCommand : IRequest<TransactionDto?>
    {
        public string CustomerDocument { get; set; } = null!;

        public string AgencyNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;

        public TransactionType Type { get; set; }
        public string Description { get; set; } = null!;
        public decimal Value { get; set; }
    }
}
