/*
 * creation date  24 jan 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _ForANDForeachANDWhileANDDoWhile_IfElseSwitch
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        ForANDForeachANDWhileANDDoWhile();
        IfElseSwitch();

        Console.ReadLine();
    }
    static void ForANDForeachANDWhileANDDoWhile()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   ForANDForeachANDWhileANDDoWhile()\n");


        // Циклы взяты из C++. nuff said

        for (int i = 0; i < 4; i++)          // for - все стандартные плюшки из Си вроде goto, continue и break имеются
        {
            Console.WriteLine("i: {0}", i);
        }
        Console.WriteLine();


        // Цикл foreach в C# выполняет примерно ту же функцию, что и foreach в C++ (там этот цикл, кстати, прозвали циклом по коллекции).
        //   Местный foreach также работает только с объектами, что могут выдать итератор, но только здесь эта называется "энумератор"
        //   /////////after reading///////////////////////////////////////////////////////////////////////
        //   // (контейнер должен поддерживать интерфейс System.Collections.IEnumerable)
        //   /////////////////////////////////////////////////////////////////////////////////////////////

        string[] carBrands = { "Ford", "BMW", "Yugo", "Honda" };
        foreach (string brand in carBrands)         // in - после этого ключевого слова может стоять контейнер
        {                                           //   /////////after reading////////////////////////////////////////////////////////////////
            Console.WriteLine("model {0}", brand);  //   // , поддерживающий IEnumerable
        }                                           //   //////////////////////////////////////////////////////////////////////////////////////
        Console.WriteLine();


        string userReady = "";
        while (userReady.ToLower() != "yes")  // while - do-while здесь тоже есть
        {
            Console.Write("Are you ready? [yes][no]: ");
            userReady = Console.ReadLine();
        }
        Console.WriteLine();


        int[,] mass = { {  1,  2,  3 },
                       {  4,  5,  6 },
                       {  7,  8,  9 },
                       { 10, 11, 12 } };
        foreach (int i in mass)           // i in mass - возможно ты будешь удевлён, но foreach здесь будет выдавать именно элементы int, ведь
        {                                 //   это прямоугольный массив (а не массив массивов)
            Console.Write($"{i} ");       //   /////////after reading///////////////////////////////////////////////////////////////////////
        }                                 //   // так устроен энумератор в System.Array - он просто проводит циклом по всем 12 элементам
        Console.WriteLine();              //   /////////////////////////////////////////////////////////////////////////////////////////////
                                          // i - ты сможешь присвоить этой переменной что-то, т.к. это 'foreach iteration variable'


        //****вырезка из метода про dynamic linking
        // string, Assembly - VS говорит о необязательности предобъявления переменных, т.е. нас слудет объявлять
        //   их прямо в цикле (в таком случае, скорее всего, компилятор сам оптимизирует всё должным образом)


        Type t = typeof(int);

        /////////after reading:ReflectionForAttributes///////////////////////////////////////////////
          t.GetCustomAttributes(false);  // t.GetCustomAttributes() - про семейство этих методов
        //                               //   написано в части энциклопедии о рефлекции для
        //                               //   атрибутов
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   ForANDForeachANDWhileANDDoWhile()");
    }
    static void IfElseSwitch()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   IfElseSwitch()\n");


        // И операторы ветвления для тебя не что-то новое

        string someString = "kjfdsgkj";
        if (someString.Length > 0)  // 0 - придётся отвыкать от привычки использовать всё подряд как условие, т.к. условие if в C#
        {                           //   работает только с bool, а приводить что-то в bool нельзя (совсем)
            Console.WriteLine("someString has more than 0 length");
        }
        else
        {
            Console.WriteLine("someString has no more than 0 length");
        }
        Console.WriteLine();


        Console.WriteLine(someString.Length > 0         // ?: - единственный тернарный оператор сильно урезан по сравнению с версией в
            ? "someString has more than 0 length"       //   Си: его можно использовать только в операциях присваивания, а это значит,
            : "someString has no more than 0 length");  //   что оба результирующих значения должны быть одного типа. Также это
        Console.WriteLine();                            //   отнимает возможность использовать его как лёгкий if-else для простых команд:
                                                        //       true ? i++ : ..Write();


        // Стандартный набор логический операций (т.е. &&, || и !) здесь работает абсолютно также, как и в Си. Это значит, что
        //   такие интересности, как сокращённые путь выполнения (т.е., если в записи вроде left || right left возвратит
        //   true, команда right выполнятся не будет, т.к. одного true в цепи достаточно, чтобы всё выражение также стало true)
        //   . Но операции & и | здесь делают другую работу - они делают то же, что и && и ||, но выполняют все команды


        Console.WriteLine("1 [C#], 2 [Perl]");
        Console.Write("Please pick your language preference: ");
        int langChoice = default;
        int.TryParse(Console.ReadLine(), out langChoice);
                             //
        switch (langChoice)  // switch - тоже довольно сильно урезан. Местный вариант требует, чтобы из каждого case (и даже default) был
        {                    //   какой-либо выход, вроде break, return или goto
            case 1:
                Console.WriteLine("Good choice, C# is a fine language.");
                break;
            case 2:
                Console.WriteLine("Larry Wall is proud for you!");
                break;
            default:
                Console.WriteLine("Okay.. good luck with that!");
                break;
        }
        Console.WriteLine();  //
                              //
        switch (5)            //
        {
            case 1:
            case 2:           // case - ..но если case не имеет никакого кода, то выход из него не обязателен
                Console.WriteLine("case 1 or 2");
                break;
            case 5:
                Console.WriteLine("case 5");
                goto case 2;  // goto - может указывать на case и default внутри switch
            case 6:
        }
        Console.WriteLine();


        Console.WriteLine("1 [Red pill], 2 [Blue pill]");
        Console.Write("Please pick your way preference: ");
        object choice = Console.ReadLine();
        var choiceVar = int.TryParse(choice.ToString(), out int choiceInt) ? choiceInt : choice;
        switch (choiceVar)          // object choice - да, именно object. Здесь я напоминаю тебе о магии наследования. choice.ToString() дальше
        {                           //   выдаст именно строку, что ввёл пользователь!
                                    // var .. = ..(..out int choiceInt) ? choiceInt : .. - да, малость запутанно, но привыкай
            case int i when i == 1:                             // cast int - в C#'овском switch можно ставить условие и по типу. Помнишь
                Console.WriteLine("@..010011011_010100100..");  //   оператор is? Ну так вот - он работает здесь за кулисами
                break;                                          // when .. - если тип совпал, можно задать дополнительное условие
            case int i when i == 2: caseTwo:                    // caseTwo - почему я просто не сделал goto к этому case? Я не нашёл способа
                Console.WriteLine("`alarm clock noise`");       //   подружить goto и case int .. when ..
                break;
            //case int _ when _ == 0:                           // _ - как оказалось, прочерк - это специальная конструкция языка, и немного
            default:                                            //   отличается от обычных переменных. VS выводит ошибку в when о том, что _ не
                Console.Write("You chose the wronп choice. ");  //   существует в данном контексте
                goto caseTwo;
        }
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   IfElseSwitch()");
    }
}