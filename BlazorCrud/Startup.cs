using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorCrud
{
    public class Startup
    {
        readonly string MiCors = "MiCors"; //Configuramos Cors para que el navegador no bloquee las solicitudes
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//Permitir el acceso al servicio desde el navegador
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MiCors,
                                builder =>
                                {
                                    builder.WithOrigins("*");
                                    builder.AllowAnyHeader();
                                    builder.AllowAnyMethod();
                                }
            );
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            var HttpClientHandler1 = new HttpClientHandler();
            HttpClientHandler1.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44324")
            });

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
            app.UseCors(MiCors);
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
