using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectById
{
    public class GetFactorByCustomerId : IRequest<IEnumerable<DataLayer02.Domain.Factor>>
    {
        public long Id { get; set; }
    }
}
