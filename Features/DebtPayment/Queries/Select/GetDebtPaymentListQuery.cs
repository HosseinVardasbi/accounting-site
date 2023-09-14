using MediatR;

namespace HamedProject02.Features.DebtPayment.Queries.Select
{
    public class GetDebtPaymentListQuery : IRequest<IEnumerable<DataLayer02.Domain.DebtPayment>>
    {
    }
}
