using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectByPage
{
    public class GetFactorByPageHandler : IRequestHandler<GetFactorByPageQuery, OutPutFactorPaging>
    {
        IFactorRepository factorRepository;
        public GetFactorByPageHandler(IFactorRepository factorRepository)
        {
            this.factorRepository = factorRepository;
        }

        public async Task<OutPutFactorPaging> Handle(GetFactorByPageQuery request, CancellationToken cancellationToken)
        {
            return await factorRepository.GetFactorsByPage(request.currentPage, request.totalRecord,
                request.search, request.option, request.sort);
        }
    }
}
