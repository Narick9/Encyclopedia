/*
 * creation date  2 feb 2021
 * last change    28 jun 2021
 * author         artur
 */
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

class _SystemCollectionsGenericLinkedList
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsGenericLinkedList_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsGenericLinkedList_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsGenericLinkedList_Silent()\n");


        // Класс System.Collections.Generic.LinkedList<T> (не sealed) - это двусвязный список (есть только в Generic). Реализует (из
        //   System.Collections.Generic) ICollection<T>, IEnumerable<T>, (из System.Collections) IEnumerable, ICollection
        /////////after reading///////////////////////////////////////////////////////////////////////
        //   , а также (из System.Collections.Generic) IReadOnlyCollection<T>, (из System.Runtime.Serialization) ISerializable,
        //   IDeserializationCallback
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsGenericLinkedList_Silent()");
    }
}