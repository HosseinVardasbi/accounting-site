using DataLayer02.Domain;
using HamedProject02.Features.Customer.Commands.Delete;
using HamedProject02.Features.Customer.Commands.Insert;
using HamedProject02.Features.Customer.Commands.Update;
using HamedProject02.Features.Customer.Queries.Select;
using HamedProject02.Features.Customer.Queries.SelectById;
using HamedProject02.Features.Customer.Queries.SelectByPage;
using HamedProject02.Features.Debt.Queries.SelectById;
using HamedProject02.Features.DebtPayment.Queries.Select;
using HamedProject02.Features.Factor.Queries.SelectById;
using HamedProject02.Features.Payment.Queries.Select;
using HamedProject02.Features.Payment.Queries.SelectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Linq;

namespace HamedProject02.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator mediator;
        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await mediator.Send(new GetCustomerListQuerie());
            return customers;
        }
        [HttpGet("Id")]
        public async Task<Customer> GetCustomerById(long Id)
        {
            var customers = await mediator.Send(new GetCustomerByIdQuery() {Id = Id});
            return customers;
        }
        public async Task<IActionResult> Details(long id)
        {
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = id });
            var factor = await mediator.Send(new GetFactorByCustomerId() { Id = id});
            var factorId = factor.Select(p => p.Id);
            var payment = await mediator.Send(new GetPaymentListQuery());

            var debts = await mediator.Send(new GetDebtByCustomerIdQuery() { Id = id });
            var debtId = debts.Select(p => p.Id);
            var debtPayments = await mediator.Send(new GetDebtPaymentListQuery());

            long debtStatus = 0;
            long bankDebt = 0;
            foreach (var fId in factorId) {
                var x = payment.Where(p => p.FactorId == fId).Select(p => p.Price).Sum();
                debtStatus += x;
            }
            debtStatus = factor.Select(p => p.Price).Sum() - debtStatus;
            foreach (var dId in debtId)
            {
                var x = debtPayments.Where(p => p.DebtId == dId).Select(p => p.Price).Sum();
                bankDebt += x;
            }
            bankDebt = debts.Select(p => p.Price).Sum() - bankDebt;
            if (customers == null)
            {
                return NotFound();
            }
            ViewBag.factorDeptStatus = debtStatus;
            ViewBag.bankDeptStatus = bankDebt;
            ViewBag.wholeDeptStatus = bankDebt + debtStatus;
            return View(customers);
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomer([Bind("Id,Name,PhoneNumber,Adress")] Customer customer)
        {
            
                var customers = await mediator.Send(new InsertCustomerCommand(
                                customer.Name, customer.PhoneNumber, customer.Adress));
            if (customers != null)
                return RedirectToAction(nameof(Index));
            else
            {
                ModelState.AddModelError(nameof(customer.Name), "کاربر با این نام وجود دارد");
                return View(customer);
            }

            return View(customer);
        }
        //[HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomer(long id)
        {

            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = id });
            if(customers == null)
                return View("notFound");
            return View(customers);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomer([Bind("Id,Name,PhoneNumber,Adress")] Customer customer)
        {

            var customers = await mediator.Send(new UpdateCustomerCommand(customer.Name, customer.Id,
                customer.PhoneNumber, customer.Adress));
            if (customers != null)
                return RedirectToAction(nameof(Index));
            return View(customers);
        }
        //[HttpPost]
        public async Task<IActionResult> DeleteCustomer(long Id)
        {
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = Id });
            if (customers == null)
                return View("notFound");
            return View(customers);
            
        }
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomerConfirme(long Id)
        {
            var customers = await mediator.Send(new GetCustomerByIdQuery() { Id = Id });
            await mediator.Send(new DeleteCustomerCommand(Id));
            return RedirectToAction(nameof(Index));

        }
        //public async Task<IActionResult> Index(string option, string search)
        //{
        //    //var customers = await mediator.Send(new GetCustomerListQuerie());
        //    //if (option == "Name")
        //    //{
        //    //    var nameSearch = customers.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        //    //    return View("CustomerView", nameSearch);
        //    //}
        //    //if (option == "Id")
        //    //{
        //    //    var idSearch = customers.Where(x => x.Id.ToString().Contains(search));
        //    //    return View("CustomerView", idSearch);
        //    //}
        //    //if (pageNumber != null)
        //    //{
        //    var customer = await mediator.Send(new GetCustomerByPageQuery()
        //    {
        //        currentPage = (int)1,
        //        totalRecord = (int)2
        //    });
        //    return View("CustomerView", customer);
        //    //}
        //    //return View("CustomerView", customers);
        //}
        //[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string search, string option,
            string sort, int? pageNumber=1, int? countRecord = 10) {
            //search = ViewBag.search;
            //if (search != null)
            //    ViewBag.search = search;
            var customers =await mediator.Send(new GetCustomerByPageQuery() { currentPage = (int)pageNumber,
            totalRecord = (int)countRecord, search = search, option = option, sort = sort});
            ViewBag.tempPaige = pageNumber+1;
            ViewBag.privPage = pageNumber - 1;
            if (search != null)
                customers.search = search;
            return View("CustomerView", customers);
        }
        public JsonResult AjaxIndex(string search)
        {
            //var customers = await mediator.Send(new GetCustomerByPageQuery()
            //{
            //    currentPage = (int)pageNumber,
            //    totalRecord = (int)countRecord,
            //    search = search,
            //    option = option,
            //    sort = sort
            //});
            //ViewBag.tempPaige = pageNumber + 1;
            //ViewBag.privPage = pageNumber - 1;
            //if (search != null)
            //    ViewBag.search = search;
            return Json(new { redirectToUrl = Url.Action("Index", "Customer", new {search = search}) });
        }

    }
}
