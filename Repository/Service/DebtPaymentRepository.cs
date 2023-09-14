using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamedProject02.Repository.Service
{
    public class DebtPaymentRepository : IDebtPaymentRepository
    {
        DB_context dB;
        public DebtPaymentRepository(DB_context dB)
        {
            this.dB = dB;
        }

        public async Task<DebtPayment> AddDebtPayment(DebtPayment debtPayment)
        {
            if (dB != null)
            {
                var result = dB.debtPayments.Add(debtPayment);
                dB.SaveChanges();
                return result.Entity;
            }
            return null;
        }

        public async Task<bool> AddListDebtPayment(List<DebtPayment> debtPayment)
        {
            if (dB != null)
            {
                dB.debtPayments.AddRange(debtPayment);
                dB.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<int> DeleteDebtPayment(int id)
        {
            var result = 0;
            if (dB != null)
            {
                var debtPayment = dB.debtPayments.SingleOrDefault(m => m.Id == id);
                if (debtPayment != null)
                {
                    dB.debtPayments.Remove(debtPayment);
                    result = dB.SaveChanges();
                }
                return result;
            }
            return result;
        }

        public async Task<DebtPayment> GetDebtPaymentById(int id)
        {
            if (dB != null)
            {
                return (from p in dB.debtPayments
                        where (p.Id == id)
                        select new DebtPayment
                        {
                            Id = p.Id,
                            Type = p.Type,
                            Price = p.Price,
                            Detail = p.Detail,
                            DebtId = p.DebtId
                        }).FirstOrDefault();
            }
            return null;
        }

        public async Task<IEnumerable<DebtPayment>> GetDebtPayments()
        {
            var x = dB.debtPayments.Include(m => m.Debt).ToList();
            return x;
        }

        public async Task<IEnumerable<DebtPayment>> GetDebtPaymentsByDebtId(int id)
        {
            if (dB != null)
            {
                var debtPayments = dB.debtPayments.Include(m => m.Debt).Where(m => m.DebtId == id).ToList();
                return debtPayments.AsEnumerable();
            }
            return null;
        }

        public async Task<int> UpdateDebtPayment(DebtPayment debtPayment)
        {
            var result = 0;
            if (dB != null)
            {
                dB.debtPayments.Update(debtPayment);
                result = dB.SaveChanges();
                return result;
            }
            return result;
        }
    }
}
