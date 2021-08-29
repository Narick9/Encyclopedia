/*
 * creation date  29 jan 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionSpecializedListDictionary
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionSpecializedListDictionary_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionSpecializedListDictionary_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionSpecializedListDictionary_Silent()\n");


        // Коллекция System.Collections.Specialized.ListDictionary (не sealed класс) удобна, пока управляет набольшим количеством элементов
        //   (~10), которые часто заменяются. Здесь применятся простой односвязный список


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionSpecializedListDictionary_Silent()");
    }
}