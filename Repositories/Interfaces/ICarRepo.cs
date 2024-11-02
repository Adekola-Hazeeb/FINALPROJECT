using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface ICarRepo
    {
        Task<Car> Add(Car car);
        Task<ICollection<Car>> GetAll();
        Task<Car> Get(Expression<Func<Car, bool>> predicate);
        Task<Car> Update(Car car);
        Task<bool> Exist(Expression<Func<Car, bool>> predicate);
        Task<int> SaveAsync();
        Task<ICollection<Car>> GetAllSpecified(string input);
    }
}
