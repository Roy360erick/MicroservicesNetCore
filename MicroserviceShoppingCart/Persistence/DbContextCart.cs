using System;
using MicroserviceShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceShoppingCart.Persistence
{
    public class DbContextCart:DbContext
    {
        public DbContextCart(DbContextOptions<DbContextCart> options) :base(options)
        {
        }

        public DbSet<SessionCart> SessionCarts { get; set; }
        public DbSet<SessionCartDetail> SessionCartDetails { get; set; }
    }
}
