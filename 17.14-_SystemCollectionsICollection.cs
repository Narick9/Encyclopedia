/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionsICollection
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsICollection_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsICollection_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsICollection_Silent()\n");


        // Интерфейс System.Collections.ICollection определяет общий набор членов для коллекций (вроде их текущей вместимости, возможности
        //   перечисления содержимого, что-то для безопасности для потоков). Создан для необобщённых типов (т.к. наследует необобщённый
        //   System.Collections.IEnumerable)


        
        /////////after reading///////////////////////////////////////////////////////////////////////
        List<string> people = new List<string> { "first", "second", "third" };  // {..} - сокращённая запись многократного вызова
                                                                                //   System.Collections.ICollection.Add() (как обычного
                                                                                //   ICollection, так и generic версию). Как видишь, выглядит
                                                                                //   хорошо
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsICollection_Silent()");
    }
}