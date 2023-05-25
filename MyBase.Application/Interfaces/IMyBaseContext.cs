using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBase.Domain.Entities;
namespace MyBase.Application.Interfaces
{
    public interface IMyBaseContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<MyBase.Domain.Entities.BorrowedBook> BorrowedBooks { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Fines> Fines { get; set; }
        //DbSet<Employee> Employees { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
