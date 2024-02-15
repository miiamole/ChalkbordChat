using ChackBoard.Data.Database;
using ChackBoard.Data.Repositories;
using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(option => option.Conventions.AuthorizeFolder("/Chalkboard/Message"));
builder.Services.AddRazorPages(option => option.Conventions.AuthorizeFolder("/Chalkboard/Settings"));


//Hämtar de connectionsStrings som behövs
var chalkDbConnectionString = builder.Configuration.GetConnectionString("ChalkboardDbConnection");
var auth_connectionString = builder.Configuration.GetConnectionString("AuthDbConnection");


//lägger till båda dbcontext till buildern 
builder.Services.AddDbContext<ChalkboardDbContext>(options => options.UseSqlServer(chalkDbConnectionString));
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<MessageServices>();

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(auth_connectionString));
builder.Services.AddScoped<UserServices>();

//lägger till identityuser och roller till buildern, och använder AuthDbContext
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
//kräver authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
