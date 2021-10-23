/*
 * creation date  24 sep 2021
 * last change    20 oct 2021
 * author         artur
 */
using System;

class __MicrosoftExtensionsHostingIHost
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        MicrosoftExtensionsHostingIHost_Silent();

        Console.ReadLine();
    }
    static void MicrosoftExtensionsHostingIHost_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   MicrosoftExtensionsHostingIHost_Silent()\n");


        // Microsoft.Extensions.Hosting.IHost - общее описание ты можешь найти в метод о ASP.NET Core (если говорить конкретнее, в файле
        //   ./SimpleWeb_ASP.NET/Program.cs), т.к. впервые затрагивали мы этот интерфейс там


        /////////after reading: Microsoft.AspNetCore.Hosting.IWebHost (****которого ещё нету)////////
        // В чём отличие от ...IWebHost? Читай в методе о ...IWebHost
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   MicrosoftExtensionsHostingIHost_Silent()");
    }
}