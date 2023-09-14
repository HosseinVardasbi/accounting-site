using DataLayer02.Domain;

namespace HamedProject02.Repository.Interface
{
    public interface IDebtPaymentRepository
    {
        Task<IEnumerable<DebtPayment>> GetDebtPayments();
        Task<DebtPayment> GetDebtPaymentById(int id);
        Task<IEnumerable<DebtPayment>> GetDebtPaymentsByDebtId(int id);
        Task<DebtPayment> AddDebtPayment(DebtPayment debtPayment);
        Task<int> UpdateDebtPayment(DebtPayment debtPayment);
        Task<int> DeleteDebtPayment(int id);

        Task<bool> AddListDebtPayment(List<DebtPayment> debtPayment);
    }
}
