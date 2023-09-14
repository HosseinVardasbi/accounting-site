using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.Update
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, int>
    {
        IPaymentRepository paymentRepository;
        public UpdatePaymentHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<int> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payments = await paymentRepository.GetPaymentById(request.Id);
            if (payments == null)
                return default;
            payments.Type = request.Type;
            payments.Price = request.Price;
            payments.Detail = request.Detail;
            payments.Paid = request.Paid;
            return await paymentRepository.UpdatePayment(payments);
        }
    }
}
