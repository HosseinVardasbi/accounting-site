using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Domain
{
    public class DebtPayment
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public int DebtId { get; set; }
        [ForeignKey("DebtId")]
        public virtual Debt Debt { get; set; }
    }
}
