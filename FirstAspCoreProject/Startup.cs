using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAspCoreProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/* Aby doda� baz� danych w sqlServer i po��czy� j� z EntityFramework oraz aby doda� us�uge baz danych do 
 dependency injection container aby m�c u�ywa� obiektu baz danych w ca�ym projekcie bez konieczno�ci 
tworzenia go za ka�dym razem:
-> Doda� NugatPackages: EntityFrameworkCore.SqlServer oraz EntityFrameworkCore.Tools
->Doda� connection String w appsettings.json
->Stworzy� klas� ApplicationDbContext (wa�ne �eby nazwa klasy ko�czy�a �i� na "DbContext") dziedzicz�c� po DbContext
->Doda� us�ug� w Startup/ConfigureServices (services.AddDbContext<ApplicationDbContext>(option=> ... patrz ni�ej)
->Przeprowadzi� migracj� za pomoc� PAckage MAnager Console : 'PM> add-migration addCategoryToDatabe' (po��czenie 
z EntityFramework)  oraz 'PM> update-database'  (abty stworzy� baz� w sqlServer)

*/
namespace FirstAspCoreProject
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
            //Dodanie naszej bazy danych do dependency injection container dzi�ki czemu b�dzie mo�na z niej 
            //korzysta� w ca�ym projekcie bez potrzeby tworzenia instancji bazy w ka�dym miejscu gdzie chcemy 
            //jej u�y�. Patrz jeszcze klasa:AplicationDbContect

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("FirstAspCoreProjectDB")));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
