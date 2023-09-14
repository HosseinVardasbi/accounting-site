using DataLayer02.Domain;
using HamedProject02.Models;

namespace HamedProject02.Repository.Interface
{
    public interface IFactorRepository
    {
        Task<IEnumerable<Factor>> GetFactor();
        Task<OutPutFactorPaging> GetFactorsByPage(int pageNumber, int countRecord, string search, string option, string sort);
        Task<OutPutDebtFactorPaging> GetDebtFactorsByPage(int pageNumber, int countRecord, string search, string option, string sort);
        Task<Factor> GetFactorById(int Id);
        Task<IEnumerable<Factor>> GetFactorByCustomerId(long Id);
        Task<Factor> AddFactor(Factor factor);

       
        Task<int> UpdateFactor(Factor factor);
        Task<int> DeleteFactor(int Id);
    }
}
