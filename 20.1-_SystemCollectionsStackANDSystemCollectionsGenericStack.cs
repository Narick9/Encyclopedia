/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionsStackANDSystemCollectionsGenericStack
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsStackANDSystemCollectionsGenericStack_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsStackANDSystemCollectionsGenericStack_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsStackANDSystemCollectionsGenericStack_Silent()\n");


        // Класс System.Collections.Stack (не sealed) - это стёк с приципом LIFO (last-in, first-out), противоположность Queue. Реализуется в
        //   нём (из System.Collections) ICollection, IEnumerable и (из System) ICloneable
        // Для того, чтобы Stack был тебе доступен, нужно подключить полноценный System.dll
        // Также есть обобщённый System.Collections.Generic.Stack<T>. Этот класс реализует (из System.Collections.Generic) IEnumerable<T>,
        //   IReadOnlyCollection<T>, (из System.Collections) IEnumerable, ICollection (!)


        Stack<int> people = new Stack<int>();                                 // System.Collections.Generic.Stack - метода Add() нет, поэтому
        people.Push(54);                                                      //   синтаксис инициализации коллекций здесь не работает
        people.Push(9);
        people.Push(42);

        Console.WriteLine("On the top of people is {0}", people.Peek());      // people.Peek() - выдаст то, что лежит на вершине стёка (т.е. то
        Console.WriteLine("Popped off: {0}\n", people.Pop());                 //   , что вошло в коллекцию последним)
        Console.WriteLine("On the top of people is {0}", people.Peek());      // people.Pop() - вытащить (считать и удалить) элемент на вершине
        Console.WriteLine("Popped off: {0}\n", people.Pop());                 //   стёка
        Console.WriteLine("On the top of people is {0}", people.Peek());
        Console.WriteLine("Popped off: {0}\n", people.Pop());
        try
        {
            Console.WriteLine("On the top of people is {0}", people.Peek());  // people.Peek() - если стёк пуст, то Peek() (как и любой другой
            Console.WriteLine("Popped off: {0}\n", people.Pop());             //   метод считывания) выдаст исключение
        }                                                                     //   System.InvalidOperationException
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error! {0}\n", ex.Message);
        }


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsStackANDSystemCollectionsGenericStack_Silent()");
    }
}