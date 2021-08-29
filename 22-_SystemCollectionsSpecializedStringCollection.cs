/*
 * creation date  1 feb 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

class _SystemCollectionsSpecializedStringCollection
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsSpecializedStringCollection_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsSpecializedStringCollection_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsSpecializedStringCollection_Silent()\n");


        // System.Collections.Specialized.StringCollection (не sealed класс) - эта коллекция обеспечивает оптимальный способ для управления
        //   крупным набором строковых данных. Реализует (из System.Collections) IList, ICollection, IEnumerable


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsSpecializedStringCollection_Silent()");
    }
}