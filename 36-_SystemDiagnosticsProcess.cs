/*
 * creation date  01 aug 2021
 * last change    01 aug 2021
 * author         artur
 */
using System;
using System.Diagnostics;
using System.Linq;

class _SystemDiagnosticsProcess
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDiagnosticsProcess();

        Console.ReadLine();
    }
    static void SystemDiagnosticsProcess()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDiagnosticsProcess()\n");


        // Общее описание ты найдёшь в методе SystemDiagnosticsNamespace


        // System.Diangostics.Process
        //
        //   Класс System.Diagnostics.Process (ныне частично находится в System.Diagnostics.Process.dll) создан для хранения инфо об одном
        //     конкретном процессе, что выполняются на заданной машине (может
        //     быть локальная или удалённая)(в отличие от System.Relfection.Assembly, это не abstract класс). Также в нём есть методы для
        //     программного запуска и завершения процесса, просмотра (или модификации) уровня его приоритета и получения списка активных
        //     потоков и загружаемых модулей. Вот основные свойства класса System.Diagnostics.Process:
        //  
        //         > ExitTime         - это свойство (с полем типа DateTime) выдаст тебе метку времени, когда процесс был завершён
        //         > Handle           - возвращает дескриптор (типа IntPtr), что был назначен процессу операционной системой (вообще, это
        //                              просто номер канала для данных в ядре оси, вроде 0, 1 и 2, или stdin, stdout и stderr в Си). Это
        //                              свойство может быть полезно, если твоё приложение задействует неуправляемый код (и ему нужен указатель
        //                              на процесс)
        //         > Id               - это PID связанного (с объектом) процесса
        //         > MachineName      - имя компьютера, на котором запущен этот процесс
        //         > MainWindowTitle  - этим получают заголовок главного окна процесса. Если у процесса нет окна - возвращается пустая
        //                              строка
        //         > Modules          - предоставляет доступ к экземпляру ..ProcessModuleCollection, хранящего загруженные модули процесса
        //         > ProcessName      - это имя процесса (а имя процесса - это имя самого приложения)
        //         > Responding       - это свойство с объектом bool указывает, реагирует ли пользовательский интерфейс процесса на ввод
        //                              пользователя (или этот процесс в зависшем состоянии?)
        //         > StartTime        - свойтсво с отметкой времени (типа System.DateTime), когда процесс был запущен
        //         > Threads          - это свойство с набором потоков процесса (в объекте System.Diagnostics.ProcessThreadCollection)
        //
        //     Помимо этих свойств, как и говорилось, есть достаточно много разнообразных методов по управления над процессов. Вот
        //     некоторые избранные из них:
        //  
        //         > CloseMainWindow()    - этот метод посылает сигнал на закрытие процесса, который содержит окно с пользовательским
        //                                  интерфейсом  ****может это неточный перевод?
        //         > GetCurrentProcess()  - этот статический метод возвращает объект Process, что представляет текущий процесс твоей проги
        //         > GetProcesses()       - тоже статический метод. Возвращает он массив процессов, что активны сейчас (в одной из версий метода
        //                                  можно задать имя машины)
        //         > Kill()               - метод, немедленно останавливающий процесс
        //         > Start()              - этот метод (есть статический и нет) запускает процесс
        //


        // Using
        //
            void ListOfRunningProcesses()  // ListOfRunningProcesses - выводит PID и имя процессов, что сейчас запущены в текующей системе
            {
                Console.WriteLine("These processes are running just now:");

                var runningProcesses = from curr in Process.GetProcesses(".") orderby curr.Id select curr;
                foreach (Process curr in runningProcesses)                                   // ..Process.GetProcesses(".") - точка означает для
                {                                                                            //   этого метода локальный компьютер. Вторая
                    Console.WriteLine($"-> PID: {curr.Id}\tName: {{0}}", curr.ProcessName);  //   перегрузка (что без параметров) тоже создана
                }                                                                            //   для этого

                Console.WriteLine();  // {{..}} - напомню, что этим мы сделаем {} в этой $"" строке (да, здесь это совершенное ненужно.
            }                         //   just for lulz)

            ListOfRunningProcesses();
        //                                                             //
        //                                                             //
            try                                                        //
            {                                                          //
                Process theProcess = Process.GetProcessById(987);      // ..Process.GetProcessById() - этот static метод выдаст тебе экзмемпляр
                Console.WriteLine("We have a process with 987 PID!");  //   процесса по его PID (есть версия и с указанием машины)
            }                                                          //
            catch (ArgumentException ex)                               // System.ArgumentException - если процесса с таким PID нет,
            {                                                          //   сгенерируется это исключение
                Console.WriteLine(ex.Message);                         //
            }                                                          //
        //                                                             //
        //                                                             //
        //
        //
        //
        //
        //   Наконец, мы попробуем запустить новый процесс, а затем завершить его с помощью статических методов Start() и Kill()
        //   Как говорит автор, чтобы запускать новые процессы, среда VS должна быть запущена от имени учётной записи администратора, иначе
        //     же возникнет ошибка времени выполнения (причём здесь VS? просто это VS запускает твою приложуху при нажатии F5. права
        //     администратора, видимо передаются по наследству)(у меня с VS 2019 без таких условий ошибки не происходит)
        //
            try
            {
                Process edgeProc = Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                        "http://englishstudypage.com/vocabulary/detailed-irregular-verbs-list/");
                Console.Write("Hit enter to kill {0}...", edgeProc.ProcessName);
                Console.ReadLine();
                edgeProc.Kill();
            }                        // ..Process.Start() - этот static метод имеет 6 перегрузок и эта версия (препдоложу я) использует те
        //                           //   же функции из WinAPI, что и cmd (это подтверждает тот факт, что ..Process.Start() поддерживает PATH)
        //                           // System.Diagnostics.Process.Start(..) - ты, возможно, вспомнишь, что тем же занималась функция system()
        //                           //   из Си
            catch (System.ComponentModel.Win32Exception ex)
            {                                          // ..Win32Exception - в ходе работ может быть несколько траблов. Этот catch отлавливает
                Console.WriteLine(ex.Message);         //   те, что связаны с системой пользователя (например, у него может и не быть
            }                                          //   Microsoft Edge'а)
            catch (InvalidOperationException ex)       // System.InvalidOperationException - а этот catch отловит исключения, связанные с
            {                                          //   состоянием
                                                       //   объекта. Например, если метод edgeProc.Kill() пытается завершить уже завершённый
                Console.WriteLine("{0}", ex.Message);  //   процесс, создастся именно оно. Если у пользователя этот браузер уже открыт, оно
            }                                          //   также произойдёт, ведь msedge.exe построен так, чтобы в таких случаях просто
            finally                                    //   отправить запрос на открытие страницы к уже имеющемуся процессу браузера. Как только
            {                                          //   это будет сделано, наш процесс завершится сам
                Console.Write("\n");
            }
        //
        //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcess()");
    }
}