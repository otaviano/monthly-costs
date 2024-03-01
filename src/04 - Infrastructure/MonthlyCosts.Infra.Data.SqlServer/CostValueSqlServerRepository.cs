using Dapper;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using System.Data;

namespace MonthlyCosts.Infra.Data.SqlServer;

public class CostValueSqlServerRepository : ICostValueSqlRepository
{
    private readonly IDbConnection _dbConnection;
    private const string GetAllQuery = "SELECT * FROM [dbo].[CostValues]";
    private const string GetByIdQuery = "SELECT * FROM [dbo].[CostValues] WHERE [id] = @id";
    private const string InsertQuery = "INSERT INTO [dbo].[CostValues] ([Id],[CostId],[Value],[Month]) VALUES(@Id,@CostId,@Value,@Month)";
    private const string DeleteQuery = "DELETE FROM [dbo].[CostValues] WHERE [id] = @id";

    public CostValueSqlServerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Create(CostValue costValue)
    {
        var @params = new
        {
            costValue.Id,
            costValue.Value,
            Month = costValue.Month.ToDateTime(TimeOnly.MinValue),
            CostId = costValue.Cost?.Id
        };

        await _dbConnection.ExecuteAsync(InsertQuery, @params);
    }
    public async Task Delete(Guid id)
    {
        await _dbConnection.ExecuteAsync(DeleteQuery, new { id });
    }
    public async Task<IEnumerable<CostValue>> GetAllAsync()
    {
        return await _dbConnection.QueryAsync<CostValue>(GetAllQuery);
    }
    public async Task<CostValue> GetAsync(Guid id)
    {
        return await _dbConnection.QueryFirstAsync<CostValue>(GetByIdQuery, new { id });
    }
}
