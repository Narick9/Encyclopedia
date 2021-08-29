/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionsListANDSystemCollectionsGenericList
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsListANDSystemCollectionsGenericList_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsListANDSystemCollectionsGenericList_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsListANDSystemCollectionsGenericList_Silent()\n");


        // Класс System.Collections.Generic.List<T> (не sealed) - простой список с изменяемым размером. Реализует интерфейсы (из
        //   System.Collections.Generic) IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T> (из
        //   System.Collections) IEnumerable, IList, ICollection


        List<int> myInts = new List<int>(new int[] { 4, 2, 9 });
        myInts.Add(5);                         // ctor(..) - всего 3-и конструктора: стандартный, принимающий int для задания вместимости и
        // myInts: 4, 2, 9, 5                  //   принимающий System.Generic.IEnumerable<T> для копирования элементов из него (последний я
        myInts.AddRange(new int[] { 0, -2 });  //   здесь и использовал)
        // myInts: 4, 2, 9, 5, 0, -2           // System.Collections.Generic.ICollection<T>.Add(T) - добавляет элемент в конец списка
                                               // myInts.AddRange() - добавляет элементы из входного System.Collections.Generic.IEnumerable<T>
                                               //   в конец списка


        List<Person> people = new List<Person>
        {                                                                        // {..} - сокращённая запись многократного вызова Add().
            new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 },  //   Выглядит хорошо
            new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 },
            new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 },
            new Person { FirstName = "Bart", LastName = "Simpson", Age = 8 },
        };
        Console.WriteLine("people amount: {0}\n", people.Count);                 // System.Collections.Generic.ICollection<T>.Count - число
        foreach (Person curr in people)                                          //   элементов в коллекции
        {
            Console.WriteLine(curr);
        }
        Console.WriteLine();
        //
        people.Insert(2, new Person { FirstName = "Maggie", LastName = "Simpson", Age = 2 });
        Console.WriteLine("people amount: {0}\n", people.Count);                 // System.Collections.Generic.IList<T>.Insert() - позволяет
                                                                                 //   вставить элемент в указанную позицию
                                                                                 //
        Person[] peopleArray = people.ToArray();                                 // System.Collections.Generic.List<T>.ToArray() - возвращает
                                                                                 //   массив с тем же содержимым. Этот метод декларируется в
                                                                                 //   классе, а не его интерфейсах (поэтому я написал List...)


        /////////after reading///////////////////////////////////////////////////////////////////////
        List<int> myInts2 = new List<int>(new int[] { 20, 52, 89, 16, 33 });
        List<int> greaterThan50 = myInts2.FindAll((n) => n > 50);  // myInts.FindAll() - этот метод выдаст тебе список всех подходящих
                                                                   //   элементов (как можно и ожидать, для отбора используются функции в
                                                                   //   делегате Predicate<>, проходят только те элементы, на которые дают
                                                                   //   добро все из них)
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsListANDSystemCollectionsGenericList_Silent()");
    }
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Person() { }
        public override string ToString() => $"{FirstName} {LastName} {Age}";
    }
}