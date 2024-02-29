using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Interfaces
{
    public interface ICostValueSqlRepository
    {
        Task Create(CostValue cost);
        Task Delete(Guid id);
        Task<CostValue> GetAsync(Guid id);
        Task<IEnumerable<CostValue>> GetAllAsync();
        Task Update(CostValue cost);
    }
}