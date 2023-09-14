using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Domain
{
    public enum PaymentType
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "وجه نقد")]
        Naqd = 0,
        [Display(Name = "کارت به کارت")]
        Cart = 2,
        [Display(Name = "دستگاه کارتخوان")]
        pos = 1,
        [Display(Name = "چک")]
        chek = 3
    }
}
