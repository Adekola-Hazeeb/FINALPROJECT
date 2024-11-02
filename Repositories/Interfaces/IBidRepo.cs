using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IBidRepo
    {
        Task<Bid> Add(Bid bid);
        Task<ICollection<Bid>> GetAll();
        Task<Bid> Get(Expression<Func<Bid, bool>> predicate);
        Task<bool> Exist(Func<Bid, bool> predicate);
        Task<ICollection<Bid>> GetAllForAuction(string id);
        Task<int> SaveAsync();
    }
}
