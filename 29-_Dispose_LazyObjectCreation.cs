/*
 * creation date  25 apr 2021
 * last change    28 jun 2021
 * author         artur
 */
using System;
using System.IO;

class _Dispose_LazyObjectCreation
{
    static void Main()
    {
        Console.WriteLine("**** _ ******");

        Dispose();
        LazyObjectCreation();

        Console.ReadLine();
    }
    static void Dispose()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   Dispose()\n");


        // Как было написано, Finalize() создан для освобождения неуправляемых ресурсов объекта перед его уничтожением. Минус здесь в
        //   том, для его запуска придётся ждать сборщика мусора (или запускать сборщик вручную, но это всё-таки дорого. Выборочно запустить
        //   GC для одного объекта не получится). Некоторые неуправляемые объекты, т.н. "ценные элементы" (дескрипторов файлов, подключений к
        //   базам данных и другие очень дорогие штучки), часто полезно освобождать как можно раньше. Для этого класс может реализовать
        //   интерфейс System.IDisposable, требующий один единственный метод Dispose():
        //       public interface IDisposable
        //       {
        //           void Dispose();
        //       }
        //   Суть в том, что этот Dispose() ты будешь вызывать сам, вручную, у нужных объектов (да, это уже Си-стайл)
        // Конечно, не стоит забывать, что ты можешь назначить любой свой метод для освобождения неуправляемых ресурсов объекта, и
        //   вызывать его, но, как и везде в .NET, для стандартизации был введён этот интерфейс
        // Ничто не мешает иметь в одном классе и Finalize() (который деструктор), и Dispose() одновременно. Правда, Dispose(), всё-таки
        //   предпочтительнее, т.к. с ним ты сразу отпускаешь уже ненужный груз


        SomeUnmanagedResourceWrapper1 my = new SomeUnmanagedResourceWrapper1();
        my.Dispose();         // Dispose() - вызывается как и любой другой метод. Понятно, что после уничтожения неуправляемой части
                              //   объекта, его использовать небезопасно. Лучше вообще не использовать
        Console.WriteLine();  // Если у тебя финализатор делает то же, что и Dispose(), то можно не бояться забыть вызвать этот Dispose().
                              //   Память в конечном итоге будет очищена, но это может занять больше времени, чем необходимо


        // Несколько типов в библиотеках базовых классов, реализующие интерфейс IDisposable, имеют метод-псевдоним для Dispose(), в попытке
        //   сделаться чуть естесственнее. В качестве примера можно взять класс System.IO.FileStream, имеющий методы Dispose() и Close(),
        //   предназначенных для одной и той же цели:
        FileStream fs = new FileStream("myFile.txt", FileMode.OpenOrCreate);
        fs.Close();
        fs.Dispose();  // fs.Close() и fs.Dispose() - делают одно и тоже. Это может слегка сбить с толку, но ошибки не вызывает. Конечно,
                       //   метод fs.Close() для закрытия файла выглядит более естественным, но вызов метода fs.Dispose() всегда гарантирует,
                       //   что произойдёт то, чего ты ждёшь (т.е. отвязка файла и других возможных временных ресурсов внутри)


        SomeUnmanagedResourceWrapper1 rw = new SomeUnmanagedResourceWrapper1();
        try
        {
            //...
        }
        finally            // try - при использовании объектов, что требуют специального использования (вроде нашего rw) часто
        {                  //   приходится делать обработку исключений с блоком finally
            rw.Dispose();  // Dispose() - что бы не случилось, в конечном итоге здесь мы вызовем этот метод
        }


        using (SomeUnmanagedResourceWrapper1 rw2 = new SomeUnmanagedResourceWrapper1())
        {          // using - хотя использование try-блока считается хорошим тоном, мало кто хочет городить это для каждого
            //...  //   освобождаемого типа. Разработчики решили повторно использовать ключевое слово using для отделения блока, где будет
        }          //   происходить работа с одним из таких disposable объектов
                   // Если посмотреть на CIL-код этого фрагмента, можно увидеть, что на этом месте находится тот же try-finally блок
                   // Попытка использования using с типом, что не поддерживает IDisposable, вызывает ошибку компиляции (что видна в VS)
                   // Стоить знать о том, что внутри using допускается объявлять несколько объектов одного типа. Компилятор вставит
                   //   вызовы метода Dispose() для каждого объекта:
                   //       using (SomeUnmanagedResourceWrapper rw2 = new SomeUnmanagedResourceWrapper(),
                   //                                           rw3 = new SomeUnmanagedResourceWrapper())
                   //       {
                   //           //...
                   //       }
        Console.WriteLine();


