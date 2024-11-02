using System.Linq;
using System.Linq.Expressions;
using System.Net;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class BidRepo : IBidRepo
    {
        private readonly ApplicationDbContext _context;
        public BidRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Bid> Add(Bid bid)
        {
            await _context.Set<Bid>().AddAsync(bid);
            return bid;
        }

        public async Task<bool> Exist(Func<Bid, bool> predicate)
        {
            return _context.Set<Bid>().Any(predicate);
        }

        public async Task<Bid> Get(Expression<Func<Bid, bool>> predicate)
        {
            var bid = await _context.Bids.Include(x => x.Auction).Include(x => x.Customer).Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return bid;
        }

        public async Task<ICollection<Bid>> GetAll()
        {
            return await _context.Bids.Include(x => x.Auction).Include(x => x.Customer).Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<ICollection<Bid>> GetAllForAuction(string id)
        {
            return await _context.Bids.Include(x => x.Auction).Include(x => x.Customer).Where(x => x.IsDeleted == false && x.AuctionId == id).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
