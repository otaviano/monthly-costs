using AutoMapper;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Events;

namespace MonthlyCost.Application.MapperProfiles;

public class CommandToEventProfile : Profile
{
    public CommandToEventProfile()
    {
        CreateMap<CreateCostCommand, CreateCostEvent>();
        CreateMap<UpdateCostCommand, UpdateCostEvent>();
        CreateMap<DeleteCostCommand, DeleteCostEvent>();
    }
}
