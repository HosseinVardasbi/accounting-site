using HamedProject02.Models;
using MediatR;

namespace HamedProject02.Features.Factor.Queries.SelectDebtByPage
{
    public class GetDebtFactorByPageQuery : IRequest<OutPutDebtFactorPaging>
    {
        public int totalRecord { get; set; }
        public int currentPage { get; set; }
        public string search { get; set; }
        public string option { get; set; }
        public string sort { get; set; }
    }
}
