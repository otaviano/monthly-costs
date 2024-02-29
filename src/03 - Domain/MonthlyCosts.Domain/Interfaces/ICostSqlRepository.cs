using MonthlyCosts.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MonthlyCosts.Domain.Interfaces
{
    public interface ICostSqlRepository
    {
        Task Create(Cost cost);
        Task Delete(Guid id);
        Task<Cost> GetAsync(Guid id);
        Task<IEnumerable<Cost>> GetAllAsync();
        Task Update(Cost cost);
    }
}