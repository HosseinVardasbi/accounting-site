using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Debt.Commands.Update
{
    public class UpdateDebtHandler : IRequestHandler<UpdateDebtCommand, int>
    {
        IDebtRepository debtRepository;
        public UpdateDebtHandler(IDebtRepository debtRepository)
        {
            this.debtRepository = debtRepository;
        }

        public async Task<int> Handle(UpdateDebtCommand request, CancellationToken cancellationToken)
        {
            var debts = await debtRepository.GetDebtById(request.Id);
            if (debts == null)
                return default;
            debts.Price = request.Price;
            debts.Detail = request.Detail;
            debts.Created = request.Created;
            return await debtRepository.UpdateDebt(debts);
        }
    }
}
