using MediatR;

namespace HamedProject02.Features.Payment.Queries.SelectById
{
    public class GetPaymentByFactorIdQuery : IRequest<IEnumerable<DataLayer02.Domain.Payment>>
    {
        public int Id { get; set; }
    }
}
