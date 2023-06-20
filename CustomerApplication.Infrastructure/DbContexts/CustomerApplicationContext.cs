using CustomerApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace CustomerApplication.Infrastructure.DbContexts
{
    public class CustomerApplicationContext : DbContext
    {
        public CustomerApplicationContext(DbContextOptions<CustomerApplicationContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToContainer("Customer").HasPartitionKey(s => s.CustomerId);

            modelBuilder.Entity<Sales>()
                    .ToContainer("Sales")
                    .HasPartitionKey(s => s.SalesId);
        }        
    }
}