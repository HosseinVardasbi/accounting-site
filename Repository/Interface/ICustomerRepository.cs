using DataLayer02.Domain;
using HamedProject02.Models;

namespace HamedProject02.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<OutPutCustomerPaging> GetCustomersByPage(int pageNumber, int countRecord, string search, string option, string sort);
        Task<Customer> GetCustomerById(long Id);
        Task<Customer> AddCustomer(Customer customer);
        Task<long> DeleteCustomer(long Id);
        Task<long> UpdateCustomer(Customer customer);
        
    }
}
