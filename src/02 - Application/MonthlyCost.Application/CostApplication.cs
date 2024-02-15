using AutoMapper;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCost.Application;

public class CostApplication : ICostApplication
{
    private readonly IMapper _autoMapper;
    private readonly IMediatorHandler _bus;
    private readonly ICostNoSqlRepository _queryRepository;

    public CostApplication(IMapper autoMapper, IMediatorHandler bus, ICostNoSqlRepository queryRepository)
    {
        _bus = bus;
        _autoMapper = autoMapper;
        _queryRepository = queryRepository;
    }

    public IEnumerable<CostResponseViewModel> Get()
    {
        var costs = _queryRepository.GetAll();

        return _autoMapper.Map<IEnumerable<CostResponseViewModel>>(costs)
            ?? throw new NullReferenceException($"Error mapping to #{nameof(CostResponseViewModel)}");
    }

    public async Task<CostResponseViewModel> GetAsync(Guid id)
    {
        var cost = await _queryRepository.GetAsync(id);

        return _autoMapper.Map<CostResponseViewModel>(cost)
            ?? throw new NullReferenceException($"Error mapping to #{nameof(CostResponseViewModel)}");
    }

    public async Task<Guid> Create(CostRequestViewModel model)
    {
        var command = _autoMapper.Map<CreateCostCommand>(model)
            ?? throw new NullReferenceException($"Error mapping to #{nameof(CreateCostCommand)}");

        await _bus.SendCommand(command);
        return command.Id;
    }

    public async Task Update(Guid id, CostRequestViewModel model)
    {
        var command = _autoMapper.Map<UpdateCostCommand>(model)
            ?? throw new NullReferenceException($"Error mapping to #{nameof(UpdateCostCommand)}");
        command.Id = id;

        await _bus.SendCommand(command);
    }

    public async Task Delete(Guid id)
    {
        // TODO : Factory
        var command = new DeleteCostCommand { Id = id };

        await _bus.SendCommand(command);
    }
}
