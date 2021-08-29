/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemCollectionsSpecializedBitVector32
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsSpecializedBitVector32_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsSpecializedBitVector32_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsSpecializedBitVector32_Silent()\n");


        // System.Collections.Specialized.BitVector32 - простенькая структура, хранящая битовые флаги и небольшие числа в участке памяти
        //   длиной в 32 бита. Может выдавать это как простой int (с помощью единственного свойства Data). Почти всё функциональность находится
        //   на уровне типа (всё, кроме Data, - static)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsSpecializedBitVector32_Silent()");
    }
}