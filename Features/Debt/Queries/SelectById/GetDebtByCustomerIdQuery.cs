using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectById
{
    public class GetDebtByCustomerIdQuery : IRequest<IEnumerable<DataLayer02.Domain.Debt>>
    {
        public long Id { get; set; }
    }
}
