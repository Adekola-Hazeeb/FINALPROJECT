using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class AuctionRepo : IAuctionRepo
    {
        private readonly ApplicationDbContext _context;
        public AuctionRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Auction> Add(Auction auction)
        {
            await _context.Set<Auction>().AddAsync(auction);
            return auction;
        }

        public Task<bool> Exist(Func<Auction, bool> predicate)
        {
            return Task.FromResult(_context.Set<Auction>().Any(predicate));
        }

        public async Task<Auction?> Get(Expression<Func<Auction, bool>> predicate)
        {
            var auction = await _context.Auctions.Include(x => x.Car)
                .Include(x => x.Shipping)
                .Include(x=> x.Payment)
                .Include(x => x.Bids) 
                .Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return auction;
        }
        public async Task<Auction?> GetD(Expression<Func<Auction, bool>> predicate)
        {
            return await _context.Auctions.Include(x => x.Car)
                                          .Include(x => x.Shipping)
                                          .Include(x => x.Payment)
                                          .Include(x => x.Bids)
                                          .FirstOrDefaultAsync(predicate);
           
        }

        public async Task<ICollection<Auction>> GetAllAsync()
        {
            return await _context.Auctions
                 .Where(x => x.IsDeleted == false)
                .Include(x => x.Car)
                 .Include(x => x.Shipping)
                 .Include(x => x.Payment)
                 .Include(x => x.Bids).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Auction> Update(Auction auction)
        {
             _context.Set<Auction>().Update(auction);
            return auction;
        }
    }
}
