using System;
using MicroserviceBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBooks.Persistence
{
    public class DbContextBook :DbContext
    {
        public DbContextBook() { }
        public DbContextBook(DbContextOptions<DbContextBook> options) :base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
