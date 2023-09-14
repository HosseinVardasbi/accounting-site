using MediatR;

namespace HamedProject02.Features.Customer.Commands.Update
{
    public class UpdateCustomerCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public UpdateCustomerCommand(string name, long id, string? phoneNumber, string? adress)
        {
            Name = name;
            Id = id;
            PhoneNumber = phoneNumber;
            Adress = adress;
        }
    }
}
