/*
 * creation date  26 oct 2021
 * last change    26 oct 2021
 * author         artur
 */
using System;
using static System.Console;  // using static - помнишь эту штуку (впревые упоминалось ...ANDUsing_...())?

class __SystemThreadingTimerANDSysetmThreadingTimerCallback
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemThreadingTimerANDSysetmThreadingTimerCallback_Silent();

        Console.ReadLine();
    }
    static void SystemThreadingTimerANDSysetmThreadingTimerCallback_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemThreadingTimerANDSysetmThreadingTimerCallback_Silent()\n");
        

        // First mention
        //   
        //   Первые упоминания этих классов были в общем методе SystemThreadingNamespace_..(). Там же были и общие описания этих классов
        //   

        // Using
        //
        //   Нередкому приложению требуется, чтобы постоянно вызывалась какая-нибудь специфичная функция через определённые интервалы времени
        //     (это может быть функция вывода времени или, может, чекер новых email'ов). Специально для ситуаций подобного рода создавался
        //     System.Threading.Timer и связанный с этим типом делегат System.Threading.TimerCallback
        //   Предположим, что нам нужен метод, что должен каждую секунду выводить текущее время до тех пор, пока пользователь на нажмёт Enter
        //
          void PrintTime(object _)             // PrintTime() - для начала нам нужен метод, что, собственно, и будет печатать время в cmd
          {                                    // object - наш метод должен подходить под делегат TimerCallBack (сигнатура - void (object))
                                               // _ - напоминаю, что это зовётся автономным отбрасывателем
                                               //   . Да, методы в этом делегате могут принимать что угодно (например, массив адресов сервера
                                               //   Microsoft Exchange в случае работы с электронными почтами)
              WriteLine(System.DateTime.Now.ToLongDateString());
          }
        //
          System.Threading.Timer theTimer = new System.Threading.Timer(  // System.Threading.Timer - тоже крохотный класс, имеющий 4 метода
              new System.Threading.TimerCallback(PrintTime),             //   (но перегруженных. половину методов он наследует от abstract
              null,                                                      //   класса) и 5 конструкторов (весьма гибких для своей малой цели)
              0,                                                         // new System.Threading.TimerCallback() - первый параметр, callback,
              1000);                                                     //   принимает, собственно, сам делегат ...TimerCallback
                                                                         // null - 2-ой параметр, state, - это object, что будет послан в
                                                                         //   нашу callback функцию. Т.к. отправлять нам нечего, у нас null
                                                                         // 0 - 3-ий параметр нашей версии конструктора, dueTime, - это
                                                                         //   задержка в миллисекундах перед первым вызовом callback функции
                                                                         //   (типа int). Как ты видишь, мы не хотим ждать
                                                                         // 1000 - 4-ый параметр, period, - это, собственно, период, что
                                                                         //   будет пропущен между вызовами callback функции (типа int, в
                                                                         //   миллисекундах)
        //                                                               //
          ReadLine();
        //
        //   По поведению метода видно, что в нём учавствует дополнительный поток
        //   Правда, вместе с использованием Timer, этот проект подцепил и проблему. Объект theTimer и его поток будут работать даже после
        //     завершения метода SystemThreadingTimer...(). Скорее всего это прикратится только при освобождении памяти объекта theTimer, но
        //     это может сделать только .NET garbage collector, управляемый средой CLR. Запустится он (как мы помним) только когда в
        //     куче не хватит места под новый объект. Это значит, что придётся создать новый проект (к счастью, тема с Timer окончена)
        //     (****это очень интересно, даже не верится)
        //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemThreadingTimerANDSysetmThreadingTimerCallback_Silent()");
    }
}