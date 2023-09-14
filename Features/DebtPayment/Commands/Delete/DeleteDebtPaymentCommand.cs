using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Delete
{
    public class DeleteDebtPaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteDebtPaymentCommand(int id)
        {
            Id = id;
        }
    }
}
