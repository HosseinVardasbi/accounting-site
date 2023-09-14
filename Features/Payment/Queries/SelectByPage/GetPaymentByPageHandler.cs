using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Payment.Queries.SelectByPage
{
    public class GetPaymentByPageHandler : IRequestHandler<GetPaymentByPageQuery, OutPutPaymentPaging>
    {
        IPaymentRepository paymentRepository;
        public GetPaymentByPageHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<OutPutPaymentPaging> Handle(GetPaymentByPageQuery request, CancellationToken cancellationToken)
        {
            return await paymentRepository.GetPaymentsByPage(request.currentPage, request.totalRecord,
                request.search, request.option, request.sort);
        }
    }
}
