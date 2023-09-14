using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Commands.Insert
{
    public class InsertDebtHandler : IRequestHandler<InsertDebtCommand, DataLayer02.Domain.Debt>
    {
        IDebtRepository debtRepository;
        ICustomerRepository customerRepository;
        public InsertDebtHandler(IDebtRepository debtRepository, ICustomerRepository customerRepository)
        {
            this.debtRepository = debtRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<DataLayer02.Domain.Debt> Handle(InsertDebtCommand request, CancellationToken cancellationToken)
        {
            var debts = new DataLayer02.Domain.Debt()
            {
                Price = request.Price,
                Detail = request.Detail,
                Created = request.Created,
                CustomerId = request.CustomerId
            };
            return await debtRepository.AddDebt(debts);
        }
    }
}
