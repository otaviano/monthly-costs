﻿using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Infra.IoC;

namespace MonthlyCosts.Infra.IoC;

public static class AutoMapperSetupExtension
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMappings();
    }
}
