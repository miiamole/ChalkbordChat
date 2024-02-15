using ChackBoard.Data.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//H�mtar de connectionsStrings som beh�vs
var chalkDbConnectionString = builder.Configuration.GetConnectionString("ChalkboardDbConnection");
var auth_connectionString = builder.Configuration.GetConnectionString("AuthDbConnection");


//l�gger till b�da dbcontext till buildern 
builder.Services.AddDbContext<ChalkboardDbContext>(options => options.UseSqlServer(chalkDbConnectionString));
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(auth_connectionString));

//l�gger till identityuser och roller till buildern, och anv�nder AuthDbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//kr�ver authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
