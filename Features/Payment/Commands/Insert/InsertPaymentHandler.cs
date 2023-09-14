using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.Insert
{
    public class InsertPaymentHandler : IRequestHandler<InsertPaymentCommand, DataLayer02.Domain.Payment>
    {
        IPaymentRepository paymentRepository;
        public InsertPaymentHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<DataLayer02.Domain.Payment> Handle(InsertPaymentCommand request, CancellationToken cancellationToken)
        {
            var payments = new DataLayer02.Domain.Payment(){
                Type = request.Type,
                Price = request.Price,
                Detail = request.Detail,
                Paid = request.Paid,
                FactorId = request.FactorId};
            return await paymentRepository.AddPayment(payments);
        }
    }
}