        // А вот в классе MyResourceWrapperFinal соблюдён рекомендованный Microsoft шаблон
        MyResourceWrapperFinal rwf = new MyResourceWrapperFinal();
        rwf.Dispose();  // Dispose() - звук издастся только один
        rwf.Dispose();  //   раз. Это знак того, что логика для     // Dispose()x2 - мы можем вызывать метод освобождения несколько раз,
                        //   обработки случая многократного вызова  //   логика внутри объекта расчитана на такую неосторожность
                        //   этого метода работает                  // После вызова метода Dispose() мы всё ещё можем пользоваться членами
                                                                    //   объекта, т.к. объект всё ещё в памяти. В идеале, в таком классе
                                                                    //   свойства и методы для доступа к этим освобождённым ресурсам тоже
                                                                    //   должны быть снабжены дополнительной логикой для такой ситуации


        //****На этом исследование особенностей объектов при сборке мусора завершено. Хотя дополнительные (и довольно экзотические) детали
        //   (такие как слабые ссылки и восстановление объектов) в книге не рассматривались, полученной информации вполне достаточно,
        //   чтобы продолжить изучение самостоятельно


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   Dispose()");
    }
    class SomeUnmanagedResourceWrapper1 : IDisposable  // IDisposable - требует один единственный метод Dispose(). Типы, что реализуют этот
    {                                                  //   интерфейс, зовутся освобождаемыми (или disposable объектами)
        public void Dispose()                          // public - напомню, что методы по декларации интерфейсов должны быть открытыми
        {                                              //   (хотя в определении интерфейсов у них не должно стоять модификаторов доступа,
            Console.WriteLine("In Dispose()!");        //   по крайней мере до C# 8.0)
        }  // Здесь полезно вызывать методы Dispose()  // Dispose() - здесь освобождаются все неуправляемые ресурсы. В отличие от метода
           //   внутренних освобождаемых объектов      //   Finalize(), из метода Dispose() вполне безопасно обращаться к другим объектам.
                                                       //   Для среды CLR Dispose() - это обычный метод, и при его запуске его объект всё
                                                       //   ещё остаётся жив, как и все другие видимые управляемые объекты


        //public void Dispose()
        //{
        //    System.GC.SuppressFinalize(this);  // ..GC.SuppressFinalize() - т.к. мы уже освободили неупровляемые ресурсы объекта, можно
        //}                                      //   оповестить сборщик мусора о ненужности финализации для этого объекта
    }
    class MyResourceWrapperFinal : IDisposable
    {                                   // MyResourceWrapperFinal - класс выше имеет несколько недостатков:
        private bool disposed = false;  //   > во первых, методы Finalize() и Dispose() должны освобождать одни и те же неупровляемые
                                        //     ресуры. Это приводит к дублированию кода, а эта проблема весьма усложнит сопровождение
                                        //   > во вторых, нужно следить, чтобы метод Finalize() не пытался освободить управляемые объекты
                                        //     (т.к. они могут быть уже освобождены к этапу финализации)
                                        //   > в третьих, нужно позаботится о том, чтобы пользователь мог многократно вызывать метод
                                        //     Dispose() без возникновения ошибки (ведь закрыть один файл несколько раз не получится)
                                        //   Этот класс решает все проблемы, применяя рекомендованный Microsoft шаблон освобождения,
                                        //   соблюдающий баланс между надёжностью, возможностью сопровождения и производительностью. Этот
                                        //   класс - его финальная стадия развития MyResourceWrapper
        ~MyResourceWrapperFinal()
        {
            CleanUp(false);  // CleanUp() - для исключения дублирования вызываем общий для обоих способов
        }                    //   вспомогательный метод для очистки
                             // false - этим мы даём понять вспомогательному методу, что его вызывают из финализации
        public void Dispose()
        {
            CleanUp(true);                     // CleanUp(true) - вызываем этот же метод, но теперь даём понять, что он вызывается методом
            System.GC.SuppressFinalize(this);  //   Dispose()
        }                                      // SuppressFinalize() - также подавляем финализацию, т.к. неуправляемые ресурсы освобождены
        private void CleanUp(bool disposing)
        {
            if (!this.disposed)      // !this.disposed - удостовериваемся, что объект неуправляемые ресурсы ещё не освобождал
            {
                if (disposing)       // disposing - освобождать управляемые ресурсы мы можем только при вызове из Dispose()
                {
                    Console.Beep();  // Console.Beep() - этот сигнал нам поможет в тестировании
                    //...
                }
                //...
            }
            disposed = true;
        }
    }
