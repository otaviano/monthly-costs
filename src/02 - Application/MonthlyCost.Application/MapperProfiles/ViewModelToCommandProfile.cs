using AutoMapper;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;

namespace MonthlyCost.Application.MapperProfiles;

public class ViewModelToCommandProfile : Profile
{
    public ViewModelToCommandProfile()
    {
        CreateMap<CostViewModel, CreateCostCommand>()
          .ForMember(p => p.Id, q => Guid.NewGuid());
        CreateMap<CostViewModel, UpdateCostCommand>();
        CreateMap<CostViewModel, DeleteCostCommand>();
    }
}
