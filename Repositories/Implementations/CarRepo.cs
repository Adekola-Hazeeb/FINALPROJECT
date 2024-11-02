using System.Linq;
using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class CarRepo : ICarRepo
    {
        private readonly ApplicationDbContext _context;
        public CarRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Car> Add(Car car)
        {
            await _context.Cars.AddAsync(car);
            return car;
        }

        public async Task<bool> Exist(Expression<Func<Car, bool>> predicate)
        {
            return _context.Set<Car>().Any(predicate);
        }

        public async Task<Car> Get(Expression<Func<Car, bool>> predicate)
        {
            var car = await _context.Cars.Include(x => x.Auctions)
               .Where(x => x.IsDeleted == false && x.Status == Domain.Enums.CarStatus.Available).FirstOrDefaultAsync(predicate);
            return car;
        }

        public async Task<ICollection<Car>> GetAll()
        {
            return await _context.Cars.Include(x => x.Auctions)
                .Where(x => x.IsDeleted == false && x.Status == Domain.Enums.CarStatus.Available).ToListAsync();
        }

        public async Task<ICollection<Car>> GetAllSpecified(string input)
        {
            return await _context.Cars.Include(x => x.Auctions)
               .Where(x => x.IsDeleted == false && x.Brand.Contains(input)).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            var sv = await _context.SaveChangesAsync();
            return sv;
        }

        public async Task<Car> Update(Car car)
        {
            _context.Set<Car>().Update(car);
            return car;
        }
    }
}
