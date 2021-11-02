using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Infrastructure;
using Order.Application.Contracts.Persistence;
using Order.Application.Models;
using Order.Infrastructure.Mail;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString"))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>)); // required for mediatr?
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
