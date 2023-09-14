using MediatR;

namespace HamedProject02.Features.Factor.Commands.Update
{
    public class UpdateFactorCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int FactorNo { get; set; }
        public long Price { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Detail { get; set; }
        public UpdateFactorCommand(int id, int factorNo, long price, DateTime? dateTime, string? detail)
        {
            Id = id;
            FactorNo = factorNo;
            Price = price;
            DateTime = dateTime;
            Detail = detail;
        }
    }
}
