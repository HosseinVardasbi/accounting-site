using MediatR;

namespace HamedProject02.Features.Customer.Queries.SelectById
{
    public class GetCustomerByIdQuery : IRequest<DataLayer02.Domain.Customer>
    {
        public long Id { get; set; }
    }
}
