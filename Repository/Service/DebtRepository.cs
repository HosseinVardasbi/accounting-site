using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamedProject02.Repository.Service
{
    public class DebtRepository : IDebtRepository
    {
        DB_context db;
        public DebtRepository(DB_context db)
        {
            this.db = db;
        }

        public async Task<Debt> AddDebt(Debt debt)
        {
            //int a = db.debts.Where(p => p. == debt.).Count();
            if (db != null)
            {
                var result = db.debts.Add(debt);
                db.SaveChanges();
                return result.Entity;
            }
            return null;
        }

        public async Task<int> DeleteDebt(int Id)
        {
            int result = 0;
            if (db != null)
            {
                var debt = db.debts.SingleOrDefault(f => f.Id == Id);
                if (debt != null)
                {
                    db.debts.Remove(debt);
                    result = db.SaveChanges();
                }
                return result;
            }
            return result;
        }

        public async Task<IEnumerable<Debt>> GetDebt()
        {
            if (db != null)
            {
                var x = db.debts.Include(m => m.Customer).ToList();
                return x;
            }
            return null;
        }

        public async Task<IEnumerable<Debt>> GetDebtByCustomerId(long Id)
        {
            if (db != null)
            {
                var debts = db.debts.Include(m => m.Customer).Where(m => m.CustomerId == Id).ToList();
                return debts.AsEnumerable();
            }
            return null;
        }

        public async Task<Debt> GetDebtById(int Id)
        {
            if (db != null)
            {
                return (from p in db.debts
                        where (p.Id == Id)
                        select new Debt
                        {
                            Id = p.Id,
                            Price = p.Price,
                            Detail = p.Detail,
                            Created = p.Created,
                            CustomerId = p.CustomerId
                        }).FirstOrDefault();
            }
            return null;
        }

        public async Task<OutPutDebtPaging> GetDebtsByPage(int pageNumber, int countRecord, string search, string option, string sort)
        {
            var query = db.debts.AsQueryable();
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
                    x.Id.ToString().ToLower().Contains(search.ToLower()));
                }
            }
            int numberOfObjectsPerPage = countRecord;
            var x = query.Count();
            query = query.OrderByDescending(p => p.Id)
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return new OutPutDebtPaging()
            {
                currentPage = pageNumber,
                totalRecord = x,
                debts = query.AsNoTracking()
            };
        }

        public async Task<int> UpdateDebt(Debt debt)
        {
            int result = 0;
            if (db != null)
            {
                db.debts.Update(debt);
                result = db.SaveChanges();
                return result;
            }
            return result;
        }
    }
}
