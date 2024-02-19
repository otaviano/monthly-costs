using AutoMapper;
using Humanizer;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles.Resolvers;

public class CreatePaymentMethodResolver : IValueResolver<CostRequestViewModel, CreateCostCommand, PaymentMethod>
{
    public PaymentMethod Resolve(CostRequestViewModel source, CreateCostCommand destination, PaymentMethod destMember, ResolutionContext context)
    {
        return source.PaymentMethod.DehumanizeTo<PaymentMethod>();
    }
}
