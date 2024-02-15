using MongoDB.Driver;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using Stocks.Billing.Infra.Data.MongoDb.Context;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Infra.Data.MongoDb;

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
    public async Task<Cost> GetAsync(Guid id)
    {
        return await dbContext.Collection
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();
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
        await dbContext.Collection.ReplaceOneAsync(filter: g => g.Id == cost.Id, replacement: cost);
    }
    public async Task Delete(Guid id)
    {
        await dbContext.Collection.DeleteOneAsync(p => p.Id == id);
    }
}