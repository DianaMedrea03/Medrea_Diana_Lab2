using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Medrea_Diana_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Medrea_Diana_Lab2Context' not found.");

builder.Services.AddDbContext<Medrea_Diana_Lab2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

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
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();