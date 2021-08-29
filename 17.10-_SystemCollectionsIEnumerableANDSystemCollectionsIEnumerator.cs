/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemCollectionsIEnumerableANDSystemCollectionsIEnumerator
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsIEnumerableANDSystemCollectionsIEnumerator();

        Console.ReadLine();
    }
    static void SystemCollectionsIEnumerableANDSystemCollectionsIEnumerator()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsIEnumerableANDSystemCollectionsIEnumerator()\n");


        // Интерфейс System.Collections.IEnumerable примечателен тем, что его использует оператор foreach в своей работе, а это значит, что
        //   если ты хочешь работоспособности foreach (в C++ также часто использовались итераторы для прохода по контейнеру)
        //
        //       public interface IEnumerable
        //       {
        //           System.Collections.IEnumerator GetEnumerator();  // GetEnumerator() - единственный требующийся метод. Заметь, что
        //       }                                                    //   возращает он ещё один интерфейс - IEnumerator (о нём ниже)
        //
        //       public interface IEnumerator                         // IEnumerator - как мы знаем, итератор - это объект, который может
        //       {                                                    //    предоставлять объекты из хранилищ вроде массивов
        //           bool MoveNext();                                 // MoveNext() - двигает вперёд внутренний курсор. Возвращает true, если
        //                                                            //   курсор после сдвига всё-ещё указывает на объект в коллеции
        //           object Current { get; }                          // Current() - выдаёт элемент под текущим курсором
        //           void Reset();                                    // Reset() - сбрасывает курсор, ставив его на позицию перед первым
        //       }                                                    //   элементом
        //
        // Также есть более безопасные (точнее, менее времязатратные) обобщённые версии этих интерфейсов IEnumerable<T> и IEnumerator<T>
        // Многие (если не все) коллекции реализуют интерфейс IEnumerable (или его обобщённого брата)


        // Ещё есть непойми откуда взявшееся пространство IEnumerable, что сильно мешает, т.к. при использовании using с System.Collections и
        //   записи IEnumerable VS почему-то думает, что я имею ввиду это самое пространство

        
        Garage carLot = new Garage();
        System.Collections.IEnumerator i = carLot.GetEnumerator();  // i - мы можем управлять энумератором вручную
         i.MoveNext();
        Console.WriteLine("{0} is going {1}\n", ((Car)i.Current).Name, ((Car)i.Current).CurrentSpeed);
        //                                                          // i - почему итераторы по умолчанию нацелены не на элемент, а на что-то
        //                                                          //   другое? Скорее всего foreach использует неявно что-то такое:
        //                                                          //      while (i.MoveNext())
        //                                                          //      {
        //                                                          //          ...
        //                                                          //      }
        //                                                          //   Здесь i.MoveNext() выполняется сразу, поэтому нужно, чтобы курсор
        //                                                          //   после такого стоял на первом элементе
        foreach (Car car in carLot)                                 // foreach - использование foreach
        {
            Console.WriteLine("{0} is going {1}", car.Name, car.CurrentSpeed);
        }
        Console.WriteLine();


        Console.Write("Garage is IEnumerable?: ");
        if (carLot is System.Collections.IEnumerable)  // is - хоть класс и реализует все нужные методы для IEnumerable, явно он не
            Console.WriteLine("true!\n");              //   поддерживает этот интерфейс
        else
            Console.WriteLine("false\n");


        GarageY carLotY = new GarageY();
        System.Collections.IEnumerator curr = carLotY.GetEnumerator();
        curr.MoveNext();                               // curr - при создании указывает не на экземпляр Car, а на что-то другое (если формально
        for (int _ = 0; _ < 4; _++)                    //   , то на элемент, что стоит перед первым. Но там ничего нет!)
        {
            Console.WriteLine("{0} is going {1}", ((Car)curr.Current).Name, ((Car)curr.Current).CurrentSpeed);
            curr.MoveNext();
        }
        Console.WriteLine("{0} is going {1}", ((Car)curr.Current).Name, ((Car)curr.Current).CurrentSpeed);
        curr.MoveNext();                                                                                    // MoveNext() - здесь мы уже
        Console.WriteLine("{0} is going {1}", ((Car)curr.Current).Name, ((Car)curr.Current).CurrentSpeed);  //   не увидим показатель i,
        Console.WriteLine();                                                                                //   т.к. метод уже закончен


        foreach (Car car in carLotY)  // foreach - заметь, что для foreach нужен метод с именем GetEnumerator(). Также foreach знает
        {                             //   когда надо остановится
            Console.WriteLine("{0} is going {1}", car.Name, car.CurrentSpeed);
        }
        Console.WriteLine();
        
        GarageY carLotN = new GarageY();
        foreach (Car car in carLotN)                                            // foreach - просто ждёт, что в объекте carLotN будет метод
        {                                                                       //   GetEnumerator()
            Console.WriteLine("{0} is going {1}", car.Name, car.CurrentSpeed);  //
        }                                                                       //
        Console.WriteLine();                                                    //
                                                                                //
        foreach (Car car in carLotN.GetCustomEnumerable(true))                  // GetCustomEnumerable() - но нам не обязательно
        {                                                                       //   использовать сам объект в качестве типа, способного
            Console.WriteLine("{0} is going {1}", car.Name, car.CurrentSpeed);  //   выдать IEnumerator. Мы можем заменить его сразу объектом
        }                                                                       //   итератора, что выдаст нам этот метод. Такие методы
        Console.WriteLine();                                                    //   называются именованными итераторами
        //
        System.Collections.IEnumerable test = carLotN.GetCustomEnumerable(true);
        Console.WriteLine("test: ...{0}...", test.GetType());
        System.Collections.IEnumerator iter = test.GetEnumerator();
        iter.MoveNext();
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsIEnumerableANDSystemCollectionsIEnumerator()");
    }
    class Car
    {
        public string Name { get; set; }
        public int CurrentSpeed { get; set; }
        public Car() { }
        public Car(string name, int currentSpeed)
        {
            Name = name;
            CurrentSpeed = currentSpeed;
        }
    }
    class Garage
    {
        private Car[] carArray = new Car[4];
        public Garage()
        {
            carArray[0] = new Car("FeeFee", 200);
            carArray[1] = new Car("Clunker", 90);
            carArray[2] = new Car("Zippy", 30);
            carArray[3] = new Car("Fred", 30);
        }
        public System.Collections.IEnumerator GetEnumerator() => carArray.GetEnumerator();  // carArray - конечно можно было сделать и
        //IEnumerator GetEnumerator()           // Конечно, если ты хочешь скрыть этот      //   полную поддержку IEnumerable, но здесь
        //{ return carArray.GetEnumerator(); }  //   метод от пользователя, то тебе следует //   мы решили схитрить и выдавать энумератор
        //                                      //   реализовать интерфейс явно. Так (без   //   из массива carArray за свой
        //                                      //   явного приведения) доступа к методу не // Как видишь, даже не обязательно писать
        //                                      //   будет, при этом foreach будет работать //   IEnumerable в объявлении класса
    }
    class GarageY
    {
        private Car[] carArray = new Car[4];
        public GarageY()
        {
            carArray[0] = new Car("FeeFee", 200);
            carArray[1] = new Car("Clunker", 90);
            carArray[2] = new Car("Zippy", 30);
            carArray[3] = new Car("Fred", 30);
        }
        public System.Collections.IEnumerator GetEnumerator()    // GetEnumerator() - если в методе применяется yield, то такой метод
        {               // Итераторные методы - это альтернатива //   становится итераторным. Итераторные методы сильно отличаются от
            int i = 0;  //   громоздкого интерфейса IEnumerable  //   обычных - они не выполняются, пока объект, принявший его итератор,
            Console.WriteLine("Hello");                          //   не воспользуется его методами (MoveNext() и Reset())
            yield return carArray[0];           // yield return - это возращает свойство Current
            Console.WriteLine("i0: {0}", i++);  // При вызове MoveNext() выполняется код ровно до следующего yield return. При первом
            yield return carArray[1];           //   вызове MoveNext() код выполняется прямо с начала метода до первого yield
            Console.WriteLine("i1: {0}", i++);  // [..] - конечно, лучше бы было использовать foreach, чтобы не трогать метод при
            yield return carArray[2];           //   расширении массива, но зато здесь всё наглядно
            Console.WriteLine("i2: {0}", i++);
            yield return carArray[3];           // yield return - если функция прошла последний yield, то при последующем вызове она
            Console.WriteLine("i3: {0}", i++);  //   пройдёт дальше (здесь выведится i3), и снова вёрнётся carArra[3]. А при дальнейших
        }                                       //   запусках уже просто будет возвращатся последний yield return, не затрагивая i3
        //  // Чуть позже я узнал, что yield разворачивается компилятором в скрытый класс, который и будет служить итератором. В первых
        //  //   версиях C# для реализации этого интерфейса требовалось под каждый класс писать полноценный итератор с свойством Current,
        //  //   методом MoveNext() и прочей рутиной. Конечно, это многих раздражало, поэтому решили ввести yield. Зачем вообще нужно
        //  //   выдавать отдельный объект? Скорее всего для стандартизации - т.е. чтобы итераторы разных типов управлялись одинаково. И ещё,
        //  //   вроде как, это как-то уберигает от проблем при многопотоке, когда к одному элементу обращаются несколько потоков
        public System.Collections.IEnumerator GetEnumeratorMode()            // GetEnumeratorMode() - если ты хочешь что-то выполнить
        {                                                                    //   при получении интератора, можешь сделать так
            Console.WriteLine("New iterator for Garage has been created!");
            return GetIt();
            System.Collections.IEnumerator GetIt()  // GetIt() - это пример одного из двух главных случаев применения локальных функций
            {                                       /////////after reading/////////////////////////////////////////////////////////////////////
                foreach (Car car in carArray)       //   (второй случай - это работа с методами async)
                    yield return car;               ///////////////////////////////////////////////////////////////////////////////////////////
            }
        }


        public System.Collections.IEnumerable GetCustomEnumerable(bool reverse)  // GetCustomEnumerable() - эта называется именнованный
        {                                                                        //   итератор. Возвращает он IEnumerable и также не выполняет
            if (reverse)                                                         //   свой код, пока не задействуется итератор объекта,
            {                                                                    //   которого он возвращает
                for (int i = carArray.Length - 1; i >= 0; i--)
                    yield return carArray[i];  // yield - требует, чтобы метод был или IEnumerator, или IEnumerable, т.к. только их можно
            }                                  //   сделать итераторными
            else
            {
                for (int i = 0; i < carArray.Length; i++)
                    yield return carArray[i];
            }
        }
    }
}