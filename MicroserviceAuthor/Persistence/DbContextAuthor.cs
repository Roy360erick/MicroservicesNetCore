using System;
using MicroserviceAuthor.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceAuthor.Persistence
{
    public class DbContextAuthor:DbContext
    {
        public DbContextAuthor(DbContextOptions<DbContextAuthor> options):base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Degree> Degrees { get; set; }
    }
}
