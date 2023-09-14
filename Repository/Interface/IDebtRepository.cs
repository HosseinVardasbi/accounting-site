using DataLayer02.Domain;
using HamedProject02.Models;

namespace HamedProject02.Repository.Interface
{
    public interface IDebtRepository
    {
        Task<IEnumerable<Debt>> GetDebt();
        Task<OutPutDebtPaging> GetDebtsByPage(int pageNumber, int countRecord, string search, string option, string sort);
        Task<Debt> GetDebtById(int Id);
        Task<IEnumerable<Debt>> GetDebtByCustomerId(long Id);
        Task<Debt> AddDebt(Debt debt);


        Task<int> UpdateDebt(Debt debt);
        Task<int> DeleteDebt(int Id);
    }
}
