using Dapper;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using System.Data;

namespace MonthlyCosts.Infra.Data.SqlServer;

public class CostValueSqlServerRepository : ICostValueSqlRepository
{
    private readonly IDbConnection _dbConnection;
    private const string GetAllQuery = "SELECT * FROM CostValues";
    private const string GetByIdQuery = "SELECT * FROM CostValues WHERE id = @id";
    private const string InsertQuery = "";
    private const string UpdateQuery = "UPDATE CostValues SET name = @Name, avarage=@Avarage WHERE id = @Id";
    private const string DeleteQuery = "DELETE FROM CostValues WHERE id = @id";

    public CostValueSqlServerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Create(CostValue cost)
    {
        await _dbConnection.ExecuteAsync(InsertQuery, cost);
    }

    public async Task Update(CostValue cost)
    {
        await _dbConnection.ExecuteAsync(UpdateQuery, cost);
    }

    public async Task Delete(Guid id)
    {
        await _dbConnection.ExecuteAsync(DeleteQuery, id);
    }

    public async Task<IEnumerable<CostValue>> GetAllAsync()
    {
        return await _dbConnection.QueryAsync<CostValue>(GetAllQuery);
    }

    public async Task<CostValue> GetAsync(Guid id)
    {
        return await _dbConnection.QueryFirstAsync<CostValue>(GetByIdQuery, id);
    }
}
