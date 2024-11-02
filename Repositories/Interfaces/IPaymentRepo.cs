using System;
using System.Linq.Expressions;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Repositories.Interfaces
{
    public interface IPaymentRepo
    {
        Task<Payment> Add(Payment payment);
        Task<Payment> Update(Payment payment);
        Task<ICollection<Payment>> GetAll();
        Task<ICollection<Payment>> GetAll(string Paymentid);
        Task<Payment> Get(Expression<Func<Payment, bool>> predicate);
        Task<int> SaveAsync();
    }
}
