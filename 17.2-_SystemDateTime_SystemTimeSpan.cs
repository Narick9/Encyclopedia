/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemDateTime_SystemTimeSpan
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDateTime();
        SystemTimeSpan();

        Console.ReadLine();
    }
    static void SystemDateTime()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDateTime()\n");


        // Структура System.DateTime своими объектами представляет просто точки времени. Весьма удобная структура

        DateTime dt = new DateTime(2020, 7, 16);                           // new DateTime(..) - есть 12 конструкторов
        Console.WriteLine("Today is {0}, {1}th month", dt.Day, dt.Month);  // dt.Day - выдаст день месяца (в виде int)

        dt = dt.AddMonths(3);
        Console.WriteLine("{0} is a summer time: {1}\n", dt, dt.IsDaylightSavingTime());
        //                                                                 // dt.AddMonths() - прибавляет месяцы к дате. Выдаст новую структуру
        //                                                                 //   , т.к. (я думаю) возоможность передавать стрктуры по ссылке
        //                                                                 //   (через ref) не поддерживается CLS


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDateTime()");
    }
    static void SystemTimeSpan()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemTimeSpan()\n");


        // Структура System.TimeSpan представляет возможность создавать интервалы времени. Просто интервалы, без учёта стартовой и конечной
        //   точки времени (если они нужны, используй System.DateTime)

        TimeSpan ts = new TimeSpan(4, 30, 0);       // new TimeSpan(..) имеет 5 конструкторов (здесь был использован конструктор
        Console.WriteLine("ts:          {0}", ts);  //   TimeSpan(int hours, int minutes, int secundes)
        Console.WriteLine("ts - 15min:  {0}\n", ts.Subtract(new TimeSpan(0, 15, 0)));
        //                                          // ts.ToString() - выдаст 04:30:00


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemTimeSpan()");
    }
}