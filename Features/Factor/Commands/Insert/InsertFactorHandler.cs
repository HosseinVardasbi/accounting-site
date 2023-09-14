using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Commands.Insert
{
    public class InsertFactorHandler : IRequestHandler<InsertFactorCommand, DataLayer02.Domain.Factor>
    {
        IFactorRepository factorRepository;
        ICustomerRepository customerRepository;
        public InsertFactorHandler(IFactorRepository factorRepository, ICustomerRepository customerRepository)
        {
            this.factorRepository = factorRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<DataLayer02.Domain.Factor> Handle(InsertFactorCommand request, CancellationToken cancellationToken)
        {
            
            var cusId = customerRepository.GetCustomers().Id;
            //factorRepository.GetFactor()

            var factors = new DataLayer02.Domain.Factor() { 
            FactorNo = request.FactorNo,
            Price = request.Price,
            DateTime = request.DateTime,
            Detail = request.Detail,
            CustomerId = request.CustomerId};
            return await factorRepository.AddFactor(factors);
        }
    }
}
