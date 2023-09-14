using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectById
{
    public class GetDebtByIdQuery : IRequest<DataLayer02.Domain.Debt>
    {
        public int Id { get; set; }
    }
}
