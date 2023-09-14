using MediatR;

namespace HamedProject02.Features.Debt.Commands.Update
{
    public class UpdateDebtCommand : IRequest<int>
    {
        public int Id { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public DateTime? Created { get; set; }
        public UpdateDebtCommand(int id, long price, string? detail, DateTime? created)
        {
            Id = id;
            Price = price;
            Detail = detail;
            Created = created;
        }
    }
}
