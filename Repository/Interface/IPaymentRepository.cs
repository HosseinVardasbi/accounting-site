using DataLayer02.Domain;
using HamedProject02.Models;

namespace HamedProject02.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPayments();
        Task<OutPutPaymentPaging> GetPaymentsByPage(int pageNumber, int countRecord, string search, string option, string sort);
        Task<Payment> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetPaymentsByFactorId(int id);
        Task<Payment> AddPayment(Payment payment);
        Task<int> UpdatePayment(Payment payment);
        Task<int> DeletePayment(int id);

        Task<bool> AddListPayment(List<Payment> payments);
        Task<bool> UpdateListPayment(List<Payment> payments);
    }
}
