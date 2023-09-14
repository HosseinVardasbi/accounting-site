using MediatR;

namespace HamedProject02.Features.Debt.Queries.Select
{
    public class GetdebtListQuery : IRequest<IEnumerable<DataLayer02.Domain.Debt>>
    {
    }
}
