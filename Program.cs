using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.EntityFrameworkCore;
using Shrink_Bank.Data;
using Shrink_Bank.Models; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages(options =>
{
   
});

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("InventoryConnection") ?? throw new InvalidOperationException("Connection string 'InventoryConnection' not found.")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; 
        options.LogoutPath = "/Logout"; 
        options.AccessDeniedPath = "/AccessDenied"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
        options.SlidingExpiration = true; 
    });


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);

    }
    catch (Exception)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();


app.UseAuthorization();

app.MapRazorPages();

app.Run();