using DataLayer02.Domain;
using System.ComponentModel.DataAnnotations;

namespace HamedProject02.Models
{
    public class FactorPaymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "خالی نباشد")]
        public int FactorNo { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "خالی نباشد")]
        public long FPrice { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Detail { get; set; }
        //public PaymentType Type { get; set; }
        //public long PayPrice { get; set; }
        public List<PaymentViewModel> paymentViewModels { get; set; }
 
    }
    public class PaymentViewModel
    {
        
        public PaymentType Type { get; set; }
        public long PayPrice { get; set; }
        public string? PDetail { get; set; }
    }
}
