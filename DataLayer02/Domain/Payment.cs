using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Domain
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public long Price { get; set; }
        public string? Detail { get; set; }
        public bool? Paid { get; set; }
        public int FactorId { get; set; }
        [ForeignKey("FactorId")]
        public virtual Factor Factor { get; set; }

    }
}
