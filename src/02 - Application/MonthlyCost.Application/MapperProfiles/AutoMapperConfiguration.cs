using AutoMapper;

namespace MonthlyCost.Application.MapperProfiles;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(p =>
        {
            p.AddProfile(new DomainToViewModelProfile());
            p.AddProfile(new DomainToCommandProfile());
            p.AddProfile(new ViewModelToCommandProfile());
            p.AddProfile(new CommandToEventProfile());
            p.AddProfile(new DomainToEventProfile());
        });
    }
}
