using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Medrea_Diana_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Medrea_Diana_Lab2Context' not found.");

builder.Services.AddDbContext<Medrea_Diana_Lab2Context>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Seed the Category table with default categories if empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Medrea_Diana_Lab2Context>();
    context.Database.EnsureCreated();
    if (!context.Category.Any())
    {
        context.Category.AddRange(
            new Medrea_Diana_Lab2.Models.Category { CategoryName = "Beletristica" },
            new Medrea_Diana_Lab2.Models.Category { CategoryName = "Economie" },
            new Medrea_Diana_Lab2.Models.Category { CategoryName = "Dezvoltare personala" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
