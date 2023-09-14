using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectById
{
    public class GetFactorByCustomerIdHandler : IRequestHandler<GetFactorByCustomerId, IEnumerable<DataLayer02.Domain.Factor>>
    {
        IFactorRepository FactorRepository;
        public GetFactorByCustomerIdHandler(IFactorRepository factorRepository)
        {
            FactorRepository = factorRepository;
        }

        public async Task<IEnumerable<DataLayer02.Domain.Factor>> Handle(GetFactorByCustomerId request, CancellationToken cancellationToken)
        {
            return await FactorRepository.GetFactorByCustomerId(request.Id);  
        }
    }
}
