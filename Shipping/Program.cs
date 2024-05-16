

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Errors;
using Shipping.Extensions;
using Shipping.Repositry.Data;
using Shipping.Repositry.Data.Dataseed;
using Shipping.Repositry.Repositories;

namespace Shipping
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region config services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShippingContext>(Options=> Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p=>p.Value.Errors.Count()>0)
                                                         .SelectMany(p=>p.Value.Errors)
                                                         .Select(e => e.ErrorMessage).ToArray();
                    var validationresponse = new ApiValidationRespons()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationresponse);

                };
                });
            builder.Services.AddAplicatinServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 8;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ShippingContext>();

            #endregion



            var app = builder.Build();

            #region update database
           using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<ShippingContext>();
            var loggerfactory = service.GetRequiredService<ILoggerFactory>();
             var usermanger =service.GetRequiredService<UserManager<AppUser>>();
            var rolManger = service.GetRequiredService<RoleManager<IdentityRole>>();
            try
            {
                await dbcontext.Database.MigrateAsync();
                await SeedData.SeedRole(rolManger);
                await SeedData.SeedAdmin(usermanger);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "an arror has been occured during apply the maigration");
                
            }
            #endregion

            #region Kesterl

            app.UseMiddleware<ApiExceptionResponse>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
