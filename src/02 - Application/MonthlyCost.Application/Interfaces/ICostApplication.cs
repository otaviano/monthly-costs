using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCost.Application.Interfaces
{
    public interface ICostApplication
    {
        Task<Guid> CreateAsync(CostRequestViewModel model);
        Task UpdateAsync(Guid id, CostRequestViewModel model);
        Task DeleteAsync(Guid id);
        IEnumerable<CostResponseViewModel> Get();
        Task<CostResponseViewModel> GetAsync(Guid id);
    }
}