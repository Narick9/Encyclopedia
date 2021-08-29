/*
 * creation date  17 jan 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

class _MainAndCommandLineArguments
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        MainAndCommandLineArguments();

        Console.ReadLine();
    }
    static void MainAndCommandLineArguments()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   MainCommandLineArguments()\n");


        // Как мы все знаем (и давно привыкли к этому), все Си-подобные программы начинаются с метода с именем main (или Main). В .NET мире
        //   метод, с которого запускается программа, называется "entry point"
        //   /////////after reading///////////////////////////////////////////////////////////////////////
        //   // (.entrypoint - этот атрибут (****атрибут?) ставится в начале пускового метода в CIL коде
        //   /////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Вариантов написания метода Main() несколько:
        //
        //     static void/int Main()/(string[] ..)
        //
        // Если в программе не окажется подходящего метода Main, компилятор выдаст ошибку
        // Если в твоём коде каким-то фигом оказалось несколько static объектов с подходящими по всем уловия Main() методами, компилер по
        //   умолчанию выдаст ошибку с обругиванием тебя. Исправить это можно, ****что-то сделав в свойствах проекта VS, или правив ..csproj:
        //       ..
        //       <PropertyGroup>
        //         ..
        //         <StartupObject>AboutAuthors_</StartupObject>    // <StartupObject></..> - здесь ты ставишь полностью заданное имя желаемого
        //       </PropertyGroup>                                  //   класса с подходящим Main()
        //       ..


        // Main() сможет принять аргументы командной строки в случае (если ты решишь прописать string[] .. в его прототипе). Класс
        //   ProgramArgs как-раз занимается этим (правда, вложенные классы не могут иметь entry point)
        /////////after reading///////////////////////////////////////////////////////////////////////
        // Есть ещё один способ получить аргументы командной строки - использовать метод System.Environment.GetCommandLineArgs()
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   MainCommandLineArguments()");
    }
    class ProgramArgs
    {
        static void Main(string[] args)  // args - интересно, что здесь первый элемент это не имя программы (а ведь это на самом деле так)
        {
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine("arg {0} is {1}", i, args[i]);

            Console.ReadLine();
        }
    }
}