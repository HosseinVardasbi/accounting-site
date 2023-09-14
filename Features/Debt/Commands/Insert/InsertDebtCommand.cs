using MediatR;

namespace HamedProject02.Features.Debt.Commands.Insert
{
    public class InsertDebtCommand : IRequest<DataLayer02.Domain.Debt>
    {
        public long Price { get; set; }
        public string? Detail { get; set; }
        public DateTime? Created { get; set; }
        public long CustomerId { get; set; }
        public InsertDebtCommand(long price, string? detail, DateTime? created, long customerId)
        {
            Price = price;
            Detail = detail;
            Created = created;
            CustomerId = customerId;
        }
    }
}
