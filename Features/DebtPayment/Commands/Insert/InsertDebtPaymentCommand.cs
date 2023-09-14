using DataLayer02.Domain;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Insert
{
    public class InsertDebtPaymentCommand : IRequest<DataLayer02.Domain.DebtPayment>
    {
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public int DebtId { get; set; }
        public InsertDebtPaymentCommand(PaymentType type, long price, string? detail, int debtId)
        {
            Type = type;
            Price = price;
            Detail = detail;
            DebtId = debtId;
        }
    }
}
