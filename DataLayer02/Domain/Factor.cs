using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Domain
{
    
    public class Factor
    {
        [Key]

        public int Id { get; set; }
        [Required(ErrorMessage = "خالی نباشد")]
        public int FactorNo { get; set; }
        [Required(ErrorMessage = "خالی نباشد")]
        public long Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateTime { get; set; }
        public string? Detail { get; set; }
        public virtual ICollection<Payment> payments { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}
