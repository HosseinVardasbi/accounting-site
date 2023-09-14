using MediatR;

namespace HamedProject02.Features.Factor.Commands.Insert
{
    public class InsertFactorCommand : IRequest<DataLayer02.Domain.Factor>
    {
        public int FactorNo { get; set; }
        public long Price { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Detail { get; set; }
        public long CustomerId { get; set; }
        public InsertFactorCommand(int factorNo, long price, DateTime? dateTime, string? detail, long customerId)
        {
            FactorNo = factorNo;
            Price = price;
            DateTime = dateTime;
            Detail = detail;
            CustomerId = customerId;
        }
    }
}
