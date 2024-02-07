using MonthlyCosts.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Domain.Interfaces
{
    public interface ICostNoSqlRepository
    {
        Task Create(Cost cost);
        Task Delete(Guid id);
        Cost Get(Guid id);
        IEnumerable<Cost> GetAll();
        Task Update(Cost cost);
        PagedResult<Cost> Search(string name, int pageNumber, int pageSize);
    }
}