using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Insert
{
    public class InsertDebtPaymentHandler : IRequestHandler<InsertDebtPaymentCommand, DataLayer02.Domain.DebtPayment>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public InsertDebtPaymentHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<DataLayer02.Domain.DebtPayment> Handle(InsertDebtPaymentCommand request, CancellationToken cancellationToken)
        {
            var debtPayments = new DataLayer02.Domain.DebtPayment()
            {
                Type = request.Type,
                Price = request.Price,
                Detail = request.Detail,
                DebtId = request.DebtId
            };
            return await debtPaymentRepository.AddDebtPayment(debtPayments);
        }
    }
}
