/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Text;

class _SystemStringComparison_SystemTextStringBuilder
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");
                
        SystemStringComparison_Silent();
        SystemTextStringBuilder();

        Console.ReadLine();
    }
    static void SystemStringComparison_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemStringComparison_Silent()\n");


        // System.StringComparison

        //****обрывок (скопированный) откуда-то из энциклопедии:
        //if (string.Equals("Q", input, StringComparison.OrdinalIgnoreCase))
        //{
        //    break;                                                          // System.StringComparison - не стоит забывать про это
        //}                                                                   //   маленькое, но полезное перечисление


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemStringComparison_Silent()");
    }
    static void SystemTextStringBuilder()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemTextStringBuilder()\n");


        // Запечатанный класс System.Text.StringBuilder как раз и создан для хранения изменяемой строки. На самом деле он создан с расчётом на
        //   то, что в нём будут хранить ооочень много текста. По умолчанию объект этого класса хранит 16 символов (даже если ты дал ему строку
        //   меньшего размера), но при необходимости это хранилище автоматически расширяется. Вместимость началоньго буффера можно также задать
        //   через аргумент одного из конструкторов


        StringBuilder sb = new StringBuilder("**** Fantastic Games ****");
        sb.Append('\n');                   // new StringBuilder() - всего есть 6-ть конструкторов. Конкрентно этот задаёт значение
        sb.AppendLine("Half Life");        //   новоиспечённому StringBuilder'у из заданной string строки
        sb.AppendLine("Portal 2");         // sb.Append() - добавляет к тексту заданный объект. Имеется аж 20-ть версий этого метода
        sb.AppendLine("Prey " + "2");      // sb.AppendLine() - добавляет строку плюс '\n'. Имеет всего 2-е версии (2-ая просто добавляет '\n')
        Console.WriteLine(sb.ToString());  // ToString() - выдаёт внутренний текст в виде объекта string
        sb.Replace("2", "2017", 51, 2);    // Replace() - позволяет заменять часть строки. Здесь использована версия с 4-мя параметрами. 3-й
        Console.WriteLine(sb);             //   параметр задаёт индекс, с которого начинается поиск совпадения, а 4-й параметр обозначает
                                           //   дальность поиска. Есть ещё 3 перегрузки


        StringBuilder someChars = new StringBuilder("rrrrfffff");
        someChars.Remove(startIndex: 4, length: 5);  // someChars.Remove() - этот метод удаляет начиная с такого индекса столько-то символов из
                                                     //   строки


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemTextStringBuilder()");
    }
}