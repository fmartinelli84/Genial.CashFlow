using FluentMigrator;
using FluentMigrator.SqlServer;
using Genial.CashFlow.Application.Dtos;
using Genial.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Migrations
{
    [Migration(20240724212500)]
    public class InsertData : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Customers").WithIdentityInsert()
                .Row(new { Id = 1, Name = "Cliente 1", Document = "11111111111", CreatedAtDate = DateTime.Now, ModifiedAtDate = DateTime.Now });
            Insert.IntoTable("Accounts").WithIdentityInsert()
                .Row(new { Id = 1, CustomerId = 1, AgencyNumber = "0001", Number = "000001", CreatedAtDate = DateTime.Now, ModifiedAtDate = DateTime.Now });
            Insert.IntoTable("Transactions").WithIdentityInsert()
                .Row(new { Id = 1, AccountId = 1, Date = DateTime.Now, Type = (byte)TransactionType.Credit, Description = "Depósito Inicial", Value = 100.99M, BalanceValue = 100.99M, CreatedAtDate = DateTime.Now });

            Insert.IntoTable("Customers").WithIdentityInsert()
                .Row(new { Id = 2, Name = "Cliente 2", Document = "22222222222", CreatedAtDate = DateTime.Now, ModifiedAtDate = DateTime.Now });
            Insert.IntoTable("Accounts").WithIdentityInsert()
                .Row(new { Id = 2, CustomerId = 2, AgencyNumber = "0001", Number = "000002", CreatedAtDate = DateTime.Now, ModifiedAtDate = DateTime.Now });
        }

        public override void Down()
        {
            Delete.FromTable("Transactions").AllRows();
            Delete.FromTable("Accounts").AllRows();
            Delete.FromTable("Customers").AllRows();
        }
    }
}
