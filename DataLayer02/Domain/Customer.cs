using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Domain
{
    
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        [DisplayName("نام")]
        [Required(ErrorMessage ="خالی نباشد")]
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }
        public virtual ICollection<Debt> Debts { get; set; }
    }
}
