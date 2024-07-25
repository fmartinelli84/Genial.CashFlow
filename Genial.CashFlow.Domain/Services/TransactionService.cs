using Azure.Core;
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Infrastructure.Repositories;
using Genial.Framework.Exceptions;
using Genial.Framework.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountService accountService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService)
        {
            this.transactionRepository = transactionRepository;
            this.accountService = accountService;
        }

        public async Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery request)
        {
            this.accountService.ValidateIdentificationParameter(request);

            if (request.StartDate is not null)
                request.StartDate = request.StartDate.Value.Date;

            if (request.EndDate is not null)
                request.EndDate = request.EndDate.Value.Date.Add(new TimeSpan(23, 59, 59));

            if (request.StartDate is not null && 
                request.EndDate is not null && 
                request.StartDate > request.EndDate)
                throw new BusinessException("A data inicial deve ser anterior a data final.");


            var result = await this.transactionRepository.GetStatementAsync(request);


            this.accountService.ValidateIdentificationResult(result);

            return result;
        }

        public async Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request)
        {
            this.accountService.ValidateIdentificationParameter(request);


            var result = await this.transactionRepository.GetBalanceAsync(request);


            this.accountService.ValidateIdentificationResult(result);

            return result;
        }

        public async Task<TransactionDto> CreateAsync(CreateTransactionCommand request)
        {
            var accountExistsResult = await this.accountService.ExistsAsync(request);

            this.accountService.ValidateIdentificationResult(accountExistsResult);

            if (request.Type != TransactionType.Debit && request.Type != TransactionType.Credit)
                throw new BusinessException($"O tipo da transação deve ser {TransactionType.Debit:d} para débito ou {TransactionType.Credit:d} para crédito.");

            if (string.IsNullOrWhiteSpace(request.Description))
                throw new BusinessException("Informe a descrição da transação.");

            if (request.Value <= 0)
                throw new BusinessException("O valor da transação deve ser maior que zero.");


            var result = await this.transactionRepository.CreateAsync(request);


            return result;
        }
    }
}
