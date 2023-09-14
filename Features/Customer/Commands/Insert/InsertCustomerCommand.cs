using MediatR;
using DataLayer02.Domain;

namespace HamedProject02.Features.Customer.Commands.Insert
{
    public class InsertCustomerCommand : IRequest<DataLayer02.Domain.Customer>
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public InsertCustomerCommand(string name, string? phoneNumber, string? adress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Adress = adress;
        }
    }
}
