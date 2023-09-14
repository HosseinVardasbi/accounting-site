using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Queries.Select
{
    public class GetCustomerListHandler : IRequestHandler<GetCustomerListQuerie, IEnumerable<DataLayer02.Domain.Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerListHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Customer>> Handle(GetCustomerListQuerie request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomers();
        }
    }
}
