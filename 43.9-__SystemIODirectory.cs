/*
 * creation date  25 sep 2021
 * last change    25 sep 2021
 * author         artur
 */
using System;
using System.IO;

class __SystemIODirectory
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemIODirectory_Silent();

        Console.ReadLine();
    }
    static void SystemIODirectory_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemIODirectory_Silent()\n");


        // System.IO.Directory - статический класс, поставляющий в себе пачку методов для работы с директориями (создания, перенисения,
        //   вывода). Часть класса хранится в mscorlib.dll (но это не точно), часть в System.IO.FileSystem.dll


        string currDir = Directory.GetCurrentDirectory();  // System.IO.Directory.GetCurrentDirectory() - выдаст абсолютное имя текущей
                                                           //   директории (той текущей, где процесс был запущен)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemIODirectory_Silent()");
    }
}