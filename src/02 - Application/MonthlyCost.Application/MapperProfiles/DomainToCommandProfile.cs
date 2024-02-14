using AutoMapper;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles;

public class DomainToCommandProfile : Profile
{
    public DomainToCommandProfile()
    {
        CreateMap<CreateCostCommand, Cost>();
    }
}
