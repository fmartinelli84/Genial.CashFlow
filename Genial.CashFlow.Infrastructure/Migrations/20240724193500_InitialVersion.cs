using FluentMigrator;
using Genial.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Migrations
{
    [Migration(20240724193500)]
    public class InitialVersion : Migration
    {
        public override void Up()
        {
            Create.Table("Customers")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(265).NotNullable()
                .WithColumn("Document").AsString(20).NotNullable().Unique()
                .WithTrackable();

            Create.Table("Accounts")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("CustomerId").AsInt64().NotNullable().ForeignKey("Customers", "Id").Indexed()
                .WithColumn("AgencyNumber").AsString(4).NotNullable()
                .WithColumn("Number").AsString(6).NotNullable()
                .WithTrackable();

            Create.UniqueConstraint("IX_Number")
                .OnTable("Accounts")
                .Columns("AgencyNumber", "Number");

            Create.Table("Transactions")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("AccountId").AsInt64().NotNullable().ForeignKey("Accounts", "Id").Indexed()
                .WithColumn("Date").AsDateTime().NotNullable().Indexed()
                .WithColumn("Type").AsByte().NotNullable()
                .WithColumn("Description").AsString(265).NotNullable()
                .WithColumn("Value").AsDecimal().NotNullable()
                .WithColumn("BalanceValue").AsDecimal().NotNullable()
                .WithReadOnlyTrackable();
        }

        public override void Down()
        {
            Delete.Table("Transactions");

            Delete.Table("Accounts");

            Delete.Table("Companies");
        }
    }
}
