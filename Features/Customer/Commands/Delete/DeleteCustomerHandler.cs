using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Commands.Delete
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, long>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<long> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customers = _customerRepository.GetCustomerById(request.Id);
            if (customers == null)
                return default;
            return await _customerRepository.DeleteCustomer(request.Id);
        }
    }
}
