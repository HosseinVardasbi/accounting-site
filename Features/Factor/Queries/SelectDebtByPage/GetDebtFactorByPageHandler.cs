using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectDebtByPage
{
    public class GetDebtFactorByPageHandler : IRequestHandler<GetDebtFactorByPageQuery, OutPutDebtFactorPaging>
    {
        IFactorRepository factorRepository;
        public GetDebtFactorByPageHandler(IFactorRepository factorRepository)
        {
            this.factorRepository = factorRepository;
        }

        public async Task<OutPutDebtFactorPaging> Handle(GetDebtFactorByPageQuery request, CancellationToken cancellationToken)
        {
            return await factorRepository.GetDebtFactorsByPage(request.currentPage, request.totalRecord,
                request.search, request.option, request.sort);
        }
    }
}
