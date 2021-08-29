/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemCollectionsSpecializedHybridDictionary
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsSpecializedHybridDictionary_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsSpecializedHybridDictionary_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsSpecializedHybridDictionary_Silent()\n");


        // Класс System.Collections.Specialized.HybridDictionary (не sealed) - пока коллекция мала, применяет внутри
        //   System.Collection.Specialized.ListDictionary, а если она разрослась, то коллекция переключается на System.Collections.Hashtable.
        //   Реализует (из System.Collection) IDictionary, ICollection, IEnumerable


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsSpecializedHybridDictionary_Silent()");
    }
}