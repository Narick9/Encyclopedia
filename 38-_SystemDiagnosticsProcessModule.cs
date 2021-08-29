/*
 * creation date  01 aug 2021
 * last change    01 aug 2021
 * author         artur
 */
using System;
using System.Diagnostics;
using System.Linq;

class _SystemDiagnosticsProcessModule
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDiagnosticsProcessModule();

        Console.ReadLine();
    }
    static void SystemDiagnosticsProcessModule()  //after SystemDiangosticsProcess
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDiagnosticsProcessModule()\n");


        // Using
        //
            void ListOfModules(int pid)  // ListOfModules() - выводит имена модулей (типа .dll или .exe), загруженных в процесс с этим pid
            {
                Process process;
                try               // try - если запущенного процесса с таким pid не имеется, сообщить об этом пользователю
                {
                    process = Process.GetProcessById(pid);
                }
                catch (ArgumentException ex)
                {
                    Console.Write("{0}\n\n", ex.Message);
                    return;
                }

                Console.WriteLine($"Mudules that the process with pid {pid} (is {process.ProcessName}) has:");
                foreach (ProcessModule curr in process.Modules)               // process.Modules - свойство (только для чтения)
                {                                                             //   выдаст нам коллекцию типа ProcessModuleCollection сюда
                    Console.WriteLine($"-> Module Name: {curr.ModuleName}");  //   войдут модули .NET, COM и написанные на Си
                }                                                             // Как оказалось, есть процессы (System,
                Console.WriteLine();                                          //   например), что не выдают свои модули
            }                                                                 //   (генерируется System.ComponentModel.Win32Exception)
        //                                                                    //
            Console.Write("Your pid: ");
            int thePid = int.Parse(Console.ReadLine());
            ListOfModules(thePid);  // thePid - если ты отправишь на исследование pid этого приложения, то увидишь, что даже в нём
                                    //   используется очень даже внушительный список модулей
        //                          //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcessModule()");
    }
}