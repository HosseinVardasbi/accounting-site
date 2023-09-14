using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Queries.SelectById
{
    public class GetDebtPaymentByDebtIdHandler : IRequestHandler<GetDebtPaymentByDebtIdQuery, IEnumerable<DataLayer02.Domain.DebtPayment>>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public GetDebtPaymentByDebtIdHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.DebtPayment>> Handle(GetDebtPaymentByDebtIdQuery request, CancellationToken cancellationToken)
        {
            return await debtPaymentRepository.GetDebtPaymentsByDebtId(request.Id);
        }
    }
}
