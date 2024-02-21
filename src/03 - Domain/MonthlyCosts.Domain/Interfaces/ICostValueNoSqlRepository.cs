using MonthlyCosts.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Domain.Interfaces
{
    public interface ICostValueNoSqlRepository
    {
        Task Create(CostValue cost);
        Task Delete(Guid id);
        Task<CostValue> GetAsync(Guid id);
        IEnumerable<CostValue> GetAll();
        Task Update(CostValue cost);
        PagedResult<CostValue> Search(DateOnly? month, int pageNumber, int pageSize);
    }
}