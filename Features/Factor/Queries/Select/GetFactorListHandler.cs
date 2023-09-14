using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.Select
{
    public class GetFactorListHandler : IRequestHandler<GetFactorListQuery, IEnumerable<DataLayer02.Domain.Factor>>
    {
        IFactorRepository FactorRepository;
        public GetFactorListHandler(IFactorRepository factorRepository)
        {
            this.FactorRepository = factorRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Factor>> Handle(GetFactorListQuery request, CancellationToken cancellationToken)
        {
            return await FactorRepository.GetFactor();
        }
    }
}
