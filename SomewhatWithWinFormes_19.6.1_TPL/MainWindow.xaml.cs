using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;    // using.. - я не знаю что это, само приползло (если после чего-то незнакомого нет моего коммантия - значит
                                  //   оно добавилось автоматически во время создания проекта)
                                  // System.Windows.Shapes - в нашем коде используется класс Path из System.IO, но это пространство также
                                  //   хранит в себе класс Path. Возникает неоднозначность, и выражение using ..Shapes было убрано
using System.Drawing;             // using .. - а это мой вручную набранный блок
using System.Threading;           // System.Drawing - находится в отдельной сборке (System.Drawing.dll), что нужно подключить вручную
using System.IO;

namespace _19._6._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancelToken = new CancellationTokenSource();  // cancelToken - это наш токен, через который мы будем
                                                                                      //   общаться с второстепенными потоками. Гораздо более
                                                                                      //   полное объяснение находится дальше в этом файле
        public MainWindow()
        {
            InitializeComponent();
        }
        private void cmdCancel_Click(object sender, EventArgs e)   // cmdCancell_Click() - разметка в MainWindow.xaml имеет кнопку, клик
        {                                                          //   которой запустит этот метод
            cancelToken.Cancel();                                  // cancelToken.Cancel() - этим методом мы отправляем сигнал второстепенным
        }                                                          //   потокам о том, что им пора остановиться. Большая часть инфо - ниже
        private void cmdProcess_Click(object sender, EventArgs e)  // cmdProcess_Click() - вторая кнопка (с именем cmdProcess) запустит это
        {                                                          //   метод
            Task.Factory.StartNew(() =>                            // System.Threading.Tasks.Task - можно считать, что этот класс - простая
                {                                                  //   альтернатива асинхронным методам. Это не статический класс с небольшой
                                                                   //   статической и нестатической частью
                    lock (cancelToken)                             // ..Task.Factory - это статической свойство предоставит тебе фабрику
                    {                                              //   ..TaskFactory, что предназначена для создания и
                        ProcessFiles();                            //   планирования задач типа ..Task (это класс небольшого размера). 
                        cancelToken = new CancellationTokenSource();
                    }  // lock - в методе ProcessFiles()           // ..Task.Factory.StartNew() - именно этот метод создаёт новый поток и
                });    //   использованы некоторые типы, у которых //   запускает его на выполнение задачи. В этой перегрузке (а их ещё 15)
        }              //   аллергия на многопоточность в          //   нужно лишь передать делегат Action (с сигнатурой void ()). Здесь было
                       //   некоторых случаях, поэтому лучше       //   использовано подходящее лямбда-выражение
                       //   старотовать этот метод в синхронной    // () => ProcessFiles() - зачем мы запускаем нашу логику обработки картинок
                       //   менере                                 //   в отдельном потоке? Это сделано потому, что в нём просто используется
                       // cancelToken = new.. - забегая наперёд,   //   метод, который стопит поток. Мы не можем допустить, чтобы наш
                       //   скажу, что эти токены - одноразовые, и //   единственный UI поток долго зависал на одном месте
                       //   повторно не используются. И да, эту    //
                       //   операцию бессмысленно запускать чаще,  //
                       //   чем со стартом нового потока. А ещё на //
                       //   ссылке в cancelToken завязан оператор  //
                       //   lock {}                                //
        private void oldProcessFiles()  // oldProcessFiles() - автор решил сначала реализовать всё без привлечения подолнительных потоков. Код
        {                               //   в этом методе приведёт к тому, что окно (только его, кстати, и видит пользователь) просто
                                        //   перестанет реагировать (наш поток будет занят обработкой картинок)
            string[] files = Directory.GetFiles(@".\72_xmen_wolverine\");
            string targetDir = @".\ModifiedPictures";  // System.IO.Directory - это статический класс (размером больше среднего) для работы с
            Directory.CreateDirectory(targetDir);      //   директориями
                                                       // Directory.GetFiles() - этот метод выдаст массив с полными именами файлов. У него есть
                                                       //   ещё 2-е довольно простых перегрузки
                                                       // Directory.CreateDirectory() - создаём папку. Этот метод имеет ещё одну версию, что
                                                       //   позволяет указать режим доступа к создаваемой директории

            foreach (string curr in files)
            {
                string filename = Path.GetFileName(curr);                      // System.IO.Path - небольшой статический класс для файловых
                using (Bitmap currBitmap = new Bitmap(curr))                   //   путей
                {                                                              // Path.GetFileName() - это метод, что выдаст нам имена файлов
                    currBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);   //   без путей к ним
                    currBitmap.Save(Path.Combine(targetDir, filename));        // using () {} - помнишь конструкцию using? Если нет, то напомню
                    // System.Drawing.Bitmap - это класс для работы с GDI+     //   , что в её круглых скобках помещается какой-нибудь объект,
                    //   биткартами (Graphics Device Interface, один из трёх   //   а после прохождения блока кода автоматически вызывается его
                    //   основных компонентов GUI в винде, кстати), т.е. с     //   метод Dipspose() (что, как мы помним, должен освободить
                    //   несжатыми картинками. Т.к. GDI - вещь древняя,        //   неуправляемые ресурсы)(т.е. тип объекта должен поддерживать
                    //   объекты этого типа содержат неуправляемые данные (и   //   System.IDisposable)
                    //   поэтому он поддерживает System.IDisposable)           //
                    // currBitmap.RotateFlip() - простенький метод, что может  //
                    //   повернуть или отразить картинку в объекте (или всё    //
                    //   сразу). Нужно лишь выбрать нужное поле перечисления   //
                    //   System.Drawing.RotateFlipType. Перегрузок нет         //
                    // Path.Combine() - в обычном случае этот метод просто     //
                    //   соединяет строки, но если во второй строке уже        //
                    //   записан полный путь, метод вернёт его. Имеется ещё 3  //
                    //   перегрузки                                            //
                    // currBitmap.Save() - сохраняет картинку на hdd. Имеет 5  //
                    //   версий                                                //

                    this.Title = $"Processing {filename} in thread {Thread.CurrentThread.ManagedThreadId}";
                                                                               // this.Title =.. - да, заголовок окна можно менять постоянно
                                                                               //   через это свойство (это часто делают всякие загрузщики)
                                                                               // {..ManagedThreadId} - т.к. у нас только один поток, всегда
                                                                               //   будет выводиться 1
                }
            }
        }
        private void ProcessFiles()                           // ProcessFiles() - это уже действующий метод, т.к. в нём применена TPL
        {
            ParallelOptions parOpts = new ParallelOptions();  // ParallelOption - очень мелкий класс, содержащий 3-и свойства и стандартный
            parOpts.CancellationToken = cancelToken.Token;    //   конструктор. Его цель в том, чтобы ты мог подстроить поведение
                                                              //   цикла для распараллеленых потоков в метода классов TPL под свои нужды (он
                                                              //   посылается прямо в эти методы)(этакая коробка с инструкциями). По умолчанию
                                                              //   эти распараллеливающие методы будут пытаться использовать все процессоры,
                                                              //   циклы в их потоках неотменяемы (стандартезированным способом), а также они
                                                              //   задействуют стандартный TaskScheduler
                                                              // parOpts.CancellationToken - это свойство типа структуры CancellationToken (что
                                                              //   описывает просто токен, т.е. индикаторы) нужно для того, чтобы через него
                                                              //   наш UI поток затем мог сообщить наплодившимся второстепенным потокам то, что
                                                              //   им прямо сейчас нужно остановиться (ты посылаешь набор parOpts с одним и тем
                                                              //   же токеном CancellationToken во все наплодившиеся потоки, заставляешь их
                                                              //   циклически проверять этот токен, и затем, когда настанет время, ты изменишь 
                                                              //   одно поле токена, заставив остановиться второстепенным потокам). Таков
                                                              //   стандартизированный подход (на самом деле объекты ParallelOptions принимают
                                                              //   только распараллеливающие методы ..Parallel.ForEach() и ..Parallel.For().
                                                              //   В остальных случаях тебе следует отправлять токен напрямую, не в наборе)
                                                              // = cancelToken.Token - как ты понял, токен - это продвинутый индикатор. Тем не
                                                              //   менее, сам себя он переключить не может (его основное свойство
                                                              //   IsCancellationRequested является get-only). Для управления им нужна фабрика,
                                                              //   что его произвела. Эта фабрика - класс CancellationTokenSource (и это наше
                                                              //   поле cancelToken). Именно у неё находотися метод Cancel(), что переключит
                                                              //   токен в режим stop-machine. Сделано это для того, чтобы никто точно не смог
                                                              //   бы управлять полученным токеном (ты бы не хотел, чтобы какой-то метод
                                                              //   случайно переключил отправленный тобой на чтение токен, который в данный
                                                              //   момент также завязан на армии второстепенных потоков, верно? Фабрика - это
                                                              //   пульт управления токеном, и она должна быть только у тебя)


            string[] files = null;
            try     // try {} - также здесь я решил добавить проверку на наличие папки
            {
                files = Directory.GetFiles(@".\72_xmen_wolverine\", "*.jpg", SearchOption.TopDirectoryOnly);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                this.Dispatcher.Invoke((Action)delegate { this.Title = ex.Message; });
            }
            
            string targetDir = @".\ModifiedPictures\";        // Direcotry.GetFiles(..) - это самая всеобъемливающая из трёх версий метода.
            Directory.CreateDirectory(targetDir);             //   Принимает она строку с путём к директории, простенький шаблон для
                                                              //   отфильтровывания подходящих файлов (но регулярные выражения не поддерживает)
                                                              //   и значение перечисления SearchOption (в нём всего два поля)

            try
            {
                System.Threading.Tasks.Parallel.ForEach(files, parOpts, curr =>    // ..Parallel.Foreach() - чтобы ускорить процесс обработки,
                    {                                                              //   будет применён этот распаралеливающий статический метод
                        parOpts.CancellationToken.ThrowIfCancellationRequested();  //   Мы используем ту его версию, что принимает первым
                        // parOpts.CancellationToken - в этом анонимном делегате   //   параметром (source) некий объект, что реализует
                        //   мы будем использовать наш набор инструкций parOpts,   //   IEnumerable<T>, вторым (parallelOptions) - объект с
                        //   чтобы получить доступ к токену. На самом деле мы      //   настройками типа ParallelOptions, а третьем (body) -
                        //   здесь могли использовать токен напрямую из            //   стать делегат Action с сигнатурой void (T) (как минимум
                        //   cancelToken                                           //   в этой версии. Альтернатив много). Весьмя значимая
                        // ..Parallel.ForEach(..parOPts..) - по правде говоря, нам //   особенность распараллеливающих методов (..For и
                        //   не нужно проверять наш токен вручную. Эта версия      //   ..Foreach) в том, что поток их запускающий также встаёт
                        //   распараллеливающего цикла сама умеет обращаться с     //   на в нём, т.к. он должен дождаться их завершения.
                        //   parOpts                                               //   Именно из-за этого приложение запускает ProcessFiles()
                        // ..CancellationToken.ThrowIfCancellationRequestad() -    //   в отдельном потоке
                        //   вообще, основным членом токенов CancellationToken     //
                        //   (как уже говорилось) ялвяется свойство                //
                        //   IsCancellationRequested говорящее, не было ли         //
                        //   команды "отмена" от фабрики этого токена. Автор       //
                        //   решил использовать метод токена, что сразу кидает     //
                        //   исключение после проверки свойства (выбросится        //
                        //   OperationCanceledException, и выбростися к нам)       //

                        using (Bitmap currBitmap = new Bitmap(curr))
                        {
                            string currFilename = Path.GetFileName(curr);
                            currBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            currBitmap.Save(Path.Combine(targetDir, currFilename));   // currBitmap.Save() - из-за этого метода мы как раз и
                                                                                      //   сделали вызов метода ProcessFiles() синхронным. Если
                                                                                      //   метод Save() в разных потоках одновременно
                                                                                      //   попытается сохранить один и тот же файл (по имени,
                                                                                      //   видимо), VS выдаст
                                                                                      //   System.Runtime.InteropServices.ExternalException:
                                                                                      //   'A generic error occurred in GDI+ ..'


                            this.Dispatcher.Invoke((Action)delegate  // this.. - как и было сказано, элементы управления графичекого окна
                            {                                        //   привязаны к потоку, в котором они создавались. Одним из лучших
                                                                     //   решений (не прибегая к async и await) является использование объекта
                                                                     //   специально созданного для такой проблемы
                                                                     //   System.Windows.Threading.Dispatcher (он имеется у всех классов, что
                                                                     //   имеют в своей ветке родителей класс
                                                                     //   System.Windows.Threading.DispatcherObject. Т.к. это абстрактный класс
                                                                     //   , он всегда стоит на верине древа. System.Windows.Windows является
                                                                     //   им)
                                                                     // this.Dispatcher.Invoke() - этот метод подставляет код в делегате типа
                                                                     //   Action (что мы отправляем) во внутреннюю очередь. Поток, отвечающий
                                                                     //   за GUI, постоянно заглядывает в эту очередь, выполняя эти функции
                                Title = $"Processing {currFilename} on thread {Thread.CurrentThread.ManagedThreadId}";
                            });
                        }
                    }
                );
                this.Dispatcher.Invoke((Action)delegate { this.Title = "Done!"; });     // this.Dispatcher.Invoke(..Title=..) - метод
            }                                                                           //   ProcessFiles() всё-таки выполняется также рабочим
            catch (OperationCanceledException ex)                                       //   потоком
            {
                this.Dispatcher.Invoke((Action)delegate { this.Title = ex.Message; });  // ex.Message - выдаст нам "The operation was canceled"
            }
        }
    }
}
