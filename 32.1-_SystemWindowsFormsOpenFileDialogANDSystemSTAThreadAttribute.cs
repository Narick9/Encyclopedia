/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemWindowsFormsOpenFileDialogANDSystemSTAThreadAttribute
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemWindowsFormsOpenFileDialogANDSystemSTAThreadAttribute_Silent();

        Console.ReadLine();
    }
    static void SystemWindowsFormsOpenFileDialogANDSystemSTAThreadAttribute_Silent()  //****after multithreading, because
                                                                                      //    System.STAThreadAttribute
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemWindowsFormsOpenFileDialogANDSystemSTAThreadAttribute_Silent()");


        // System.Windows.Forms.OpenFileDialog - объект этого класса может выдать то самое
        //   окно проводника Windows, что предлагает выбрать нужный файл или папку. Может
        //   быть настроен очень гибко (благодаря изобилию разного рода свойств). Не имеет
        //   своих открытых static членов и конструкторов с параметрами
        // System.Windows.Forms - находится в своей отдельной .dll сборке, не в mscorlib.dll


        // InitialDirectory - этим свойством можно задать стартовую
        //   директорию для своего окна (в виде string)
        // Filter - фильтр по типу нужных файлов в виде чего-то вроде
        //   регулярного выражения. Здесь у нас указаны два фильтра
        // FilterIndex - это свойство задаёт фильтр, что будет стоять по
        //   по умолчанию (точнее его порядковый номер). Логика такова:
        //
        //       FilterIndex = -2  -  ..(*.*)
        //       FilterIndex = -1  -  ..(*.*)
        //       FilterIndex = 0   -  ..(*.dll)
        //       FilterIndex = 1   -  ..(*.dll)
        //       FilterIndex = 2   -  ..(*.*)
        //       FilterIndex = 3   -  ..(*.*)
        //


        //OpenFileDialog dlg = new OpenFileDialog();****пишу из-под gnu/linux'а, такого класса у нас нету. поэтому не тестил
        //
        // Атрибут [STAThread] (System.STAThreadAttribute) нужен для диалогового окна открытия файла (метода dlg.ShowDialog()), т.к. все
        //   графические Windows программы однопоточны в плане GUI. Пока достаточно знать, что для работы
        //   с диалоговым окном, в
        //   котором пользователь выбирает файл, метод Main() должен быть помечен атрибутом [STAThread]


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemWindowsFormsOpenFileDialogANDSystemSTAThreadAttribute_Silent()");
    }
}