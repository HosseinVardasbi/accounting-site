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
using MediatR;
using HamedProject02.Features.Payment.Queries.Select;
using HamedProject02.Features.Factor.Queries.Select;
using HamedProject02.Features.Payment.Commands.Insert;
using HamedProject02.Features.Payment.Queries.SelectById;
using HamedProject02.Features.Factor.Queries.SelectById;
using HamedProject02.Features.Payment.Commands.Update;
using HamedProject02.Features.Payment.Commands.Delete;
using HamedProject02.Features.Customer.Queries.SelectById;
using HamedProject02.Features.Payment.Queries.SelectByPage;

namespace HamedProject02.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await mediator.Send(new GetPaymentByPageQuery()
            {
                currentPage = 1,
                totalRecord = 2
            });
            return View(payments);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search, string option,
            string sort, int? pageNumber = 1, int? countRecord = 2)
        {
            var payments = await mediator.Send(new GetPaymentByPageQuery()
            {
                currentPage = (int)pageNumber,
                totalRecord = (int)countRecord,
                search = search,
                option = option,
                sort = sort
            });
            return View(payments);
        }
        public async Task<IActionResult> FactorIdPayment(int id)
        {
            var payments = await mediator.Send(new GetPaymentByFactorIdQuery() { Id = id });
            var factor = await mediator.Send(new GetFactorByIdQuery { Id = id });
            var customer = await mediator.Send(new GetCustomerByIdQuery { Id = factor.CustomerId });
            ViewBag.Name = customer.Name;
            ViewBag.Id = id;
            return View(payments);
        }
        public async Task<IActionResult> CreateByFactor(int id, string Name)
        {
            List<PaymentType> payType = new List<PaymentType>();
            payType.Add(PaymentType.Naqd);
            payType.Add(PaymentType.Cart);
            payType.Add(PaymentType.pos);
            payType.Add(PaymentType.chek);
            IEnumerable<PaymentType> pt = payType;
            ViewBag.Type = new SelectList(pt, "");
            ViewBag.Name = Name;
            ViewBag.FactorId = id;
            return View(new Payment() { FactorId = id });
        }
        //// GET: Payments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Payment == null)
        //    {
        //        return NotFound();
        //    }

        //    var payment = await _context.Payment
        //        .Include(p => p.Factor)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(payment);
        //}

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            var factors = await mediator.Send(new GetFactorListQuery());
            ViewBag.FactorId = new SelectList(factors, "Id", "FactorNo", "Price");
            List<PaymentType> payType = new List<PaymentType>();
            payType.Add(PaymentType.Naqd);
            payType.Add(PaymentType.Cart);
            payType.Add(PaymentType.pos);
            payType.Add(PaymentType.chek);
            IEnumerable<PaymentType> pt = payType;
            ViewBag.Type = new SelectList(pt, "");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Price,Paid,FactorId")] Payment payment)
        {
            var payments = await mediator.Send(new InsertPaymentCommand(payment.Type,
                payment.Price,
                payment.Detail,
                payment.Paid,
                payment.FactorId));
            //return View(payment);
            if (payments != null)
                return RedirectToAction(nameof(Index));
            return View(payments);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await mediator.Send(new GetPaymentByIdQuery() { Id = id});
            if (payment == null)
            {
                return NotFound();
            }
            var factors = await mediator.Send(new GetFactorByIdQuery() { Id = payment.FactorId });
            List<Factor> f = new List<Factor>();
            f.Add(factors);
            f.AsEnumerable();
            ViewBag.FactorId = new SelectList(f, "Id", "FactorNo");
            List<PaymentType> payType = new List<PaymentType>();
            payType.Add(payment.Type);
            payType.AsEnumerable();
            ViewBag.Type = new SelectList(payType, "");
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Price,Paid,FactorId")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            var payments = await mediator.Send(new UpdatePaymentCommand(payment.Id, payment.Type,
                payment.Price, payment.Paid, payment.FactorId, payment.Detail));
            if (payments != null)
                return RedirectToAction(nameof(Index));
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await mediator.Send(new GetPaymentByIdQuery() { Id = id });
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await mediator.Send(new GetPaymentByIdQuery() { Id = id });
            await mediator.Send(new DeletePaymentCommand(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool PaymentExists(int id)
        //{
        //  return (_context.Payment?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
