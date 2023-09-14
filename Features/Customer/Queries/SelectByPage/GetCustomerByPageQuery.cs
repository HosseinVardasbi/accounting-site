using HamedProject02.Models;
using MediatR;

namespace HamedProject02.Features.Customer.Queries.SelectByPage
{
    public class GetCustomerByPageQuery :IRequest<OutPutCustomerPaging>
    {
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string search { get; set; }
        public string option { get; set; }
        public string sort { get; set; }
    }
}
