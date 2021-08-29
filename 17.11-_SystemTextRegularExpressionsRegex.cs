/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Text.RegularExpressions;

class _SystemTextRegularExpressionsRegex
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemTextRegularExpressionsRegex();

        Console.ReadLine();
    }
    static void SystemTextRegularExpressionsRegex()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemTextRegularExpressionsRegex()\n");


        // Класс System.Text.RegularExpressions.Regex (не статический!)(среднего размера) представляет собой стандартное регулярное выражение.
        //   Да, .NET разработчики решили хранить регулярные выражения в отдельных объектах, а не в строках, как все. Ещё стоит сказать, что
        //   объекты этого класса немутабельны (как и строки)

        string newForm = Regex.Replace("someemail@somemachine.somecom", ".*", ".net");
        Console.WriteLine(newForm);


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemTextRegularExpressionsRegex()");
    }
}