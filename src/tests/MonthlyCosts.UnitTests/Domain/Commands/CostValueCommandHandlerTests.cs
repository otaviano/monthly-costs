using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Services.CommandHandlers;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain.Commands;

public class CostValueCommandHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidCreateCostValueCommand_ShouldReturnsNotNull(
       [Substitute] CostValueCommandHandler sut,
       CreateCostValueCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        sut._eventBus.Received(1).Publish(Arg.Any<IEvent>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidDeleteCostValueCommand_ShouldReturnsNotNull(
      [Substitute] CostValueCommandHandler sut,
      DeleteCostValueCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        sut._eventBus.Received(1).Publish(Arg.Any<IEvent>());
    }
}
