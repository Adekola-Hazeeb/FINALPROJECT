using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IAuctionRepo
    {
        Task<Auction> Add( Auction auction);
        Task<ICollection<Auction>> GetAllAsync();
        Task<Auction> Get(Expression<Func<Auction, bool>> predicate);
        Task<Auction?> GetD(Expression<Func<Auction, bool>> predicate);
        Task<Auction> Update(Auction auction);
        Task<bool> Exist(Func<Auction, bool> predicate);
        Task<int> SaveAsync();
    }
}
