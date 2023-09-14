using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Update
{
    public class UpdateDebtPaymentHandler : IRequestHandler<UpdateDebtPaymentCommand, int>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public UpdateDebtPaymentHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<int> Handle(UpdateDebtPaymentCommand request, CancellationToken cancellationToken)
        {
            var DebtPayments = await debtPaymentRepository.GetDebtPaymentById(request.Id);
            if (DebtPayments == null)
                return default;
            DebtPayments.Type = request.Type;
            DebtPayments.Price = request.Price;
            DebtPayments.Detail = request.Detail;
            return await debtPaymentRepository.UpdateDebtPayment(DebtPayments);
        }
    }
}
