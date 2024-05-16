using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.Repositry.Repositories;
using Shipping.Services.Handler;

namespace Shipping.Extensions
{
    public static class AplicationServicesExtension
    {
        public static IServiceCollection AddAplicatinServices(this IServiceCollection services)
        {
            //services.AddScoped<IOrderHandler, OrderHandler>();
            //services.AddScoped<ScreenPermissionHandler>();
            //services.AddScoped<IScreenPermisssionReprosatary, ScreenPermisssionReprosatary>();
            //services.AddScoped<IOrderReposiatry, OrderReposiatry>();
            //services.AddScoped<IBranchesRepository, BranchesRepository>();
            //services.AddScoped<IBranchesHandler, BranchesHandler>();
            services.AddScoped<ICityReprosatriy, CityReprosatriy>();
            //services.AddScoped<ICityHandler, CityHandler>();
            //services.AddScoped<IGovernorateHandler, GovernorateHandler>();
            //services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            //services.AddScoped<ISpecialPriceRopresatry, SpecialPriceRoprisatry>();
            //services.AddScoped<IProductRepeosatry, ProductReprosatry>();
            //services.AddScoped<IWeightHandler, WeightHandler>();
            //services.AddScoped<IWeightReperosatry, WeightReprosatry>();
            //services.AddScoped<IAccountUser, AccountUser>();
            //services.AddScoped<IRoleRopersiatry, RoleRopersatriy>();
            //services.AddScoped<IRoleHandler, RoleHandler>();
            //services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();
            //services.AddScoped<IMerchantHandler, MerchantHandler>();
            //services.AddScoped<IMerchantReprosatry, MerchantReprosatry>();
            //services.AddScoped<IRepresentativeRepository, RepresentativeRepository>();
            //services.AddScoped<IRepresentativeHandler, RepresentativeHandler>();

            return services;
        }
    }
}
