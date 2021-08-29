/*
 * creation date  21 mar 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemCollectionsObjectModelReadOnlyCollection
{
    static void Main()
    {
        Console.WriteLine("***** _ ******");

        SystemCollectionsObjectModelReadOnlyCollection_Silent(); //F12, это соседние файлы!****что это значит? файлы нельзя раздвигать? врядли
                                                                 //  . ниже был этот вызов:
                                                                 //  SystemCollectionsObjectModelReadOnlyObservableCollection_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsObjectModelReadOnlyCollection_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   void()\n");


        // System.Collections.ObjectModel.ReadOnlyCollection


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   void()");
    }
}