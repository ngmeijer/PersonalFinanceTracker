using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PFT.Data;
using PFT.Identity;
using PFT.Repositories.Budgets;
using PFT.Repositories.Investments;
using PFT.Services.Budgets;
using PFT.Services.Investments;

var builder = WebApplication.CreateBuilder(args);

//Set up database connection
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PFTContext>(options => {
    options.UseSqlServer(connectionString);
});


//Set up user registering functionality
builder.Services.AddDefaultIdentity<PFTUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PFTContext>();


//Set up services and repositories
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();

builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();


//Initialize controllers with views
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build(); 

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "investments",
//    pattern: "{controller=Investments}/{action=RefreshData}");
app.MapRazorPages();

app.Run();
