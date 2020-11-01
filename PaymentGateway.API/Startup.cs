using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentGateway.API.Data;
using PaymentGateway.API.Services;
using PaymentGateway.API.Validators;
using System;

namespace PaymentGateway.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payment Gateway API",
                    Description = "Checkout.com Building a Payment Gateway Case API",
                    Contact = new OpenApiContact
                    {
                        Name = "Metin Ogurlu",
                        Email = "a.metinugurlu@gmail.com",
                        Url = new Uri("https://github.com/metinogurlu"),
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddDbContext<PaymentContext>(options =>
                options.UseNpgsql(Configuration["ConnectionString"]));

            services.AddTransient<ICardValidator, CardValidator>();
            services.AddTransient<IProcessPaymentRequestValidator, ProcessPaymentRequestValidator>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IAcquiringBankSimulator, AcquiringBankSimulator>();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PaymentContext>();
                context.Database.Migrate();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway API V1");
                c.RoutePrefix = string.Empty;
            });

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