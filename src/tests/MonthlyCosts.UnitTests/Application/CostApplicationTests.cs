using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCost.Application;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Application;

public class CostApplicationTests
{
    [Theory, AutoNSubstituteData]
    public async Task CreateAsync_GivenAValidCostRequestViewModel_ShouldReturnsAValidGuid(
            [Substitute] CostApplication sut,
            CostRequestViewModel viewModel)
    {
        var result = await sut.CreateAsync(viewModel);

        result.Should().NotBeEmpty();
        await sut._bus.Received(1).SendCommand(Arg.Any<CreateCostCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task CreateAsync_GivenAnInvalidCostRequestViewModel_ShouldThrowsArgumentNullException(
        CostApplication sut)
    {
        Func<Task> func = async () => await sut.CreateAsync(null);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateAsync_GivenAValidCostRequestViewModel_ShouldCallSendCommandOnce(
           [Substitute] CostApplication sut,
           CostRequestViewModel viewModel,
           Guid id)
    {
        await sut.UpdateAsync(id, viewModel);
        viewModel.Should().NotBeNull();
        await sut._bus.Received(1).SendCommand(Arg.Any<UpdateCostCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateAsync_GivenAnInvalidCostRequestViewModel_ShouldThrowsArgumentNullException(
        CostApplication sut)
    {
        Func<Task> func = async () => await sut.UpdateAsync(Guid.Empty, null);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory, AutoNSubstituteData]
    public async Task DeleteAsync_GivenAValidCostRequestViewModel_ShouldCallSendCommandOnce(
          [Substitute] CostApplication sut,
          Guid id)
    {
        await sut.DeleteAsync(id);
        await sut._bus.Received(1).SendCommand(Arg.Any<DeleteCostCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task DeleteAsync_GivenAnInvalidCostRequestViewModel_ShouldThrowsArgumentNullException(
        CostApplication sut)
    {
        Func<Task> func = async () => await sut.DeleteAsync(Guid.Empty);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }
}
