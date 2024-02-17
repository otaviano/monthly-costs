using System.ComponentModel;

namespace MonthlyCosts.Domain.Entities
{
    public enum PaymentMethod
    {
        [Description("Boleto")]
        Bill = 1,
        [Description("Cartão de crédito")]
        CreditCard = 2,
        [Description("Cartão de débito")]
        DebitCard = 3,
        [Description("Pix")]
        Pix = 4,
        [Description("Dinheiro")]
        Cash = 5
    }
}