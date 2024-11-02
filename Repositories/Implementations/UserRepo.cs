using System.Linq;
using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class UserRepo:IUserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Set<User>().AddAsync(user);
            return user;
        }

        public async Task<bool> Exist(Func<User, bool> predicate)
        {
            return _context.Set<User>().Any(predicate);
        }

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
            var user = await _context.Users
               .Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return user;
        }

        public async Task<ICollection<User>> GetAll()
        {
           return await _context.Users
               .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<User> Update(User user)
        {
            _context.Set<User>().Update(user);
            return user;
        }
    }
}
