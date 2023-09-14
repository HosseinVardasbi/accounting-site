using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer02.Domain;
using HamedProject02.Data;
using DataLayer02.Context;
using HamedProject02.Repository.Interface;

namespace HamedProject02.Controllers
{
    public class CustomersReposController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersReposController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: CustomersRepos
        public ActionResult Index()
        {
            //var x = customerRepository.GetCustomers();
            return View(customerRepository.GetCustomers().Result);
                //!= null ? 
                //          View(await customerRepository.GetCustomers().) :
                //          Problem("Entity set 'HamedProject02Context.Customer'  is null.");
        }

        // GET: CustomersRepos/Details/5
        //public async Task<IActionResult> Details(long id)
        //{
        //    if (id == null || customerRepository.GetCustomers() == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = customerRepository.GetCustomerById(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        // GET: CustomersRepos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomersRepos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Customer customer)
        {
            //if (ModelState.IsValid)
            //{
                customerRepository.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            //}
            return View(customer);
        }

        // GET: CustomersRepos/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (id == null || customerRepository.GetCustomers() == null)
            {
                return NotFound();
            }

            //var customer = await _context.customers.FindAsync(id);
            var customer = await customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: CustomersRepos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long Id, [Bind("Id,Name")] Customer customer)
        {
            if (Id != customer.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    customerRepository.UpdateCustomer(customer);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CustomerExists(customer.Id))
                    //{
                        return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(customer);
        }

        // GET: CustomersRepos/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null || customerRepository.GetCustomers() == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersRepos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (customerRepository.GetCustomers() == null)
            {
                return Problem("Entity set 'HamedProject02Context.Customer'  is null.");
            }
            var customer = await customerRepository.GetCustomerById(id);
            if (customer != null)
            {
                await customerRepository.DeleteCustomer(id);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CustomerExists(long id)
        //{
        //  return (_context.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
