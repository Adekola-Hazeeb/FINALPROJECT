using System.Linq.Expressions;
using FINALPROJECT.Context;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Repositories.Implementations
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Add(Payment payment)
        {
            await _context.Set<Payment>().AddAsync(payment);
            return payment;
        }

        public async Task<Payment> Get(Expression<Func<Payment, bool>> predicate)
        {
            var payment = await _context.Payments.Include(x => x.Auction)
                .Include(x => x.Customer)
                .Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return payment;
        }

        public async Task<ICollection<Payment>> GetAll()
        {
            return await _context.Payments.Include(x => x.Auction)
                .Include(x => x.Customer)
                .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetAll(string customerid)
        {
            return await _context.Payments.Include(x => x.Auction)
               .Include(x => x.Customer)
               .Where(x => x.IsDeleted == false && customerid == x.CustomerId).ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Payment> Update(Payment payment)
        {
            _context.Set<Payment>().Update(payment);
            return payment;
        }
    }
}
