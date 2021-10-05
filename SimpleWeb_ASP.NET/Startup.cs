using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleWeb_ASP.NET
{
    public class Startup                    // Startup --- ты уже знаешь, что этот класс просто конфигурирует сервисы и конвеер запросов
    {                                       //   приложения
                                            // Имя Startup выдаётся по шаблону, но ты можешь сменить его (главное не забыть подправить
                                            //   метод webBuilder.UseStartup<>() в ./Program.cs)

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)  // ConfigureServices() --- этот метод не обязателен. Он может
        {                                                           //   зарегестрировать и сконфигурировать сервисы приложения. Этот метод,
                                                                    //   по идее, запускается в классе первым
                                                                    // Microsoft.Extensions.DependencyInjection.IServiceCollection --- этот
                                                                    //   интерфейс средних размеров задаёт контракт для коллекций, что
                                                                    //   хотят хранить сервисы
                                                                    //   
                                                                    //   С помощью методов расширений объект services сможет производить
                                                                    //   конфигурацию приложения приложения сервисами. Все его методы
                                                                    //   выглядят как Add<названиеСервиса>()
                                                                    //   
                                                                    // Текущий шаблон (web) оставляет данный метод пустым. Шаблон mvc же
                                                                    //   наполняет его одним вызовом:
                                                                    //   
                                                                    //       services.AddControllerWithViews()
                                                                    //   
                                                                    //   Этот метод добавляет в коллекцию services сервисы, что обеспечивают
                                                                    //   работу контроллеров MVC
                                                                    //
                                                                    // Даже если в этом методе не будет сконфигурирован ни один сервис,
                                                                    //   приложение всё-равно будет иметь несколько сервисом по умолчанию
                                                                    //   /////////after reading: ..IWebHostEnvironment/////////////////////////
                                                                    //   // (например, Microsoft.AspNetCore.Hosting.IWebHostEnvironment)
                                                                    //   //////////////////////////////////////////////////////////////////////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  // Configure() --- этот метод отвечает за конвеер запросов, и
        {                                                                        //   он обязательный. Он принимает 2'а аргумента, второй из
                                                                                 //   них необязателен
                                                                                 // Microsoft.AspNetCore.Builder.IApplicationBuilder ---
                                                                                 //   интерфейс, что определяет объекты-appbuilder'ы
                                                                                 //
                                                                                 //   Наш метод Configure() принимает один из таких объектов
                                                                                 //   в параметр app
                                                                                 // Microsoft.AspNetCore.Hosting.IWebHostEnvironment ---
                                                                                 //   интерфейс, что определеяет объекты, поставляющие методы
                                                                                 //   для получения инфо о нашем приложении (sic!) и методы
                                                                                 //   для взаимодействии с ним
                                                                                 //   
                                                                                 //   У нас здесь это параметр env
            if (env.IsDevelopment())                                             // bool env.IsDevelopment() --- приложение в процессе
            {                                                                    //   разработки (т.е. под статусом Development)?
                app.UseDeveloperExceptionPage();                                 // ..IApplicationBuilder app.UseDeveloperExceptionPage() ---
                                                                                 //   то заставить будущее приложение вывести все пойманные
                                                                                 //   exceptions в отдельную HTML error страницу
                                                                                 //   
                                                                                 //   В статусе Production такие сообщения нежелательно, ведь
                                                                                 //   трассировка об ошибках может раскрыть какую-то инфо о
                                                                                 //   структуре сайта
            }

            app.UseRouting();                                                    // ..IApplicationBuilder app.UseRouting() --- будущее
                                                                                 //   приложение будет использовать маршрутизацию
                                                                                 //   (****что именно произойдёт?)

            app.UseEndpoints(endpoints =>                                        // ..IApplicationBuilder app.UseEndpoints(..) ---
            {                                                                    //   устанавливаем адреса, что будут обрабатываться ещё не
                                                                                 //   построенным приложением
                                                                                 //   
                                                                                 //   У метода один параметр - Action<..IEndPointRouteBuilder>
                                                                                 //   
                                                                                 //       Интерфейс
                                                                                 //       Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
                                                                                 //       - это уже builder маршрута, т.е. билдер адреса
                                                                                 //   
                                                                                 //   В соответсвии с шаблоном мы отправляем лябмда-выражение,
                                                                                 //   параметр которого назван endpoints
                endpoints.MapGet("/", async context =>                           // endpoints.MapGet(..) --- обработка запроса. Получаем
                {                                                                //   контекст запроса в виде объекта context (****непонятно)
                    await context.Response.WriteAsync("Hello World!");           // await context.Response.WriteAsync(..) --- отправка ответа
                                                                                 //   в виде строки "Hello World!"
                                                                                 //   
                                                                                 //   
                                                                                 //   
                });
            });
        }


        // Также опционально ты можешь выставить ctor в этом классе
    }
}
