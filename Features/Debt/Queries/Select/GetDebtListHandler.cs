using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Queries.Select
{
    public class GetDebtListHandler : IRequestHandler<GetdebtListQuery, IEnumerable<DataLayer02.Domain.Debt>>
    {
        IDebtRepository debtRepository;
        public GetDebtListHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Debt>> Handle(GetdebtListQuery request, CancellationToken cancellationToken)
        {
            return await debtRepository.GetDebt();
        }
    }
}
