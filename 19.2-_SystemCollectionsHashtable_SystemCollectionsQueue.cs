/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionsHashtable_SystemCollectionsQueue
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsHashtable_Silent();
        SystemCollectionsQueue_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsHashtable_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsHashtable_Silent()\n");


        // Класс System.Collections.Hashtable (незапечатан) - местный ассоциативный массив, хранящий пары ключ-значение. В отличие от словаря
        //   (что, конечно, тоже присутствует в .NET), в нём хранятся не сами ключи, а хеш-коды от них (хеш-таблица же). Класс реализует
        //   интерфейсы (из System.Collections) IDictionary, IColletion, IEnumerable и (из System) ICloneable


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsHashtable_Silent()");
    }
    static void SystemCollectionsQueue_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsQueue_Silent()\n");


        // Класс System.Collections.Queue (тоже незапечатанный) - это контейнер, работающий как очередь (т.е. по принципу FIFO - first-in,
        //   first-out). Готов спорить, что внутри он напоминает список. Класс реализует (из System.Collections) ICollection, IEnumerable и (из
        //   System) ICloneable
        // Класс Queue определён в System.dll
        // Есть также обобщённая версия - System.Collections.Generic.Queue<T>, что реализует (из System.Collections) ICollection (!),
        //   IEnumerable, (из System.Collections.Generic) IEnumerable<T>, IReadOnlyCollection<T>


        void GiveCoffee(string p)
        {
            Console.WriteLine("{0} got coffee!", p);
        }
        Queue<string> visitors = new Queue<string>();  // System.Collections.Generic.Queue - метода Add() здесь тоже нет - значит используем
        visitors.Enqueue("Gerald Casale");             //    местные средства
        visitors.Enqueue("Mark Mothersbough");         // visitors.Enqueue<T>() - добавляем элемент в очередь (перегрузок нет)
        visitors.Enqueue("Bob Mothersbough");
        visitors.Enqueue("Josh Freese");
        visitors.Enqueue("Josh Hager");


        Console.WriteLine("{0} is first in the queue!", visitors.Peek());
        GiveCoffee(visitors.Dequeue());                // visitors.Peek() - здесь также можно заглянуть в очередь, получив первый её элемент
        GiveCoffee(visitors.Dequeue());                // visitors.Dequeue() - достать (считать и удалить) элемент из
        GiveCoffee(visitors.Dequeue());                //   очереди (достаётся тот, что был добавлен первее других)
        try
        {
            GiveCoffee(visitors.Dequeue());
        }
        catch (InvalidOperationException ex)           // System.InvalidOperationException - здесь также сгенерируется исключение, если ты
        {                                              //   попытаешься получить значение из пустой коллекции
            Console.WriteLine("Error! {0}", ex.Message);
        }


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsQueue_Silent()");
    }
}