using Azure.Core;
using Dapper;
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Commands;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using Genial.Framework.Serialization;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Genial.CashFlow.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext dapperContext;

        public AccountRepository(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<(bool CustomerExists, bool AccountExists)> ExistsAsync(AccountIdentificationParameterDto parameter)
        {
            using var connection = this.dapperContext.CreateConnection();

            var sql = @"
            SELECT 
                c.[Id]
            FROM [Customers] AS c
            WHERE
                c.[Document] = @CustomerDocument;

            SELECT 
                a.[Id]
            FROM [Customers] AS c 
            INNER JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber;";


            var param = parameter;

            using var gridReader = await connection.QueryMultipleAsync(sql, param);

            var result = 
            (
                (await gridReader.ReadFirstOrDefaultAsync<CustomerDto>()) is not null,
                (await gridReader.ReadFirstOrDefaultAsync<AccountDto>()) is not null
            );

            return result;
        }
    }
}
