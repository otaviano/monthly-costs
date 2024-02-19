using AutoMapper;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;
using System.Reflection;

namespace MonthlyCost.Application;

public class CostApplication : ICostApplication
{
    private readonly IMapper _autoMapper;
    public readonly IMediatorHandler _bus;
    public readonly ICostNoSqlRepository _queryRepository;

    public CostApplication(IMapper autoMapper, IMediatorHandler bus, ICostNoSqlRepository queryRepository)
    {
        _bus = bus;
        _autoMapper = autoMapper;
        _queryRepository = queryRepository;
    }

    public IEnumerable<CostResponseViewModel> Get()
    {
        var costs = _queryRepository.GetAll()
            ?? Enumerable.Empty<Cost>();

        return _autoMapper.Map<IEnumerable<CostResponseViewModel>>(costs);
    }

    public async Task<CostResponseViewModel> GetAsync(Guid id)
    {
        var cost = await _queryRepository.GetAsync(id);

        return _autoMapper.Map<CostResponseViewModel>(cost);
    }

    public async Task<Guid> CreateAsync(CostRequestViewModel model)
    {
        var command = _autoMapper.Map<CreateCostCommand>(model)
            ?? throw new ArgumentNullException(nameof(model), $"Error mapping to #{nameof(CreateCostCommand)}");

        await _bus.SendCommand(command);
        return command.Id;
    }

    public async Task UpdateAsync(Guid id, CostRequestViewModel model)
    {
        var command = _autoMapper.Map<UpdateCostCommand>(model)
            ?? throw new ArgumentNullException(nameof(model), $"Error mapping to #{nameof(UpdateCostCommand)}");
        command.Id = id;

        await _bus.SendCommand(command);
    }

    public async Task DeleteAsync(Guid id)
    {
        if(id.Equals(Guid.Empty))
            throw new ArgumentNullException(nameof(id), $"Error mapping to #{nameof(DeleteCostCommand)}");

        var command = new DeleteCostCommand { Id = id };

        await _bus.SendCommand(command);
    }
}
