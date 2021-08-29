/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemChar_SystemString
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemChar();
        SystemString();

        Console.ReadLine();
    }
    static void SystemChar()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemChar()\n");


        // И структуру System.Char ты также много раз видел (или её аналоги в других языках). .NET'овский char в корне отличается от многих
        //   других лишь тем, что изначально хранит 2-байтный UTF-16 юникод


        Console.WriteLine("char.IsDigit('a'):  {0}", char.IsDigit('a'));   // IsDigit() - этот статический метод выдаст true, если символ
        Console.WriteLine("char.IsLetter('a'): {0}", char.IsLetter('a'));  //   окажется цифрой (имеется перегрузка и на string)
        Console.WriteLine("char.IsWhiteSpace(\"Hello there!\", 5): {0}\n", char.IsWhiteSpace("Hello there!", 5));
        //                                                                 // IsLetter() - а этот выдаст true, если символ окажется буквой
        //                                                                 //   (также имеет перегрузку для работы со string)
        //                                                                 // IsWhiteSpace() - а вот как выглядит string версия этих близких
        //                                                                 //   по смыслу методов. IsWhiteSpace() выдаст true, если указанный
        //                                                                 //   индексом символ в строке окажется пробелом (у него имеется
        //                                                                 //   перегрузка для работы с char)

        Console.WriteLine("char.Parse(\"w\"): {0}\n", char.Parse("w"));    // Parse() - то же, что у int, но с char (перегрузок нет)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemChar()");
    }
    static void SystemString()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemString()\n");


        // А вот стандартный класс строк (System.String) в .NET с приколом - они неизменяемы. Да, ты не можешь просто взять и изменить
        //   какой-нибудь символ в ней - придётся создавать новую строку (такие типы называют неизменяемыми. Т.е. System.String - immutable тип
        //   . Запомни это, на собеседывании это может тебя подставить!)
        // Как ты понял, лучше не использовать string там, где нужно часто изменятьобъёмом текста
        //   /////////after reading///////////////////////////////////////////////////////////////////////
        //   // . Для этого был создан System.Text.StringBuilder
        //   /////////////////////////////////////////////////////////////////////////////////////////////
        // Да, как ты заметил, в .NET почему-то все BCL структуры (и System.Object) ведут себя так (хотя, в этом есть и свои плюсы. Длину строк
        //   в string, например, теперь достаточно посчитать лишь однажды)


        string firstName = "Freddy";

        Console.WriteLine("firstName:  {0}", firstName);                         // firstName.ToString() - выдаёт ..ээ.. себя
        Console.WriteLine("firstName has {0} characters.", firstName.Length);    // firstName.Length - выдаёт int с числом символов в строке
        Console.WriteLine("firstName in uppercase:  {0}", firstName.ToUpper());  // firstName.ToUpper() - выдаст новую строку, где все символы
        Console.WriteLine("firstName in lowercase:  {0}", firstName.ToLower());  //   переведены в верхний регистр
        Console.WriteLine("Does firstName contain letter y?:  {0}", firstName.Contains("y"));
        Console.WriteLine("firstName after replace:  {0}\n", firstName.Replace("dy", ""));
        //                                                                       // firstName.ToLower() - выдаст новую строку, где все символы
        //                                                                       //   переведены в нижний регистр
        //                                                                       // firstName.Contains() - выдаст true, если в строке найдётся
        //                                                                       //   вхождение строки параметра value
        //                                                                       // firstName.Replace() - выдаст новую строку, где вхождение
        //                                                                       //   строки oldValue заменено строкой параметра newValue.
        //                                                                       //   Имеется перегрузка для работы с char


        string s1 = "Musya is ";
        string s2 = "the superior";
        string s3 = string.Concat(s1, s2);                                       // Concat() - построит новую строку, сложив две входные. Ещё
        Console.WriteLine(s3);                                                   //   есть оператор +, вызывающий этот же метод. Имеется ещё аж
                                                                                 //   10 версий, работающих с char, object и контейнерами


        string fth = "Hello!";
        string snd = "Yo!";
        Console.WriteLine("fth == snd:  {0}", fth == snd);                       // == - этот оператор также переопеределён в структуре string
        Console.WriteLine("fth == Hello!:  {0}", fth == "Hello!");


        Console.WriteLine("Yeah!.Equals(snd):  {0}", snd.Equals("Yeah!"));       // snd.Equals() - то же, что ==. String - это тоже ссылочный
                                                                                 //   тип, но этот метод для него был переопределен так, чтобы
                                                                                 //   сравнивались значения объектов. Имеется ещё 5 перегрузок
        Console.WriteLine("String.Equals(Youp!, snd):  {0}\n", String.Equals("Youp!", snd));

        s1 = "Hello!";
        s2 = "HELLO!";
        Console.WriteLine("s1:  {0}", s1);
        Console.WriteLine("s2:  {0}", s2);
        Console.WriteLine("s1.Equals(s2, StringComparison.OrdinalIgnoreCase):  {0}",  // System.StringComparison - специально созданное
                          s1.Equals(s2, StringComparison.OrdinalIgnoreCase));         //   перечисление, с которым работают перегруженные
        Console.WriteLine("s1.Equals(s2, StringComparison.InvariantCulture):  {0}",   //   верисии методов, вроде Equals()
                          s1.Equals(s2, StringComparison.InvariantCulture));          //   /////////after reading//////////////////////////////
        Console.WriteLine("s1.IndexOf('E'):  {0}", s1.IndexOf('E'));                  //   // (ещё IndexOf())
                                                                                      //   ////////////////////////////////////////////////////
        Console.WriteLine("s1.IndexOf(\"E\", StringComparison.OrdinalIgnoreCase):  {0}\n",
                          s1.IndexOf("E", StringComparison.OrdinalIgnoreCase));       // IndexOf() - выдаст индекс (в виде int) вхождения этого
        Console.WriteLine("s1.CompareTo(s2): {0}", s1.CompareTo(s2));                 //   символа. Если символ в строку не входит, выдаст -1.
                                                                                      //   Имеется 9-ть версий метода
                                                                                      // CompareTo() - сравнивает объекты, выдвавая индикатор
                                                                                      //   (типа int) места после сортировки (-1 - первый
                                                                                      //   объект должен быть левее, 0 - они одинаковы, 1 -
                                                                                      //   первый объект должен быть правее)


        string formattedString = string.Format("You have {0}", 1442);  // string.Format() - это стандартный тип для форматирования строк. Как
        Console.WriteLine($"{formattedString}\n");                     //   ты видишь, большинство версий System.Console.Write()/WriteLine() -
                                                                       //   это всего лишь оболочки над string.Format() (но это не точно).
                                                                       //   Символы для форматирования (вроде {0:d}) также присутствуют. Python
                                                                       //   , кстати, тоже пользуется таким видом форматирования


        string hello = string.Join("; ", new char[] { 'h', 'e', 'l', 'l', 'o' });
        // Output: "h; e; l; l; o"  // string.Join() - принимает резделитель separator и массив чего-то там, а затем соединяет строковое
        //                          //   представление этих элементов, разделяя их separator'ом


        string[] symbols = "r r".Split(' ');  // "r r".Split() - возвращает куски данной строки в виде string массива, разделённые
                                              //   определёнными символами (есть 6-ть версий)


        "rrrrffff".Substring(3);
        // Выдаст: "rffff"        // "rrrrffff".Substring() - этот метод выдаст кусок строки (т.е. субстроку) своего объекта, что
        //   начинается с заданного индекса (если startindex оказывается равен длине строки, то ты
        //   получишь string.Empty. Ещё есть перегрузка для задания длины выходного куска


        "rrffffttt".IndexOf('t');  // "rrffffttt".IndexOf() - а этот метод вернёт тебе индекс первого вхождения данного char (или string в
                                   //   большинстве других версиях. Всего 9 версий)


        "vvvvvv".StartsWith('v');  // "vvvvvv".StartsWith() - название говорит само за себя (т.е. метод вернёт true, только если такая-то
                                   //   строка начинается с такого-то символа). Имеется 4 версии - 1-а для char и 3-и для string


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemString()");
    }
}