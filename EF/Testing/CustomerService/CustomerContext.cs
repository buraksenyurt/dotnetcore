using Microsoft.EntityFrameworkCore;

namespace CustomerService
{
    public class CustomerContext
    : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext()
        { }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=PDOSVIST01;Database=ATPMasters.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}