/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Numerics;

class _SystemVoid_SystemGuid_SystemNumericsBigInteger
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemVoid();
        SystemGuid();
        SystemNumericsBigInteger();

        Console.ReadLine();
    }
    static void SystemVoid()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemVoid()\n");


        // На самом деле такого типа нету


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemVoid()");
    }
    static void SystemGuid()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemGuid()\n");


        // Структура System.Guid - это такой уникальный id (global unique identifier). Генерируется он почти случайно, и он настолько
        //   вместительный (128 бит), что вероятность повторения guid-а ничтожна. Но если это произойдёт в какой-нибудь гигантской бд, то
        //   настанет полный песец

        Guid myGuid1 = new Guid();                     // new Guid(..) - имеет 6 версий конструкторов. Здесь использован стандартный
        Guid myGuid2 = Guid.NewGuid();                 //   конструктор, что выдаст guid целиком из 0-ей. Вообще, Guid обычно не создают через
                                                       //   его конструктора, а используют его статический метод NewGuid()
        Console.WriteLine("myGuid1: {0}", myGuid1);    // NewGuid() - как-раз этот статический метод выдаст случайный guid
        Console.WriteLine("myGuid2: {0}\n", myGuid2);  // myGuid1.ToString() - выдаст 16-ричное число вида xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx

        Console.WriteLine(Guid.Empty);                 // Empty - это readonly свойство выдаст guid, состоящий полностью из 0-лей. Да, то же
                                                       //   самое выдаёт и стандартный конструктор


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemGuid()");
    }
    static void SystemNumericsBigInteger()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemNumericsBigInteger()\n");


        // Пространство System.Numerics находится в отдельной сборке System.Numerics.dll (ныне System.Runtime.Numerics.dll),
        //   поэтому тебе следует сначала её подключить
        // Структура System.Numerics.BitInteger представляет произвольно большое число (ограничивается только памятью твоей машины). Да,
        //   используется редко где, но когда нужно - вещь незаменимая (если не строить свой велосипед).  После того, как ты задал значение,
        //   модифицировать его уже не получиться, но можно создать новое число из имеющегося, используя некоторые встроенные методы


        BigInteger biggy = BigInteger.Parse("9999999999999999999999999999999999999999999999");
        //                                                                        // Parse() - есть много способов создать BigInteger. Т.к. мы
        //                                                                        //   не хотим вызвать ошибку переполнения (компилятор просто
        //                                                                        //   не примет такое большое число), то здесь используется
        //                                                                        //   строка, из которой этот метод получит нужное нам число

        Console.WriteLine("Value of biggy is          {0}", biggy);               // biggy.ToString() - выдаст то самое число
        Console.WriteLine("Is biggy a even number?:   {0}", biggy.IsEven);        // biggy.IsEven - это свойство (с типом bool) выдаст true,
        Console.WriteLine("Is biggy a power of two?:  {0}", biggy.IsPowerOfTwo);  //   если твоё число - чётное
                                                                                  // biggy.IsPowerOfTwo - это свойство (с типом bool) также
                                                                                  //   говорит за себя

        BigInteger reallyBig = BigInteger.Multiply(biggy, BigInteger.Parse("88888888888888888888888888888888888888888"));
        Console.WriteLine("Value of reallyBig is      {0}\n", reallyBig);          // Multiply() - у BigInteger есть некоторые стандартные
                                                                                  //   методы для арифметики. Можно было бы и использовать
                                                                                  //   внутренние операторы, вроде +, -, *, /


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemNumericsBigInteger()");
    }
}