﻿using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Task<(bool CustomerExists, bool AccountExists)> ExistsAsync(AccountIdentificationParameterDto parameter);
    }
}