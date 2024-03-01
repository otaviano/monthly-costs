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
    public async Task Delete(Guid id)
    {
        var result = await dbContext.Collection.DeleteOneAsync(p => p.Id == id);

        Console.WriteLine(result); 
    }
    public async Task<decimal> SumAsync(Guid costId)
    {
        var queryResult = await dbContext.Collection
            .FindAsync(x => x.Cost.Id == costId && x.Month.Month == DateTime.Now.Month);
        var total = await queryResult.ToListAsync();

        return total.Sum(x => x.Value);
    }
}