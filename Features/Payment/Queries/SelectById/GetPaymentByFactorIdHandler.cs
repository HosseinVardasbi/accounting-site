using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Queries.SelectById
{
    public class GetPaymentByFactorIdHandler : IRequestHandler<GetPaymentByFactorIdQuery, IEnumerable<DataLayer02.Domain.Payment>>
    {
        IPaymentRepository paymentRepository;
        public GetPaymentByFactorIdHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Payment>> Handle(GetPaymentByFactorIdQuery request, CancellationToken cancellationToken)
        {
            return await paymentRepository.GetPaymentsByFactorId(request.Id);
        }
    }
}
