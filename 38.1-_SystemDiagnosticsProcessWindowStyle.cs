/*
 * creation date  01 aug 2021
 * last change    01 aug 2021
 * author         artur
 */
using System;

class _SystemDiagnosticsProcessWindowStyle
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDiagnosticsProcessWindowStyle_Silent();

        Console.ReadLine();
    }
    static void SystemDiagnosticsProcessWindowStyle_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDiagnosticsProcessWindowStyle_Silent()\n");


        // System.Diagnostics.ProcessWindowStyle - это перечисление для
        //   задания того, как откроется окно процесса (в плане
        //   размера). Там всего 4 режима: Normal = 0, Hidden = 1,
        //   Minimized = 2, Maximized = 3


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcessWindowStyle_Silent()");
    }
}