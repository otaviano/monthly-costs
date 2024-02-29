using FluentMigrator;

namespace MonthlyCosts.Infra.Data.SqlServer.Migrations;

[Migration(20240229025359)]
public class CreateCostsTableMigration : Migration
{
    public override void Down()
    {
        Delete.Table("Costs");
    }

    public override void Up()
    {
        Create.Table("Costs")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Avarage").AsDecimal(18, 2).NotNullable()
            .WithColumn("PaymentMethod").AsInt16().NotNullable();
    }
}
