using DataLayer02.Domain;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Update
{
    public class UpdateDebtPaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public int DebtId { get; set; }
        public UpdateDebtPaymentCommand(int id, PaymentType type, long price, string? detail, int debtId)
        {
            Id = id;
            Type = type;
            Price = price;
            Detail = detail;
            DebtId = debtId;
        }
    }
}
