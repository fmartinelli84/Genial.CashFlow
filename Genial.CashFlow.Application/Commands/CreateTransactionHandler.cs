using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Commands
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, TransactionDto?>
    {
        public CreateTransactionHandler()
        {
        }

        public async Task<TransactionDto?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
