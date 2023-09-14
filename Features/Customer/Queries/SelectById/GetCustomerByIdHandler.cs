using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Queries.SelectById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, DataLayer02.Domain.Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByIdHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<DataLayer02.Domain.Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerById(request.Id);
        }
    }
}
