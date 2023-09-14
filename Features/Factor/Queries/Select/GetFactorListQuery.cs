using MediatR;

namespace HamedProject02.Features.Factor.Queries.Select
{
    public class GetFactorListQuery : IRequest<IEnumerable<DataLayer02.Domain.Factor>>
    {
    }
}
