using DataLayer02.Domain;
using HamedProject02.Features.Payment.Commands.Insert;
using MediatR;

namespace HamedProject02.Features.Payment.Commands.InserList
{
    public class InsertListPaymentCommand : IRequest<bool>
    {
        public List<InsertPaymentCommand>? InsertListPaymentCommands { get; set; }
    }
}
