using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Queries.Select
{
    public class GetDebtPaymentListHandler : IRequestHandler<GetDebtPaymentListQuery, IEnumerable<DataLayer02.Domain.DebtPayment>>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public GetDebtPaymentListHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.DebtPayment>> Handle(GetDebtPaymentListQuery request, CancellationToken cancellationToken)
        {
            return await debtPaymentRepository.GetDebtPayments();
        }
    }
}
