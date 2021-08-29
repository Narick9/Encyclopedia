/*
 * creation date  24 jan 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections;
using System.Collections.Generic;

class SystemVersion_SystemOperationSystemANDSystemPlatformID
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemVersion_Silent();
        SystemOperationSystemANDSystemPlatformID_Silent();

        Console.ReadLine();
    }
    static void SystemVersion_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemVersion_Silent()");


        // System.Version

        /////////after_reading:SystemReflectionAssembly//////////////////////////////////////////////
        //****копи из метода про System.Reflection.Assembly
        //    myAsmName.Version = new Version("1.0.0.1");      // Version - тоже очень простой класс
        //    try                                              //   . Как говорит автор, он часто
        //                                                     //   используется в паре с объектами
        //                                                     //   AssemblyName
        /////////////////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemVersion_Silent()");
    }
    static void SystemOperationSystemANDSystemPlatformID_Silent()
    {
        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemOperationSystemANDSystemPlatformID_Silent()\n");


        // Класс System.OperationSystem - мелкий запечатанный класс, объекты которого будут хранить аж несколько сведений о твоей оси (одной из
        //   заранее определённых)(что за система, версия, версия в виде string и сервис пак)
        /////////after reading///////////////////////////////////////////////////////////////////////
        //  Реализует System.Runtime.Serialization.ISerializable, System.ICloneable
        /////////////////////////////////////////////////////////////////////////////////////////////


        OperatingSystem unix = new OperatingSystem(PlatformID.Unix, new Version(11, 4, 0, 0));
                                                                      // ctor() - конструтор всего один, и именно в нём ты и задаёшь начинку
                                                                      //   объекта (у объектов будут только { get; } свойства)
                                                                      // System.PlatformID - мелкое перечисление. Вот всё, что в нём есть:
                                                                      //       public enum PlatformID
                                                                      //       { 
                                                                      //           Win32S = 0, 
                                                                      //           Win32Windows = 1, 
                                                                      //           Win32NT = 2,
                                                                      //           WinCE = 3,
                                                                      //           Unix = 4,
                                                                      //           Xbox = 5,
                                                                      //           MacOSX = 6
                                                                      //       }
        unix.ToString();                                              // unix.ToString() - этот метод переопределён на выдачу названия оси.
                                                                      //   Cтранно, но для Unix'а выдаёт    <unknown> 11.4    . Для win'а
                                                                      //   выдаётся нормальное полное имя системы
        Console.WriteLine(unix.Platform);                             // unix.Platform - выдаст утсановленное значение перечисления
                                                                      //   System.PlatformID


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemOperationSystemANDSystemPlatformID_Silent()\n");
    }
}