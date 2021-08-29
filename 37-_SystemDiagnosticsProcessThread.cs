/*
 * creation date  01 aug 2021
 * last change    01 aug 2021
 * author         artur
 */
using System;
using System.Diagnostics;
using System.Linq;

class _SystemDiagnosticsProcessThread
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDiagnosticsProcessThread();

        Console.ReadLine();
    }
    static void SystemDiagnosticsProcessThread()
    {
        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcessThread()");


        // Общее описание ты найдёшь в методе SystemDiagnosticsNamespace


        // More about System.Diagnostics.ProcessThread
        //
        //   Конечно, помимо Id, StartTime и PriorityLevel тип ProcessThread содержит ещё много чего другого. Вот самые интересные его члены
        //     (и все они - свойства):
        //
        //     > CurrentPriority     - позволяет получить текущий приоритет потока (get-only)
        //     > Id                  - выдаст уникальный идентефикатор потока (также только для чтения)
        //     > IdealProcessor      - это set-only свойство устанавилвает предпочитаемый (идеальный) процессор для его потока
        //     > PriorityLevel       - этим можно задать и узнать уровень приоритета потока
        //     > ProcessorAffinity   - свойство для задания процессоров, на которых может выполнятся этот поток
        //     > StartAddress        - get-only свойство для получения адреса памяти (типа IntPtr), по которому лежала стартовая функция этого
        //                             потока
        //     > StartTime           - тоже get-only свойство, выдаёт объект DateTime с отметкой времени запуска
        //     > ThreadState         - свойство (тоже { get; }) для получения текущего состояния потока (в виде перечисления
        //                             System.Diagnostics.ThreadState)
        //     > TotalProcessorTime  - создано для получения объекта типа System.TimeSpan с инфо о общем времени, что процессор потратил на
        //                             поток. get-only
        //     > WaitReason          - { get; } свойство для получения причины, по которой поток находятся в состоянии ожидания (в виде
        //                             значения перечисления System.Diagnostics.ThreadWaitReason
        //
        //   Следует уяснить, что класс System.Diagnostics.ProcessThread не создан для полного манипулирования (создания, приастоновки,
        //     уничтожения) потоков (возможность убивать потоки других процессов была бы странной). Это именно диагностическое средство для
        //     получения инфо о потоке процесса (Windows процесса)
        //     /////////after reading:System.Threading//////////////////////////////////////////////////////
        //     // О том, как манипулировать потоками мы поговорим, когда дойдём до System.Threading
        //     /////////////////////////////////////////////////////////////////////////////////////////////


        // Using
        //
        //                                                             //
        //                                                             //
            void ListOfThreads(int pID)  // ListOfThreads() - эта функция выводит id, время старта и приоритет каждого потока в процессе с этим
            {                            //   PID
                Process process;
                try               // try - проверка на то, есть ли вообще такой процесс
                {
                    process = Process.GetProcessById(pID);
                }
                catch (ArgumentException ex)
                {
                    Console.Write("{0}\n\n", ex.Message);
                    return;
                }
        //      //
                Console.WriteLine("-=---");
                Console.WriteLine($"Thread of process with {pID} PID (is {process.ProcessName}):");
                foreach (ProcessThread curr in process.Threads)  // process.Threads - свойство, что выдаст коллекцию типа
                {                                                //   System.Diagnostics.Process.ThreadCollection
                    Console.WriteLine($"-> Thread ID: {curr.Id}\tStart Time: {curr.StartTime}\tPriority: {curr.PriorityLevel}");
                }                     // curr.. - да, экземпляры ProcessThread тоже много рассказать о себе достаточно много (но гораздо меньше
                Console.WriteLine();  //   , чем объекты Process)
            }                         // Как оказалось, есть процессы, доступ к которым запрещён (набрасывается
        //                            //   System.ComponentModel.Win32Exception)
            Console.Write("PID of process you want: ");
            string input = Console.ReadLine();
            ListOfThreads(int.Parse(input));  // ListOfThreads() - теперь пользователь может ввести PID любого процесса и получить инфо о всех
        //                                    //   потоках в нём
        //                                    //
        //                                    //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcessThread()");
    }
}