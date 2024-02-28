using AutoFixture.AutoNSubstitute;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Infra.Bus;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain;

public class RabbitMQConsumerTests
{
    [Theory, AutoNSubstituteData]
    public void Subscribe_GivenAValidCreateCostEvent_ShouldBindOnQueue(
       [Substitute] RabbitMQConsumer sut)
    {
        sut.Subscribe<CreateCostEvent>();
        sut.Channel.Received(1).QueueBind(Arg.Any<string>(), Arg.Any<string>(), nameof(CreateCostEvent), null);
    }
}
