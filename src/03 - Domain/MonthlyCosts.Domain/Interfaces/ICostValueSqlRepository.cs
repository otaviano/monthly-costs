using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Interfaces
{
    public interface ICostValueSqlRepository
    {
        Task Create(CostValue costValue);
        Task Delete(Guid id);
        Task<CostValue> GetAsync(Guid id);
        Task<IEnumerable<CostValue>> GetAllAsync();
    }
}