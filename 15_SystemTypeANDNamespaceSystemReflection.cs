/*
 * creation date  22 jan 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Reflection;

class _SystemTypeANDNamespaceSystemReflection
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemTypeANDNamespaceSystemReflection();

        Console.ReadLine();
    }
    static void SystemTypeANDNamespaceSystemReflection()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemTypeANDNamespaceSystemReflection()\n");


        // Рефлексия в .NET - это процесс получения инфо о типах прямо в runtime (слово довольно часто упоминалось раньше). На самом деле
        //   службы рефлексии дают те же метаданные, что ты можешь просмотреть в ildasm.exe (т.е. все типы, их поля, пространства имён, методы,
        //   параметры методов, события, делегаты и т.д.)(скорее всего эти службы просто роются файле сборки)
        // Вот основные типы, что представлены в namespace'е System.Reflection (что из mscorlib.dll. Ныне System.Runtime.dll):
        //
        //   > System.Reflection.Assembly       - этот абстрактный класс содержит несколько членов, что позволяют загружать сборки и
        //                                        манипулировать ими. msdn говорит, что этот класс просто представляет объект сборки
        //   > System.Reflection.AssemblyName   - объекты этого класса хранят полное писание своей сборки, связанные с идентичностью (т.е. имя,
        //                                        версия, ..)
        //   > System.Reflection.EventInfo      - этот абстрактный класс создан для исследования события и предоставления его метаданных
        //   > System.Reflection.FieldInfo      - а этот абстрактный класс для исследования атрибута поля и предоставления доступа к его
        //                                        метаданным
        //   > System.Reflection.MemberInfo     - это тоже абстрактный класс, его расширяют EventInfo, FieldInfo, MethodInfo и PropertyInfo.
        //                                        Сам класс определяет методы для получения атрибутов одного члена и предоставляет доступ к ним
        //   > System.Reflection.MethodInfo     - тоже абстрактный класс. Он создан для исследования метода (точнее его атрибутов) и
        //                                        предоставления его метаданных
        //   > System.Reflection.Module         - снова асбтрактный класс. Он позволяет получить доступ к заданному модулю внутри многофайловой
        //                                        сборки
        //   > System.Reflection.ParameterInfo  - объект этого класса хранит инфо (атрибуты и метаданные) о своём параметре
        //   > System.Reflection.PropertyInfo   - абстрактный класс, созданный для исследования атрибутов свойства и предоставления его
        //                                        метаданных
        //
        //   (странно, что всё здесь крутится вокруг абстрактных классов)
        //
        // Чтобы понять, как пользоваться пространством System.Configuration, сначала следует ознакомиться с классом System.Type


        // Класс System.Type находится в mscorlib.dll (по Microsoft Docs'у ещё и в System.Runtime.dll). Этот тип очень примечателен тем, что
        //   его объекты представляет из себя типы (да, типы!). Как? Просто его объекты хранят инфу, что прописана в метаданных
        // Класс имеет набор статических членов, что могут по твоему желанию рыться в этих метаданных типов текущей сборки. Многие из
        //   них возвращают типы из
        //   пространства System.Reflection (например, метод System.Type.GetMethods() возвращает массив System.Reflection.MethodInfo[])
        // На самом деле объекты этого класса (а точнее типов, совместимых с ним) имеют довольно
        //   много членов, но общее представление о многих из них по этой схеме можно получить:
        //
        //   > IsAbstract               Эти свойства (readonly) позволяют выяснить базовые характеристики типа (в основном это флаги, что
        //   > IsArray                  мы видели в метаданных)
        //   > IsClass
        //   > IsCOMObject
        //   > IsEnum
        //   > IsGenericTypeDefinition
        //   > IsGenericParameter
        //   > IsInterface
        //   > IsPrimitive
        //   > IsNestedPrivate
        //   > IsNestedPublic
        //   > IsSealed
        //   > IsValueType
        //
        //   > GetConstructors()        Эти методы выдают массивы интересующих элементов. Каждый метод возвращает массив соответсвующих (т.е.
        //   > GetEvents()              GetConstructors() выдаст массив объектов ConstructorInfo). Также для каждого метода есть версия,
        //   > GetFields()              что возвращает один элемент, подходящий по отправляемым параметрам
        //   > GetInterfaces()
        //   > GetMembers()
        //   > GetMethods()
        //   > GetNestedTypes()
        //   > GetProperties()
        //
        //   > FindMembers()            Этот метод возвращает массив объектов MemberInfo, что подходят по указанным критериям
        //
        //   > GetType()                Этот статический метод возвращает экземпляр типа Type по заданному строковому имени (у него также
        //                              есть ещё 5 перегрузок для уточнения)(работает в схожем с методами Parse() духе)
        //
        //   > InvokeMember()           Этот метод вызывает указанный член, подходящий по заданным ограничениям.
        //                              /////////after reading///////////////////////////////////////////////////////////////////////
        //                              // Автор называет это "поздним связыванием" (об этом чуть позже)
        //                              /////////////////////////////////////////////////////////////////////////////////////////////


        /////////after reading:System.Object/////////////////////////////////////////////////////////
        // Хоть напрямую создать объект абстрактного класса Type нельзя (по вполне понятным причинам
        //   ), его всегда можно получить из метода
        //   GetType() любого объекта (т.к. GetType() все наследуют от System.Object). Как говорит
        //   автор, это будет работать,
        //   только если подвергаемый рефлексии тип будет известен на этапе компиляции
        //                                                                 
        /////////////////////////////////////////////////////////////////////////////////////////////
        // Объект Type можно получить и без экзмемпляра типа, применив оператор typeof():
        //                                                             //
        Console.WriteLine("typeof(DateTime): {0}", typeof(DateTime));  // typeof() - этот C# оператор выдаст объект System.Type типа, что ты
        //                                                             //   поместишь в скобках
        //                                                             /////////after reading//////////////////////////////////////////////////
        //                                                             //   . Это часть RTTI (Run-Time Type Information), поэтому typeof()
        //                                                             //   здесь имеет мало общего с typeof() из gcc
        //                                                             ////////////////////////////////////////////////////////////////////////
        //****DateTime - описание его идёт только в следующем файле    //
        //                                                                 
        Type t = Type.GetType("Phones.Phone, OtherProject", false, true);  // System.Type.GetType() - ещё можно вызвать статический метод
        Console.WriteLine("And type is .. {0}\n", t);                      //   GetType(), выводающий объект System.Type по его полностью
        //                                                                 //   заданному строковому имени (и сборке, где он лежит, но эта
        // Если нужно получить объект System.Type вложенного типа, то      //   часть необязательна, если тип находится в текущей сборке или в
        //   следует указать символ +.                                     //   mscorlib.dll). В одной из его перегрузок можно задать
        //   Например:                                                     //   true/false значения для параметров throwOnError (выдать ли
        //       Type.GetType("CarLibrary.SpyCar+CarMissle, 14.2");        //   исключение, если такой тип не найден?) и ignoreCase
        //   (этот символ говорит о вложенности типа)                      //   (игнорировать ли регистр символов?)
        //                                                                 // Здесь уже не обязательно иметь метаданные типа при
        //                                                                 //   компиляции, т.к. в строке можно написать что угодно, и поиск
        //                                                                 //   будет вестись ****не дописано, т.к. не имею понятия как идёт
        /////////after reading:System.Collections.Generic.List///////////////                     поиск
        // ..Type.GetType() - если для вложенные типов следует             //
        //   использовать символ +, то для обобщённых нужно добавит        //
        //   символ ` и число, говорящее о том, сколько обобщённы          //
        //   параметров должен принимать нужный тебе тип. Например, во     //
        //   что следует ввести, если хочешь узнать инфо о классе List     //
        //       system.collections.generic.list`                          //
        //   Да, помним, что мы задали игнорирование для регистра символов //
        /////////////////////////////////////////////////////////////////////


        /////////after reading:System.String.Comparison,DynamicLinking/////////////////////////////////////////////////////////////////////////
        //
            void DisplayMethodsList(Type type)
            {
                Console.WriteLine("Methods list:");
                foreach (MethodInfo current in type.GetMethods())  // .. in type.GetMethods() - как мы помним, чтобы объект мог обходиться в
                {                                                  //   foreach, он должен реализовать интерфейс IEnumerable
                    Console.WriteLine("\t{0}", current);           // Напомню, что если тебе надо пройтись по коллекции с чуть более сложной
                }                                                  //   выборкой, ты можешь воспользоваться запросами LINQ. Вот как здесь
                Console.WriteLine();                               //   могла бы применятся технология Linq to Object:
            }                                                      //       var methods = from type.GetMethods() select n;
            void DisplayFieldsList(Type type)                      // На самом деле ты мог добится того же результата, отдельно беря имя
            {                                                      //   каждого метода, затем его возвращаемый тип и параметры (так изначально
                Console.WriteLine("Fields list:");                 //   и сделал автор), но нам повезло - то же делает их метод ToString()
                foreach (FieldInfo current in type.GetFields())  // type.Get..() - заметь, что все эти методы выдают уже отсортированный
                {                                                //   массив (он сразу строился с учётом порядка или это энумератор
                    Console.WriteLine("\t{0}", current);         //   выдаёт значения по порядку)
                }
                Console.WriteLine();
            }
            void DisplayPropertiesList(Type type)
            {
                Console.WriteLine("Properties list:");
                foreach (PropertyInfo current in type.GetProperties())
                {
                    Console.WriteLine("\t{0}", current.Name);  // current.Name - да, здесь я решил вывести только имя свойства
                }
                Console.WriteLine();
            }
            void DisplayInterfacesList(Type type)
            {
                Console.WriteLine("Interfaces list:");
                foreach (Type current in type.GetInterfaces())  // type.GetInterfaces() - здесь интересно то, что этот метод выдаёт массив
                {                                               //   объектов System.Type! На самом деле логично, т.к. интерфейсы - это тоже
                    Console.WriteLine("\t{0}", current);        //   типы (хоть и в особой форме)
                }                                               // Многие Get..s() методы перегружены так, чтобы получать параметром значение
            }                                                   //   перечисления BindingFlags, с помощью которого можно отфильтровывать
            void DisplayVariousDetails(Type type)               //   определённые типы (только открытые члены, только статические, только
            {                                                   //   закрытые, только те типы, объекты которых не могуть быть созданы и т.д.
                Console.WriteLine("Various other details:");    //   Заметь, что многие значения этого перечисления созданы для других целей)
                                                                                       // DisplayVariousDetails() - в конце просто будет
                Console.WriteLine("\tBase class is         {0}", type.BaseType);       //   выведена некоторая общая сводка о типе
                Console.WriteLine("\tIt is a abstract    : {0}", type.IsAbstract);
                Console.WriteLine("\tIt is a sealed      : {0}", type.IsSealed);
                Console.WriteLine("\tIt is a generic type: {0}", type.IsGenericType);
                Console.WriteLine("\tIt is a class       : {0}\n", type.IsClass);
            }                                                        //
        //                                                           //
        //                                                           //
            string input = string.Empty;                             //
            do                                                       // do {} - здесь мы будем запрашивать у пользователя полностью заданное
            {                                                        //   имя типа, после чего попробуем найти по имени тип в виде объекта Type
                Console.WriteLine("Enter a type name to evaluate");  //   статическим методом System.Type.GetType() и разошлём этот
                Console.Write("or enter Q to quit: ");               //   объект нашим локальным функциям. И да, автор использовал do-while
                input = Console.ReadLine();                          // input = .. - имена типов из подключённых сборок также можно
                                                                     //   использовать
                if (string.Equals("Q", input, StringComparison.OrdinalIgnoreCase))  // ..Equals("Q", ..) - если будет введено Q, цикл
                {                                                                   //   прекратится
                    break;                                                          // System.StringComparison - не стоит забывать про это
                }                                                                   //   маленькое, но полезное перечисление
                try                                                                 //
                {                                                                   //
                    Type type = Type.GetType(input, true, true);
                    DisplayMethodsList(type);
                    DisplayFieldsList(type);
                    DisplayPropertiesList(type);
                    DisplayInterfacesList(type);
                    DisplayVariousDetails(type);
                }
                catch
                {
                    Console.WriteLine("Sorry, can't find this type");
                }
            } while (true);
        //  //
            Console.WriteLine();
        //  //
        //  // Итак, мы создали (как говорит автор, довольно мощный) браузер типов. Главное его ограничение в том, что он не может подвергать
        //  //   рефлексии ничего кроме заранее подключённых сборок (15.2.exe, 14.2.dll и mscorlib.dll). Как убрать это ограничение? Достаточно
        //  //   применить динамическую линковку
        //
        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////after reading:ReflectionForAttributes///////////////////////////////////////////////
            "someobject".GetType().GetCustomAttributes();  // "..".GetType().GetCustomAttribute() -
        //                                                 //   есть такой метод. Подробнее в методе
        //                                                 //   ReflectionForAttributes_..(). На
        //                                                 //   самом деле это 1-н из 5-ти похожих
        //                                                 //   друг на друга методов
        /////////////////////////////////////////////////////////////////////////////////////////////


        // Также у System.Type есть static члены. Вот они:
        //
        Console.WriteLine(Type.Missing);  // System.Type.Missing - static readonly поле типа object. Сейчас эта штука пограмистам ненужна
        //                                //   /////////after reading:COMExtra//////////////////////////////////////////////////////////////
        //                                //   // , но раньше (до .NET 3.5) это поле вручную отправляли болванкой в вызовах COM
        //                                //   // методов/функций, что имели значения по умолчанию. Сейчас эту работу делает компилер
        //                                //   /////////////////////////////////////////////////////////////////////////////////////////////
        //                                //   


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemTypeANDNamespaceSystemReflection()");
    }
}