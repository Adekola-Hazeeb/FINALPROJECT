using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<User> Add(User user);
        Task<ICollection<User>> GetAll();
        Task<User> Get(Expression<Func<User, bool>> predicate);
        Task<User> Update(User user);
        Task<bool> Exist(Func<User, bool> predicate);
        Task<int> SaveAsync();
    }
}
