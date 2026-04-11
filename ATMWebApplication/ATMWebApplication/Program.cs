using ATMWebApplication.Domain.Entities;
using ATMWebApplication.Domain.ValueObjects;
using ATMWebApplication.Services;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.State;
using ATMWebApplication.State.Interfaces;
using ATMWebApplication.Strategies;
using ATMWebApplication.Strategies.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// 🔥 STEP 1: Create initial ATM state

var account = new Account("A1", 10000); // Example account

var initialCash = new Dictionary<Denomination, int>
{
    { new Denomination(2000), 5 },
    { new Denomination(500), 10 },
    { new Denomination(200), 20 },
    { new Denomination(100), 50 }
};

var inventory = new CashInventory(initialCash);


// STEP 2: Register StateStore as Singleton

builder.Services.AddSingleton<IATMStateStore>(
    new InMemoryATMStateStore(account, inventory)
);


// 🔥 STEP 3: Register Strategy

builder.Services.AddSingleton<IDenominationStrategy, GreedyDenominationStrategy>();


// 🔥 STEP 4: Register Service

builder.Services.AddScoped<IATMService, ATMService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ATM}/{action=Withdraw}/{id?}")
    .WithStaticAssets();

app.Run();