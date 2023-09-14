using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectById
{
    public class GetDebtByCustomerIdHandler : IRequestHandler<GetDebtByCustomerIdQuery, IEnumerable<DataLayer02.Domain.Debt>>
    {
        IDebtRepository debtRepository;
        public GetDebtByCustomerIdHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Debt>> Handle(GetDebtByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await debtRepository.GetDebtByCustomerId(request.Id);
        }
    }
}
