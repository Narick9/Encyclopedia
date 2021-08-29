/*
 * creation date  12 dec 2020
 * last change    28 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

class _SystemEventArgs
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemEventArgs_Silent();

        Console.ReadLine();
    }
    static void SystemEventArgs_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemEventArgs_Silent()\n");


        void MyBetty_ExplodedHandler(object sender, Car_ForEventStandard.CarEventArgs args)
        {
            if (sender is Car car)  // sender is Car - не очень понял, почему все решили применять object, а не тип самого объекта, при этом
            {                       //   меняя тип args (да, там без этого нельзя, но раз уж на то пошло, то почему бы не изменить object?
                Console.WriteLine("{0} has sended a message: {1}", car.PetName, args.msg);
            }
        }
        Car_ForEventStandard myBetty = new Car_ForEventStandard("Betty", 80, 100);
        myBetty.ExplodedHandlers += MyBetty_ExplodedHandler;
        myBetty.Accelerate(30);
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemEventArgs_Silent()");
    }
    class Car_ForEventStandard : Car
    {
        public class CarEventArgs : EventArgs                                     // CarEventsArgs - мы будем отправлять объекты этого класса с
        {                                                                         //   полезной информацией будущим обработчикам
            public readonly string msg;
            public CarEventArgs(string _msg) { msg = _msg; }
        }
        public delegate void CarEngineHandler(object sender, CarEventArgs args);  // CarEngineHandler(..) - здесь соблюдён рекомендованный
        //public class EventArgs                                                  //   Microsoft шаблон для событий:
        //{                                                                       //       delegate ..(object sender System.EventArgs e)
        //    public static readonly EventArgs Empty;                             //   sender здесь представляет ссылку самого объекта,
        //    public EventArgs();                                                 //   который запустил события из себя, а e - объект с
        //}       // System.EventArgs - вот полное определение этого класса       //   некоторой дополнительной информацией
        //        // Empty - как я понял, это просто заглушка, используемая,      // Как правило, от EventArgs создаётся новый класс,
        //        //   если тебе нечего отправлять в аргумент e. Конечно, можно   //   специфичный для типа, чтобы была возможность оправлять
        //        //   отправлять null в функции текущего event'а, но тогда в них //   в нём что-то действительно полезное (сам по себе
        //        //   пришлось реализовывать проверку на null. Использовать      //   System.EventArgs ничего не хранит)
        //        //   Empty проще                                                // Есть обобщённые делегат System.EventHandler<> (и его
        //                                                                        //   необобщённая версия), специально созданный для
        //                                                                        //   использования в событиях (он также соблюдает
        //                                                                        //   стандарт Microsoft)
        private bool carIsDead = false;
        public event CarEngineHandler ExplodedHandlers;
        public Car_ForEventStandard(string petName, int currentSpeed, int maxSpeed) : base(petName, currentSpeed, maxSpeed)
        {
        }
        public void Accelerate(int delta)
        {
            if (carIsDead)
            {
                ExplodedHandlers?.Invoke(this, new CarEventArgs("Sorry, this car is dead..."));
            }
            CurrentSpeed += delta;
            if (CurrentSpeed > MaxSpeed)
            {
                ExplodedHandlers?.Invoke(this, new CarEventArgs("Sorry, this car is dead..."));
            }
            else
                Console.WriteLine("{0} is going {1}", PetName, CurrentSpeed);
        }
    }
}