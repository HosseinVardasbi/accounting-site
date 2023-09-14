using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Models;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamedProject02.Repository.Service
{
    public class CustomerRepository : ICustomerRepository
    {
        DB_context db;
        public CustomerRepository(DB_context db)
        {
            this.db = db;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            int a = db.customers.Where(p => p.Name == customer.Name).Count();
            if (db != null && a == 0)
            {
                var result = db.customers.Add(customer);
                db.SaveChanges();
                return result.Entity;
            }
            return null;
            
        }

        public async Task<long> DeleteCustomer(long Id)
        {
            long result = 0;
            if(db != null)
            {
                var cust = db.customers.SingleOrDefault(c => c.Id == Id);
                if(cust != null)
                {
                    db.customers.Remove(cust);
                    result = db.SaveChanges();
                }
                return result;
            }
            return result;
        }

        public async Task<Customer> GetCustomerById(long Id)
        {
            if (this.db != null)
            {
                return (from p in db.customers 
                              where p.Id == Id
                              select new Customer { Id = p.Id, Name = p.Name, PhoneNumber = p.PhoneNumber,
                              Adress = p.Adress}).FirstOrDefault();
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            if (this.db != null)
            {
                var x = db.customers.ToList();
                return (from p in db.customers
                       select new Customer { Id = p.Id, Name = p.Name,
                           PhoneNumber = p.PhoneNumber,
                           Adress = p.Adress
                       }).OrderBy(p => p.Id).ToList();
            }
            return null;
        }

        public async Task<OutPutCustomerPaging> GetCustomersByPage(int pageNumber, int countRecord,
            string search, string option, string sort)
        {
            //var x = db.customers.Count();
            //var totalPage02 = x / countRecord;
            var query = db.customers.AsQueryable();
            if(search != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()) ||
                x.PhoneNumber.ToLower().Contains(search.ToLower()) || 
                x.Adress.ToLower().Contains(search.ToLower()));
                //if (option == "Name")
                //{
                //    query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
                //}
                //if (option == "Id")
                //{
                //    query = query.Where(x => x.Id.ToString().Contains(search.ToLower()));
                //}
            }
            int numberOfObjectsPerPage = countRecord;
            var x = query.Count();
            query = query.OrderByDescending(p => p.Id)
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return new OutPutCustomerPaging()
            {
                currentPage = pageNumber,
                customers = query.AsNoTracking(),
                
                 totalRecord = x
            };
        }

        public async Task<long> UpdateCustomer(Customer customer)
        {
            long result = 0;
            if (db != null)
            {
                db.customers.Update(customer);
                result = db.SaveChanges();
                return result;
            }
            return result;
        }


        
        //Task<List<Customer>> ICustomerRepository.GetCustomers()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
