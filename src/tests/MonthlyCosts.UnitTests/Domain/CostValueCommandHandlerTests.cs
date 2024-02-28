using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Services.CommandHandlers;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain;

public class CostValueCommandHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidCreateCostValueCommand_ShouldReturnsNotNull(
       [Substitute] CostValueCommandHandler sut,
       CreateCostValueCommand request)
    {
        var result = await sut.Handle(request,new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Create(Arg.Any<CostValue>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidUpdateCostValueCommand_ShouldReturnsNotNull(
        [Substitute] CostValueCommandHandler sut,
        UpdateCostValueCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Update(Arg.Any<CostValue>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidDeleteCostValueCommand_ShouldReturnsNotNull(
      [Substitute] CostValueCommandHandler sut,
      DeleteCostValueCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Delete(Arg.Any<Guid>());
    }
}
