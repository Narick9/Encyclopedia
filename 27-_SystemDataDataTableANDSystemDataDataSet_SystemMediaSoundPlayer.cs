/*
 * creation date  25 apr 2021
 * last change    28 jun 2021
 * author         artur
 */
using System;

class _SystemDataDataTableANDSystemDataDataSet_SystemMediaSoundPlayer
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDataDataTableANDSystemDataDataSet_Silent();
        SystemMediaSoundPlayer();

        Console.ReadLine();
    }
    static void SystemDataDataTableANDSystemDataDataSet_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDataDataTableANDSystemDataDataSet_Silent()\n");


        // Класс System.Data.DataTable - по сути это таблица. Класс объявлен в System.Data.dll (и это, кстати, ADO.NET)

        System.Data.DataTable myTable = new System.Data.DataTable();

        myTable.Columns.Add("FirstName", typeof(string));         // myTable.Columns.Add() - так мы можем добавить новый столбец в таблицу
        myTable.Columns.Add("LastName");
        myTable.Columns.Add("Age");
        myTable.Rows.Add("Mel", "Appleby", 60);                   // myTable.Rows.Add() - а этим мы добавляем строку в конец
        Console.WriteLine("FirstName: {0}", myTable.Rows[0][0]);
        Console.WriteLine("LastName: {0}", myTable.Rows[0][1]);   // Rows[0][0] - если ты помнишь, это ступенчатая многомерность
        Console.WriteLine("Age: {0}\n", myTable.Rows[0][2]);      //   (т.к. мы используем массив массивов)


        //****System.Data.DataSet было сказано только то, что для этого класса нужно подключить System.Data.dll


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDataDataTableANDSystemDataDataSet_Silent()");
    }
    static void SystemMediaSoundPlayer()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemMediaSoundPlayer()\n");


        var myMusicPlayer = new System.Media.SoundPlayer();  // var - этого немного сокращает расходы горизонтальной недвижимости, но для
                                                             //   читающего этот код человека увеличивается нагрузка на моск. Не делай так (это
                                                             //   даже не мои слова)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemMediaSoundPlayer()");
    }
 }