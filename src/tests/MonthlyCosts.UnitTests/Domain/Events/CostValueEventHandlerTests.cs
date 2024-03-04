using AutoFixture.AutoNSubstitute;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Services.EventHandlers;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain.Events;

public class CostValueEventHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidCreateCostValueEvent_ShouldCreateANewElement(
       [Substitute] CostValueEventHandler sut,
       CreateCostValueEvent request)
    {
        await sut.Handle(request, new CancellationToken());
        await sut._costValueNoSqlRepository.Received(1).Create(Arg.Any<CostValue>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidDeleteCostValueEvent_ShouldDeleteTheElement(
      [Substitute] CostValueEventHandler sut,
      DeleteCostValueEvent request)
    {
        await sut.Handle(request, new CancellationToken());
        await sut._costValueNoSqlRepository.Received(1).Delete(Arg.Any<Guid>());
    }
}
