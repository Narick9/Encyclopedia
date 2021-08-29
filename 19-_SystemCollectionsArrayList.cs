/*
 * creation date  29 jan 2020
 * last change    29 jun 2021
 * author         artur
 */
using System;
using System.Collections;
using System.Collections.Generic;

class _SystemCollectionsArrayList
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsArrayList_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsArrayList_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsArrayList_Silent()\n");


        // Класс System.Collections.ArrayList (не sealed) - стандартный список (т.е. хранит элементы цепочкой вразсброс в управляемой куче, а
        //   не линией, как массив). Реализует интерфейсы (из System.Collections) IList, ICollection, IEnumerable и (из System) ICloneable


        ArrayList myStrs = new ArrayList();                                    // ArrayList() - всего 3 конструктора
        myStrs.AddRange(new string[] { "Left", "Middle", "Right" });           // AddRange() - принимет хранилище, поддерживающее
        Console.WriteLine("This collection has {0} items", myStrs.Count);      //   ICollection, для добавления значений к себе из него
                                                                               //   (перегрузок нет)
        myStrs.Add("After-right");                                             // Count - число объектов, которое хранит коллекция. Не
        Console.WriteLine("Not this collection has {0} items", myStrs.Count);  //   путать с Capacity - числом объектов, которых она
                                                                               //   готова хранить
                                                                               // Add() - добавить новый экземпляр в коллекцию


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsArrayList_Silent()");
    }
}