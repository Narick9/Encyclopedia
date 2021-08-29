/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _DelegatesANDAnonymousFunctionANDLambda_Events
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        DelegatesANDAnonymousFunctionANDLambda_Silent();
        Events_Silent();

        Console.ReadLine();
    }
    static void DelegatesANDAnonymousFunctionANDLambda_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   DelegatesANDAnonymousFunctionANDLambda_Silent()\n");


        // Делегат - это уже чисто .NET-овская штука. По своей сути, это безопасный к типам (т.е. в отношении к типизации) эквивалент
        //   указателям на функции из Си. Основная разница в том, что делегаты .NET - это классы (ООП от самых корней, как-никак)
        // Делегаты критически важны, ведь с помощью них ты можешь отправлять в методы не простые данные, а целый код (в Си такой аргумент
        //   звался агрегатом)
        //   /////////after reading///////////////////////////////////////////////////////////////////////
        //   // Делегаты формируют архитектуру событий в .NET
        //   // А ещё через объект делегата можно легко вызвать хранящийся внутри него метод в асинхронной манере
        //   /////////////////////////////////////////////////////////////////////////////////////////////
        // В отличие от Си (где указатели на функции строились прямо в коде) в .NET тип делегата с нужным прототипом требуется сначала объявить
        //   (ведь это класс). Ты можешь посмотреть на то, как это выглядит ниже


        int Add(int left, int right)        // Add() - здесь используется функция, но годятся и методы (на самом деле это одно и то же)
        {
            return left + right;
        }
        BinaryInt my = new BinaryInt(Add);  // BinaryInt - этот делегат может хранить методы только с таким .. типом: int (int,int) (на самом
                                            //   деле это называется сигнатурой, и ты это уже видел). Заметь, именно эту сигнатуру требует
                                            //   конструктор делегата (если верить VS)
                                            // new ctor() - не стоит забывать, что делегаты в C# - это те же классы, и нам следует оперировать
                                            //   с их объектами
        Console.WriteLine("10 + 52 is {0}\n", my(10, 52));
                                            // my - этот делегат дальше можно использовать как функцию (в действительности компилятор подменит
                                            //   my(..) на my.Invoke(..). Это видно в CIL-коде. И да, вызывать my.Infoke() можно вручную)


        void DelegateInfoDisplay(MulticastDelegate delObj)                 // DelegateInfoDisplay() - просто выводит некоторую информацию
        {                                                                  //   о функциях, что хранятся в делегате (а именно - имена
            foreach (Delegate curr in delObj.GetInvocationList())          //   функций и тип объектов, откуда их можно вызвать)
            {
                Console.WriteLine("Method name: {0}", curr.Method);        // Console.WriteLine(curr) - ToString() объектов MethodInfo
                Console.WriteLine("Type of the method: {0}",               //   выводит полный прототип функций
                                                curr.Target ?? "NoType");  // curr.Target - т.к. my хранит указатель на функцию (а не
            }                                                              //   метод), то это свойство выдаст нам null
        }                                                                  // .. ?? .. - напомню, что есть операция объединения с null
                                                                           //   (гл.4)
        DelegateInfoDisplay(my);                    // my - на самом деле указывает только на одну фукнцию - значит, foreach в
        Console.WriteLine();                        //   DelegateInfoDisplay() пройдёт только один раз


        void SetInt1(int _)
        {
        }
        JustReceiver<int> receiver1 = new JustReceiver<int>(SetInt1);  // JustReceiver<int> - делегаты легко обобщаются (комментарии к
                                                                       //   объявлению JustReceiver также имеются)


        void SetInt2(int _)
        {
            Console.Write("SetInt2!");
        }
        JustReceiver<int> receiver2 = new JustReceiver<int>(SetInt2);
        receiver1 += receiver2;  // += - так мы можем добавить функции из receiver2 в наш делегат (    .. += receiver2
                                 //   развёртывается в    .. = .. + receiver2    , и я готов спорить, что оператор + вызывает метод
                                 //   Delegate.Combine(). В этоге мы получаем    .. = Delegate.Combine(.., receiver2)    ). Ещё
                                 //   заметь, что оператор + у нашего JustReceiver<> работает только с объектами этого же JustReceiver<>
                                 //   (это значит, что оператор + генерируется компилятором в объявлении конкрентных делегатов)
                                 //
        receiver1 += SetInt2;    // += SetInt2 - мы отправляем не экземпляр делегата, а сразу функцию. Это называется "групповым
                                 //   преобразованием метода", и это сделано для упрощения (теперь во внутреннем списке receiver1 есть 2-а
                                 //   указателя на одну и ту же фукнцию)
                                 //
        receiver1 -= SetInt2;    // -= - а так мы можем удалить из внутренней очереди какие-то функции (и я уверен, что здесь самешан
                                 //   Delegate.Remove())(теперь мы снова имеем лишь 1-н указатель на SetInt2()). Здесь JustReceiver<> также
                                 //   не создавался явно


        JustReceiver<int> receiver3 = null;
        receiver3 += receiver1;                                                 // receiver3 += .. - хоть наш объект и равен null'у,
                                                                                //   операторы работают с ним
        receiver3 = (JustReceiver<int>)Delegate.Combine(null, receiver2);       // Delegate.Combine() - эта статическая фукнция также
                                                                                //   может работать с null.. . Намёков слишком много



        // Стоит сказать, что MulticastDelegate'ы вызывают свои методы в порядке внутренней очереди (по принципу FIFO, first-in, first-out)


        // И ещё - если объект MulticasDelegate хранит в себе методы, что возвращают что-то (non-void), то при вызове их ты получишь вывод
        //   только последнего из них. Выводы остальных будут отброшены
        /////////after reading///////////////////////////////////////////////////////////////////////
        //   . Во всяком случае так работает синхронный вызов
        /////////////////////////////////////////////////////////////////////////////////////////////
        // В большинстве случаев подобные делегаты не хранят методы с non-void возвращаемым типом, поэтому этой тонкости не возникает


        // Action, Func - если тебе не важно само имя делегата, то, чтобы не плодить кучу своих, можно
        //   воспользоваться этими определёнными в пространстве System делегатами
        void DisplayMessage(string msg, ConsoleColor textColor)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
            Console.WriteLine(msg);
            Console.ForegroundColor = previous;
        }
        Action<string, ConsoleColor> myAction = new Action<string, ConsoleColor>(DisplayMessage);
        myAction("Hello!", ConsoleColor.Green);                              // System.Action<..> - этот делегат создан для хранения функций,
        Console.WriteLine();                                                 //   возвращающей void и хранящей аж до 16 параметров
                                                                             //   (System.Action<> перегружен для этого)
        int Add2(short left, short right)                                    //
        {
            return left + right;
        }
        Func<short, short, int> myFunc = new Func<short, short, int>(Add2);  // System.Func<..> - а этот делегат уже может возращать какое-то
        Console.WriteLine("5 + 4 is {0}\n", myFunc(5, 4));                   //   значение. В остальном он такой же (но перегружен на 17
                                                                             //   версий). Возращаемый тип всегда указывается последеним в
                                                                             //   параметрах типов (почему? ****не знаю)


        void CarIsDeathEvent(string msg)
        {
            Console.WriteLine("Message from Car instance --> {0}", msg);
        }
        Car myCar = new Car("SlugBug", 10, 100);
        myCar.SetCarEngineHandler(new Car.CarEngineHandler(CarIsDeathEvent));
        for (int _ = 0; _ < 5; _++)                    // new Car.CarEngineHandler - т.к. метод myCar.SetCarEng..() тербует уже
        {                                              //   готовый делегат, мы его создаём прямо внутри, не забыв
            myCar.Accelerate(40);                      //   задать ему нужную нам функцию, которую myCar будет вызвать (ну и да,
        }                                              //   мы могли воспользоваться здесь групповым преобразованием метода)
        Console.WriteLine();                           // myCar.Asselerate() - ускоряем нашу машину. Т.к. мы делаем это слишком
        //                                             //   рьяно, в итоге машина как-бы ломается, и этот метод вызовет
        //                                             //   отправленную нами функцию из своего внутреннего делегата
        //
        void CarIsDeathEvent2(string msg)              // CarIsDeathEvent2() - эта функция чисто для демонстрации
        {
            Console.WriteLine("-- Also message from car instance --> {0}", msg.ToUpper());
        }
        Car myCar2 = new Car("SlugBug", 100, 200);
        myCar2.AddCarEngineHandler(CarIsDeathEvent);   // AddCarEngineHandler() - даже если мы не присваивали для listOfHandlers что-то,
        myCar2.AddCarEngineHandler(CarIsDeathEvent2);  //   Delegate.Combine() выдаст ему значение второго аргумента (т.е. CarIsDeathEvent)
        for (int _ = 0; _ < 4; _++)                    //   (спасибо методу Delegate.Combine(), возможно)
        {
            myCar2.Accelerate(50);                     // myCar2.Accelerate() - ускоривши машину слишком сильно, метод вызовет внутренний
        }                                              //   делегат из myCar2, и вызовутся наши 2-е функции
        Console.WriteLine();
        //
        myCar2.RemoveCarEngineHandler(new Car.CarEngineHandler(CarIsDeathEvent2));
        myCar2.Accelerate(10);                         // RemoveCarEngineHandler() - туда мы посылаем делегат
        Console.WriteLine();                           //   с функцией, подписку на которую мы хотим убрать
                                                       // myCar2.Accelerate() - как мы видим, так и вышло


        Action myAction1 = delegate                            // delegate {} - это называется анонимная функция (т.к. у неё нет имени).
        {                                                      //   С её помощью тебе необязательно создавать целый отдельный метод, если
            Console.WriteLine("Eek! We are going too fast!");  //   его ты используешь только раз. Интересно, что анонимные функции - это
        };                                                     //   полноценная часть языка, и они отражаются в CIL-коде по своему
        Action<int> myAction2 = default;
        myAction2 += delegate (int num)                                                            // delegate (..) - как обычная функция,
        {                                                                                          //   но без имени
            Console.WriteLine("Anon method 2 have gotten {0}", num);                               // Применятся они могут только при
        };                     // Анонимные методы ведут себя как локальные функции, поэтому       //   работе с делегатами
        myAction2(30);         //   им не доступны ref и out параметры методов, в которых они      // Как ты видишь, если параметров нет,
        Console.WriteLine();   //   определены, они не могут объявлять новые переменные, имена     //   то необязательно ставить пустые
                               //   которых уже используются в их области видимости, но зато       //   круглые скобки (но можно)
                               //   также, как и другие методы (включая локальные функции),        // И ещё - с анонимными функциями не
                               //   могут объявлять переменные с именем, что заняты полями класса  //   работает =>


        Predicate<int> greaterThan5 = delegate (int num) { return num > 5; };  // System.Predicate<T> - этот обобщённый делегат создан специально
        // Console.WriteLine(greater5(3));                                 //   для ситуаций, когда нужно определить, подходит ли данное что-то
        //   Output: false                                                 //   . Если да, то возвращается true


        Predicate<int> isEven1 = num => num % 2 == 0;                       // .. => .. - это лямбда-выражение. Оно создано для упращения
        // Console.WriteLine(isEven(9));                                    //   уже анонимных методов (и встречается во многих языках).
        //   Output: false                 // На самом деле компилятор      //   То, что слева от => - это параметры, а то, что справа -
        //                                 //   разворачивает их в простые  //   само тело. Мы не задали тип для number, поэтому
        //                                 //   анонимные методы            //   компилятор сам попытается вывести его из делегата, в
        //                                 // => Да, здесь применяется      //   который мы это посылаем, и самого тела функции. Это весьма
        //                                 //   синтаксис членов, сжатых до //   удобно, но мы могли и указать это явно:
        //                                 //   выражения (т.е. =>)         //       ..(int num) => number % 2 == 0;
        //                                                                  //   (здесь нужна пара круглых скобок. На самом деле скобки - это
        //                                                                  //   часть полной формы лямбда-выражения)
        //                                                                  // Заметь, с лямбда-выражениями также работает групповое
        //                                                                  //   преобразование (понятно, ведь для компилятора они - анонимные
        //                                                                  //   методы)
        //
        Predicate<int> isEven2 = (int num) =>                               // .. => {..} - если тебе нужно больше одной команды в
        {                                                                   //   лямбда-выражении, то используй фигурные скобки (всё-ещё меньше
            Console.WriteLine("num is {0}", num);                           //   работы, чем с анонимными методами)
            bool isEven = num % 2 == 0;
            return isEven;
        };
        //
        Func<string> getHello = () => "Hello!";                             // () => - если параметров у тебя нет, то просто оставляй круглые
        Console.WriteLine("And it says ... {0}\n", getHello());             //   скобки
        //
        Action<int, int, string> add = new Action<int, int, string>((a, b, msg) =>
            Console.WriteLine("{0} + {1} is {2}, and {3}", a, b, a + b, msg) );
        add(3, 4, "be happy!");                                             // (a, b, msg) - для нескольких параметров тоже нужно
        Console.WriteLine();                                                //   добавить скобки
                                                                            // Если задашь тип явного для кого-то из них, то ты должен задать и
                                                                            //   для других


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   DelegatesANDAnonymousFunctionANDLambda_Silent()");
    }
    delegate int BinaryInt(int l, int r);  // delegate - в Си была очень полезная возможность иметь указатели на функции. Как говорит автор
    // Модификаторы параметров (это ref,   //   , это позволило создать привычный нам интерфейс, где мышь может двигаться и нажимать на
    //   out и params) также хорошо        //   кнопки (видимо, функция мыши принимала функцию кнопки в качестве параметра и запускала её,
    //   работают с делегатами             //   когда нажималась кнопка). В .NET для этого ввели делегаты - фактически классы, способные
    // Да, эта информация нужна чисто для  //   хранить список указателей на совместимые методы и функции
    //   справки (и, может, собеседывания) /////////after reading///////////////////////////////////////////////////////////////////////
                                           //   и запускать их синхронно и асинхронно
                                           /////////////////////////////////////////////////////////////////////////////////////////////
                                           //   . Совместимы те методы, что повторяют прототип делегата (кроме имени)(прототип функции без
                                           //   на самом деле называется "сигнатура")
                                           // Когда компилятор обрабатывает тип делегата, он создаёт запечатанный (sealed) класс,
                                           //   производный от System.MulticastDelegate (который сам произведён от System.Delegate). Этот
                                           //   класс (со своим родителем) предоставляет нужную инфраструктуру для нового делегата (ещё
                                           //   его можно увидеть в меню ildasm.exe)
                                           // В новоиспечённом классе окажутся три метода, и, вероятно, главным из них является Invoke(),
                                           //   вызывающий каждый из методов по очереди
                                           /////////after reading///////////////////////////////////////////////////////////////////////
                                           //   (т.е. синхронно)
                                           /////////////////////////////////////////////////////////////////////////////////////////////
                                           //   . Методы BeginInvoke() и EndInvoke() для вызова хранящихся методов используют дополнительных
                                           //   поток
                                           /////////after reading///////////////////////////////////////////////////////////////////////
                                           //   (т.е. BeginInvoke() и EndInvoke() служат для асинхронного вызова)
                                           /////////////////////////////////////////////////////////////////////////////////////////////
    //sealed class BinaryInt_ : System.MulticastDelegate  // BinaryInt_ - примерно то, что сгенерирует компилятор при виде нашего BinaryInt
    //{
    //    public int Invoke(int l, int r);                // int Invoke(int l, int r) - метод полностью повторяет опредиление делегата
    //    public IAsyncResult BeginInvoke(int l, int r,   // BeginInvoke(int l, int r..) - начальные параметры этого метода также основаны
    //                AsyncCallback cb, object state);    //   на нём. Дальше идёт два финальных параметра - cb и state
    //    public int EndInvoke(IAsyncResult result);      /////////after reading///////////////////////////////////////////////////////////////
    //}                                                   //   , что используются для облегчения ассинхронного вызова методов
    //                                                    /////////////////////////////////////////////////////////////////////////////////////
    //                                                    // int EndInvoke() - а этот метод повторяет возвращаемый тип нового делегата.
    //                                                    //   Интересно, но параметры с модификаторами здесь также будут отражаться:
    //                                                    //       EndInvoke(ref int l, out int r, IAsyncResult result);
    //public abstract class MulticastDelegate : Delegate                               // MulticastDelegate - здесь это часть класса с
    //{                                                                                //   избранными методами. Заметь, что класс абстрактный!
    //    public sealed override Delegate[] GetInvokationList();                       // GetInvokationList() - возвращает массив методов,
    //    public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2);  //   по делегату на указатель метода
    //    public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2);  // operator ==/!= - некоторые статические операторы
    //    private IntPtr _invocationCount;                                             // _invocationCount/_invocationList - внутренние
    //    private object _invocationList;                                              //   вспомогательные поля
    //}
    //public abstract class Delegate : ICloneable, ISerializable              // Delegate - прародитель всех делегатов. Это его фрагмент.
    //{                                                                       //   Собственно, этот тип элементов и хранит указатель на
    //    public static Delegate Combine(params Delegate[] delegates);        //   функцию. MulticastDelegate - это Delegate, но с
    //    public static Delegate Combine(Delegate a, Delegate b);             //   способный хранить больше одного указателя
    //    public static Delegate Remove(Delegate source, Delegate value);     // Combine(..)/Remove..() - методы для работы со списком
    //    public static Delegate RemoveAll(Delegate source, Delegate value);  //   указателей. Функция Combine() здесь может принимать
    //    public static bool operator ==(Delegate d1, Delegate d2);           //   группу делегатов и возвращать новый делегат со списком,
    //    public static bool operator !=(Delegate d1, Delegate d2);           //   хранящим функции из первого и второго делегата. Как Delegate
    //    public object Target { get; }                                       //   , способный хранить только одну ссылку на метод, здесь на
    //                                                                        //   выходе имеет больше одного? Всё-ещё никак, но ты забыл одну
    //                                                                        //   важную вещь - класс Delegate абстрактен, и операции на деле
    //                                                                        //   ведутся с потомками MulticastDelegate (но это не точно).
    //                                                                        //   Remove() также возвращает новый делегат, но со списком
    //                                                                        //   первого делегата без фукнций из списков последующих
    //                                                                        // Target - это свойство выдаст тебе ссылку на объект, откуда ты
    //                                                                        //   сможешь вызвать текущую функцию твоего Delegate (или null,
    //                                                                        //   если она вызывается без объекта)(текущая функция для
    //                                                                        //   MulticastDelegate - это та это та, что добавлена последней)
    /////////after reading///////////////////////////////////////////////////////////////////////
    //    public MethodInfo Method { get; }                                   // Method - свойство для доступа к инфо о текущей функции
    //                                                                        //   делегата. Method возвращает объект
    //                                                                        //   System.Reflection.MethodInfo, хранящий детали этой функции
    /////////////////////////////////////////////////////////////////////////////////////////////
    //}
    delegate void JustReceiver<T>(T arg);  // <T> - делегаты также легко обобщаются. Например, этот может хранить функции, возвращающие void
                                           //   , и принимающие один аргумент любого типа
                                           // arg - заметь, что в C# нельзя оставлять параметры без имён, как это делалось Cи
    class Car                                                        // Car - вот как выглядит стандартный класс с делегатом (во всяком случае,
    {                                                                //   такой пример показал Троелсен)
        public delegate void CarEngineHandler(string msgForCaller);  // CarEngineHandler - мы объявили внутренний для Car делегат, чтобы
                                                                     //   подчеркунть, что он расчитан на работу именно здесь. И всё-же мы
                                                                     //   дали ему модификатор доступа public, т.к. его должны знать извне
                                                                     //   (как минимум для вызова местных методов)
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }
        private bool carIsDead = false;
        CarEngineHandler listOfHandlers;                             // listOfHandlers - здесь было решено сделать делегат закрытым, разрешив
                                                                     //   заполнять его через специальные методы (хотя использование свойства
                                                                     //   было бы менее громоздко)
        public Car()
        {
        }
        public Car(string petName, int currentSpeed, int maxSpeed)
        {
            PetName = petName;
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;
        }
        public void SetCarEngineHandler(CarEngineHandler handler)
        { listOfHandlers = handler; }
        public void AddCarEngineHandler(CarEngineHandler handler)
        { listOfHandlers += handler; }
        public void RemoveCarEngineHandler(CarEngineHandler handler)
        { listOfHandlers -= handler; }
        public void Accelerate(int delta)                            // Accelerate() - это метод для ускарения. Если машина привысит скорость,
        {                                                            //   мы провзоним всех слушателей в listOfHandlers, и будем отказывать
            if (carIsDead)                                           //   в дальнейших ускарениях
            {
                listOfHandlers?.Invoke("Sorry, this car is dead..");
                return;
            }
            CurrentSpeed += delta;
            if (MaxSpeed - CurrentSpeed <= 10 && listOfHandlers != null)
            {
                listOfHandlers("Carefule buddy! It gonna blow!");
            }
            if (CurrentSpeed > MaxSpeed)
                carIsDead = true;
            else
                Console.WriteLine("Current speed is {0}", CurrentSpeed);
        }
    }
    static void Events_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   Events_Silent()\n");


        void CarAboutToBlowHandler(string msg)
        {
            Console.WriteLine(msg);
        }
        void CarExplodedHandler(string msg)
        {
            Console.WriteLine("Critical message from Car: {0}", msg);
        }
        void CarExplodedHandler2(string msg)
        {
            Console.WriteLine("I told you!");
        }
        Car_ForEvent myBetty = new Car_ForEvent("Betty", 40, 90);
        myBetty.AboutToExplode += new Car_ForEvent.CarEngineHandler(CarAboutToBlowHandler);  // AboutToExplode - мы видим наш event. От
        myBetty.Exploded += new Car_ForEvent.CarEngineHandler(CarExplodedHandler);           //   всех других полей он отличается тем, что
        myBetty.Exploded += new Car_ForEvent.CarEngineHandler(CarExplodedHandler2);          //   не является объектом, и ведёт себя иначе
        for (int _ = 0; _ < 3; _++)  // new Car_Fore...(CarExplodedHandler) - на самом деле  // += - из доступного у нас имеются удобные
        {                            //   здесь также можно применить групповое              //   операторы += и -= дла добавления и удаления
            myBetty.Accelerate(30);  //   преобразование методов:                            //   новых функций в список обработчика (именно
        }                            //       .. += CarExplodedHandler2    , но ты не        //   эти операторы, операторов + и - из у event'ов
        Console.WriteLine();         //   сможешь использовать его, если нужно добавить или  //   нет). В случае += за кулисами вызывается
                                     //   убрать сразу несколько методов                     //   сгенерированный метод add_..(), а при -= -
                                     //                                                      //   remove_..()


        // VS - при записи += для регистрации события выходит окно от IntelliSense с предложением
        //   нажать Tab. Если это сделать, то по соседству автоматически сгенерируется private
        //   static макет функции, подходящий для этого делегата этого event'а. Ниже ты можешь
        //   увидеть такую фукнцию. Клацая по Tab, ты можешь переходить между объявлением этой самой
        //   функции и объектом, к которому ты ранее приписал +=. Имя новоиспечённой функции
        //   будет подсвечено, а изменения в нём отражаются одновременно в обоих местах
        //
        myBetty.Exploded += MyBetty_Exploded;
        //
        // Если имя MyBetty_Exploded уже будет занято, VS просто припишет сзади номер:    MyBetty_Exploded1
        //
        // Это может помочь в будущем


        //****sender - просто ссылка экземпляр, откуда event был вызван, который вызвал этот метод


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   Events_Silent()");
    }
    class Car_ForEvent : Car
    {
        public delegate void CarEngineHandler(string msg);
        //                                             // CarEgineHandler - ****странный warning
        private bool carIsDead = false;
        public event CarEngineHandler AboutToExplode;  // event - чтобы не писать для каждого делегата по набору методов для поддержания
        public event CarEngineHandler Exploded;        //   инкапсуляции, придумали такую вещь как событие. При встрече ключевого слова
                                                       //   event компилятор автоматически генерирует делегат (как видно, его мы и определяем
                                                       //   , но с добавкой event впереди) и два метода: add_.. (.. - это имя объекта
                                                       //   делегата)(вызывает Delegate.Combine()) и remove_.. (использует Delegate.Remove())
        public Car_ForEvent(string petName, int currentSpeed, int maxSpeed) : base(petName, currentSpeed, maxSpeed)
        {                                              // ctor - начиная с C# 7 конструкторы могут применять синтаксис членов, сжатых до
        }                                              //   выражения (т.е. =>)
                                                       /////////after reading//////////////////////////////////////////////////////////////////
                                                       //   , и вместе с конструкторами в этой же версии это научились делать и финализаторы
                                                       ////////////////////////////////////////////////////////////////////////////////////////
        public void Accelerate(int delta)
        {
            if (carIsDead)
            {
                Exploded?.Invoke("Sorry, this car has exploded...");
            }

            CurrentSpeed += delta;
            if (CurrentSpeed > MaxSpeed)
            {
                carIsDead = true;
                Exploded("Sorry, this car has exploded...");
            }
            else if (MaxSpeed - CurrentSpeed == 10)
            {
                AboutToExplode("Careful buddy! Gonna blow!");
            }
            else
                Console.WriteLine("{0} is going {1}", PetName, CurrentSpeed);
        }
    }
    private static void MyBetty_Exploded(string msg)  // MyBetty_Exploded() - автосгенерированная функция-обработчик для события, при
    {                                                 //   котором нажали Tab
        throw new NotImplementedException();
    }
}