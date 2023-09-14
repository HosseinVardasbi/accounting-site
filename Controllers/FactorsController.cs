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
using HamedProject02.Features.Factor.Queries.Select;
using HamedProject02.Features.Factor.Commands.Insert;
using HamedProject02.Features.Customer.Queries.Select;
using HamedProject02.Features.Factor.Queries.SelectById;
using HamedProject02.Features.Factor.Commands.Update;
using HamedProject02.Features.Factor.Commands.Delete;
using HamedProject02.Features.Customer.Queries.SelectById;
using HamedProject02.Helper;
using System.Globalization;
using HamedProject02.Features.Factor.Queries.SelectByPage;
using Microsoft.AspNetCore.Authorization;
using HamedProject02.Models;
using HamedProject02.Features.Payment.Commands.Insert;
using HamedProject02.Features.Payment.Queries.Select;
using HamedProject02.Features.Payment.Queries.SelectById;
using HamedProject02.Features.Payment.Commands.InserList;
using HamedProject02.Features.Payment.Commands.Update;
using HamedProject02.Features.Payment.Commands.UpdateList;
using HamedProject02.Features.Factor.Queries.SelectDebtByPage;

namespace HamedProject02.Controllers
{
    public class FactorsController : Controller
    {
        private readonly IMediator mediator;

        public FactorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: Factors
        //[Authorize("Admin")]
        //public async Task<IActionResult> Index(string option, string search)
        //{
        //    //var factors = await mediator.Send(new GetFactorListQuery());
        //    //if (option == "Price")
        //    //{
        //    //    var priceSearch = factors.Where(p => p.Price.ToString().Contains(search));
        //    //    return View(priceSearch);
        //    //}
        //    //if (option == "FactorNo")
        //    //{
        //    //    var idSearch = factors.Where(x => x.FactorNo.ToString().Contains(search));
        //    //    return View(idSearch);
        //    //}

        //    var factors = await mediator.Send(new GetFactorByPageQuery() {
        //        currentPage = 1,
        //        totalRecord = 2
        //    });
        //    factors.customers = await mediator.Send(new GetCustomerListQuerie());
        //    return View(factors);
        //}
        //[HttpPost]
        //[AllowAnonymous]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy ="Admin")]
        public async Task<IActionResult> Index(string search, string option,
            string sort, int? pageNumber = 1, int? countRecord = 10)
        {
            var factors = await mediator.Send(new GetFactorByPageQuery()
            {
                currentPage = (int)pageNumber,
                totalRecord = (int)countRecord,
                search = search,
                option = option,
                sort = sort
            });
            factors.customers = await mediator.Send(new GetCustomerListQuerie());
            factors.payments = await mediator.Send(new GetPaymentListQuery());
            ViewBag.tempPaige = pageNumber + 1;
            ViewBag.privPage = pageNumber - 1;
            if (search != null)
                factors.search = search;
            return View(factors);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DebtIndex(string search, string option,
            string sort, int? pageNumber = 1, int? countRecord = 10)
        {
            var factors = await mediator.Send(new GetDebtFactorByPageQuery()
            {
                currentPage = (int)pageNumber,
                totalRecord = (int)countRecord,
                search = search,
                option = option,
                sort = sort
            });

            List<Factor> factorsList = new List<Factor>();
            //foreach(var item in factors.factors)
            //{
                //var nn = item.payments.Sum(p => p.Price);

                //var payment = await mediator.Send(new GetPaymentByFactorIdQuery() { Id = item.Id });
                //var priceKol = item.Price;
                //var payPrice = payment.Sum(p => p.Price);
                //var debtStatus = priceKol - payPrice;
                //if (debtStatus != 0)
                //{
                //    factorsList.Add(item);
                //   // factors.factors.Skip(1).Where(p => p.Id == item.Id);
                //}
            //}
            //var debts = factors.factors.Where(p => p.)
           // factors.customers = await mediator.Send(new GetCustomerListQuerie());
           // factors.payments = await mediator.Send(new GetPaymentListQuery());
            ViewBag.tempPaige = pageNumber + 1;
            ViewBag.privPage = pageNumber - 1;
            if (search != null)
                factors.search = search;
            //factors.factors = factorsList;


          //  factors.totalRecord = factorsList.Count();
            return View(factors);
        }
        public async Task<IActionResult> CustomerIdFactor(long id)
        {
            var factors = await mediator.Send(new GetFactorByCustomerId() { Id = id});
            var customer = await mediator.Send(new GetCustomerByIdQuery { Id = id });
            ViewBag.Name = customer.Name;
            ViewBag.Id = id;
            return View(factors);
        }

        // GET: Factors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var factors = await mediator.Send(new GetFactorByIdQuery() { Id = id });
            var customer = await mediator.Send(new GetCustomerByIdQuery() { Id = factors.CustomerId });
            var payment = await mediator.Send(new GetPaymentByFactorIdQuery() { Id= id });
            var debtStatus = factors.Price - payment.Select(p => p.Price).Sum();
            if(factors == null)
                return NotFound();
            ViewBag.customerName = customer.Name;
            ViewBag.debtStatus = debtStatus;
            ViewBag.payments = payment.ToList();
            return View(factors);
        }
        //[Authorize(Roles = "Admin")]
        // GET: Factors/Create
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var customers = await mediator.Send(new GetCustomerListQuerie());
            ViewBag.CustomerId = new SelectList(customers, "Id", "Name");            
            return View();
        }

