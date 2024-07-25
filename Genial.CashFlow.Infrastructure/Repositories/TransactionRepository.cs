using Dapper;
using Genial.CashFlow.Application.Dtos;
using Genial.CashFlow.Application.Dtos.Queries;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using Genial.Framework.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DapperContext dapperContext;

        public TransactionRepository(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<GetStatementQueryResult> GetStatementAsync(GetStatementQuery request)
        {
            using var connection = this.dapperContext.CreateConnection();

            var sql = @"
            SELECT 
                c.[Id], 
                c.[Name],
                c.[Document]
            FROM [Customers] AS c
            WHERE
                c.[Document] = @CustomerDocument;

            SELECT 
                a.[Id], 
                a.[AgencyNumber],
                a.[Number]
            FROM [Customers] AS c 
            INNER JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber;

            SELECT 
                t.[Id],
                t.[Date],
                t.[Type],
                t.[Description],
                t.[Value],
                t.[BalanceValue]
            FROM [Customers] AS c 
            INNER JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
            INNER JOIN [Transactions] AS t ON t.[AccountId] = a.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber";


            if (request.StartDate is not null)
                sql += " AND t.[Date] >= @StartDate";

            if (request.EndDate is not null)
                sql += " AND t.[Date] <= @EndDate";


            using var gridReader = await connection.QueryMultipleAsync(sql, request);

            var result = new GetStatementQueryResult()
            {
                Customer = (await gridReader.ReadFirstOrDefaultAsync<CustomerDto>())!,
                Account = (await gridReader.ReadFirstOrDefaultAsync<AccountDto>())!,
                Transactions = (await gridReader.ReadAsync<TransactionDto>()).ToList()
            };

            return result;
        }

        public async Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request)
        {
            using var connection = this.dapperContext.CreateConnection();

            var sql = @"
            SELECT 
                c.[Id], 
                c.[Name],
                c.[Document]
            FROM [Customers] AS c
            WHERE
                c.[Document] = @CustomerDocument;

            SELECT 
                a.[Id], 
                a.[AgencyNumber],
                a.[Number]
            FROM [Customers] AS c 
            INNER JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber;

            SELECT TOP 1
                t.[Id],
                t.[BalanceValue]
            FROM [Customers] AS c 
            INNER JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
            INNER JOIN [Transactions] AS t ON t.[AccountId] = a.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber
            ORDER BY t.[Id] DESC";


            using var gridReader = await connection.QueryMultipleAsync(sql, request);

            var result = new GetBalanceQueryResult()
            {
                Customer = (await gridReader.ReadFirstOrDefaultAsync<CustomerDto>())!,
                Account = (await gridReader.ReadFirstOrDefaultAsync<AccountDto>())!,
                BalanceValue = (await gridReader.ReadFirstOrDefaultAsync<TransactionDto>())?.BalanceValue ?? 0
            };

            return result;
        }
    }
}
