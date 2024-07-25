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
                    c.[Document],

                    a.[Id], 
                    a.[AgencyNumber],
                    a.[Number],

                    t.[Id],
                    t.[Date],
                    t.[Type],
                    t.[Description],
                    t.[Value],
                    t.[BalanceValue]

                FROM [Customers] AS c 
                LEFT JOIN [Accounts] AS a ON a.[CustomerId] = c.Id
                LEFT JOIN [Transactions] AS t ON t.[AccountId] = a.Id
            WHERE
                c.[Document] = @CustomerDocument AND
                a.[AgencyNumber] = @AgencyNumber AND
                a.[Number] = @AccountNumber";

            
            request.CustomerDocument = request.CustomerDocument.OnlyNumbers()?.PadLeft(11, '0')!;
            request.AgencyNumber = request.AgencyNumber.OnlyNumbers()?.PadLeft(4, '0')!;
            request.AccountNumber = request.AccountNumber.OnlyNumbers()?.PadLeft(6, '0')!;

            if (request.StartDate is not null)
            {
                request.StartDate = request.StartDate.Value.Date;
                sql += " AND t.[Date] >= @StartDate";
            }
            if (request.EndDate is not null)
            {
                request.EndDate = request.EndDate.Value.Date.Add(new TimeSpan(23, 59, 59));
                sql += " AND t.[Date] <= @EndDate";
            }


            var resultLookup = new Dictionary<long, GetStatementQueryResult>();

            var results = await connection
                .QueryAsync(
                    sql,
                    [
                        typeof(CustomerDto),
                        typeof(AccountDto),
                        typeof(TransactionDto)
                    ],
                    obj =>
                    {
                        var customer = (CustomerDto)obj[0];
                        var account = (AccountDto)obj[1];
                        var transaction = (TransactionDto)obj[2];
                        GetStatementQueryResult? result;

                        if (!resultLookup.TryGetValue(account.Id, out result))
                        {
                            result = new GetStatementQueryResult()
                            {
                                Customer = customer,
                                Account = account
                            };

                            resultLookup.Add(account.Id, result);
                        }

                        if (transaction is not null)
                            result.Transactions.Add(transaction);

                        return result;
                    },
                    request);

            if (!results.Any())
                throw new BusinessException("Cliente ou conta corrente não encontrados.");

            return results.First();
        }

        public async Task<GetBalanceQueryResult> GetBalanceAsync(GetBalanceQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
