using MediatR;

namespace HamedProject02.Features.Payment.Queries.Select
{
    public class GetPaymentListQuery : IRequest<IEnumerable<DataLayer02.Domain.Payment>>
    {
    }
}
