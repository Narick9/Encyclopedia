/*
 * creation date  26 jun 2021
 * last change    01 jul 2021
 * author         artur
 */
using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using CommonShareableTypes;

class _TrueOfReflectionLateBindingAttributes
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        TrueOfReflectionLateBindingAttributes_Silent();

        Console.ReadLine();
    }
    static void TrueOfReflectionLateBindingAttributes_Silent()  //after System.Windows.Forms.OpenFileDialog
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   TrueOfReflectionLateBindingAttributes_Silent()\n");


        // Ты уже мог задаться вопросом (подсознательно - точно): "А где и когда тебе стоит использовать рассмотренную рефлексию, позднее
        //   связывание и эти ваши атрибуты?". На самом деле правильный ответ таков: "Когда ты сам почуствуешь это нужным". Не стоит
        //   использовать технологии, если всё легко решается и без них (если ты делаешь это не для собственного интереса. Эти слова
        //   писались уже множество раз в разных формах). А когда ты это почувствуешь? Например, когда у тебя стоит такая цель:
        //
        //   > продукт должен позже расшириться за счёт использования дополнительных сторонних инструментов
        //
        //   Именно эта цель и стояля у разработчиков Visual Studio. Очевидно, что они не могли использовать библиотеки через
        //   ранее связывание, в то время, когда Reference Manager'а ещё не было (и не могло быть). В коде просто делались "зацепки", по
        //   которым затем просто "привязывались" модули. Что за зацепки и как они делались? Вот один из путей постройки такого приложения:
        //
        //   > Во-первых, в расширяемом приложении должен быть некоторый механизм ввода (консоль/диалоговое окно), через который можно
        //     указать модуль для подключения. Это требует динамической заргузки (той, что делается статическим методами
        //     System.Reflection.Assembly.Load()/..LoadFrom())
        //   > Во-вторых, этому расширяемому приложению следует иметь возможность выяснить, действительно ли все подключенные модули
        //     имеют нужную ему функциональность (типы/функции/ресурсы)(этот вопрос решается рефлексией)
        //   > В-третьих, расширяемое приложение должно иметь возможность пользоваться всеми полученными через модули данными (т.е. во всю
        //     используется позднее связывание)
        //
        //   Именно такой подход был принят создателями VS (как видишь, чего-то мудрёного здесь нет)


        // Теперь давай попробуем собственноручно простроить такое расширяемое приложение, что может быть дополнено функциональностью
        //   внешних сборок. Вот модули, из которых оно будет состоять:
        //
        //   > CommonShareableTypes.dll  - здесь будут определены типы на экспорт (всего интерфейс и атрибут). Эти типы дальше будут
        //                                 использовать оснастки. Наше главное расширяемое приложение (dotnetCoreVersion.exe) также будет знать
        //                                 эти типы
        //   > CSharpShareIn.dll         - собственно, оснастка. Эта сборка дальше и будет служить расширением нашего главного приложения. Как
        //                                 наше приложение сможет использовать типы из этой оснастки? Эти типы прост будут подчиняться
        //                                 деклорации того самого интерфейса
        //   > VbShareIn.dll             - вторая оснастка, написанная уже на VB
        //   > dotnetCoreVersion.exe     - наше главное консольное приложение, что сможет расширяться функциональностью этих оснасток (должен
        //                                 быть запущен именно этот метод энциклопдеии, что ты читаешь, конечно)
        //
        //   Как ты понял, в нашем приложении будут использоваться динамическая загрузка, рефлексия и позднее связывание
        //
        //
        // Как говорит автор, вряд ли тебе действительно понадобится строить консольное приложение. Подавляющее большинство приложений, что
        //   написаны на C# - это либо действительно развитые клиенты (что применяет графику из Windows Forms или WPF), либо веб-приложения
        //   (что используют ASP.NET Web Forms или ASP.NET MVC). Консольные приложения применяются, чтобы не отвлекать внимание на ненужной
        //   сейчас структуре. Как строить реальные энтерпрайз приложения ты узнаешь позже в энциклопедии
        //
        //
        // Для начала создадим проект в нашем решении (т.е. в решении энциклопедии), и назовём его CommonShareableTypes (ты уже знаешь как это
        //   сделать)(я выделил папку ./CommonShareableTypes/ для этого проекта). В .cs файле этого проекта есть несколько комментариев. Ссылка
        //   на этот проект проведена мной через dotnet cli
        //
        // Проект CSharpShareIn тоже будет создан (и помещён в ./CSharpShareIn15.7/). В нём будет объявлен тип, и этот тип реализует интерфейс
        //   CommonShareableTypes.IAppFunctionality из проекта CommonShareableTypes. Этот проект тоже будет .dll. И этот проект готов (в нём
        //   есть комменты)
        //
        // Для эмуляции стороннего производителя (предпочитающего, например, Visual Basic), будет создан ещё один проект - VbShareIn. Сам
        //   проект - это тот же CSharpShareIn, но в обёртке Visual Basic. Он тоже реализует класс, использующий начинку из
        //   CommonShareableTypes
        //
        //
        // Теперь в нашем решении 3-и проекта. Как задать тот, что будет запускаться F5, ты уже знаешь (из метода про проекты и решения)
        //
        //
        // "Что там по зависимостям?" скажеть ты. А всё хорошо. Мы хотели бы, чтобы при изменении CommonShareableTypes изменялись остальные
        //   проекты. Т.к. у них имеется прямая ссылка на него, он уже числится у всех в зависимостях
        //
        //
        // Теперь, когда мы имеем всю нужную инфраструктуру, можно заняться созданием логики расширяемого приложения. Напомню, что его цель
        //   заключается в использовании начинки из независимого файла посредством динамической заргузки, рефлексии и позднего связывания
        //
        // Дальше всё закоменченно, т.к. gnu/linux'овский .NET не имеет в своём составе пространства System.Windows.Forms и штук из него (а
        //   ведь блок ниже сильно завязан на них)
        void LoadSharein()                            // LoadSharein() - этот метод сначала предлагает пользователю выбрать нужную ему
        {                                             //   оснастку, затем проверяет её сначала простым способом, а затем вызывает функцию
            OpenFileDialog dlg = new OpenFileDialog   //   LoadExternalModule() (если он вернёт false, выйдет сообщение об ошибке)
            { 
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Filter = "assemblies (*.dll)|*.dll|All files (*.*)|*.*",
                FilterIndex = 1
            };
            if (dlg.ShowDialog() != DialogResult.OK)  // ShowDialog - собственно, выводит на экран окно. Возвращает значение перечисления
            {                                         //   DialogResult в зависимости от действий пользователя. Требует связки с System.dll
                Console.WriteLine("User canceled out of the open file dialog");  //****не могу понять откуда это перечисление, т.к. делаю
                return;                                                          //    вслепую из gnu/linux'а. Да и вообще здесь много всякого
            }                                                                    //    в этом методе, что следует разгрести из винды
            if (dlg.FileName.Contains("CommonShareableTypes"))  // dlg.FileName.Contains(..) - это наша первичное требование к сборке,
            {                                                   //   которую пользователь хочет использовать в качестве оснастки
                Console.WriteLine("CommonSnareableTypes has no snap-ins!");
            }
            else if (!LoadExternalModule(dlg.FileName))  // LoadExternalModule() - это уже более глубокая проверка (на нужные типы). Если
            {                                            //   вернёт false, выйдет сообщение о том, что в сборке не найден нужный интерфейс
                Console.WriteLine("Nothing Implements IAppFunctionality!");
            }
        }
        bool LoadExternalModule(string path)  // LoadExternalModule() - сначала метод пробует загрузить нужную сборку по заданному пути, а
        {                                     //   затем и создать объекты, чей класс реализует требуемый интерфейс. Если что-то по ходу
            bool theSnapinIsFounded = false;  //   пойдёт не так, или не будет найден нужный тип, вернёт false. Если всё хорошо - true
            Assembly theSnapinAsm = null;
            try
            {
                theSnapinAsm = Assembly.LoadFrom(path);  // System.Reflection.Assembly.LoadFrom() - здесь мы применяем динамическую загрузку
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured loading the snarein: {ex.Message}");
                return theSnapinIsFounded;
            }
            var theClassTypes = from t in theSnapinAsm.GetTypes()               // from.. - напомню, что запросы LINQ часто требуют
                where t.IsClass && t.GetInterface("IAppFunctionality") != null  //   использования    using System.Linq    (для того, чтобы
                select t;                                                       //   расширяемые методы, в которые превратится это выражение,
                                                                                //   действительно были видны)
                                                                                // ..GetTypes() - а здесь применяется рефлексия
            foreach (Type t in theClassTypes)
            {
                theSnapinIsFounded = true;
                IAppFunctionality itsInstance = (IAppFunctionality)theSnapinAsm.CreateInstance(t.Name, true);
                itsInstance?.DoIt();
                DisplayCompanyData(t);
            }
            return theSnapinIsFounded;  // itsInstance?.DoIt() - позднее свзяывание - если всё хорошо, вызываем этот метод из каждого
        }                               //   подходящего объекта
                                        // DisplayCompanyDate() - и выводим информацию из их атрибутов [CompanyInfo] (если они есть)
  
        void DisplayCompanyData(Type t)  // DisplayCompanyDate() - просто выводим всю информацию из наших атрибутов [CompanyInfo] из
        {                                //   заданного типа (если они есть)
            var theAttributes = from att in t.GetCustomAttributes(false) where att is CompanyInfoAttribute select att;
            foreach (CompanyInfoAttribute current in theAttributes)  // where att is .. - да, такую подборку можно было получить прямо из
            {                                                        //   GetCustomAttributes(), но автор решил использовать запрос LINQ
                Console.WriteLine($"More info about3 {current.CompanyName} can be found at {current.CompanyUrl}");
            }
        }
        //
        //
        do
        {
            Console.WriteLine("Would you like to load a snapin? [Y,N]");
            string input = Console.ReadLine();
            if (!input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            try  // try-catch - на случай других непредвиденных исключений
            {
                LoadSharein();
            }
            catch
            {
                Console.WriteLine("Sorry, internal error, can't find a snapin");
            }
        } while (true);
  
        Console.WriteLine();
        //
        //
        // Итак, теперь ты знаешь один из путей создания расширяемого приложения. Здесь показана полезность позднего связывания,
        //   рефлексии и динамической загрузки. И, как ты видишь, их можно применять не только для постройки программистких утилит


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   TrueOfReflectionLateBindingAttributes_Silent()");
    }
}