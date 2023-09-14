using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.UpdateList
{
    public class UpdateListPaymentHandler : IRequestHandler<UpdateListPaymentCommand, bool>
    {
        IPaymentRepository paymentRepository;
        public UpdateListPaymentHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(UpdateListPaymentCommand request, CancellationToken cancellationToken)
        {
            var pay = await paymentRepository.GetPaymentsByFactorId(request.UpdateListPaymentCommands.
                Select(p => p.FactorId).SingleOrDefault());
            
            //var paymentById = await paymentRepository.GetPaymentById(request.UpdateListPaymentCommands.
            //    Select(p => p.Id).FirstOrDefault());
            List<DataLayer02.Domain.Payment> payments = new List<DataLayer02.Domain.Payment>();
            foreach (var item in request.UpdateListPaymentCommands)
            {
                var ppp = pay.Where(p => p.Id == item.Id).SingleOrDefault();
                ppp.Type = item.Type;
                ppp.Price = item.Price;
                ppp.Detail = item.Detail;
                ppp.Paid = item.Paid;
                //payments.Add(new DataLayer02.Domain.Payment()
                //{
                //    Type = item.Type,
                //    Price = item.Price,
                //    Paid = item.Paid,
                //    FactorId = item.FactorId
                //});
                payments.Add(ppp);
            }

            return await paymentRepository.UpdateListPayment(payments);
        }
    }
}
