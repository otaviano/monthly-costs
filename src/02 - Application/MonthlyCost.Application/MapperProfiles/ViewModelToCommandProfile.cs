using AutoMapper;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;

namespace MonthlyCost.Application.MapperProfiles;

public class ViewModelToCommandProfile : Profile
{
    public ViewModelToCommandProfile()
    {
        CreateMap<CostViewModel, CreateCostCommand>()
          .ForMember(p => p.Id, q => q.MapFrom(p => Guid.NewGuid()));
          //.ForMember(p => p.PaymentMethod, q => q.MapFrom(p => p.PaymentMethod.Humanize()));
        CreateMap<CostViewModel, UpdateCostCommand>();
        CreateMap<CostViewModel, DeleteCostCommand>();
    }
}
