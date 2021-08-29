/*
 * creation date  26 jun 2021
 * last change    28 jun 2021
 * author         artur
 */
using System;

class _SystemEnvironment_ConfigurationFileExtraData
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemEnvironment();
        ConfigurationFileExtraData_Silent();

        Console.ReadLine();
    }
    static void SystemEnvironment()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemEnvironment()\n");


        // System.Environment - это статический класс, что предоставляет всякого рода инфу о окружении, в котором работает твоё поделие. Вот
        //   некоторые полезные его члены:


        string[] theArgs = Environment.GetCommandLineArgs();                              // GetCommandLineArgs() - это второй способ получить
        foreach (string arg in theArgs)                                                   //   аргументы коммандной строки (в виде массива
            Console.WriteLine("arg: {0}", arg);                                           //   string[]). Здесь первый элемент - это уже имя
                                                                                          //   программы
                                                                                          //
        foreach (string drive in Environment.GetLogicalDrives())                          // GetLogicalDrivers() - возвратит массив string[]
            Console.WriteLine("Drive:         {0}", drive);                               //   со именами всех логических разделов на твоём
                                                                                          //   пека (обычно начинается с "C:\")
                                                                                          //
        Console.WriteLine("OS:            {0}", Environment.OSVersion);                   // OSVersion - это свойство выдаст объект объект
        Console.WriteLine("Number of processors:     {0}", Environment.ProcessorCount);   //   класса System.OperationSystem с инфо о текущей
        Console.WriteLine(".NET version:  {0}", Environment.Version);                     //   оси
                                                                                          // ProcessorCount - выдаст количество потоков в виде
                                                                                          //   int (да, потоков, не процессоров)
                                                                                          // Version - выдаст версию твоего CLR (в формате
                                                                                          //   версий сборок, т.е. число.число.число.число)
                                                                                          //
        Environment.ExitCode = -2;                                                        // ExitCode - так можно задать код выхода процесса
        Console.WriteLine("Is it x86_64:  {0}", Environment.Is64BitOperatingSystem);      //   (свойство типа int)
        Console.WriteLine("Machine name:  {0}", Environment.MachineName);                 // Is64BitOpeartionSystem - тут понятно (тип bool)
        Console.Write("new line for this enviroment: {0}", Environment.NewLine);          // MachineName - 15-тисимвольное имя твоего пека в
        Console.WriteLine("Full path to system dir:  {0}", Environment.SystemDirectory);  //   сети TCP/IP (типа string)
        Console.WriteLine("User name:     {0}\n", Environment.UserName);                  // NewLine - это свойство выдаст символы новой строки
                                                                                          //   для твоей системы ("\r\n" для не Unix и "\n" для
                                                                                          //   Unix)(типа string)
                                                                                          // SystemDirectory - выдаст полный путь к твоей
                                                                                          //   system32 директории (в виде string)
                                                                                          // UserName - тут всё понятно, да, %USERNAME%?
                                                                                          //   (выдаст имя текущей учётной записи в виде
                                                                                          //   string)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemEnvironment()");
    }
    static void ConfigurationFileExtraData_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   ConfigurationFileExtraData_Silent()\n");


        // Конфигурационный файл может использоваться не только для настройки CLR, но и для хранения каких-то данных, что будут доступны из
        //   приложения. На самом деле я вижу мало смысла в этом, но .NET предоставляет целое пространство имён System.Configuration для
        //   работы с этими данными (почему бы не использовать уже имеющуюся библиотеку для чтения файлов, нежели вводить новую?). В нём есть
        //   небольшой набор типов (так говорит автор)
        // Такие специальные настройки должны быть помещены в элементы <add>, что должны содержаться в элементе <appSettings> (больше
        //   информации в конфигурационном файле 14.6.exe.config)


        // Вот что я писал в проекте 14.6:
        //   // AppSettingsReader ar = new AppSettingsReader();  // System.Configuration.AppSettingsReader - этот тип имеет метод
        //   //                                                  //   GetValue(), через
        //   //                                                  //   который мы и получим нужные нам данные. Класс находится в
        //   //                                                  //   mscorlib.dll (как
        //   //                                                  //   и многие другие классы из System.Configuration)(но, видимо, эта инфа
        //   //                                                  //   устарела. Выхлоп ошибок dotnet build'а говорит о том, что этот класс
        //   //                                                  //   был переправлен в сборку
        //   //                                                  //   System.Configuration.ConfigurationManager.dll)
        //   // ConsoleColor before = Console.ForegroundColor;
        //   // Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), (string)ar.GetValue("TextColor", typeof(string)));
        //   //                                                                                         // ar.GetValue() - мы должны
        //   // Console.WriteLine("{0}  {1}", "Musician", ar.GetValue("Musician", typeof(string)));     //   отправить строковой ключ и тип
        //   // Console.WriteLine("{0}  {1}", "YearOfBirth", ar.GetValue("YearOfBirth", typeof(int)));  //   нужной записи
        //   //                                                                                         // Имей ввиду, что если что-то
        //   // // Как говорит автор, конфигурационные файлы на самом деле применяются повсеместно в    //   подёт не так (не будет записи
        //   // //   API-интерфейсах (вроде WCF или ASP.NET). На самом деле здесь была показано лишь    //   с таким ключом или будет
        //   // //   несколько из доступных элементов настройки приложения (хоть и самых интересных).   //   неверный тип), то выйдет
        //   // //   Если ты зайдёшь в msdn, то ты можешь найти полное описание их всех                 //   исключение (****какое?)
        //   //
        //   // Console.ForegroundColor = before;


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   ConfigurationFileExtraData_Silent()");
    }
}