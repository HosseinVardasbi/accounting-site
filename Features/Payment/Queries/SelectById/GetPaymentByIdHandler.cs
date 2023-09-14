using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Queries.SelectById
{
    public class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, DataLayer02.Domain.Payment>
    {
        IPaymentRepository paymentRepository;
        public GetPaymentByIdHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<DataLayer02.Domain.Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await paymentRepository.GetPaymentById(request.Id);
        }
    }
}
