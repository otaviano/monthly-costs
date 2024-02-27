using AutoMapper;
using MonthlyCost.Application.MapperProfiles.Resolvers;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;

namespace MonthlyCost.Application.MapperProfiles;

public class ViewModelToCommandProfile : Profile
{
    public ViewModelToCommandProfile()
    {
        CreateMap<CostRequestViewModel, CreateCostCommand>()
          .ForMember(p => p.Id, q => q.MapFrom(p => Guid.NewGuid()))
          .ForMember(p => p.PaymentMethod, q => q.MapFrom(new CreatePaymentMethodResolver()));
        CreateMap<CostRequestViewModel, UpdateCostCommand>()
          .ForMember(p => p.PaymentMethod, q => q.MapFrom(new UpdatePaymentMethodResolver()));

        CreateMap<CostValueRequestViewModel, CreateCostValueCommand>()
          .ForMember(p => p.Id, q => q.MapFrom(p => Guid.NewGuid()));
        CreateMap<CostValueRequestViewModel, UpdateCostValueCommand>();
    }
}
