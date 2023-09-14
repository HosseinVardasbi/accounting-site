using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.Delete
{
    public class DeleteDebtPaymentHandler : IRequestHandler<DeleteDebtPaymentCommand, int>
    {
        IDebtPaymentRepository debtPaymentRepository;
        public DeleteDebtPaymentHandler(IDebtPaymentRepository debtPaymentRepository)
        {
            this.debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<int> Handle(DeleteDebtPaymentCommand request, CancellationToken cancellationToken)
        {
            var debtPayment = debtPaymentRepository.GetDebtPaymentById(request.Id);
            if (debtPayment == null)
                return default;
            return await debtPaymentRepository.DeleteDebtPayment(request.Id);
        }
    }
}
