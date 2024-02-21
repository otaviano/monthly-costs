using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCost.Application.Interfaces
{
    public interface ICostValueApplication
    {
        Task<Guid> CreateAsync(CostValueRequestViewModel model);
        Task UpdateAsync(Guid id, CostValueRequestViewModel model);
        Task DeleteAsync(Guid id);
        IEnumerable<CostValueResponseViewModel> Get();
        Task<CostValueResponseViewModel> GetAsync(Guid id);
    }
}