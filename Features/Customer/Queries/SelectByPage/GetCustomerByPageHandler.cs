using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Queries.SelectByPage
{
    public class GetCustomerByPageHandler : IRequestHandler<GetCustomerByPageQuery, OutPutCustomerPaging>
    {
        ICustomerRepository customerRepository;
        public GetCustomerByPageHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<OutPutCustomerPaging> Handle(GetCustomerByPageQuery request, CancellationToken cancellationToken)
        {
            return await customerRepository.GetCustomersByPage(request.currentPage, request.totalRecord,
                request.search, request.option, request.sort);
        }
    }
}
