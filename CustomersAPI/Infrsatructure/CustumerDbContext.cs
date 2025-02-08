using CapgAppLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CustomersAPI.Infrsatructure
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(   options) { }  
        public DbSet<Customer>Customers { get; set; }       
    }
}
