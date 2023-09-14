using HamedProject02.Features.DebtPayment.Commands.Insert;
using MediatR;

namespace HamedProject02.Features.DebtPayment.Commands.InsertList
{
    public class InsertDebtPaymentListCommand : IRequest<bool>
    {
        public List<InsertDebtPaymentCommand>? insertDebtPaymentListCommands { get; set; }
    }
}
