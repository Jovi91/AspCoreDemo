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

/* Aby dodaæ bazê danych w sqlServer i po³¹czyæ j¹ z EntityFramework oraz aby dodaæ us³uge baz danych do 
 dependency injection container aby móc u¿ywaæ obiektu baz danych w ca³ym projekcie bez koniecznoœci 
tworzenia go za ka¿dym razem:
-> Dodaæ NugatPackages: EntityFrameworkCore.SqlServer oraz EntityFrameworkCore.Tools
->Dodaæ connection String w appsettings.json
->Stworzyæ klasê ApplicationDbContext (wa¿ne ¿eby nazwa klasy koñczy³a œiê na "DbContext") dziedzicz¹c¹ po DbContext
->Dodaæ us³ugê w Startup/ConfigureServices (services.AddDbContext<ApplicationDbContext>(option=> ... patrz ni¿ej)
->Przeprowadziæ migracjê za pomoc¹ PAckage MAnager Console : 'PM> add-migration addCategoryToDatabe' (po³¹czenie 
z EntityFramework)  oraz 'PM> update-database'  (abty stworzyæ bazê w sqlServer)

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
            //Dodanie naszej bazy danych do dependency injection container dziêki czemu bêdzie mo¿na z niej 
            //korzystaæ w ca³ym projekcie bez potrzeby tworzenia instancji bazy w ka¿dym miejscu gdzie chcemy 
            //jej u¿yæ. Patrz jeszcze klasa:AplicationDbContect

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
