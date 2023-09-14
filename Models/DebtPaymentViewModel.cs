using DataLayer02.Domain;
using System.ComponentModel.DataAnnotations;

namespace HamedProject02.Models
{
    public class DebtPaymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "خالی نباشد")]
        public long DPrice { get; set; }
        public string? DDetail { get; set; }
        public DateTime? Created { get; set; }
        public long CustomerId { get; set; }
        public List<DPaymentViewModel> DPaymentViewModels { get; set; }
    }
    public class DPaymentViewModel
    {
        public PaymentType Type { get; set; }
        public long PPrice { get; set; }
        public string? PDetail { get; set; }
    }
}
