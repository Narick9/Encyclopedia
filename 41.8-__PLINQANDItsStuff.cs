/*
 * creation date  28 oct 2021
 * last change    28 oct 2021
 * author         artur
 */
using System;
using System.Linq;

class __PLINQANDItsStuff
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        PLINQANDItsStuff_Silent();

        Console.ReadLine();
    }
    static void PLINQANDItsStuff_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   PLINQANDItsStuff_Silent()\n");


        // WhatItIs
        //
        //   Как ты помнишь, LINQ - это набор расширяющих методов в контейнерах, что используются для упращённой работы с сими (и ещё есть
        //     набор слов-операторов, за которые обычно и понимают LINQ. на самом деле это LINQ to Objects).
        //     Ну так вот, добавив всего одну деталь
        //     в привычном запросе, ты переведёшь этот запрос на полностью параллельную манеру. Уже чуешь весь профит, да? В правильном месте
        //     это обеспечивит сильнейший буст по производительности. Запросы LINQ, что используют эту деталь, зовутся запросами
        //     Parallel LINQ (или PLINQ)
        //   Инфраструктура PLINQ оптимизированна во многих отношениях. Например, PLINQ просто анализирует общую структуру запроса. Если есть
        //     достаточно большая вероятность того, что запрос выиграет от распараллеливания, он будет выполнятся параллельно (опция
        //     отключения распараллеливания вручную также есть). Да, когда возникает выбор между потенциально затратным в плане ресурсов
        //     параллельным алгоритмом и экономным последовательным, предпочтения (по умолчанию) отдаётся последовательному алгоритму
        //   Необходимые для PLINQ расширяющие методы находятся в классе System.Linq.ParallelEnumerable. Вот некоторые
        //     интересные представители оттуда:
        //  
        //     > AsParallel()              -  указывает, что остаток запроса должен быть (если возможно) распараллелен
        //     > WithCancellation()        -  инфраструктура PLINQ дложна периодически отслеживать состояние признака отмены (т.е. объекта
        //                                    System.Threading.CancellationToken) и при получении этой команды отменить выполнение
        //     > WithDegreeOfParallelism() -  с помощью этого метода ты можешь указать максимальное кол-во (именно максимальное) потоков, что
        //                                    могуть быть задействованы инфраструктурой PLINQ для этого запроса
        //     > ForAll()                  -  этот расширяющий метод (кроме того, что выполняет заданное действие над элементами контейнера)
        //                                    вадаёт свой результат по частям. Он не выполняет "слияния" с потоком потребителя (т.е. не ждёт,
        //                                    пока его выходной контейнер не наполнится полностью), отдавая свой результат сразу и наполняя
        //                                    его со временем. Выход этого метода прекрасно работает с циклом foreach
        //   Если расширяющие методы LINQ выдают объекты, реазилующие System.Collections.Generic.IEnumerable<>, то методы PLINQ оставят
        //     после себя System.Linq.ParallelQuery<> (и, кстати, каждый из них привнесёт в результат что-то своё. WithCancellation(),
        //     например, подключит проверку токена во всю цепь запроса после него)
        //


        // Using
        //
        //   В качестве демонстрации выступит код, что из сверхбольшого массива отсеивает числа, кратные 3-ём
        //
          int[] numbers = Enumerable.Range(1, 100_000_000).ToArray();  // System.Linq.Enumerable.Range() - как ты видишь, LINQ To Objects
          DateTime time;                                               //   умеет работать не только с имеющимеся контейнерами. Этот метод
                                                                       //   создаёт последовательность чисел от параметра start до параметра
                                                                       //   count (и умеет выбрасывать System.ArgumentOutOfRangeException)
                                                                       //   Ещё есть ..Enumerable.Empty() (для генерации пустого контейнера)
                                                                       //   и ..Enumerable.Repeat() (для контейнера с одинаковым числом)
                                                                       //****инфо сверху следует перенести в метод о LINQ
                                                                       // time - мы будем сравнивать затраты процессорного времени
          //
          time = DateTime.Now;
          int[] modThreeIsZero = (from num in numbers.AsParallel() where num % 3 == 0 select num).ToArray();
          Console.WriteLine("PLINQ: {0}", DateTime.Now - time);   // .. numbers.AsParallel() .. - чтобы дальнейший запрос шёл в многопоточной
                                                                  //   манере, достаточно вклинить в нужное место этот метод
          //
          time = DateTime.Now;
          modThreeIsZero = (from num in numbers where num % 3 == 0 select num).ToArray();
          Console.WriteLine("LINQ: {0}", DateTime.Now - time);
        //
          Console.WriteLine("There are {0} numbers that match the query!\n", modThreeIsZero.Count());
        //
        //


        // CancelIt
        //
        //   С помощью объекта System.Threading.CancellationTokenSource ты также можешь проинформировать запрос PLINQ о необходимости
        //     прекратить работу. Этот метод расширяет только объекты типа ParallelQuery (и, конечно, типы, являющиеся им)
        //
          System.Threading.CancellationTokenSource cancelToken = new System.Threading.CancellationTokenSource();
        //
          do
          {
              Console.Write("Press any key to start: ");
              Console.ReadLine();

              Console.WriteLine("Processing...");
              System.Threading.Tasks.Task.Factory.StartNew(  //****System.Threading.Tasks.Task.Factory - мы вроде как ещё не знаем что такое
                  () => {                                    //    Task
                      int[] modThreeIsZero = (from num in Enumerable.Range(1, 100_000_000).AsParallel().WithCancellation(cancelToken.Token)
                                              where num % 3 == 0 select num).ToArray();
                      Console.WriteLine("Done! There is {0} matched numbers", modThreeIsZero.Count());
                      Console.WriteLine("Press any other key to repeat:");
                  });

              Console.WriteLine("Press Q to stop:");
              if ("Q".Equals(Console.ReadLine(), System.StringComparison.OrdinalIgnoreCase))
              {
                  cancelToken.Cancel();
                  break;
              }
          } while (true);

          Console.WriteLine();
        //
        //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   PLINQANDItsStuff_Silent()");
    }
}