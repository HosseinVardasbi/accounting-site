using DataLayer02.Domain;
using MediatR;

namespace HamedProject02.Features.Customer.Queries.Select
{
    public class GetCustomerListQuerie : IRequest<IEnumerable<DataLayer02.Domain.Customer>>
    {
    }

}
