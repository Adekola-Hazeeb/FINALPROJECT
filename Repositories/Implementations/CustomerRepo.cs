using System.Linq;
using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Set<Customer>().AddAsync(customer);
            return customer;
        }

        public async Task<bool> Exist(Func<Customer, bool> predicate)
        {
            return _context.Set<Customer>().Any(predicate);
        }

        public async Task<Customer> Get(Expression<Func<Customer, bool>> predicate)
        {
            var customer = await _context.Customers.Include(x => x.AuctionsPartaken)
                .Include(x => x.BidsMade)
                    .Include(x => x.PaymentsMade)
                     .Include(x => x.Shippings)
                     .Include(x=>x.Addresses)
                .Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return customer;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            return await _context.Customers.Include(x => x.AuctionsPartaken)
                 .Include(x => x.BidsMade)
                     .Include(x => x.PaymentsMade)
                      .Include(x => x.Shippings)
                 .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Set<Customer>().Update(customer);
            return customer;
        }
    }
}
