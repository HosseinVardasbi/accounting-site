using DataLayer02.Domain;

namespace HamedProject02.Models
{
    public class OutPutDebtPaging
    {
        public IEnumerable<Debt> debts { get; set; }
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string? search { get; set; }
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<DebtPayment> debtPayments { get; set; }
    }
}
