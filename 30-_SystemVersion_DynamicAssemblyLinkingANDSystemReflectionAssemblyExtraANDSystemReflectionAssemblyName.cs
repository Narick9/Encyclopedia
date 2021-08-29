/*
 * creation date  01 jun 2021
 * last change    28 jun 2021
 * author         artur
 */
using System;
using System.Reflection;

class _SystemVersion_DynamicAssemblyLinkingANDSystemReflectionAssemblyExtraANDSystemReflectionAssemblyName
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemVersion();
        DynamicAssemblyLinkingANDSystemReflectionAssemblyExtraANDSystemReflectionAssemblyName();

        Console.ReadLine();
    }
    static void SystemVersion()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemVersion()\n");


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemVersion()");
    }
    static void DynamicAssemblyLinkingANDSystemReflectionAssemblyExtraANDSystemReflectionAssemblyName()  // после System.Version
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   " +
            "DynamicAssemblyLinkingANDSystemReflectionAssemblyExtraANDSystemReflectionAssemblyName()\n");


        // Как мы знаем, CLR перед тем, как запустить сборку, заглядывает в её манифест. Там она смотрит какие внешние сборки нужны для
        //   этого
        //   приложения, и подключает их (по своим правилам или по тем, что написаны в .config-файле). Тем не менее, если тебе нужно
        //   подключить сборку прямо в runtime, можешь использовать специально созданный для этого класс System.Reflection.Assembly
        // И даже не важно какую сборку ты загружаешь - закрытую, разделяемую или находящуюся в произвольном местоположении - методы 
        //   System.Reflection.Assembly.Load() и System.Reflection.Assembly.LoadFrom() позволят тебе подключать и использовать всех их
        //   (с помощью них ты своим кодом будешь задать те же настройки, что и в .config-файле)
        //
        // Вообще, System.Reflection.Assembly - это абстрактный класс, что наследует напрямую от System.Object
        //   /////////after reading://////////////////////////////////////////////////////////////////////
        //   // И реализует (****из пространства ..) ICustomAttributeProvider  ISerializable
        //   /////////////////////////////////////////////////////////////////////////////////////////////
        //
        // "Dynamic Linking" - там называется этот процесс по Троелсену. Microsoft Docs же
        //   выбрал немного другое именование - "dynamic assembly load" (иногда Троелсен также использует это название)


        void DisplayTypesInAssembly(Assembly asm)                          // asm - напомню, что локальные функции не могут использовать те
        {                                                                  //   имена, что уже заняты в их пространстве (т.е. объявить параметр
            Console.WriteLine("Type in the assembly {0}:", asm.FullName);  //   с именем assembly нельзя, и здесь пришлось сделать как автор,
            foreach (Type current in asm.GetTypes())                       //   т.е. объявить asm)
            {
                Console.WriteLine(current);
            }
            Console.WriteLine();   //
        }                          //
                                   //
        string assemblyName = "";  //
        Assembly assembly = null;  // string, Assembly - VS говорит о необязательности предобъявления переменных, т.е. нас слудет объявлять
        do                         //   их прямо в цикле (в таком случае, скорее всего, компилятор сам оптимизирует всё должным образом)
        {
            Console.WriteLine("Enter an assembly name to evaluate");
            Console.Write("or enter Q to quit: ");
            assemblyName = Console.ReadLine();
            if (assemblyName.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                assembly = Assembly.Load(assemblyName);  // System.Reflection.Assembly.Load() - выдаст нам объект сборки. Нам следует
                DisplayTypesInAssembly(assembly);        //   отправить строку с дружественным именем нужной сборки. Например, "14.2".
            }                                            //   (****а может "'14.2'") в этот метод (у него есть ещё 7-мь перегрузок). Для всё
            catch                                        //   просиходит по обычному сценарию, что CLR использует для зондирования сборок
            {                                            //   (ты уже видел этот сценарий в методе LinkingAssemlbies..()). Разве что теперь,
                                                         //   если CLR таки не смогла найти файл сборки даже после последнего пункта (когда она
                                                         //   попыталась найти его в одноимённой папке в текущей dir'ки), уже дропнится
                                                         //   исключение System.IO.FileNotFoundException
                                                         // Если будет найден файл сборки с таким дружественным именем, но имя в её
                                                         //   манифесте манифесте не будет совпадать, то произойдёт System.IO.FileLoadException
                                                         //   (я пытался переименовать 14.2.dll в CarLibrary.dll и загрузить его в домен)
                                                                      // Я копировал сборку 14.2 в энциклопедию (это
                Console.WriteLine("Sorry, can't find the assembly");  //   ./bin/Debug/net5.0/14.2.dll, и её копия - ./14.2.dll), при этом
                                                                      //   не связав проект с ней. Теперь, если пользователь введёт её
                                                                      //   дружественное имя (или дружественное имя + .dll),
                                                                      //   будут выведены её внутренние типы (даже
            }                                                         //   закрытые, ведь рефлексия берёт данные из
        } while (true);                                               //   метаданных типов, и, может, манифеста)
        Console.WriteLine();                                          // Интересно то, что приложение увидит только те сборки, что были
                                                                      //   изначально видны. Т.е. если запустить приложение, в папке
        // Мы могли бы ещё довольно сильно улучшить приложение,       //   которого есть 14.2.dll, и затем эту сборку оттуда убрать, то
        //   используя вместо Assembly.Load() метод                   //   приложение всё ещё сможет работать с ней. Если запустить
        //   Assembly.LoadFrom(), который принимает абсолютный (и     //   приложение, и только после вставить 14.2.dll в папку, оно его
        //   отностиельный) путь к сборке (поэтому проект сможет      //   видеть не будет. Также на сильное причастие здесь стартовых
        //   работать с вообще любыми досягаемыми сборками). Там      //   действий CLR здесь указывает то, что сборку 14.2.dll можно
        //   также есть эта странность (приложение видит устаревшие   //   засунуть в папку 14.2, и приложение будет использовать её оттуда
        //   данные по директориям), но теперь ввод *.dll обязателен  // Метод Assembly.Load() невоспреимчив к регистру символов
        //   (это говорит о том, что за связывание сборки теперь      //
        //   отвечает другая часть CLR)                               //
        // Автор говорит, что по сути метод Assembly.LoadFrom()       //
        //   программно предоставляет элемент <codeBase> (и не        //
        //   трогая .config-файл)                                     //


        // На самом деле в метод System.Reflection.Assembly.Load() (т.к. он принимает длинную форму имени сборки, или, как это прозвал
        //   автор, "отображаемым именем") можно отправлять строку вида:
        //       Имя[, Version=<старшийНомер>.<младшийНомер>.<номерСборки>.<номерРедакции>][,Culture=<маркерКультуры]
        //       [,PublicKeyToken=<маркерОткрытогоКлюча>]
        //   Ты можешь выставлять пробелы между частями [..] и между символами =, вводить части в любом порядке (и даже в некоторых случаях
        //   писать всякую ересь), и метод всё-равно поймёт. Как ты понял, отображаемые имена всегда отображаются (точнее, выдаются)
        //   стандартным методом ToString() у сборок
        // Метод оказался умнее, чем я думал. Если ты отправил не ту версию сборки, он всё-равно найдёт её (если альтернатив по версии не
        //   будет, конечно)
        //   Интересно ещё то, что если ты перейдёшь край в 65535 в одном из чисел версии, Assembly.Load() просто выдаст исключение, говоря
        //   , что не смог найти сборку
        // Как и везде, указание пустой строки в части Culture (=""), говорит CLR использовать стандартную культуру в поиске. А в части
        //   PublicKeyToken можно задать null, говоря, что это не строго именованная сборка
        // Ещё, вместо того чтобы хранить в переменной строку с длинной формой имени сборки, легче использовать поставляемый .NET класс
        //   System.Reflection.AssemblyName, имеющий ещё несколько плюшек (но, правда, создающийся из той же длинной формы)
        //
        AssemblyName myAsmName = new AssemblyName();
        myAsmName.Name = "14.2";                         // myAsmName.Name - ****
        myAsmName.Version = new Version("1.0.0.1");      // myAsmName.Version - свойство типа System.Version (этот простенький класс часто
        try                                              //   используется в паре с объектами System.Reflection.AssemblyName)
        {                                                // System.Reflection.Assembly.Load(myAsmName) - одна из перегрузок этого метода
            Assembly myAsm = Assembly.Load(myAsmName);   //   принимает объект такого типа (т.е. типа System.Reflection.AssemblyName)
            Console.WriteLine("{0}\n", myAsm.Location);  // Интересно, но можно писать и так:    @"..Culture="""    ****что это? сейчас на
                                                         //                           ****VS Code со сломанными ассетами проверить не могу
        }                                                // myAsm.Location - в качестве доказательства того, что мы нашли именно ту сборку,
        catch (System.IO.FileNotFoundException)          //   что нам нужна, выведем её полное местоположение
        {                                                // Если ты хочешь получить разделяемую сборку (т.е. имеющую цифровую подпись), то
            Console.WriteLine("Can't find 14.2.dll\n");  //   тебе нужно задать её версию (можно также не точную), значение открытого ключа
        } // Да, врядли тебе ещё раз доведётся           //   и культуру. Иначе не найдёт:
          //   разрабатывать браузер объектов, но        //        System, culture = "", publickeytoken = b77a5c561934e089, version = 4.0..
          //   понимание работы классов из               //
          //   System.Reflection весьма ценно,           //
          //   особенно если учесть то, что на нём       //
          //   завязаны многие технологии                //
          /////////after reading://////////////////////////
          //   (например, позднее связывание, о котором  //
          //   далее)                                    //
          /////////////////////////////////////////////////


        System.Reflection.Assembly currentAsm = System.Reflection.Assembly.GetExecutingAssembly();
                                                            // ..Assembly.GetExecutingAssembly() - этот статический метод выдаст тебе объект
                                                            //   прям таки текущей сборки. Перегрузок, конечно, нет


        /////////after reading:LateBinding///////////////////////////////////////////////////////////////
        // Это обрывок из кода, что будет чуть дальше, с добавленными коммента про Assembly 
        // System.Reflection.Assembly theSnapinAsm = Assembly.LoadFrom(..);
        // Type t = typeof(..);
        // IAppFunctionality itsInstance = (IAppFunctionality)theSnapinAsm.CreateInstance(t.Name, true);
                                        // theSnapinAsm.CreateInstance() - да, у объектов типа
                                        //   System.Reflection.Assembly также есть этот метод. На
                                        //   самом деле это просто удобная обёртка над методом
                                        //   System.Activator.CreateInstance()
                                        // theSnapinAsm.CreateInstance(..) - вообще, описание метода
                                        //   просит отправить ему t.FullName, но и t.Name прекрасно
                                        //   работает (несмотря на то, что полное имя типа в объекте t
                                        //   имеет namespace'овые префиксы). Второй параметр здесь - 
                                        //   bool ingoreCase
        /////////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   " +
            "DynamicAssemblyLinkingANDSystemReflectionAssemblyExtraANDSystemReflectionAssemblyName()");
    }
}