using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoHotel.Extension;
using ProjetoHotel.Helpers;
using ProjetoHotel.Infrastructure.Context;

namespace ProjetoHotel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<SqlDbContext>(o =>
            {
                o.UseSqlServer(SqlDbContext.ConnectionString);
            });

            //apenas para não poluir o startup caso o projeto 
            //tivesse inumeros services para Injeção de Dependencia
            services.ConfigureServices();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //so para nao precisar add IWebHost como ID so para pegar o path do wwwroot
            ImagemHelper.pathString = env.WebRootPath;


            //apenas para rodar as migrações ao iniciar o projeto
            //e não precisar sempre dar update-database para o banco de produção
            try
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<SqlDbContext>();
                    context.Database.Migrate();
                }
            }
            catch { }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
