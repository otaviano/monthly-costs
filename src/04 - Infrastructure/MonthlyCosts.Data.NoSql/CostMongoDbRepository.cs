using MongoDB.Driver;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using Stocks.Billing.Infra.Data.NoSql.Context;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Data.NoSql;

public class CostMongoDbRepository : ICostNoSqlRepository
{
    private readonly MongoDbContext<Cost> dbContext;

    public CostMongoDbRepository(MongoDbContext<Cost> dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Cost> GetAll()
    {
        return dbContext.Collection.AsQueryable();
    }
    public Cost Get(Guid id)
    {
        return dbContext.Collection.AsQueryable()
            .Where(p => p.Id == id)
            .FirstOrDefault(new Cost());
    }
    public PagedResult<Cost> Search(string name, int pageNumber, int pageSize)
    {
        var source = dbContext.Collection.AsQueryable()
          .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name));

        var result = new PagedResult<Cost>
        {
            Queryable = source.Skip((pageNumber - 1) * pageSize).Take(pageSize),
            CurrentPage = pageNumber,
            PageSize = pageSize,
            PageCount = source.Count()
        };

        return result;
    }
    public async Task Create(Cost cost)
    {
        await dbContext.Collection.InsertOneAsync(cost);
    }
    public async Task Update(Cost cost)
    {
        var update = Builders<Cost>.Update
            .Set(p => p, cost);

        await dbContext.Collection.UpdateOneAsync(p => p.Id == cost.Id, update);
    }
    public async Task Delete(Guid id)
    {
        await dbContext.Collection.DeleteOneAsync(p => p.Id == id);
    }
}