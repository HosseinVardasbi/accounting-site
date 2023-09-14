using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer02.Domain;
using HamedProject02.Data;
using MediatR;
using HamedProject02.Features.Debt.Queries.SelectByPage;
using HamedProject02.Features.Customer.Queries.Select;
using HamedProject02.Features.DebtPayment.Queries.Select;
using HamedProject02.Models;
using HamedProject02.Helper;
using HamedProject02.Features.Debt.Commands.Insert;
using HamedProject02.Features.DebtPayment.Commands.Insert;
using HamedProject02.Features.DebtPayment.Commands.InsertList;
using HamedProject02.Features.Debt.Queries.SelectById;
using HamedProject02.Features.Customer.Queries.SelectById;
using HamedProject02.Features.DebtPayment.Queries.SelectById;
using Microsoft.AspNetCore.Authorization;
using HamedProject02.Features.Debt.Commands.Update;

namespace HamedProject02.Controllers
{
    public class DebtsController : Controller
    {
        private readonly IMediator mediator;

        public DebtsController(IMediator mediator) { this.mediator = mediator; }

        // GET: Debts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string search, string option,
            string sort, int? pageNumber = 1, int? countRecord = 10)
        {
            var debts = await mediator.Send(new GetDebtByPageQuery()
            {
                currentPage = (int)pageNumber,
                totalRecord = (int)countRecord,
                search = search,
                option = option,
                sort = sort
            });
            debts.customers = await mediator.Send(new GetCustomerListQuerie());
            debts.debtPayments = await mediator.Send(new GetDebtPaymentListQuery());
            ViewBag.tempPaige = pageNumber + 1;
            ViewBag.privPage = pageNumber - 1;
            if (search != null)
                debts.search = search;
            return View(debts);
        }

        // GET: Debts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var debts = await mediator.Send(new GetDebtByIdQuery() { Id = id });
            var customer = await mediator.Send(new GetCustomerByIdQuery() { Id = debts.CustomerId });
            var debtPayment = await mediator.Send(new GetDebtPaymentByDebtIdQuery() { Id = id });
            var debtStatus = debts.Price - debtPayment.Select(p => p.Price).Sum();
            if (debts == null)
                return NotFound();
            ViewBag.customerName = customer.Name;
            ViewBag.debtStatus = debtStatus;
            ViewBag.payments = debtPayment.ToList();
            return View(debts);
        }

        // GET: Debts/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var customers = await mediator.Send(new GetCustomerListQuerie());
            ViewBag.CustomerId = new SelectList(customers, "Id", "Name");
            return View();
        }

        // POST: Debts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create([Bind("Id,DPrice,DDetail,Created,CustomerId, DPaymentViewModels")]
        DebtPaymentViewModel debtPaymentViewModel, string myDate)
        {
            DateTime date = DateTime.Now;
            if (debtPaymentViewModel.Created != null)
            {
                date = DateConverter.shamsiToMiladi(debtPaymentViewModel.Created.ToString());
            }
            var debts = await mediator.Send(new InsertDebtCommand(
                debtPaymentViewModel.DPrice,
                debtPaymentViewModel.DDetail,
                date,
                debtPaymentViewModel.CustomerId
                ));
            if (debts != null)
            {
                if (debtPaymentViewModel.DPaymentViewModels != null
                   &&
                   debtPaymentViewModel.DPaymentViewModels.Count > 0)
                {
                    List<InsertDebtPaymentCommand> insertPaymentCommands = new List<InsertDebtPaymentCommand>();
                    foreach (var item in debtPaymentViewModel.DPaymentViewModels)
                    {
                        insertPaymentCommands.Add(new InsertDebtPaymentCommand(item.Type, item.PPrice, item.PDetail ,debts.Id));
                    }
                    var payy = await mediator.Send(new InsertDebtPaymentListCommand()
                    {
                        insertDebtPaymentListCommands = insertPaymentCommands
                    });
                    if (payy)
                    {
                        return new JsonResult(true);
                        // return RedirectToAction(nameof(Index));
                    }
                }
                else
                    return new JsonResult(true);
                // return RedirectToAction(nameof(Index));

            }
            var customers = await mediator.Send(new GetCustomerListQuerie());
            ViewBag.CustomerId = new SelectList(customers, "Id", "Name");

            return new JsonResult(false);
        }

        // GET: Debts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            DebtPaymentViewModel debtPayment = new DebtPaymentViewModel();
            var debts = await mediator.Send(new GetDebtByIdQuery() { Id = id });
            var payment = await mediator.Send(new GetDebtPaymentByDebtIdQuery() { Id = id });
            if (debts == null)
                return View("NotFound");
            debtPayment.Id = debts.Id;
            debtPayment.DPrice = debts.Price;
            debtPayment.Created = debts.Created;
            debtPayment.DDetail = debts.Detail;
            List<DPaymentViewModel> paymentViewModels = new List<DPaymentViewModel>();
            if (payment != null)
            {
                foreach (var item in payment)
                {
                    paymentViewModels.Add(new DPaymentViewModel()
                    {
                        Type = item.Type,
                        PPrice = item.Price,
                        PDetail = item.Detail
                    });

                }
            }
            if (paymentViewModels != null && paymentViewModels.Count > 0)
            {
                debtPayment.DPaymentViewModels = paymentViewModels;
            }
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = debts.CustomerId });
            List<Customer> ccc = new List<Customer>();
            ccc.Add(customers);
            //ccc.AsEnumerable();
            IEnumerable<Customer> c = ccc;

            ViewBag.CustomerId = new SelectList(c, "Id", "Name");
            return View(debtPayment);
        }

        // POST: Debts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,DPrice,DDetail,DPaymentViewModels,Created,CustomerId")]
        DebtPaymentViewModel debtPayment,
            string myDate)
        {
            var debts = mediator.Send(new UpdateDebtCommand(debtPayment.Id, debtPayment.DPrice,
                debtPayment.DDetail, debtPayment.Created));
            if (debts != null)
            {
                if (debtPayment.DPaymentViewModels != null
                   &&
                   debtPayment.DPaymentViewModels.Count > 0)
                {
                    List<InsertDebtPaymentCommand> insertPaymentCommands = new List<InsertDebtPaymentCommand>();
                    foreach (var item in debtPayment.DPaymentViewModels)
                    {
                        insertPaymentCommands.Add(new InsertDebtPaymentCommand(item.Type, item.PPrice, item.PDetail, debtPayment.Id));
                    }
                    var payy = await mediator.Send(new InsertDebtPaymentListCommand()
                    {
                        insertDebtPaymentListCommands = insertPaymentCommands
                    });
                    if (payy)
                    {
                        return new JsonResult(true);
                        // return RedirectToAction(nameof(Index));
                    }
                }
                else
                    return new JsonResult(true);
                // return RedirectToAction(nameof(Index));

            }
            if (debts != null)
                return RedirectToAction(nameof(Index));
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = debtPayment.CustomerId });
            ViewBag.CustomerId = new SelectList((IEnumerable<Customer>)customers, "Id", "Name");
            return View(debtPayment);
        }

        //// GET: Debts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Debt == null)
        //    {
        //        return NotFound();
        //    }

        //    var debt = await _context.Debt
        //        .Include(d => d.Customer)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (debt == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(debt);
        //}

        //// POST: Debts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Debt == null)
        //    {
        //        return Problem("Entity set 'HamedProject02Context.Debt'  is null.");
        //    }
        //    var debt = await _context.Debt.FindAsync(id);
        //    if (debt != null)
        //    {
        //        _context.Debt.Remove(debt);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool DebtExists(int id)
        //{
        //  return (_context.Debt?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
