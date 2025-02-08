using CapgAppLibrary;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.Infrsatructure
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string CustomerId);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }
        public async Task<Customer> GetCustomer(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException(nameof(customerId), "Missing Value");
            }
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
    }
}