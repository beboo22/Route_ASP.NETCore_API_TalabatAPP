using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repostiries_contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repository;
using Talabat.Repository.Data.Data_Seeding;
using Talabat.api.Hellpers;
using Microsoft.Extensions.Configuration;
using Talabat.api.Errors;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Midlewares;
using StackExchange.Redis;
using Talabat.api.Controllers;
using Talabat.Repository.Data.identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entity.Identity;
using Talabat.Core;
using Talabat.Repository;
using Talabat.Core.ServiceContract;
using Talabat.Service;
using Talabat.Core.ServiceContract.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.ServiceContract.Payment;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region configure services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();
            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));



            builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidIssuer = builder.Configuration["Jwt:validIssuer"],
                                    ValidateAudience = true,
                                    ValidAudience = builder.Configuration["Jwt:VaildAudience"],
                                    ValidateLifetime = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Authkey"] ?? string.Empty)),
                                    ValidateIssuerSigningKey = true
                                };
                            });


            builder.Services.AddScoped<UserManager<AppUser>>();
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            builder.Services.AddScoped(typeof(IAuthentication), typeof(Authentication));
            builder.Services.AddScoped(typeof(IPaymentIntent), typeof(PaymentIntent));
            builder.Services.AddScoped(typeof(ICacheService), typeof(CacheService));

            //builder.Services.AddScoped(typeof(IGenaricRepository<>),
            //                            typeof(GenaricRepository<>));
            //builder.Services.AddSingleton<IConfiguration>();

            builder.Services.AddAutoMapper(typeof(AtoMapper));

            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Errorlist = actionContext.ModelState.Where(R => R.Value.Errors.Count() > 0)
                                             .SelectMany(E => E.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToList();

                    //var Scode = actionContext.ModelState.Where(R=>R.Value.)
                    var apiBehaviorError = new ApiBehaviorError(400, Errorlist);
                    return new BadRequestObjectResult(apiBehaviorError);
                };
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(
                (_) =>
                //(ServiceProvider) => // we can use ServiceProvider if we need to resolve another service from the DI container inside the lambda
                {

                    //var config = ServiceProvider.GetRequiredService<IConfiguration>();

                    //var connection = config.GetConnectionString("redis");


                    var connection = builder.Configuration.GetConnectionString("redis");
                    return ConnectionMultiplexer.Connect(connection);
                });



            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));



            //builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            //});





            #endregion





            var app = builder.Build();




            #region Update-Datebase
            using var scop = app.Services.CreateScope();///create continer have service provider 
                                                        /// Creates a new <see cref="IServiceScope"/> that can be
                                                        /// used to resolve scoped services.

            var service = scop.ServiceProvider; // from here will can Access on Service
            var db = service.GetRequiredService<StoreDbContext>();
            var Identitydb = service.GetRequiredService<AppIdentityDbContext>();
            var _userManger = service.GetRequiredService<UserManager<AppUser>>();

            try
            {
                await db.Database.MigrateAsync();
                await Identitydb.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                await Seeding.DataSeeding(db);
                await Seeding.IdentityDataSeeding(_userManger);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while calling DataSeeding method : {ex.Message}");
            }

            #endregion

            #region middleware


            app.UseMiddleware<ExceptionMiddleware>();


            app.UseAuthentication();
            app.UseAuthorization();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
