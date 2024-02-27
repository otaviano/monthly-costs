using AutoMapper;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Events;

namespace MonthlyCost.Application.MapperProfiles;

public class ViewModelToEventProfile : Profile
{
    public ViewModelToEventProfile()
    {
        CreateMap<CreateCostCommand, CreateCostEvent>();
    }
}
