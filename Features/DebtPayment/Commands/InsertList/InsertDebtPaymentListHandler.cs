using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.InsertList
{
    public class InsertDebtPaymentListHandler : IRequestHandler<InsertDebtPaymentListCommand, bool>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public InsertDebtPaymentListHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<bool> Handle(InsertDebtPaymentListCommand request, CancellationToken cancellationToken)
        {
            List<DataLayer02.Domain.DebtPayment> debtPayments = new List<DataLayer02.Domain.DebtPayment>();
            foreach (var item in request.insertDebtPaymentListCommands)
            {
                debtPayments.Add(new DataLayer02.Domain.DebtPayment()
                {
                    Type = item.Type,
                    Price = item.Price,
                    Detail = item.Detail,
                    DebtId = item.DebtId
                });
            }

            return await debtPaymentRepository.AddListDebtPayment(debtPayments);
        }
    }
}
