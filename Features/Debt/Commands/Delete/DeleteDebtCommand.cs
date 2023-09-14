using MediatR;

namespace HamedProject02.Features.Debt.Commands.Delete
{
    public class DeleteDebtCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteDebtCommand(int id)
        {
            Id = id;
        }
    }
}