        public async Task<IActionResult> CreateByCustomer(long id, string Name)
        {
            ViewBag.Name = Name;
            return View(new Factor() { CustomerId = id});
        }
        // POST: Factors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create([Bind("Id,FactorNo,FPrice,paymentViewModels,Detail,CustomerId")] 
        FactorPaymentViewModel factorPayment, 
            string myDate)
        {
            
            DateTime date = DateTime.Now;
            if (myDate != null)
            {
                date = DateConverter.shamsiToMiladi(factorPayment.DateTime.ToString());
            }
            var factors = await mediator.Send(new InsertFactorCommand(
                factorPayment.FactorNo,
                factorPayment.FPrice,
                date,
                factorPayment.Detail,
                factorPayment.CustomerId
               
                ));
 
            if (factors != null)
            {
                if (  factorPayment.paymentViewModels != null
                   && 
                   factorPayment.paymentViewModels.Count > 0)
                {
                    List<InsertPaymentCommand> insertPaymentCommands = new List<InsertPaymentCommand>();
                    foreach (var item in factorPayment.paymentViewModels)
                    {
                        insertPaymentCommands.Add(new InsertPaymentCommand(item.Type, item.PayPrice, item.PDetail, false, factors.Id));
                    }
                    var payy = await mediator.Send(new InsertListPaymentCommand()
                    {
                        InsertListPaymentCommands = insertPaymentCommands
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

            else
            {
                //ModelState.AddModelError(nameof(factorPayment.FactorNo), "فاکتور با این شماره وجود دارد");
                //return View(factorPayment);
            }
            var customers = await mediator.Send(new GetCustomerListQuerie());
                ViewBag.CustomerId = new SelectList(customers, "Id", "Name");

            return new JsonResult(false);
            // return View(factors);
        }

        // GET: Factors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            //ViewBag.Fid = id;
            FactorPaymentViewModel factorPayment = new FactorPaymentViewModel();
            var factors = await mediator.Send(new GetFactorByIdQuery() { Id = id });
            var payment = await mediator.Send(new GetPaymentByFactorIdQuery() { Id = id});
            //factorPayment.
            if (factors == null)
                return View("NotFound");
            factorPayment.Id = factors.Id;
            factorPayment.FactorNo = factors.FactorNo;
            factorPayment.FPrice = factors.Price;
            factorPayment.DateTime = factors.DateTime;
            factorPayment.Detail = factors.Detail;
            List<PaymentViewModel> paymentViewModels = new List<PaymentViewModel>();
            if (payment != null)
            {
                foreach (var item in payment)
                {
                    paymentViewModels.Add(new PaymentViewModel()
                    {
                        Type = item.Type,
                        PayPrice = item.Price,
                        PDetail = item.Detail
                    });

                }
            }
            if(paymentViewModels != null && paymentViewModels.Count > 0)
            {
                factorPayment.paymentViewModels = paymentViewModels;
            }
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = factors.CustomerId });
            List<Customer> ccc = new List<Customer>();
            ccc.Add(customers);
            //ccc.AsEnumerable();
            IEnumerable<Customer> c = ccc;
            
            ViewBag.CustomerId = new SelectList(c, "Id", "Name");
            return View(factorPayment);
        }

        // POST: Factors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([Bind("Id,FactorNo,FPrice,paymentViewModels,Detail,CustomerId")]
        FactorPaymentViewModel factorPayment,
            string myDate)
        {
            //if (id != factor.Id)
            //{
            //    return NotFound();
            //}
            //var factors = await mediator.Send(new UpdateFactorCommand(factor.Id, factor.FactorNo,
            //    factor.Price, factor.DateTime, factor.Detail));
            var factors = mediator.Send(new UpdateFactorCommand(factorPayment.Id, factorPayment.FactorNo,
                factorPayment.FPrice, factorPayment.DateTime, factorPayment.Detail));
            //if (factors != null)
            //{
            //    if (factorPayment.paymentViewModels != null
            //       &&
            //       factorPayment.paymentViewModels.Count > 0)
            //    {
            //        List<UpdatePaymentCommand> updatePaymentCommands = new List<UpdatePaymentCommand>();
            //        foreach (var item in factorPayment.paymentViewModels)
            //        {
            //            updatePaymentCommands.Add(new UpdatePaymentCommand(id, item.Type, item.PayPrice,  false, factors.Id, item.PDetail));
            //        }
            //        var payy = await mediator.Send(new UpdateListPaymentCommand()
            //        {
            //            UpdateListPaymentCommands = updatePaymentCommands,
            //        });
            //        if (payy)
            //        {
            //            return new JsonResult(true);
            //            // return RedirectToAction(nameof(Index));
            //        }
            //    }
            //    else
            //        return new JsonResult(true);
            //    // return RedirectToAction(nameof(Index));

            //}
            if (factors != null)
            {
                if (factorPayment.paymentViewModels != null
                   &&
                   factorPayment.paymentViewModels.Count > 0)
                {
                    List<InsertPaymentCommand> insertPaymentCommands = new List<InsertPaymentCommand>();
                    foreach (var item in factorPayment.paymentViewModels)
                    {
                        long payPrice = Convert.ToInt64(item.PayPrice.ToString().Replace(",", ""));
                        insertPaymentCommands.Add(new InsertPaymentCommand(item.Type, payPrice
                            , item.PDetail, false, factorPayment.Id));
                    }
                    var payy = await mediator.Send(new InsertListPaymentCommand()
                    {
                        InsertListPaymentCommands = insertPaymentCommands
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
            if (factors != null)
                return RedirectToAction(nameof(Index));
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = factorPayment.CustomerId });
            ViewBag.CustomerId = new SelectList((IEnumerable<Customer>)customers, "Id", "Name");
            return View(factorPayment);
        }

        // GET: Factors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var factors = await mediator.Send(new GetFactorByIdQuery() { Id = id });
            if(factors == null)
                return NotFound();
            return View(factors);
        }

        // POST: Factors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factors = await mediator.Send(new GetFactorByIdQuery() { Id = id });
            await mediator.Send(new DeleteFactorCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public JsonResult AddPaymentFactor(int type, long price, int factorId)
        {
            return Json(true);
        }
        //private bool FactorExists(int id)
        //{
        //  return (_context.Factor?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
