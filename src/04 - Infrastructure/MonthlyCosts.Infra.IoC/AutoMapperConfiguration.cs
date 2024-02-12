using AutoMapper;
using MonthlyCost.Application.MapperProfiles;

namespace MonthlyCosts.Infra.IoC;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(p =>
        {
            p.AddProfile(new DomainToViewModelProfile());
            p.AddProfile(new ViewModelToCommandProfile());
        });
    }
}
