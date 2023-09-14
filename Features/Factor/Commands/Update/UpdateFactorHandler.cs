using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Commands.Update
{
    public class UpdateFactorHandler : IRequestHandler<UpdateFactorCommand, int>
    {
        IFactorRepository factorRepository;
        public UpdateFactorHandler(IFactorRepository factorRepository)
        {
            this.factorRepository = factorRepository;
        }

        public async Task<int> Handle(UpdateFactorCommand request, CancellationToken cancellationToken)
        {
            var factors = await factorRepository.GetFactorById(request.Id);
            if (factors == null)
                return default;
            factors.FactorNo = request.FactorNo;
            factors.Price = request.Price;
            factors.DateTime = request.DateTime;
            factors.Detail = request.Detail;
            return await factorRepository.UpdateFactor(factors);
        }
    }
}
