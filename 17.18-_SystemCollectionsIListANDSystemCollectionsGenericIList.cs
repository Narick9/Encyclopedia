/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemCollectionsIListANDSystemCollectionsGenericIList
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsIListANDSystemCollectionsGenericIList_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsIListANDSystemCollectionsGenericIList_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsIListANDSystemCollectionsGenericIList_Silent()\n");


        // Интерфейс System.Collections.IList - гарантирует наличие методов добавления, удаления и индексирования элементов в списке. Наследует
        //   (из System.Collections) ICollection, IEnumerable
        // Есть обобщённая версия - System.Collections.Generic.IList<T>. Она наследует (из System.Collections.Generic) ICollection<T>,
        //   IEnumerable<T>, (из System.Collections) IEnumerable


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsIListANDSystemCollectionsGenericIList_Silent()");
    }
}