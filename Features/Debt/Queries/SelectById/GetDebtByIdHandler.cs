using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectById
{
    public class GetDebtByIdHandler : IRequestHandler<GetDebtByIdQuery, DataLayer02.Domain.Debt>
    {
        IDebtRepository debtRepository;
        public GetDebtByIdHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<DataLayer02.Domain.Debt> Handle(GetDebtByIdQuery request, CancellationToken cancellationToken)
        {
            return await debtRepository.GetDebtById(request.Id);
        }
    }
}
