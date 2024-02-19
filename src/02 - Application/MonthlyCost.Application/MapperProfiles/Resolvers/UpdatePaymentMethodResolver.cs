using AutoMapper;
using Humanizer;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles;

public class UpdatePaymentMethodResolver : IValueResolver<CostRequestViewModel, UpdateCostCommand, PaymentMethod>
{
    public PaymentMethod Resolve(CostRequestViewModel source, UpdateCostCommand destination, PaymentMethod destMember, ResolutionContext context)
    {
        return source.PaymentMethod.DehumanizeTo<PaymentMethod>();
    }
}
