/*
 * creation date  01 aug 2021
 * last change    01 aug 2021
 * author         artur
 */
using System;

class _SystemSecuritySecureString
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemSecuritySecureString_Silent();

        Console.ReadLine();
    }
    static void SystemSecuritySecureString_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemSecuritySecureString_Silent()\n");


        // System.Security.SecureString (sealed класс) - это безопасная
        //   версия строки. Примечателен
        //   этот тип тем, что имеет много интересных методов для
        //   управления, вроде Clear() для удаления строки из памяти.
        //   Ещё такие строки не дублируются в памяти при создании
        //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemSecuritySecureString_Silent()");
    }
}