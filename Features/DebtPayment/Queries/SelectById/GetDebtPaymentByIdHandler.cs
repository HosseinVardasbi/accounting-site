using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Queries.SelectById
{
    public class GetDebtPaymentByIdHandler : IRequestHandler<GetDebtPaymentByIdQuery, DataLayer02.Domain.DebtPayment>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public GetDebtPaymentByIdHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<DataLayer02.Domain.DebtPayment> Handle(GetDebtPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await debtPaymentRepository.GetDebtPaymentById(request.Id);
        }
    }
}
