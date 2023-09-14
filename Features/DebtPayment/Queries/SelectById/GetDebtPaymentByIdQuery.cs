using MediatR;

namespace HamedProject02.Features.DebtPayment.Queries.SelectById
{
    public class GetDebtPaymentByIdQuery : IRequest<DataLayer02.Domain.DebtPayment>
    {
        public int Id { get; set; }
    }
}
