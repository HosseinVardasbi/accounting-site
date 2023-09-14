using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.Delete
{
    public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, int>
    {
        IPaymentRepository paymentRepository;
        public DeletePaymentHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<int> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = paymentRepository.GetPaymentById(request.Id);
            if (payment == null)
                return default;
            return await paymentRepository.DeletePayment(request.Id);
        }
    }
}
