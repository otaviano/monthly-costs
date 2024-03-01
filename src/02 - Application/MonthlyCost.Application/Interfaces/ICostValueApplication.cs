using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCost.Application.Interfaces
{
    public interface ICostValueApplication
    {
        Task<Guid> CreateAsync(CostValueRequestViewModel model);
        Task DeleteAsync(Guid id);
        IEnumerable<CostValueResponseViewModel> Get();
        Task<CostValueResponseViewModel> GetAsync(Guid id);
        Task<decimal> SumAsync(Guid id);
    }
}