using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using AutoMapper;
using Humanizer;
using MonthlyCost.Application.MapperProfiles;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace LinxCommerce.Delivery.UnitTests.NSubstitute
{
    [ExcludeFromCodeCoverage]
    public partial class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(FixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture();

            fixture.Register(ConfigureMapper);
            fixture.Register(() => {
                var gen = new Random();
                
                var start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;

                return DateOnly.FromDateTime(start.AddDays(gen.Next(range)));
            });
            fixture.Customize(new AutoNSubstituteCustomization());
            fixture.Customize<CostRequestViewModel>(p => p
                .With(p => p.PaymentMethod, GetRandomPaymentMethod()));

            return fixture;
        }

        private static string GetRandomPaymentMethod()
        {
            return Enum.GetValues(typeof(PaymentMethod))
                    .OfType<PaymentMethod>()
                    .OrderBy(e => Guid.NewGuid())
                    .FirstOrDefault()
                    .Humanize();
        }

        private static IMapper ConfigureMapper()
        {
            var config = AutoMapperConfiguration.RegisterMappings();

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
