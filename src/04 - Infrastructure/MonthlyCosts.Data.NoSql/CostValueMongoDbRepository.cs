using MongoDB.Driver;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using Stocks.Billing.Infra.Data.MongoDb.Context;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Infra.Data.MongoDb;

public class CostValueMongoDbRepository : ICostValueNoSqlRepository
{
    private readonly MongoDbContext<CostValue> dbContext;

    public CostValueMongoDbRepository(MongoDbContext<CostValue> dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<CostValue> GetAll()
    {
        return dbContext.Collection.AsQueryable();
    }
    public async Task<CostValue> GetAsync(Guid id)
    {
        return await dbContext.Collection
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();
    }
    public PagedResult<CostValue> Search(DateOnly? month, int pageNumber, int pageSize)
    {
        var source = dbContext.Collection.AsQueryable()
          .Where(p => month == null || p.Month == month);

        var result = new PagedResult<CostValue>
        {
            Queryable = source.Skip((pageNumber - 1) * pageSize).Take(pageSize),
            CurrentPage = pageNumber,
            PageSize = pageSize,
            PageCount = source.Count()
        };

        return result;
    }
    public async Task Create(CostValue cost)
    {
        await dbContext.Collection.InsertOneAsync(cost);
    }
    public async Task Update(CostValue cost)
    {
        await dbContext.Collection.ReplaceOneAsync(filter: g => g.Id == cost.Id, replacement: cost);
    }
    public async Task Delete(Guid id)
    {
        await dbContext.Collection.DeleteOneAsync(p => p.Id == id);
    }
}