using MediatR;

namespace HamedProject02.Features.Payment.Commands.Delete
{
    public class DeletePaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeletePaymentCommand(int id)
        {
            Id = id;
        }
    }
}
