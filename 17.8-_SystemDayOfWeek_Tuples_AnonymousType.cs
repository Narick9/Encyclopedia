/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemDayOfWeek_Tuples_AnonymousType
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDayOfWeek();
        Tuples();
        AnonymousType();

        Console.ReadLine();
    }
    static void SystemDayOfWeek()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDayOfWeek()\n");


        // Перечисление System.DayOfWeek предоставляет дни недели. Всего-то! 


        Console.Write("What is your favorite day of the week?: ");
        try
        {
            DayOfWeek userFavDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), Console.ReadLine());
            //                          // System.DayOfWeek - удобное перечисление для дней недели
            switch (userFavDay)         // switch - перечисления и даже строки (в отличие от Си) здесь также поддерживаются (в Си строки в
            {                           //   switch не разрешались)
                case DayOfWeek.Sunday:
                    Console.WriteLine("Hobby time!");
                    break;
                case DayOfWeek.Monday:
                    Console.WriteLine("Another day, another dollar");
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("At least it is not Monday");
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("A fine day");
                    break;
                case DayOfWeek.Thursday:
                    Console.WriteLine("Almost Friday..");
                    break;
                case DayOfWeek.Friday:
                    Console.WriteLine("Yes, Friday rules!");
                    break;
                case DayOfWeek.Saturday:
                    Console.WriteLine("Great day indeed");
                    break;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Hmm.. I have never heard about this day");
        }
        Console.WriteLine();


        Console.WriteLine("1 [int], 2 [string], 3 [float]");
        Console.Write("Your choice: ");
        object choice;  // object - благодаря магии наследования object (т.к. он на вершине цепи) может стать любым другим типом .NET
        switch (Console.ReadLine())
        {
            case "1":
                choice = 5;
                break;
            case "2":
                choice = "Hi";
                break;
            case "3":
                choice = 5.2F;
                break;
            default:
                Console.WriteLine("I'll pretend that it was 1..");
                choice = 5;
                break;
        }
        switch (choice)  // switch - в C# 7 switch научился сравнивать типы
        {
            case int i:  // i - при соотвествии типов в i копируется значение choice
                Console.WriteLine("You have chosen [int]: {0}", i);
                break;
            case string s:
                Console.WriteLine("You have chosen [string]: {0}", s);
                break;
            case float f:
                Console.WriteLine("You have chosen [float]: {0}", f);
                break;
        }
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDayOfWeek()");
    }
    static void Tuples()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   Tuples()\n");


        (string, int, char) valuesFth = ("hello", 42, 'n');  // () - C# поддерживает на должном уровне очень удобную вещь - кортежи.
                                                             //   Здесь они фактически появились в C# 6, но там была довольно урезанная
        Console.WriteLine($"First: {valuesFth.Item1}");      //   реализация (поля не проверялись на достоверность, нельзя было
        Console.WriteLine($"Second: {valuesFth.Item2}");     //   добавлять собственные методы и, что возможно самое важное, какждое
        Console.WriteLine($"Third: {valuesFth.Item3}\n");    //   свойство относилось к ссылочному типу, что убавляло темпы работы).
        // Item - так мы получаем доступ к значениям. Заметь /////////after reading////////////////////////////////////////////////////////////
        //   , что отчёт идёт с 1-го                         //   boxing-unboxing всё-таки операция недешёвая
                                                             //////////////////////////////////////////////////////////////////////////////////
                                                             //   В C# 7 вместо бытия обычным ссылочным типом использована другая основа -
                                                             //   структура System.ValueTuple<>. Это обобщение, создающее новые структуры для
                                                             //   предоставления кортежей
                                                             // ("hello",..) - да, этого уже достаточно для создания кортежа


        (string FirstLetter, int TheNumber, char LowerN) valuesSnd = ("Le Freak", 1978, 'n');
        var valuesTrd = (FirstLetter: "Stakka Bo", 230, GoodChar: 'M');           // FirstLetter - вместо ItemX можно задать свои именя полей,
                                                                                  //   причём задать их можно как до =, так и после.
        (string LeftStr, int LeftInt) target = (RightStr: "etj", RightInt: 235);  //   Можно даже в обоих сразу (это не приведёт к
                                                                                  //   ошибке компиляции), но браться будут имена слева (но
                                                                                  //   появится warning). Следует знать, что если типы будут
        Console.WriteLine("valuesTrd first: {0}", valuesTrd.FirstLetter);         //   заданы в левой части конкретно (т.е. без var), то
        Console.WriteLine("valuesTrd second: {0}", valuesTrd.Item2);              //   компилятор будет использовать имена именно из левой
        Console.WriteLine("valuesTrd third: {0}\n", valuesTrd.GoodChar);          //   части, даже (имена из правой части будут
        //      // Item2 - хоть это и первый Item, но номер у него 2              //   проигнорированы)
        //      // FirstLetter - хоть автор и уверяет, что даже после назначения  //
        //      //   кастомных имён полям члены ItemX остаются, здесь такого н    //
        //      //   происходи                                                    //
        //      /////////after reading/////////////////////////////////////////// //
        //      // Важно понимать, что кастомные имена кортеже                    //
        //      //   существуеют только до этапа компиляции и недоступны при      //
        //      //   рефлексии                                                    //
        //      ///////////////////////////////////////////////////////////////// //


        (int Left, string Middle, char Right) GetValues()  // (int,..) - раньше пользовались другими более объёмными способами (вроде
        {                                                  //   возврата объекта специального класса или использования множества
            return (8, "sometext", 'r');                   //   модификаторов параметра out)
        }

        var values = GetValues();
        Console.WriteLine("Int is {0}", values.Left);
        Console.WriteLine("String is {0}", values.Middle);
        Console.WriteLine("Char is {0}\n", values.Right);


        (string, char, int) GetSet() => ("othertext", 'v', 225);

        (string leftStr, char midChar, int rightInt) = GetSet();    // (..leftStr..) - это называется деконструированием кортежа: мы
        Console.WriteLine("leftStr: {0}", leftStr);                 //   извлекаем свойства, чтобы работать с ними по отдельности. Не
        Console.WriteLine("midChar: {0}", midChar);                 //   видишь разницы со стандартным объявлением кортежа? Посмотри на имя
        Console.WriteLine("rightInt: {0}\n", rightInt);             //   после скобок. Его нет, и в этом отличие! Деконструирование можно
        //      // Стоит знать, что деконструкцией называют         //   реализовать по-разному:
        //      //   расщипление не только кортежей, но и типов.    //       (.. left, .. mid, .. right) = ...
        //      //   Например, у нас была структура Point2D. Мы     //       var (left, mid, right) = ...
        //      //   могли добавить метод для получения кортежа     //   Заметь, что последняя запись применяется только к var
        //      //   некоторых внутренних полей:                    //
        //      //       (int PosX, int PosY) Deconstruct => (x, y) //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   Tuples()");
    }
    static void AnonymousType()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   AnonymousType()\n");


        string color = "blue";
        var myFox = new { Kind = "fox", Color = color, Name = "Marta" };             // {} - так строятся анонимные типы. Они нужны, чтобы
                                                                                     //   избавить тебя от необходимости строить целый
        Console.WriteLine("{0} is a {1} {2}", myFox.Name, myFox.Color, myFox.Kind);  //   новый класс на один раз. Из себя они
        Console.WriteLine("{0}\n", myFox);  // WriteLine(..myFox..) - т.к. он, как и //   представляют спрятанный в CIL-коде класс без
        // GetHashCode() переопределён так, //   все, расширяет System.Object, в нём //   собственных методов, поддерживающий базовую
        //   чтобы посылать каждое поле     //   определён стандартный набор         //   инкапсуляцию (используя свойства только для
        //   в некий объект типа            //   некоторых методов. ToString() в нём //   чтения)(поля типизируются автоматически)
        //   EqualityComparer<T> (это тип   //   переопределён для вывода начинки    // Полученный класс будет работать по семантике на
        //   для обозначения равенства двух //   (также Equals() и GetHashCode())    //   основе значений, а это значит его объекты не будут
        //   объектов типа T) и из этого    //                                       //   приводить к лишним накладным расходам
        //   делать сам хешкод. Автор       //                                       // Введено в C# 3
        //   уверяет, что это даёт 100%-ую  //                                       // Анонимные типы всегда неявно запечатаны, а их        
        //   гарантию отсутсвия коллизий    //                                       //   экземпляры всегда создаются с помощью стандартных  
        //   (столкновения, совпадений)     //                                       //   конструкторов                                      
        //   двух хешкодов объект этого     //
        //   анонимного типа                //
        //                                  //
        Console.WriteLine("obj is instance of {0}", myFox.GetType().Name);
        Console.WriteLine("Base type of it is _{0}_", myFox.GetType().BaseType);
        Console.WriteLine("ToString(): {0}", myFox.ToString());
        Console.WriteLine("GetHashCode(): {0}\n", myFox.GetHashCode());


        var tempThings = new { Prop1 = "left", Prop2 = "right" };
        var valuesByPropers = (tempThings.Prop1, tempThings.Prop2);
        Console.WriteLine($"Prop1: {valuesByPropers.Prop1}, Prop2: {valuesByPropers.Prop2}\n");
        //      // Prop - автор акцентирует внимание на том, что
        //      //   в C# 7.1 добавили возможность использования
        //      //   выведения имени и значения прямо из свойств
        //      //   других объектов. Здесь было задано свойство
        //      //   tempThings.Prop1, и имя Prop1 стало именем
        //      //   поля у кортежа valuesByPropers. Значине в это
        //      //   время просто копируется. Малонужное и более
        //      //   запутывающее нововведение? - Да


        var firstCar = new { Color = "Bright Pink", Mark = "Saab", CurrentSpeed = 55 };
        var secondCar = new { Color = "Bright Pink", Mark = "Saab", CurrentSpeed = 55 };
        //
        if (firstCar == secondCar)                                // == - здесь неожиданным может стать то, что здесь выйдет false. Анонимные
            Console.WriteLine("same anonymous object");           //   типы не определяют собственных оперетаров, поэтому используется оператор
        else                                                      //   == от object, что сравнивает ссылки (интересно, что этот оператор не
            Console.WriteLine("not same anonymous object");       //   определён в классе object, если верить VS)
        //
        if (firstCar.GetType().Name == secondCar.GetType().Name)  // GetType() - здесь будет true. Это тонкий, но важный момент
            Console.WriteLine("types are same");                  //   компилятора: он сгенерирует новый анонимный класс, только если он
        else                                                      //   будет содержать свой уникальный набор свойств. Это значит, что secondCar
            Console.WriteLine("types aren't same");               //   будет использовать уже созданный ранее скрытый класс (правда, если мы
            //                                                    //   имзеним порядок свойств в определении secondCar, оставив тот же набор,
            //                                                    //   компиялтор всё-же решит создать новый тип. Да, это странность)
            //                                                    //
        firstCar = secondCar;                                     // first.. = second.. - да, вполне совместимы
            //                                                    // VS - как видишь, VS видит, что один объект анонимного типа присваивается
        Console.WriteLine();                                      //   другому, и затемняет firstCar с сообщением о необязательности этой команды
                                                                  //   (т.е. бесполезности)(и ведь правда - зачем нам копия итак неизменяемой
                                                                  //   переменной?)


        var purchaseItem = new
        {
            TimeBought = DateTime.Now,
            ItemBought = new { Color = "Red", Mark = "Saab", CurrentSpeed = 55 },  // {new} - анонимные типы могут содержать другие анонимные
            Price = 34000                                                          //   типы
        };

        
        /////////after reading///////////////////////////////////////////////////////////////////////
        // Обычно анонимные типы редко где применяются, кроме как
        //   с LINQ. Их ограничения, вроде свойств только для
        //   чтения и наследуемости только от System.Object,
        //   делают их полезными в весьмя специфичных ситуациях,
        //   но в них они действительно раскрывают свою мощь
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   AnonymousType()");
    }
}