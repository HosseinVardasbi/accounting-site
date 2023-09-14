using HamedProject02.Models;
using MediatR;

namespace HamedProject02.Features.Debt.Queries.SelectByPage
{
    public class GetDebtByPageQuery : IRequest<OutPutDebtPaging>
    {
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string search { get; set; }
        public string option { get; set; }
        public string sort { get; set; }
    }
}
