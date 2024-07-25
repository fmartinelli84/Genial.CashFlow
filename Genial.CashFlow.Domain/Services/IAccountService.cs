using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Domain.Services
{
    public interface IAccountService
    {
        Task<(bool CustomerExists, bool AccountExists)> ExistsAsync(AccountIdentificationParameterDto parameter);

        void ValidateIdentificationParameter(AccountIdentificationParameterDto parameter);
        void ValidateIdentificationResult(AccountIdentificationResultDto result);
        void ValidateIdentificationResult((bool CustomerExists, bool AccountExists) result);
    }
}