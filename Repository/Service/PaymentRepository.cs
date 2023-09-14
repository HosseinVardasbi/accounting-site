using DataLayer02.Context;
using DataLayer02.Domain;
//using DataLayer02.Migrations;
using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamedProject02.Repository.Service
{
    public class PaymentRepository : IPaymentRepository
    {
        DB_context dB;
        public PaymentRepository(DB_context dB)
        {
            this.dB = dB;
        }

        public async Task<bool> AddListPayment(List<Payment> payments)
        {
            if (dB != null)
            {
                try
                {
                    await dB.payments.AddRangeAsync(payments);
                    await dB.SaveChangesAsync();
                }
                catch (Exception ex)
                { 
                var message = ex.Message;
                }
                return true;
            }
            return false;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            if(dB != null)
            {
                var result = dB.payments.Add(payment);
                dB.SaveChanges();
                return result.Entity;
            }
            return null;
        }

        public async Task<int> DeletePayment(int id)
        {
            var result = 0;
            if(dB != null)
            {
                var payment = dB.payments.SingleOrDefault(m => m.Id == id);
                if(payment != null)
                {
                    dB.payments.Remove(payment);
                    result = dB.SaveChanges();   
                }
                return result;
            }
            return result;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            if(dB != null)
            {
                return (from p in dB.payments
                        where(p.Id == id)
                        select new Payment { Id = p.Id,
                        Type = p.Type,
                        Price  = p.Price,
                        Detail = p.Detail,
                        Paid = p.Paid,
                        FactorId = p.FactorId}).FirstOrDefault();
            }
            return null;
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            var x = dB.payments.Include(m => m.Factor).ToList();
            return x;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByFactorId(int id)
        {
            if (dB != null) {
                var payments = dB.payments.Include(m => m.Factor).Where(m => m.FactorId == id).AsNoTracking();
                return payments.ToList();
            }
            return null;
        }

        public async Task<OutPutPaymentPaging> GetPaymentsByPage(int pageNumber, int countRecord, string search, string option, string sort)
        {
            var x = dB.payments.Count();
            var query = dB.payments.AsQueryable();
            if (search != null)
            {
                if (option == "FactorId")
                {
                    query = query.Where(x => x.FactorId.ToString().ToLower().Contains(search.ToLower()));
                }
                if (option == "Id")
                {
                    query = query.Where(x => x.Id.ToString().Contains(search.ToLower()));
                }
            }
            int numberOfObjectsPerPage = countRecord;
            query = query
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return new OutPutPaymentPaging()
            {
                currentPage = pageNumber,
                totalRecord = x,
                payments = query.AsNoTracking()
            };
        }

        public async Task<bool> UpdateListPayment(List<Payment> payments)
        {
            if (dB != null)
            {
                dB.payments.UpdateRange(payments);
                dB.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<int> UpdatePayment(Payment payment)
        {
            var result = 0;
            if(dB != null)
            {
                dB.payments.Update(payment);
                result = dB.SaveChanges();
                return result;
            }
            return result;
        }
    }
}
