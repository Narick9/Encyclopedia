using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleWebApp_ASP.NET
{
    public class Startup                    // Startup - ты уже знаешь, что этот класс просто конфигурирует сервисы и конвеер запросов
    {                                       //   приложения(****всё ещё незнаешь что это)
                                            // Имя Startup выбрано по соглашению (****только класс с таким именем будет работать?)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)  // ConfigureServices() - этот метод отвечает за конфигурирование
        {                                                           //   сервисов приложения (этот метод не обязателен). ASP.NET Core runtime
            services.AddRazorPages();                               //   вызывет этот метод во время старта (поэтому имя менять не следует)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  // Configure() - а этот отвечает за конвеер запросов.
        {                                                                        //   ASP.NET Core runtime будет вызывать его
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
