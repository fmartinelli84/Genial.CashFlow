using Azure.Core;
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.CashFlow.Infrastructure.Repositories;
using Genial.Framework.Exceptions;
using Genial.Framework.Serialization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<(bool CustomerExists, bool AccountExists)> ExistsAsync(AccountIdentificationParameterDto parameter)
        {
            this.ValidateIdentificationParameter(parameter);

            return await this.accountRepository.ExistsAsync(parameter);
        }

        public void ValidateIdentificationParameter(AccountIdentificationParameterDto parameter)
        {
            if (parameter is null)
                throw new BusinessException("Informe a identificação da conta corrente.");

            if (string.IsNullOrWhiteSpace(parameter.CustomerDocument))
                throw new BusinessException("Informe o documento do cliente.");
            if (string.IsNullOrWhiteSpace(parameter.AgencyNumber))
                throw new BusinessException("Informe o número da agência.");
            if (string.IsNullOrWhiteSpace(parameter.AccountNumber))
                throw new BusinessException("Informe o número da conta corrente.");

            parameter.CustomerDocument = parameter.CustomerDocument.OnlyNumbers()?.PadLeft(11, '0')!;
            parameter.AgencyNumber = parameter.AgencyNumber.OnlyNumbers()?.PadLeft(4, '0')!;
            parameter.AccountNumber = parameter.AccountNumber.OnlyNumbers()?.PadLeft(6, '0')!;
        }

        public void ValidateIdentificationResult(AccountIdentificationResultDto? result)
        {
            this.ValidateIdentificationResult((result?.Customer is not null, result?.Account is not null));
        }

        public void ValidateIdentificationResult((bool CustomerExists, bool AccountExists) result)
        {
            if (!result.CustomerExists)
                throw new NotFoundBusinessException("Cliente não encontrado.");

            if (!result.AccountExists)
                throw new NotFoundBusinessException("Conta corrente não encontrada para esse cliente.");
        }
    }
}
