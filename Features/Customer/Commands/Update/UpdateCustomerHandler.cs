using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Commands.Update
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, long>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<long> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetCustomerById(request.Id);
            if (customers == null)
                return default;
            customers.Name = request.Name;
            customers.PhoneNumber = request.PhoneNumber;
            customers.Adress = request.Adress;
            return await _customerRepository.UpdateCustomer(customers);
        }
    }
}
