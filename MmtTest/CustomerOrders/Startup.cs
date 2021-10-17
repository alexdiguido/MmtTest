using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.DataAccess;
using CustomerOrders.Helpers;
using CustomerOrders.Mapping;
using CustomerOrders.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CustomerOrders
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
            services.AddSingleton<ICustomerOrderControllerService, CustomerOrderControllerService>();
            services.AddSingleton<ICustomerOrderControllerResult, CustomerOrderControllerResult>();
            services.AddSingleton<IGetCustomerOrderService, GetCustomerOrderService>();
            services.AddSingleton<IGetCustomerOrderMapper, GetCustomerOrderMapper>();
            services.AddSingleton<IGetCustomerDetailsService, GetCustomerDetailsService>();
            services.AddSingleton<IGetOrderService, GetOrderService>();
            services.AddSingleton<IApiGateway, ApiGateway>();
            services.AddSingleton<IConnectionProvider, SqlServerConnectionProvider>();
            services.AddSingleton<IOrderQueries, OrderQueries>();

            services.AddHttpClient();

            services.AddControllers();
            services.AddHttpClient("CustomerDetails", c =>
            {
                c.BaseAddress = new Uri(Configuration["CustomerDetailsEndpoint:Url"]);
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
