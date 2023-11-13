using Microsoft.EntityFrameworkCore;
using Common.Entities;

namespace NorthwindWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            string databasePath = Path.Combine("..", "Northwind.Common.EntityModels.Sqlite", "Northwind.db");
            services.AddDbContext<NorthwindContext>(options => 
            options.UseSqlite($"Data Source = {databasePath}"));
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseDefaultFiles(); 
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}