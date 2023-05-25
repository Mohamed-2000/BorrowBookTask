using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBase.Application.Interfaces;
using MyBase.Domain.Entities;

namespace MyBase.Infrastructure.Persistence;

public class MyBaseDbContext : IdentityDbContext<AppUser, ApplicationRole, Guid>, IMyBaseContext
{


    public MyBaseDbContext(DbContextOptions<MyBaseDbContext> options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Fines> Fines { get; set; }
    //public DbSet<Employee> Employees { get; set; }
    public override DatabaseFacade Database => base.Database;

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{

    //    modelBuilder.Entity<IdentityUserLogin<Guid>>()
    //   .HasKey(u => u.UserId);
    //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyBaseDbContext).Assembly);
    //}
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}

