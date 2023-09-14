using MediatR;

namespace HamedProject02.Features.Payment.Queries.SelectById
{
    public class GetPaymentByIdQuery : IRequest<DataLayer02.Domain.Payment>
    {
        public int Id { get; set; }
    }
}
