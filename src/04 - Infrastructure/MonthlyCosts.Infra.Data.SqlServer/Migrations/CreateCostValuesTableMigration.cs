using FluentMigrator;

namespace MonthlyCosts.Infra.Data.SqlServer.Migrations;

[Migration(20240229030506)]
public class CreateCostValuesTableMigration : Migration
{
    public override void Down()
    {
        Delete.Table("CostValues");
    }

    public override void Up()
    {
        Create.Table("CostValues")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("CostId").AsGuid().NotNullable().ForeignKey("Costs", "Id")
            .WithColumn("Value").AsDecimal(18, 2).NotNullable()
            .WithColumn("Month").AsDate().NotNullable();
    }
}
