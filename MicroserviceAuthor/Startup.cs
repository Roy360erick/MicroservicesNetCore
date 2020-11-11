using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MessengerService.Mail.SendGrid.Services;
using MicroserviceAuthor.Application;
using MicroserviceAuthor.Persistence;
using MicroserviceAuthor.RabbitDriver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbirtMQ.Bus.EventQueue;
using RabbirtMQ.Bus.Implements;
using RabbirtMQ.Bus.RabbitBus;

namespace MicroserviceAuthor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(QueryFilter.Driver));

            services.AddDbContext<DbContextAuthor>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("myconn"));

            });
            services.AddControllers().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<New>();
            });

            services.AddSingleton<IRabbitEventBuss, RabbitEventBuss>(sp => 
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitEventBuss(sp.GetService<IMediator>(), scopeFactory);

            });
            services.AddSingleton<ISendGridService, SendGridService>();
            services.AddTransient<EventMainDriver>();

            services.AddTransient<IEventDrive<EventMailQueue>, EventMainDriver>();
           
            services.AddMediatR(typeof(New.Driver).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var eventBuss = app.ApplicationServices.GetRequiredService<IRabbitEventBuss>();

            eventBuss.Subscribe<EventMailQueue,EventMainDriver>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
