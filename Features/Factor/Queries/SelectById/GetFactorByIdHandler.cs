using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectById
{
    public class GetFactorByIdHandler : IRequestHandler<GetFactorByIdQuery, DataLayer02.Domain.Factor>
    {
        IFactorRepository FactorRepository;
        public GetFactorByIdHandler(IFactorRepository factorRepository)
        {
            this.FactorRepository = factorRepository;
        }

        public async Task<DataLayer02.Domain.Factor> Handle(GetFactorByIdQuery request, CancellationToken cancellationToken)
        {
            return await FactorRepository.GetFactorById(request.Id);
        }
    }
}
