using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Commands.Delete
{
    public class DeleteDebtHandler : IRequestHandler<DeleteDebtCommand, int>
    {
        IDebtRepository debtRepository;
        public DeleteDebtHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<int> Handle(DeleteDebtCommand request, CancellationToken cancellationToken)
        {
            var debts = debtRepository.GetDebtById(request.Id);
            if (debts == null)
                return default;
            return await debtRepository.DeleteDebt(request.Id);
        }
    }
}
