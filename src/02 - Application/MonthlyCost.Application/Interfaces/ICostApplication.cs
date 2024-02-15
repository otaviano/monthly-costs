using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCost.Application.Interfaces
{
    public interface ICostApplication
    {
        Task<Guid> Create(CostRequestViewModel model);
        Task Update(Guid id, CostRequestViewModel model);
        Task Delete(Guid id);
        IEnumerable<CostResponseViewModel> Get();
        Task<CostResponseViewModel> GetAsync(Guid id);
    }
}