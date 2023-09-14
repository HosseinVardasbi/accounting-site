using DataLayer02.Domain;

namespace HamedProject02.Models
{
    public class OutPutPaymentPaging
    {
        public IEnumerable<Payment> payments { get; set; }
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
    }
}
