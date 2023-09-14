using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.InserList
{
    public class InsertListPaymentCommandHandler : IRequestHandler<InsertListPaymentCommand, bool>
    {
        IPaymentRepository paymentRepository;
        public InsertListPaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(InsertListPaymentCommand request, CancellationToken cancellationToken)
        {
            List<DataLayer02.Domain.Payment> payments = new List<DataLayer02.Domain.Payment>();
            foreach (var item in request.InsertListPaymentCommands)
            {
                payments.Add(new DataLayer02.Domain.Payment()
                {
                    Type = item.Type,
                    Price = item.Price,
                    Detail = item.Detail,
                    Paid = item.Paid,
                    FactorId = item.FactorId
                });
            }
          
            return await paymentRepository.AddListPayment(payments);
        }
    }
}
