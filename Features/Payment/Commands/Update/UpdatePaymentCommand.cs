using DataLayer02.Domain;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.Update
{
    public class UpdatePaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public bool? Paid { get; set; }
        public int FactorId { get; set; }
        public UpdatePaymentCommand(int id,PaymentType type, long price, bool? paid, int factorId, string? detail)
        {
            Id = id;
            Type = type;
            Price = price;
            Paid = paid;
            FactorId = factorId;
            Detail = detail;
        }
    }
}
