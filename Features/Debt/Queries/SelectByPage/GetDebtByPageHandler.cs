using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectByPage
{
    public class GetDebtByPageHandler : IRequestHandler<GetDebtByPageQuery, OutPutDebtPaging>
    {
        IDebtRepository debtRepository;
        public GetDebtByPageHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<OutPutDebtPaging> Handle(GetDebtByPageQuery request, CancellationToken cancellationToken)
        {
            return await debtRepository.GetDebtsByPage(request.currentPage, request.totalRecord,
                request.search, request.option, request.sort);
        }
    }
}
