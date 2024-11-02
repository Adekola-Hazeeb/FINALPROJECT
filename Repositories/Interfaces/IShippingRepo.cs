using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IShippingRepo
    {
        Task<Shipping> Add(Shipping shipping);
        Task<ICollection<Shipping>> GetAll();
        Task<ICollection<Shipping>> GetAll(string customerid);
        Task<Shipping> Get(Expression<Func<Shipping, bool>> predicate);
        Task<int> SaveAsync();
    }
}
