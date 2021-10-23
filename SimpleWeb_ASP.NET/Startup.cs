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
        {                                                                        //   он обязательный. Он принимает 2'а аргумента (второй
                                                                                 //   необязателен)
                                                                                 //   
                                                                                 //   
                                                                                 //     Microsoft.AspNetCore.Builder.IApplicationBuilder ---
                                                                                 //       интерфейс, что определяет объекты-appbuilder'ы
                                                                                 //       
                                                                                 //           Интерфейсу посвящён отдельный метод
                                                                                 //
                                                                                 //       В наш метод Configure() отправится один из таких
                                                                                 //         объектов-appbuilder'ов, который мы здесь и
                                                                                 //         сконфигурируем
                                                                                 //
                                                                                 //     Microsoft.AspNetCore.Hosting.IWebHostEnvironment ---
                                                                                 //       объекты этого интерфейса будут иметь методы
                                                                                 //       для взаимодействии с нашим приложением (и методы
                                                                                 //       для получения инфо о нём (sic!))
                                                                                 //   
                                                                                 //   Метод Configure вызовется всего один раз, при создании
                                                                                 //     объекта класса Startup
                                                                                 //   
                                                                                 // Почему конвеер? Потому что запрос, приходящий В
                                                                                 //   виде (****в виде чего?) сначала проходит в первый
                                                                                 //   компонент, затем второй и так далее, пока кто-то из них
                                                                                 //   не выполнит запрос окончательно
                                                                                 //   
                                                                                 //     "Компонент" в ASP.NET Core - это ****что-то.
                                                                                 //       Компоненты конфигурируются через методы расширений
                                                                                 //       Run..(), Map..() и Use..() в объектах
                                                                                 //       ...IApplicationBuilder
                                                                                 //   
            if (env.IsDevelopment())                                             //     bool env.IsDevelopment() --- приложение в процессе
            {                                                                    //       разработки (т.е. под статусом Development)?
                app.UseDeveloperExceptionPage();                                 //     ...IApplicationBuilder app.UseDeveloperExceptionPage()
                                                                                 //       --- задействовать компнонет DeveloperExceptionPage
                                                                                 //       (ещё его называют компонентом Diagnostics).
                                                                                 //       Это заставит будущее приложение вывести все
                                                                                 //       пойманные exceptions в отдельную HTML error страницу
                                                                                 //       
                                                                                 //       В статусе Production такие сообщения нежелательно,
                                                                                 //         ведь трассировка об ошибках может раскрыть какую-то
                                                                                 //         инфо о структуре сайта
            }

            app.UseRouting();                                                    //     ...IApplicationBuilder app.UseRouting() --- 
                                                                                 //       Задействовать компонент EndpointRoutingMiddleware.
                                                                                 //       Это заставит приложение будет использовать
                                                                                 //       маршрутизацию (****что именно произойдёт?)

            app.UseEndpoints(endpoints =>                                        //     ...IApplicationBuilder app.UseEndpoints(..) ---
            {                                                                    //       задействовать компонент EndpointMiddleware.
                                                                                 //       Это заставит приложение отправлять ответы на запросы
                                                                                 //       по заданным адресам
                                                                                 //
                                                                                 //       У метода один параметр - Action<..IEndPointRouteBuilder>
                                                                                 //       
                                                                                 //           Интерфейс
                                                                                 //           Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
                                                                                 //           - это уже builder маршрута (builder адреса)
                                                                                 //       
                                                                                 //       В соответсвии с шаблоном мы отправляем лябмда-выражение,
                                                                                 //       параметр которого назван endpoints
                                                                                 //
                endpoints.MapGet("/", async context =>                           //         endpoints.MapGet(..) --- обработка запроса.
                {                                                                //
                    await context.Response.WriteAsync("Hello World!");           //     
                                                                                 //           > "/" --- если юзер постучится в корневой раздел
                                                                                 //           > await context.Response.WriteAsync(..) --- то
                                                                                 //             отправить ответ строкой "Hello World!"
                                                                                 //   
                                                                                 //   Эти компоненты, обрабатывающие HTTP-запросы, зовутся
                                                                                 //     "middleware"
                                                                                 //   
                                                                                 //   Порядок задействования компонентов, кстати, тоже важен
                                                                                 //       
                                                                                 //       Если поменять местами app.UseEndpoints() и
                                                                                 //         app.UseRouting(), то app.UseEndpoints() выдаст
                                                                                 //         исключение. Этот метод ожидал, что в app уже
                                                                                 //         задеуйствуется компонент маршрутизации
                                                                                 //       
                                                                                 //   Знай, что, т.к. метод Configure() вызывется лишь один раз
                                                                                 //     , все задействованные здесь компоненты останутся в
                                                                                 //     приложении до конца (т.е. все запросы проходят через
                                                                                 //     одни и те же компоненты)

                });




                int x = 2;
                app.Run(async (context) =>
                {
                    x = x * 2;
                    await context.Response.WriteAsync($"Result: {x}");  // x --- вроде как 2 * 2 = 4. Так и будет в первый раз, но ты обнови
                                                                        //   страницу (получишь 8-ку)!
                                                                        //   
                                                                        //     Но на chromium-браузерах (вроде opera) ты скорее всего
                                                                        //       получишь 8-ку сразу, т.к. они за каким-то фигом могут посылать
                                                                        //       сразу по 2-а запроса (один к нашему приложению app, второй
                                                                        //       к иконке favicon.ico (****а что это?))
                });                                                     // app.Run(..) - это просетйший способ добавления своего компонента в
                                                                        //   конвеер
                                                                        //     
                                                                        //   Правда, компонент, добавленный через Run(), не вызывет
                                                                        //     следующий компонент и дальше обработку запроса не передаёт
                                                                        //   
                                                                        //   Принимает метод app.Run() делегат
                                                                        //     Microsoft.AspNetCore.Http.RequestDelegate, что объявлен так:
                                                                        //   
                                                                        //         public delegate ...Task RequestDelegate (...HttpContext context)
                                                                        //   


            });
        }


        // Также опционально ты можешь выставить ctor в этом классе, что, как правило, производит начальную конфигурацию приложения
    }
}
