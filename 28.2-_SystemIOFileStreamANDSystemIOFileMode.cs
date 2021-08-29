/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemIOFileStreamANDSystemIOFileMode
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemIOFileStreamANDSystemIOFileMode();

        Console.ReadLine();
    }
    static void SystemIOFileStreamANDSystemIOFileMode()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemIOFileStreamANDSystemIOFileMode()\n");


        // Класс System.IO.FileStream (не sealed) - (как ты понял) это System.IO.Stream для файлов. Класс слегка расширяет функционал своего
        //   абстрактного предка
        System.IO.FileStream somefile = default;  // default - да, здесь можно было и явно написать null
        try
        {
            somefile = new System.IO.FileStream(".\\somefile", System.IO.FileMode.Open);
                                                 // new ..FileStream() - всего конструкторов аж 15-ть. Этот - самый простой. Как ты видишь,
                                                 //   работает он примерно как классическая сишная фукнция fopen()
                                                 // System.IO.FileMode - это перечисление для задания режима открытия файла. Вот как выглядит
                                                 //   его определение:
                                                 //       public enum FileMode
                                                 //       {
                                                 //           CreateNew = 1,     // CreateNew - создать файл. Если такой файл уже есть,
                                                 //           Create = 2,        //   выброситься System.IO.IOException
                                                 //           Open = 3,          // Create - создать файл. Если такой уже есть - перезаписать
                                                 //           OpenOrCreate = 4,
                                                 //           Truncate = 5,      // Truncate - открыть существующий файл. После открываения
                                                 //           Append = 6         //   файл обрезает до 0-ля байт (т.е. стирается под 0-ль)
                                                 //                              //   (truncate - с англ. обрезать, сократить, усечь)
                                                 //       }
        }
        catch (System.IO.FileNotFoundException)  // ..FileNotFoundException - этот класс исходит из System.IO.IOException. Такое исключение
        {                                        //   выкинится, если ты попытаешься достучаться до файла, которого на диске нет
        }


        somefile.Close();    // somefile.Close() - этим методом можно вручную закрыть поток и ещё некоторые вне .NET'ные ресурсы
                             //   (****класс имеет декструктор?)
                             //
        /////////after reading///////////////////////////////////////////////////////////////////////
        somefile.Dispose();  // somefile.Dispose() - в somefile.Close() выполняются те же команды, что и здесь
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemIOFileStreamANDSystemIOFileMode()");
    }
}