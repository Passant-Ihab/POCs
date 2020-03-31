
using AutoMapper;
using BusinessLayer.Interfaces.Services;
using BusinessLayer.Services;
using BusinessLayer.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using MongoDB.Driver;
using Project1.MongoDB.Repository.Implementation;
using Project1.MongoDB.Repository.Interfaces;
using Service.DataAccess.MongoDB;
using SMS.Web.Models;

namespace SMS.Web
{
    public class Startup
    {
        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllers ();
            services.AddAutoMapper (c => c.AddProfile<AutoMapping>(),typeof(Startup));

            services.AddSingleton<IMongoClient> ( s => new MongoClient ( Configuration.GetSection ( "MongoDb:ConnectionString" ).Value ) );
            services.AddScoped ( s => new AppDbContext ( s.GetRequiredService<IMongoClient> (), Configuration.GetSection ( "MongoDb:Database" ).Value ) );
            
            services.AddScoped<ISubscriberService, SubscriberService> ();
            services.AddScoped<ISubscriberRepository, SubscriberRepository> ();
            services.AddScoped<IValidator<MessageModelRequest>, MessageValidator> ();
            services.AddCors ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment () )
            {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints ( endpoints =>
              {
                  endpoints.MapControllers ();
              } );
        }
    }

    public class AutoMapping : Profile
    {
        public AutoMapping ()
        {
            CreateMap<Message, MessageModelRequest> ();
        }
    }
}
