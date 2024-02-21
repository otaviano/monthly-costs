using AutoMapper;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCost.Application;

public class CostValueApplication : ICostValueApplication
{
    private readonly IMapper _autoMapper;
    public readonly IMediatorHandler _bus;
    public readonly ICostValueNoSqlRepository _queryRepository;

    public CostValueApplication(IMapper autoMapper, IMediatorHandler bus, ICostValueNoSqlRepository queryRepository)
    {
        _bus = bus;
        _autoMapper = autoMapper;
        _queryRepository = queryRepository;
    }

    public IEnumerable<CostValueResponseViewModel> Get()
    {
        var costs = _queryRepository.GetAll()
            ?? Enumerable.Empty<CostValue>();

        return _autoMapper.Map<IEnumerable<CostValueResponseViewModel>>(costs);
    }

    public async Task<CostValueResponseViewModel> GetAsync(Guid id)
    {
        var cost = await _queryRepository.GetAsync(id);

        return _autoMapper.Map<CostValueResponseViewModel>(cost);
    }

    public async Task<Guid> CreateAsync(CostValueRequestViewModel model)
    {
        var command = _autoMapper.Map<CreateCostValueCommand>(model)
            ?? throw new ArgumentNullException(nameof(model), $"Error mapping to #{nameof(CreateCostValueCommand)}");

        await _bus.SendCommand(command);
        return command.Id;
    }

    public async Task UpdateAsync(Guid id, CostValueRequestViewModel model)
    {
        var command = _autoMapper.Map<UpdateCostValueCommand>(model)
            ?? throw new ArgumentNullException(nameof(model), $"Error mapping to #{nameof(UpdateCostValueCommand)}");
        command.Id = id;

        await _bus.SendCommand(command);
    }

    public async Task DeleteAsync(Guid id)
    {
        if(id.Equals(Guid.Empty))
            throw new ArgumentNullException(nameof(id), $"Error mapping to #{nameof(DeleteCostValueCommand)}");

        var command = new DeleteCostCommand { Id = id };

        await _bus.SendCommand(command);
    }
}
