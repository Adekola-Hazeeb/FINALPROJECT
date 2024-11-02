using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer> Add(Customer customer);
        Task<ICollection<Customer>> GetAll();
        Task<Customer> Get(Expression<Func<Customer, bool>> predicate);
        Task<Customer> Update(Customer customer);
        Task<bool> Exist(Func<Customer, bool> predicate);
        Task<int> SaveAsync();
    }
}
