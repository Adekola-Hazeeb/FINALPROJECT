using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IAddressRepo
    {
        Task<Address> AddAsync (Address address);
        Task<ICollection<Address>> GetAllAsync();
        Task<Address> Get(Expression<Func<Address, bool>> predicate);
        Task<Address> Update(Address address);
        Task<bool> Exist(Expression<Func<Address, bool>> predicate);
        Task<int> SaveAsync();
    }
}
