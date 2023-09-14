using DataLayer02.Domain;

namespace HamedProject02.Models
{
    public class OutPutFactorPaging
    {
        public IEnumerable<Factor> factors { get; set; }
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string? search { get; set; }
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<Payment> payments { get; set; }
    }
    public class OutPutDebtFactorPaging
    {
        public IEnumerable<OutPutFactorPayment> factors { get; set; }
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string? search { get; set; }
        //public IEnumerable<Customer> customers { get; set; }
        //public IEnumerable<Payment> payments { get; set; }
    }
    public class OutPutFactorPayment
    {
        public Factor factor { get; set; }
        public long Price { get; set; }
        //public IEnumerable<Payment> payments { get; set; }
    }
}
