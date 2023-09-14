using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Factor.Commands.Delete
{
    public class DeleteFactorHandler : IRequestHandler<DeleteFactorCommand, int>
    {
        IFactorRepository factorRepository;
        public DeleteFactorHandler(IFactorRepository factorRepository)
        {
            this.factorRepository = factorRepository;
        }

        public async Task<int> Handle(DeleteFactorCommand request, CancellationToken cancellationToken)
        {
            var factors = factorRepository.GetFactorById(request.Id);
            if (factors == null)
                return default;
            return await factorRepository.DeleteFactor(request.Id);
        }
    }
}
