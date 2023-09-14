using DataLayer02.Domain;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.Insert
{
    public class InsertPaymentCommand : IRequest<DataLayer02.Domain.Payment>
    {
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public bool? Paid { get; set; }
        public int FactorId { get; set; }
        public InsertPaymentCommand(PaymentType type, long price, string? detail, bool? paid, int factorId)
        {
            Type = type;
            Price = price;
            Paid = paid;
            FactorId = factorId;
            Detail = detail;
        }
    }
}
