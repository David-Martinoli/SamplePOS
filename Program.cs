using SamplePOS_ServerSide.Components;
using Microsoft.EntityFrameworkCore;
using SamplePOS_ServerSide.Data;
using SamplePOS_ServerSide.Models;
using SamplePOS_ServerSide.ViewModels;
using SamplePOS_ServerSide.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<BillingService>();
builder.Services.AddSingleton<Cart>();
builder.Services.AddScoped<ProductsViewModel>();
builder.Services.AddScoped<CartViewModel>();
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
