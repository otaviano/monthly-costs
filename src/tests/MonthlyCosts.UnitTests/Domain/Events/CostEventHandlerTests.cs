using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Services.CommandHandlers;
using MonthlyCosts.Domain.Services.EventHandlers;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain.Events;

public class CostEventHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidCreateCostEvent_ShouldCreateANewElement(
       [Substitute] CostEventHandler sut,
       CreateCostEvent request)
    {
        await sut.Handle(request, new CancellationToken());
        await sut._costNoSqlRepository.Received(1).Create(Arg.Any<Cost>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidUpdateCostEvent_ShouldUpdateTheElement(
        [Substitute] CostEventHandler sut,
        UpdateCostEvent request)
    {
        await sut.Handle(request, new CancellationToken());
        await sut._costNoSqlRepository.Received(1).Update(Arg.Any<Cost>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidDeleteCostEvent_ShouldDeleteTheElement(
      [Substitute] CostEventHandler sut,
      DeleteCostEvent request)
    {
        await sut.Handle(request, new CancellationToken());
        await sut._costNoSqlRepository.Received(1).Delete(Arg.Any<Guid>());
    }
}
