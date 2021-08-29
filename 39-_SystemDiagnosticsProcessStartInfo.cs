/*
 * creation date  01 aug 2021
 * last change    09 aug 2021
 * author         artur
 */
using System;
using System.Diagnostics;
using System.Linq;

class _SystemDiagnosticsProcessStartInfo
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemDiagnosticsProcessStartInfo();

        Console.ReadLine();
    }
    static void SystemDiagnosticsProcessStartInfo()  //after System.Diagnostics.Process
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemDiagnosticsProcessStartInfo()\n");


        // More about System.Diagnostics.ProcessStartInfo
        //
        //   Одна из версий System.Diagnostics.Process.Start() принимает объект System.Diagnostics.ProcessStartInfo, и из него метод получает
        //     всю нужную информацию для запуска процесса. Вот как это выглядит:
        //
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.WorkingDirectory = @".\";
            cmdStartInfo.FileName = "cmd.exe";
        //
        // Вот частичное определение этого класса (с пояснением неочевидностей):
        //  
        //         public sealed class ProcessStartInfo : object
        //         {
        //             public ProcessStartInfo();
        //             public ProcessStartInfo(string fileName);
        //             public ProcessStartInfo(string fileName, string arguments();
        //             public string Arguments { get; set; }
        //             public bool CreateNoWindow { get; set; }               // CreateNoWindow - если true, процесс не будет создан в новом
        //             public StringDictionary EnvironmentVariables { get; }  //   окне
        //             public bool ErrorDialog { get; set; }                  //
        //             public IntPtr ErrorDialogParentHandle { get; set; }    //
        //             public string FileName { get; set; }                   //
        //             public bool LoadUserProfile { get; set; }              // ErrorDialog - показывать ли сообщение об ошибке, если
        //             public SecureString Password { get; set; }             //   процесс создать не удалось
        //             public bool RedirectStandardError { get; set; }        // ErrorDialogParentHandle - (****насколько я понял) это свойство
        //             public bool RedirectStandardInput { get; set; }        //   для задания родительского окна у диалогового, и его полезно
        //             public bool RedirectStandardOutput { get; set; }       //   указывать, чтобы это диалоговое окно держалось перед ним
        //             public Encoding StandardErrorEncoding { get; set; }    // LoadUserProfile - загружать ли данные из HKEY_USERS (это
        //             public Encoding StandardOutputEncoding { get; set; }   //   может быть user name, password и его domain)
        //             public bool UseShellExecute { get; set; }              //
        //             public string Verb { get; set; }                       //
        //             public string[] Verbs { get; set; }                    //
        //             public ProcessWindowStyle WindowsStyle { get; set; }   //
        //             public string WorkingDirectory { get; set; }           // RedirectStandrad.. - перенаправить ли потоки со стандартных
        //         }                                                          //   stderr, stdin и stdout на те, что выдадут позже свойства
        //                                                                    //   с именами StandardError../StandardOutput.. этого же объекта
        //                                                                    //   (****насколько я понял)
        //                                                                    // UseShellExecute - нужно ли использовать отдельный shell
        //                                                                    //   (в Windows это cmd) для запуска этого процесса? Если true
        //                                                                    //   (это по умолчанию), то файл запустится как-бы из
        //                                                                    //   отдельного shell'а, если false, то из имеющегося (стоит
        //                                                                    //   сказать, что этой командой ты не дабьёшся открытия
        //                                                                    //   полноценного терминала, который можно потрогать или даже
        //                                                                    //   увидеть. если нужен отдельный shell, запускай именно его)
        //                                                                    // Verb - это действие, что будет применятся к файлу, что задан
        //                                                                    //   в свойстве FileName (действие из контекстного меню)(список
        //                                                                    //   действий, что могут применятся
        //                                                                    //   к файлам с таким расширением, ты можешь найти в массиве
        //                                                                    //   Verbs, о котором ниже) (если здесь будет что-то задано, то
        //                                                                    //   в процесс попадёт именно это действие, а не программа
        //                                                                    //   в файле)
        //                                                                    // Verbs - собственно, массив действий, что могу применятся к
        //                                                                    //   такому расширению файлов (ты можешь сам посмотреть на
        //                                                                    //   доступные действия, нажав ПКМ для нужного файла. Это может
        //                                                                    //   быть Edit, Print, Open, Open with и т.д.)
        //  
        //   А вот как запускается наш карманный процесс:
        //
            cmdStartInfo.UseShellExecute = true;
            cmdStartInfo.WindowStyle = ProcessWindowStyle.Minimized; // ..UseShellExecute - если выставить false, то нового окна создано не
        //                                                           //   будет, а cmd.exe  откроется прямо в нашем cmd.exe (выйти из shell'а в
        //                                                           //   shell'е можно введя exit)
        //                                                           // .. = ProcessWindowStyle.Minimized - окно новой cmd появится в свёрнутом
        //                                                           //   виде
            try
            {
                Process cmdProc = Process.Start(cmdStartInfo);                   // ..Process.Start(..) - на этот раз мы пользуемся этой версией
                Console.Write("Hit enter to kill {0}...", cmdProc.ProcessName);  //   метода
                Console.ReadLine();
                cmdProc.Kill();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
            Console.WriteLine();
        //
        //


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemDiagnosticsProcessStartInfo()");
    }
}