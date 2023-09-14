using HamedProject02.Features.Payment.Commands.Update;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.UpdateList
{
    public class UpdateListPaymentCommand : IRequest<bool>
    {
        public List<UpdatePaymentCommand>? UpdateListPaymentCommands { get; set; }
    }
}
