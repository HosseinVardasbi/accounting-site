using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectById
{
    public class GetFactorByIdQuery : IRequest<DataLayer02.Domain.Factor>
    {
        public int Id { get; set; }
    }
}
