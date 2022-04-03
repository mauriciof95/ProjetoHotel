using Microsoft.Extensions.DependencyInjection;
using ProjetoHotel.Services;

namespace ProjetoHotel.Extension
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<HotelServices>();
            services.AddScoped<QuartoServices>();
        }
    }
}
