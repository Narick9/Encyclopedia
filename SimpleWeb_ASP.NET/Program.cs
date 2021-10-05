using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimpleWeb_ASP.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
                                                    // Суть в том, что для запуска приложения ASP.NET нужен объект типа
                                                    //   Microsoft.Extensions.Hosting.IHost (т.е. объект-хост, в котором и будет ютиться наше
                                                    //   веб-приложение)(больше информации об этом типе читай в методе о ..IHost)
            CreateHostBuilder(args).Build().Run();
                                                    // CreateHostBuilder() --- этим static методом мы как раз получим Microsoft..IHostBuilder,
                                                    //   через который создадим заветный хост
               //****решил попробовать выставить    // ..IHost ..IHostBuilder.Build() --- у полученного IHostBuilder'а вызывается этот метод,
               //    возвращаемый параметр - IHost  //   что, собственно, build'ит хост
                                                    // void ..IHost.Run() --- у полученного объекта хоста мы здесь вызываем этот метод, что,
                                                    //   наконец, запускает весь web-хост
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                                                         // Microsoft.Extensions.Hosting.IHostBuilder - это интерфейс для постройки объектов
                                                         //   Microsoft..IHost (у ..IHostBuilder также есть свой метод с разъяснением)
                                                         // CreateHostBuilder() - этот статик метод просто создаёт builder типа ..IHostBuilder
                                                         //   из полученных аргументов
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {                                        
                    webBuilder.UseStartup<Startup>();
                });
                                                         // Microsoft.Extensions.Hosting.Host --- это крошечный static класс, хранящий всего
                                                         //   два метода
                                                         // Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(..) --- этот метод создаёт и
                                                         //   возвращает объект типа ..IHostBuilder со значениями по умолчанию или
                                                         //   полученными из аргументов командной строки (т.е. из args)(ещё считываются
                                                         //   $переменные окружения). Вот что именно делается:
                                                         //       > Новому хост'builder'у ставится текущий корневой каталог (полученный из
                                                         //         System.IO.Directory.GetCurrentDirectory())
                                                         //       > Хост'builder конфигурируется аргументами коммандной строки и переменными
                                                         //         окружения (что с префиксом DOTNET_)
                                                         //       > Конфигурируется приложение (хост. Здесь это синонимы), загружая в него
                                                         //         настройки из appsettings.json и appsettings.<environment>.json
                                                         //         , ещё из аргументов cmd.
                                                         //             
                                                         //             Что такое <environment>? Как ты помнишь, по шаблону создаётся сразу
                                                         //             2-а файла appsettings.json. Один из них - appsettings.Development.json.
                                                         //             Это потому что ASP.NET Core приложение может быть в одном из трёх
                                                         //             стостояний:
                                                         //                 > Development  - разработка
                                                         //                 > Staging      - подготовка к развёртыванию
                                                         //                 > Production   - полноценное использование, когда оно уже
                                                         //                                  развёрнуто на каком-нибудь сервере
                                                         //             Как ты понял, из шаблона приложение находится в состоянии Development,
                                                         //             но это легко изменить (****как?)
                                                         //             
                                                         //         Если приложение находится в статусе разработки, то также
                                                         //         задействуются данные Secret Manager'а (что позволяет сохранить
                                                         //         конфиденциальные данные, используемые при разработке)
                                                         //       > Добавляются провайдеры логгирования (****что это?)
                                                         //       > Обеспечивается валидация серсисов (****что это?)(если проект в статусе
                                                         //         разработки ****всё ещё не знаешь что это)
                                                         // ..ConfigureWebHostDefaults(..) --- этот метод объекта ..IHostBuilder (не родной,
                                                         //   поставляется расширением из ****откуда?) конфигурирует его. Вот что именно
                                                         //   произойдёт:
                                                         //       > Сконфигурируются параметры, что были заданны в переменных среды с префиксом
                                                         //         ASPNETCORE_ . Если конкрентно, доступны эти параметры:
                                                         //             ASPNETCORE_FORWARDEDHEADERS_ENABLED  - если true, в builder добавится
                                                         //                                                    компонент Forwarded Headers (что
                                                         //                                                    позволит считывать из запроса
                                                         //                                                    заголовки "X-Forwaded-"
                                                         //                                                    ****что это такое?)
                                                         //             
                                                         //       > Запустится и настроится веб-сервер Kestrel (****ещё не знаешь что это),
                                                         //         в котором наше приложение и развернётся
                                                         //       > Добавится компонент Host Filtering (что позволит настраивать адреса для
                                                         //         веб-сервера Kestrel)
                                                         //       > Если этот IHostBuilder намеревается создать хост на IIS сервере, то данный
                                                         //         метод ещё и обеспечит интеграцию с ним
                                                         //   Параметр у данного расширяющего метода всего один - типа
                                                         //   System.Action<Microsoft.AspNetCore.Hosting.IWebHostBuilder>.
                                                         //       Интерфейс ..IWebHostBuilder представляет builder'ы, что строят IWebHost
                                                         //       (****чем отличается от IHost?)(в энциклопедии имеется метод об этом типе)
                                                         // webBuilder.UseStartup<>() --- этот метод типов ..IWebHostBuilder устанавливает
                                                         //   стартовый класс приложения. Мы задали наш Startup (что объявлен в ./Startup.cs).
                                                         //   Такой класс, кстати, и начнёт обрабатывать входящие запросы (будет вызываться
                                                         //   Startup.Configure())
    }
}
