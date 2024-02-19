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
            var config = new MapperConfiguration(p =>
            {
                p.AddProfile(new DomainToViewModelProfile());
                p.AddProfile(new DomainToCommandProfile());
                p.AddProfile(new ViewModelToCommandProfile());
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