////I think it shouldn't be here (maybe you should delete)
//  class Car
//  {
//      public string PetName { get; set; }
//      public int CurrentSpeed { get; set; }
//      public int MaxSpeed { get; set; }
//      public Car() { }
//      public Car(string petName, int currentSpeed, int maxSpeed)
//      {
//          PetName = petName;
//          CurrentSpeed = currentSpeed;
//          MaxSpeed = maxSpeed;
//      }
//      public override string ToString() => $"{PetName} is going {CurrentSpeed}";
//  }
    static void LazyObjectCreation()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   LazyObjectCreation()\n");


        // При создании объекта очень часто бывает так, что мы не пользуемся всеми его полями, и это может быть довольно досадно, ведь под них
        //   нередко выделяются огромные объёмы памяти (это также добавляет немалой работы сборщику мусора CLR)

        System.Lazy<int> superInt = new System.Lazy<int>();     // superString - допустим, создание объекта этого класса приведёт к
                                                                   //   огромному расходу памяти 
        // Мы могли бы решить эту проблему своими более            // System.Lazy<> - но есть способ проще - библиотеки базовых классов
        //   традиционными способами (например, создав фабричный   //   содержат этот удобный обобщённый класс, работающий по принципу
        //   метод, создающий этот объект только при своём вызове) //   класса System.Nullable<> (т.е. это тоже просто обёртка). Такой объект
        // new ..Lazy<T>() - всего конструкторов 6-ть              //   сам не создаваться, пока ты к нему не обратишься (а обратишься ты к
        //                                                         //   нему через свойство)


        Console.WriteLine("_{0}_", superInt);           // superString - для начала посмотрим что выдаст метод superString.ToString() нашей
                                                        //   обёртки (заметь, superString - это объект типа System.Lazy<>, не System.String). А
                                                        //   выдаётся нам строка    "Value is not created."
        Console.WriteLine("_{0}_", superInt.Value);     // superString.Value - обёртка Lazy<> добавляет всего пару get-only свойств (заметь,
                                                        //   только get-only!). Свойство superString.Value здесь основное - оно и создаст
                                                        //   объект при своём первом использовании (ты не сможешь сменить ссылку в свойстве
                                                        //   superString.Value, но сможешь изменить внутренние поля объекта, на который эта
                                                        //   ссылка указывает)
                                                        // Второе свойство - это superString.IsValueCreated, выдающее true, если объект уже
                                                        //   создан


        Lazy<string> superString = new Lazy<string>();            // new Lazy<..>() - конструктор по умолчанию для создания внутреннего объекта
                                                                  //   использует его стандартный коснтруктор. Как ты понял, это не всегдя
                                                                  //   хорошо (здесь мы не сможешь изменить значение строки!)
        Lazy<string> superString1 = new Lazy<string>(() =>        // () => .. - конечно, Lazy<> расчитан и на то, чтобы задать объекту что-то.
            {                                                     //   другое. Один из конструкторов принимает делегат, что запустится в
                Console.WriteLine("You are making a new song!");  //   свойстве Value, и именно этот делегат и должнен должен дать значение
                return "Life's What You Make It";                 //   объекту
            });
        string str = superString1.Value;                          // Value - именно здесь и запустится код, что мы передали делегатом


        //****Как и многие другие классы библиотекек базовых классов, Lazy<> способен ещё на несколько фокусов, о которых здесь ничего не
        //   говорилось. Для такой подробной информации лучше заглянуть в раздел документации о .NET Framework 4.7 SDK


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   LazyObjectCreation()");
    }
    static void SystemDiagnosticsProcess_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDiagnosticsProcess_Silent()");


        //****это из моего проекта code-launch-dotnet, что ты создал 26 мая 2021
        //
        //      static void Main(string[] args)
        //      {
        //          string workDir = args.Length >= 1 ? args[0] : "/";
        //              System.Console.WriteLine("{0}", args.Length);
        //              if (args.Length >= 1)
        //                  Console.WriteLine("args[0]: _{0}_", args[0]);
        //
        //          Process codeWithArgs = new Process();
        //          codeWithArgs.StartInfo.FileName = "code";
        //
        //          string codeArgs = "--user-data-dir=/home/art/.config/Code/ " + "--extensions-dir=/home/art/.vscode/extensions/ ./";
        //              Console.WriteLine("codeArgs: _{0}_", codeArgs);
        //          codeWithArgs.StartInfo.Arguments = codeArgs;
        //          codeWithArgs.StartInfo.UseShellExecute = true;
        //          codeWithArgs.StartInfo.CreateNoWindow = false;
        //          codeWithArgs.StartInfo.WorkingDirectory = workDir;  // ..StartInfo.WorkingDirectory - эта штука должна хранить читсый путь
        //                                                              //   к директории без всяких \ перед пробелами (т.е.
        //                                                              //   /my photos/ - верно, /my\ photos/ - не верно), иначе выйдет
        //                                                              //   исключение System.ComponentModel.Win32Exception (даже на
        //                                                              //   gnu/linux'е)
        //              Console.WriteLine("codeWithArgs.StartInfo.WorkingDirectory: {0}", codeWithArgs.StartInfo.WorkingDirectory);
        //
        //          codeWithArgs.Start();
        //          
        //          System.Diagnostics.Process.Start(/*"sudo", */"code", );
        //      }


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcess_Silent()\n");
    }
}