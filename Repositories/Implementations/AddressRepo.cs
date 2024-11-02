using System.Linq;
using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class AddressRepo : IAddressRepo
    {
        private readonly ApplicationDbContext _context;
        public AddressRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Address> AddAsync(Address address)
        {
            await _context.Set<Address>().AddAsync(address);
            return address;
        }

        public async Task<bool> Exist(Expression<Func<Address, bool>> predicate)
        {

            return await _context.Set<Address>().AnyAsync(predicate);
        }

        public async Task<Address> Get(Expression<Func<Address, bool>> predicate)
        {
            var address = await _context.Addresses.Include(x => x.Customer).Where(x => x.IsDeleted == false ).FirstOrDefaultAsync(predicate);
            return address;
        }

        public async Task<ICollection<Address>> GetAllAsync()
        {
            return await _context.Addresses.Include(x => x.Customer).Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Address> Update(Address address)
        {
            _context.Set<Address>().Update(address);
            return address;
        }
    }
}
