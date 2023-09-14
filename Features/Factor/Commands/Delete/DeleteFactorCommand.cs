using MediatR;

namespace HamedProject02.Features.Factor.Commands.Delete
{
    public class DeleteFactorCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteFactorCommand(int id)
        {
            Id = id;
        }
    }
}
