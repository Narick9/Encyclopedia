/*
 * date    29 jul 20
 * author  artur
 */
using System;


partial class ClassAndInterface_StructAndOOPAndSystemNullable
{                                                           // ClassAndInterface_StructAndOOPAndSystemNullable - почему я за-partial'ил ещё и
                                                            //   этот класс? Дело в том, что все аспекты полностью заданного имени у
                                                            //   желаемых классов должны быть учтены
                                                            // Пространства имён, кстати, partial'ить не надо (да и нельзя)
    partial class Trolley
    {
        public Trolley() { }
        public Trolley(string _Name, double _Salary)
        {
            trolName = _Name;
            trolWeight = _Salary;
        }

        public void Display()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Salary: {0}", Weight);
        }
    }
}
