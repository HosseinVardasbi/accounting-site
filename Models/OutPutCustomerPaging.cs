using DataLayer02.Domain;

namespace HamedProject02.Models
{
    public class OutPutCustomerPaging
    {
        public IEnumerable<Customer> customers { get; set; }
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string? search { get; set; }
    }
}
