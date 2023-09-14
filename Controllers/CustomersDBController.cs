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

namespace HamedProject02.Controllers
{
    public class CustomersDBController : Controller
    {
        private readonly DB_context _context;

        public CustomersDBController(DB_context context)
        {
            _context = context;
        }

        // GET: CustomersDB
        public async Task<IActionResult> Index()
        {
              return _context.customers != null ? 
                          View(await _context.customers.ToListAsync()) :
                          Problem("Entity set 'HamedProject02Context.Customer'  is null.");
        }

        // GET: CustomersDB/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var customer = await _context.customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: CustomersDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomersDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Customer customer)
        {
            //if (ModelState.IsValid)
            //{
                _context.customers.Add(customer);
                _context.SaveChanges();//.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(customer);
        }

        // GET: CustomersDB/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: CustomersDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: CustomersDB/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var customer = await _context.customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.customers == null)
            {
                return Problem("Entity set 'HamedProject02Context.Customer'  is null.");
            }
            var customer = await _context.customers.FindAsync(id);
            if (customer != null)
            {
                _context.customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(long id)
        {
          return (_context.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
