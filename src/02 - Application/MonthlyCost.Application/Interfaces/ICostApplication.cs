using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCost.Application.Interfaces
{
    public interface ICostApplication
    {
        Task<Guid> Create(CostViewModel model);
        Task Update(CostViewModel model);
        Task Delete(Guid id);
        IEnumerable<CostViewModel> Get();
        CostViewModel Get(Guid id);
    }
}