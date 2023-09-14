using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Customer.Commands.Insert
{
    public class InsertCustomerHandler : IRequestHandler<InsertCustomerCommand, DataLayer02.Domain.Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        public InsertCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<DataLayer02.Domain.Customer> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            var customers = new DataLayer02.Domain.Customer()
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Adress = request.Adress
            };
            return await _customerRepository.AddCustomer(customers);
        }
    }
}
