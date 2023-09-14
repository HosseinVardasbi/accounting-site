using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Queries.Select
{
    public class GetPaymentListHandler : IRequestHandler<GetPaymentListQuery, IEnumerable<DataLayer02.Domain.Payment>>
    {
        IPaymentRepository paymentRepository;
        public GetPaymentListHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Payment>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
        {
            return await paymentRepository.GetPayments();
        }
    }
}
