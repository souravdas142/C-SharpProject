using ATMWebApplication.Domain.Entities;
using ATMWebApplication.Domain.ValueObjects;
using ATMWebApplication.Services;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.State;
using ATMWebApplication.State.Interfaces;
using ATMWebApplication.Strategies;
using ATMWebApplication.Strategies.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//   Create initial ATM state

Account account = new Account("sourav", 10_000); // Example account

Dictionary<Denomination, int> initialCash = new Dictionary<Denomination, int>
{
    { new Denomination(500), 10 },
    { new Denomination(200), 20 },
    { new Denomination(100), 50 }
};

CashInventory inventory = new CashInventory(initialCash);


// Register StateStore as Singleton

builder.Services.AddSingleton<IStateStore>(
    new InMemoryStateStore(account, inventory)
);


// Register Strategy

builder.Services.AddSingleton<IDenominationStrategy, GreedyDenominationStrategy>();


// Register Service

builder.Services.AddScoped<IATMService, ATMService>();


WebApplication app = builder.Build();


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