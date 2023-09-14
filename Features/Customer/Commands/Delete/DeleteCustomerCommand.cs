using MediatR;

namespace HamedProject02.Features.Customer.Commands.Delete
{
    public class DeleteCustomerCommand : IRequest<long>
    {
        public long Id { get; set; }
        public DeleteCustomerCommand(long id)
        {
            Id = id;
        }
    }
}
