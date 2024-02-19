using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Services.CommandHandlers;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Domain;

public class CostCommandHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidCreateCostCommand_ShouldReturnsNotNull(
       [Substitute] CostCommandHandler sut,
       CreateCostCommand request)
    {
        var result = await sut.Handle(request,new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Create(Arg.Any<Cost>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidUpdateCostCommand_ShouldReturnsNotNull(
        [Substitute] CostCommandHandler sut,
        UpdateCostCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Update(Arg.Any<Cost>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_GivenAValidDeleteCostCommand_ShouldReturnsNotNull(
      [Substitute] CostCommandHandler sut,
      DeleteCostCommand request)
    {
        var result = await sut.Handle(request, new CancellationToken());

        result.Should().NotBeNull();
        await sut._costNoSqlRepository.Received(1).Delete(Arg.Any<Guid>());
    }
}
