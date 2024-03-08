using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Events;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain;

public class RabbitMQEventBusTests
{
    [Theory, AutoNSubstituteData]
    public void Handle_GivenAValidCreateCostEvent_ShouldNotThrowException(
       [Substitute] IMessageBusPublisher sut,
       CreateCostEvent request)
    {
        var result = sut.SendMessageAsync(request);

        result.Should().NotBeNull();
    }
}
