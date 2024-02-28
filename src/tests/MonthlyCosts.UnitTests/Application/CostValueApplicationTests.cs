using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using LinxCommerce.Delivery.UnitTests.NSubstitute;
using MonthlyCost.Application;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using NSubstitute;
using Xunit;

namespace MonthlyCosts.UnitTests.Application;

public class CostValueApplicationTests
{
    [Theory, AutoNSubstituteData]
    public async Task CreateAsync_GivenAValidCostValueRequestViewModel_ShouldReturnsAValidGuid(
            [Substitute] CostValueApplication sut,
            CostValueRequestViewModel viewModel)
    {
        var result = await sut.CreateAsync(viewModel);

        result.Should().NotBeEmpty();
        await sut._bus.Received(1).SendCommand(Arg.Any<CreateCostValueCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task CreateAsync_GivenAnInvalidCostValueRequestViewModel_ShouldThrowsArgumentNullException(
        CostValueApplication sut)
    {
        Func<Task> func = async () => await sut.CreateAsync(null);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateAsync_GivenAValidCostValueRequestViewModel_ShouldCallSendCommandOnce(
           [Substitute] CostValueApplication sut,
           CostValueRequestViewModel viewModel,
           Guid id)
    {
        await sut.UpdateAsync(id, viewModel);
        viewModel.Should().NotBeNull();
        await sut._bus.Received(1).SendCommand(Arg.Any<UpdateCostValueCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateAsync_GivenAnInvalidCostValueRequestViewModel_ShouldThrowsArgumentNullException(
        CostValueApplication sut)
    {
        Func<Task> func = async () => await sut.UpdateAsync(Guid.Empty, null);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory, AutoNSubstituteData]
    public async Task DeleteAsync_GivenAValidCostValueRequestViewModel_ShouldCallSendCommandOnce(
          [Substitute] CostValueApplication sut,
          Guid id)
    {
        await sut.DeleteAsync(id);
        await sut._bus.Received(1).SendCommand(Arg.Any<DeleteCostValueCommand>());
    }

    [Theory, AutoNSubstituteData]
    public async Task DeleteAsync_GivenAnInvalidCostValueRequestViewModel_ShouldThrowsArgumentNullException(
        CostValueApplication sut)
    {
        Func<Task> func = async () => await sut.DeleteAsync(Guid.Empty);

        await func.Should().ThrowAsync<ArgumentNullException>();
    }
}
