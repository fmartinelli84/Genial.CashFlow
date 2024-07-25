using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Infrastructure.Repositories;
using Genial.Framework.Exceptions;
using Genial.Framework.Serialization;
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

        public async Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery request)
        {
            request.CustomerDocument = request.CustomerDocument.OnlyNumbers()?.PadLeft(11, '0')!;
            request.AgencyNumber = request.AgencyNumber.OnlyNumbers()?.PadLeft(4, '0')!;
            request.AccountNumber = request.AccountNumber.OnlyNumbers()?.PadLeft(6, '0')!;

            if (request.StartDate is not null)
                request.StartDate = request.StartDate.Value.Date;

            if (request.EndDate is not null)
                request.EndDate = request.EndDate.Value.Date.Add(new TimeSpan(23, 59, 59));


            var result = await this.transactionRepository.GetStatementAsync(request);


            if (result.Customer is null)
                throw new NotFoundBusinessException("Cliente não encontrado.");

            if (result.Account is null)
                throw new NotFoundBusinessException("Conta Corrente não encontrada para esse cliente.");

            return result;
        }

        public async Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request)
        {
            request.CustomerDocument = request.CustomerDocument.OnlyNumbers()?.PadLeft(11, '0')!;
            request.AgencyNumber = request.AgencyNumber.OnlyNumbers()?.PadLeft(4, '0')!;
            request.AccountNumber = request.AccountNumber.OnlyNumbers()?.PadLeft(6, '0')!;


            var result = await this.transactionRepository.GetBalanceAsync(request);


            if (result.Customer is null)
                throw new NotFoundBusinessException("Cliente não encontrado.");

            if (result.Account is null)
                throw new NotFoundBusinessException("Conta Corrente não encontrada para esse cliente.");

            return result;
        }


        public async Task<TransactionDto> CreateAsync(CreateTransactionCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
