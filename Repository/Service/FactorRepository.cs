using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HamedProject02.Repository.Service
{
    public class FactorRepository : IFactorRepository
    {
        DB_context db;
        public FactorRepository(DB_context db)
        {
            this.db = db;
        }

        public async Task<Factor> AddFactor(Factor factor)
        {
            int a = db.factors.Where(p => p.FactorNo == factor.FactorNo).Count();
            if(db != null && a == 0)
            {
                var result = db.factors.Add(factor);
                db.SaveChanges();
                return result.Entity;
            }
            return null;
        }

       

        public async Task<int> DeleteFactor(int Id)
        {
            int result = 0;
            if(db != null)
            {
                var fac = db.factors.SingleOrDefault(f => f.Id == Id);
                if(fac != null)
                {
                    db.factors.Remove(fac);
                    result = db.SaveChanges();
                }
                return result;
            }
            return result;
        }

        public async Task<IEnumerable<Factor>> GetFactor()
        {
            if(db != null)
            {
                var x = db.factors.Include(m => m.Customer).ToList();

                return x;
                    //(from p in db.factors
                    //    select new Factor
                    //    {                ********use join for include
                    //        Id = p.Id,
                    //        FactorNo = p.FactorNo,
                    //        Price = p.Price,
                    //        DateTime = p.DateTime,
                    //        Detail = p.Detail
                    //    }).ToList();
            }
            return null;
        }

        public async Task<IEnumerable<Factor>> GetFactorByCustomerId(long Id)
        {
            if (db != null)
            {
                var factors = db.factors.Include(m => m.Customer).Where(m => m.CustomerId == Id).ToList();
                return factors.AsEnumerable();
            }
            return null;
        }

        public async Task<Factor> GetFactorById(int Id)
        {
            if(db != null)
            {
                return (from p in db.factors
                        where (p.Id == Id)
                        select new Factor {Id = p.Id, FactorNo = p.FactorNo, Price = p.Price,
                        DateTime = p.DateTime, Detail = p.Detail, CustomerId = p.CustomerId}).FirstOrDefault();
            }
            return null;
        }

        public async Task<OutPutFactorPaging> GetFactorsByPage(int pageNumber, int countRecord, string search, string option, string sort)
        {
            //var x = db.factors.Count();
            var query = db.factors.Include(p => p.Customer).AsQueryable();
            if (search != null)
            {
                if (search.Contains("$$$"))
                {
                    string s = search.Remove(search.Length - 4);
                    query = query.Where(x => x.CustomerId.ToString().ToLower().Contains(s.ToLower()));
                }
                else
                {
                    query = query.Where(x => x.Price.ToString().ToLower().Contains(search.ToLower()) ||
                    x.Customer.Name.Contains(search.ToLower()) ||
                    x.FactorNo.ToString().ToLower().Contains(search.ToLower()));

                    //query = query.Where(p => p.Customer.Name.Contains("h"));

                }
            }

           
            int numberOfObjectsPerPage = countRecord;
            var x = query.Count();



            query = query.OrderByDescending(p => p.Id)
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage).AsNoTracking();
            //  .Include(p => p.Customer).AsNoTracking();
               

          var  queryfinal =query.Join(db.payments, fa => fa.Id, pay => pay.FactorId, (fa, pay) => new Payment {
          Price = pay.Price,
          FactorId=pay.FactorId,
          });

            return new OutPutFactorPaging()
            {
                currentPage = pageNumber,
                totalRecord = x,
                factors = query.AsNoTracking(),
                payments = queryfinal
            };
        }
        public async Task<OutPutDebtFactorPaging> GetDebtFactorsByPage(int pageNumber, int countRecord, string search, string option, string sort)
        {
            List<OutPutFactorPayment> outPutFactorPayments = new List<OutPutFactorPayment>();
          //  var payment = db.payments.Include(p=>p.Factor).GroupBy(p=>p.FactorId).AsQueryable();
            var query0 = db.factors.Include(p=>p.Customer).AsNoTracking().AsQueryable();//.Select(p => p.Price) - db.payments.Where(p => p.FactorId == 1).Select(p=>p.Price).Sum();

            if (search != null)
            {
                if (search.Contains("$$$"))
                {
                    string s = search.Remove(search.Length - 4);
                    query0 = query0.Where(x => x.CustomerId.ToString().ToLower().Contains(s.ToLower()));
                }
                else
                {
                    query0 = query0.Where(x => x.Price.ToString().ToLower().Contains(search.ToLower()) ||
                    x.Customer.Name.Contains(search.ToLower()) ||
                    x.FactorNo.ToString().ToLower().Contains(search.ToLower()));
                }
            }
            var query=query0.ToList();
            foreach (var item in query)
            {
                var payment = db.payments.Where(p => p.FactorId == item.Id).AsNoTracking().ToList();
                var minn = item.Price - payment.Select(p => p.Price).Sum();
                if (minn != 0)
                {
                    outPutFactorPayments.Add(new OutPutFactorPayment()
                    {
                        factor = item,
                        Price = minn
                    });
                }
            }
             
            int numberOfObjectsPerPage = countRecord;
            var x = outPutFactorPayments.Count();



            outPutFactorPayments = outPutFactorPayments.AsQueryable().OrderByDescending(p => p.factor.Id)
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage).AsNoTracking().ToList();


            //var queryfinal = query.Join(db.payments, fa => fa.Id, pay => pay.FactorId, (fa, pay) => new Payment
            //{
            //    Price = pay.Price,
            //    FactorId = pay.FactorId,
            //});
            

            return new OutPutDebtFactorPaging()
            {
                currentPage = pageNumber,
                totalRecord = x,
                factors = outPutFactorPayments
               // factors = query.AsNoTracking(),
               // payments = queryfinal
            };
        }

        public async Task<int> UpdateFactor(Factor factor)
        {
            int result = 0;
            if(db != null)
            {
                db.factors.Update(factor);
                result = db.SaveChanges();
                return result;
            }
            return result;
        }
    }
}
