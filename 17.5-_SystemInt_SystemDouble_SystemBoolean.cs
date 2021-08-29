/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemInt_SystemDouble_SystemBoolean
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemInt();
        SystemDouble();
        SystemDouble();

        Console.ReadLine();
    }
    static void SystemInt()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemInt()\n");


        // Т.к. все вариации int - фактически одно и то же, я не стал выделять под каждую из них по методу
        // Структуры System.[Int/UInt][16/32/64] нам самом деле уже давно нам интуитивно понятны. Вот несколько интересных членов, что имеет
        //   каждая из них:


        Console.WriteLine("Max of int: {0}", int.MaxValue);            // MaxValue - это статическое константное поле (типа int) хранит
        Console.WriteLine("Min of int: {0}\n", int.MinValue);          //   наибольшее число, что может вместить в себя эта версия int. На
                                                                       //   самом деле ты мало где ещё в BCL найдёшь открытые поля, кроме как у
                                                                       //   стандартных типов
                                                                       // MinValue - а это поле (также типа int) хранит минимально возможное число

        Console.WriteLine("int.Parse(\"53\"): {0}", int.Parse("53"));  // Parse() - этот метод разберёт int в string и возвратит его. Имеется
                                                                       //   4-е версии этого метода. Если переданная строка будет содержать
                                                                       //   что-то некорректное, метод выбросит System.FormatException


        short num1 = 10;                    //  short - система приведения в C# довольно сильно отличается от системы Си (там вообще не было
        short num2 = 25;                    //    ограничений). Компилятор не пропустит неявного понижающего приведения (например, от int к
        short sum1 = (short)(num1 + num2);  //    short), т.к. есть возможность потери данных
                                            //  + - интересно, что при арифметике с целыми числами используются операторы от System.Int32 (даже
                                            //    если ты работаешь с типами short или byte):
                                            //    short ans = short_left + short_right;   `cannot implicitly convert type int to short`

        byte b1 = 100;
        byte b2 = 250;
        byte sum2 = (byte)(b1 + b2);                                  // + - здесь происходит переполнение, но т.к. мы явно преобразуем
        Console.WriteLine($"b1 + b2: sum  ->  {b1} + {b2}: {sum2}");  //   int в byte, ничего не происходит


        try
        {
            sum2 = checked((byte)(b1 + b2));  // checked - с этим ключевым словом компилятор выпускает дополнительные инструкции,
        }                                     //   проверяющие наличие переполнения. Если оно произошло, то выбрасывается исключение
        catch (OverflowException ex)          //   System.OverflowException. Конечно, проверку можно было бы делать и вручную, но зачем?
        {                                     //   Ещё checked может охватывать целые блоки кода:   checked { ... }
            Console.WriteLine($"checked((byte)(b1 + b2)) ->  {ex.Message}");
        }                                     // Ещё стоит сказать, что проверку переполнения можно сделать на уровне проекта, поставив
                                              //   параметр компилятора -checked или включив флаг в окне свойств проекта -> Build ->
                                              //   -> Advanced. Будет проверяться каждая арифметическая операция. В Release можно
                                              //   отключить этот флаг, слегка увеличив производительность компилятора
                                              // unchecked - если в каком-то месте переполнение тебя не смущает, но ты хочешь оставить
                                              //   флаг -checked, то можешь воспользоваться ключевым словом unchecked, отменяющим
                                              //   генерацию исключения:    unchecked()    unchecked{}


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemInt()");
    }
    static void SystemDouble()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDouble()\n");


        // Структуру System.Double ты также видишь далеко не первый раз


        //*****уже было в 3-NETAndBCL_COM_CLI_CLRAndJIT_TypeAndCTS_CLS.cs
        //
        double l; System.Double l_;    // double - 64-битное, от ±5.0х10^324 до ±1.7х10^308 (по заявлению автора)
                                       //
        // Что есть System.Deciaml? Это такой тип с дробным числом, что даже точнее, чем double! Если double занимает в памяти 8 байт, и может
        //   хранить 16 знаков после запятой, то decimal занимает аж 16 байт, и хранит до 28 знаков. Ну и ещё - decimal более нацелен на дробь
        //   чем double. Хотя он и занимает больше места в памяти, его максимальный потолок по значинию меньше, чем у double. Вот их сравнение:
        //
        //       Decimal            Double
        //
        //       ~10^28             ~10^308      Наибольшее значение                    
        //       10^-28             ~10^-323     Наименьшее значение (без учёта 0-я)
        //       28                 15-16        Знаков после запятой
        //       16 байт            8 байт       Разрядность
        //       сотни миллионов    миллиарды    Операций в секунду
        //


        Console.WriteLine("double.Epsilon:           {0}", double.Epsilon);             // Epsilon - это статическое const поле хранит double
        Console.WriteLine("double.PisitiveInfinity:  {0}", double.PositiveInfinity);    //   с наименьшей возможной положительной дробью
        Console.WriteLine("double.NegativeInfinity:  {0}\n", double.NegativeInfinity);  // PosifitveInfinity - уникальное для float/double
                                                                                        //   статическое поле, что хранит положительное
                                                                                        //   бесконечное число. Почему оно имеется только
                                                                                        //   float/double? Это связано с их двоичным строением
                                                                                        //   (у них есть неиспользующиеся нигде комбинации)
                                                                                        // NegativeInfinity - то же, но бесконечность
                                                                                        //   отрицательная

        Console.WriteLine("double.Parse(\"99,59\"): {0}\n", double.Parse("99,59"));     // Parse() - всё то же, что и у int.Parse(), но про
                                                                                        //   double (даже число перегрузок то же). И ещё - для
                                                                                        //   дробной части должна быть именно запятая, не точка

        string value = "Hallo";
        if (double.TryParse(value, out double d))                                       // TryParse() - выдаёт false если не удалось корректно
        {                                                                               //   конвертировать строку. Если всё-таки удалось, то
            Console.WriteLine("TryParse d: {0}", d);                                    //   выдаст true, а вывод записывается в параметр
        }                                                                               //   result. Скорее всего это просто обёртка для
        else                                                                            //   метода Parse() (она также не восприимчева к
        {                                                                               //   регистру)
            Console.WriteLine("Failed to convert input \"{0}\" to a double", value);
        }
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDouble()");
    }
    static void SystemBoolean()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemBoolean()\n");


        // Стоит ли говорить что-то о структуре System.Boolean?


        Console.WriteLine("bool.FalseString: {0}", bool.FalseString);   // FalseString - это readonly string поле выдаст "False"
        Console.WriteLine("bool.TrueSTring:  {0}\n", bool.TrueString);  // TrueString - а это хранит (кто бы мог подумать) "True"

        Console.WriteLine("bool.Parse(\"False\"):   {0}\n", bool.Parse("False"));
        //                                                              // Parse() - всё то же, но с int (правда, перегрузок нет). Ну и ещё -
        //                                                              //   этому методу без разницы регистр каждого твоего символа

        if (bool.TryParse("trUe", out bool b))                          // TryParse() - всё то же, что и у double, но с System.Boolean
        {
            Console.WriteLine("TryParse b: {0}\n", b);
        }


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemBoolean()");
    }
}