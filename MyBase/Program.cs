using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBase.Application.Books.Queries;
using MyBase.Application.Infrastructure;
using MyBase.Application.Interfaces;
using MyBase.Domain.Entities;
using MyBase.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<MyBaseDbContext>(
dbContextOptions => dbContextOptions
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    // The following three options help with debugging, but should
    // be changed or removed for production.
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

builder.Services.AddDbContext<IMyBaseContext, MyBaseDbContext>(
dbContextOptions => dbContextOptions
   .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
   .LogTo(Console.WriteLine, LogLevel.Information)
   .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
   .EnableDetailedErrors());


//For Quireies  entities
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
//builder.Services.AddTransient<IRouteURLResolver, RouteURLResolver>();

builder.Services.AddMediatR(typeof(MyBaseDbContext).Assembly);
builder.Services.AddMediatR(typeof(GetAllBooksQuery).Assembly);

builder.Services.AddScoped<IFileUploader, FileUploader>();



//Identity
builder.Services.AddIdentity<AppUser, ApplicationRole>()
    .AddEntityFrameworkStores<MyBaseDbContext>()
    .AddDefaultTokenProviders();
#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                       builder =>
                       {
                           builder.WithOrigins("https://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                       });
});
#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
         name: "MyArea",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoint.MapControllerRoute(
         name: "api",
         pattern: "api/{controller}/{action}/{id?}");

    endpoint.MapControllerRoute(
         name: "default",
         pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
