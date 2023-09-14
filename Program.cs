using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Features.Customer.Queries.Select;
using HamedProject02.Repository.Interface;
using HamedProject02.Repository.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HamedProject02.Data;
using Microsoft.AspNetCore.Authentication;
using HamedProject02.Helper;
using Microsoft.AspNetCore.Authorization;
using HamedProject02.Helper.CustomHandler;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HamedProject02Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HamedProject02Context") ?? throw new InvalidOperationException("Connection string 'HamedProject02Context' not found.")));


builder.Services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "UserLoginCookie"; // Name of cookie     
                    config.LoginPath = "/Account/SignIn"; // Path for the redirect to user login page    
                    config.AccessDeniedPath = "/Account/UserAccessDenied";
                    config.LogoutPath = "/Account/SignIn";
                    config.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("UserPolicy", policyBuilder =>
    {
        policyBuilder.UserRequireCustomClaim(ClaimTypes.Email); 
    });
});

builder.Services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();



// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,
//    BasicAuthenticationHandler>("BasicAuthentication", null);
//builder.Services.AddMvc(p => p.EnableEndpointRouting = false);
builder.Services.AddDbContext<DB_context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_test"));
});

//builder.Services.AddMediatR(typeof(GetCustomerListHandler));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IFactorRepository, FactorRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IDebtRepository, DebtRepository>();
builder.Services.AddScoped<IDebtPaymentRepository, DebtPaymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddSwaggerGen();
var app = builder.Build();
MyMethodHere();
//app.UseRouting();
//app.UseAuthorization();
static void MyMethodHere()
{

    //CustomerService customerService = new CustomerService();
    //customerService.Name("hamed");
    //customerService.Name("mehdi");
    //FactorService factorService = new FactorService();
    //factorService.FactorNo(1234).price(3500).DateTime(DateTime.Now);
    //factorService.FactorNo(5678).price(8900).DateTime(DateTime.Now);
    //PaymentService paymentService = new PaymentService();
    //paymentService.FactorNo(1234).paid(false).price(1000).Type("چک");
    //paymentService.FactorNo(1234).paid(true).price(1500).Type("نقد");
    //paymentService.FactorNo(1234).paid(true).price(1000).Type("کارت به کارت");
    //paymentService.FactorNo(5678).paid(true).price(2900).Type("نقد");
    //paymentService.FactorNo(5678).paid(false).price(6000).Type("");

    //DB_context context = new DB_context();
    ////var notPaid = context.payments.Where(p => p.paid == false).ToList();
    //var x = context.customers.ToList();
    //var notPaid02 = context.customers.Include(p => p.Factors).ThenInclude(p => p.payments.Where(p => p.paid == false));
    //foreach (var p in notPaid02)
    //{
    //    Console.WriteLine(p.Name);
    //}
    Console.WriteLine("MyMethodHere says hello World!");
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
