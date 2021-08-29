/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.Generic;

class _SystemCollectionsGenericDictionary
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsGenericDictionary_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsGenericDictionary_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsGenericDictionary_Silent()\n");


        // Класс System.Collections.Generic.Dictionary<T,Y> (не sealed) - это обобщённая коллекция ключей и значений. Реализует (из
        //   System.Collections.Generic) IDictionary<T,Y>, ICollection<KeyValuePair<T,Y>>, IEnumerable<KeyValuePair<T,Y>>,
        //   IReadOnlyDictionary<T,Y>, IReadOnlyCollection<KeyValuePair<T,Y>>, (из System.Collections) IEnumerable, IDictionary, ICollection
        /////////after reading///////////////////////////////////////////////////////////////////////
        //   (из System.Runtime.Serialization) ISerializable, IDeserializationCallback
        /////////////////////////////////////////////////////////////////////////////////////////////


        Dictionary<string, uint> numberStationsA = new Dictionary<string, uint>  // System.Collections.Generic.Dictionary<T,Y> - метод Add()
        {                                                                        //   имеется
            ["HM01"] = 13435,                                                    // ctor() - всего 6 конструкторов (здесь мы пользуется
            ["E11"] = 4505,                                                      //   стандартным, но ()-ки опущены)
            ["E07a"] = 11123,                                                    // {..} - специально для словарей ввели более
        };                                                                       //   удобный синтаксис инициализации словаря
                                                                                 //   (он уже отличается от синтаксиса инициализаций
        Console.WriteLine("A: E11: {0}", numberStationsA["E11"]);                //   для массивов и коллекций)(думаю, здесь также
                                                                                 //   вовлечён Add())
        Dictionary<string, uint> numberStationsB = new Dictionary<string, uint>  //
        {                                                                        // {{..}, ...} - но можно использовать и обычный синтаксис
            { "XPA2", 9317 },                                                    //   инициализаци коллекций. Вложенные {}-ки - это синтаксис
            { "F06a",  18276 },                                                  //   синтаксис инициализации массивов (как говорит VS), и да,
            { "V07",  13893 },                                                   //   его можно применять и в других коллекциях. Я думаю, что
        };                                                                       //   так строится массив агрументов для передачи в метод Add()
        Console.WriteLine("B: F06a: {0}\n", numberStationsB["F06a"]);            //   (и в другие функции)
                                                                                 //
        try                                                                      //
        {                                                                        //
            numberStationsB.Add("V07", 14693);                                   // Add() - если ты попытаешься добавить
        }                                                                        //   новый объект с уже имеющимся ключом
        catch (ArgumentException ex)                                             //   , сгенерируется исключение
        {                                                                        //   System.ArgumentException
            Console.WriteLine("Error!: {0}", ex.Message);
        }
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsGenericDictionary_Silent()");
    }
}