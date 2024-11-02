using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class ShippingRepo : IShippingRepo
    {
        private readonly ApplicationDbContext _context;
        public ShippingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Shipping> Add(Shipping shipping)
        {
            await _context.Set<Shipping>().AddAsync(shipping);
            return shipping;
        }

        public async Task<Shipping> Get(Expression<Func<Shipping, bool>> predicate)
        {
            var shipping = await _context.Shipping.Include(x => x.Customer)
                .Include(x => x.Auction)
               .Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return shipping;
        }

        public async Task<ICollection<Shipping>> GetAll()
        {
            return await _context.Shipping.Include(x => x.Customer)
               .Include(x => x.Auction)
              .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<ICollection<Shipping>> GetAll(string customerid)
        {

            return await _context.Shipping.Include(x => x.Customer)
               .Include(x => x.Auction)
              .Where(x => x.IsDeleted == false && customerid==x.CustomerId).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
