using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Domain.Services;
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
        private readonly ITransactionService transactionService;

        public CreateTransactionHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<TransactionDto?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await this.transactionService.CreateAsync(request);
        }
    }
}
