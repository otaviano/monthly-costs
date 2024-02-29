using Dapper;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using System.Data;

namespace MonthlyCosts.Infra.Data.SqlServer;

public class CostSqlServerRepository : ICostSqlRepository
{
    private readonly IDbConnection _dbConnection;
    private const string GetAllQuery = "SELECT * FROM [dbo].[Costs]";
    private const string GetByIdQuery = "SELECT * FROM [dbo].[Costs] WHERE id = @id";
    private const string InsertQuery = "INSERT INTO [dbo].[Costs]([Id],[Name],[Avarage],[PaymentMethod]) VALUES(@Id,@Name,@Avarage,@PaymentMethod)";
    private const string UpdateQuery = "UPDATE [dbo].[Costs] SET [Name] = @Name, [Avarage] = @Avarage, [PaymentMethod] = @PaymentMethod WHERE [Id] = @Id";
    private const string DeleteQuery = "DELETE FROM [dbo].[Costs] WHERE id = @id";

    public CostSqlServerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Create(Cost cost)
    {
        await _dbConnection.ExecuteAsync(InsertQuery, cost);
    }

    public async Task Update(Cost cost)
    {
        await _dbConnection.ExecuteAsync(UpdateQuery, cost);
    }

    public async Task Delete(Guid id)
    {
        await _dbConnection.ExecuteAsync(DeleteQuery, new { id });
    }

    public async Task<IEnumerable<Cost>> GetAllAsync()
    {
        return await _dbConnection.QueryAsync<Cost>(GetAllQuery);
    }

    public async Task<Cost> GetAsync(Guid id)
    {
        return await _dbConnection.QueryFirstAsync<Cost>(GetByIdQuery, new { id });
    }
}
