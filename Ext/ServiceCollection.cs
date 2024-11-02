using FINALPROJECT.Context;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Implementations;
using FINALPROJECT.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Ext
{
    public static class ServiceCollection
    {
        //public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        //{
        //    return services
        //        .AddDbContext<ApplicationDbContext>(a => a.UseMySQL(connectionString));

        //}
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IAddressRepo, AddressRepo>()
                .AddScoped<IFileRepo, FileRepo>()
                .AddScoped<IUserRepo, UserRepo>()
                .AddScoped<IAuctionRepo, AuctionRepo>()
                .AddScoped<IBidRepo, BidRepo>()
                .AddScoped<ICarRepo, CarRepo>()
                .AddScoped<ICustomerRepo, CustomerRepo>()
                .AddScoped<IPaymentRepo, PaymentRepo>()
              .AddScoped<IShippingRepo, ShippingRepo>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAddresssService, AddressService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuctionService, AuctionService>()
                .AddScoped<IBidService, BidService>()
                .AddScoped<ICarService, CarService>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IPaymentService, PaymentService>()
              .AddScoped<IShippingService, ShippingService>()
                .AddScoped<IEmailSender, EmailSender>();
        }
    }
}
