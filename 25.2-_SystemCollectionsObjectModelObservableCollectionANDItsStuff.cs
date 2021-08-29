/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

class _SystemCollectionsObjectModelObservableCollectionANDItsStuff
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemCollectionsObjectModelObservableCollectionANDItsStuff_Silent();

        Console.ReadLine();
    }
    static void SystemCollectionsObjectModelObservableCollectionANDItsStuff_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemCollectionsObjectModelObservableCollectionANDItsStuff_Silent()\n");


        // Класс System.Collections.ObjectModel.ObservableCollection<T> (не sealed) - этот класс уникален тем, что имеет событие
        //   CollectionChanged, которое запускается каждый раз, как что-то происходит со внутренними элементами (добавление, удаление,
        //   изменение и т.д.)
        // (внезапно) Наследует System.Collections.ObjectModel.Collection<T>. Реализует эта коллекция (из System.Collections.Generic)
        //   Collection<T>, (из System.Collection.Specialized) INotifyCollectionChanged
        /////////after reading///////////////////////////////////////////////////////////////////////
        //   , (из System.ComponentModel) INotifyPropertyChanged
        /////////////////////////////////////////////////////////////////////////////////////////////
        // Поставляется в System.dll
        //
        // Что за System.Collections.Specialized.INotifyCollectionChanged? Вот как выглядит его определение:
        //
        //       public interface INotifyCollectionChanged
        //       {
        //           event NotifyCollectionChangedEventHandler CollectionChanged;
        //       }
        //
        //   Collection Changed - этот то самое события. Суть такая: если в коллекции, что реализует этот интерфейс, изменится содержимое
        //   (e.g. что-то удалили), то она тут же пройдётся по всем делегатам в этом CollectionChanged (т.е. вызовет их)(как-бы прозвонив всех
        //   слушателей)
        //
        // Что за NotifyCollectionChangedEventHandler? На самом деле полное имя этого делегата -
        //   System.Collections.Specialized.NotifyCollectionChangedEventHandler, и вот как он выглядит:
        //
        //       public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);
        //
        // Что за NotifyCollectionChangedEventArgs? Класс System.Collections.Specialized.NotifyCollectionChangedEventArgs (не sealed) - это
        //   System.EventArgs, расширенный для работы с System.Collections.Specialized.INotifyCollectionChanged-реализующими классами. Как ты,
        //   возможно, понял, методам, что будут хранится в свойствах CollectionChanged, будут передаваться, во-первых this, а во-вторых ещё и
        //   объект этого типа (с упакованной в него информацией о том, что, собственно, произошло)


        //****r - найди более лучший пример использования этого класса
        NotifyCollectionChangedEventArgs r = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                                                       // ctor() - имеется аж 11 версий конструктора
                                                       // System.Collections.Specialized.NotifyCollectionChangedAction - объект этого
                                                       //   перечисления затем расскажет о том, что, случилось с коллекцией. Вот все поля этого
                                                       //   перечисления
                                                       //
                                                       //       public enum NotifyCollectionChangedAction
                                                       //       {
                                                       //           Add = 0,
                                                       //           Remove = 1,
                                                       //           Replace = 2,
                                                       //           Move = 3,
                                                       //           Reset = 4
                                                       //       }
                                                       //
                                                       //   Как ты понял, значение этого перечисления и есть основная информация в объектах
                                                       //   NotifyCollectionChangedEventArgs
        Console.WriteLine("r.Action: {0}", r.Action);  // r.Action - это свойство выдаст нам значение этого перечисления
                                                       //
        Console.WriteLine("r.OldItems.Count: {0}", r.OldItems?.Count);
                                                       // r.OldItems - это свойтсво System.Collections.IList со списком элементов коллекции,
                                                       //   которые были изменены (т.е. из-за них был NotifyCollectionChangedAction.Replace,
                                                       //   ..Remove или ..Move)(если таких элементов нет, OldItems равен null)
                                                       // ?. - помнишь null-условную операцию?
        Console.WriteLine("r.NewItems.Count: {0}", r.NewItems?.Count);
                                                       // r.NewItems - а это список новых элементов (с NotifyCollectionChangedAction.Add)(если
                                                       //   таких нет, NewItems также будет равнятся null'у)


        void peopleChangePrint(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs events)
        {                                                     // peopleChangePrint() - метод, который будет вызываться внутри people при
            Console.WriteLine("Action: {0}", events.Action);  //   каком-то изменении хранилища
            switch (events.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Console.Write("Here are the removed items: ");
                    foreach (int curr in events.OldItems)
                    {
                        Console.WriteLine("{0}, ", curr);
                    }
                    Console.WriteLine();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Console.Write("Here are the added items: ");
                    foreach (int curr in events.NewItems)
                    {
                        Console.Write("{0}, ", curr);
                    }
                    Console.WriteLine();
                    break;
            }
        }
        ObservableCollection<int> people = new ObservableCollection<int>() { 1, 5, -4 };
        people.CollectionChanged += peopleChangePrint;        // ctor() - на самом деле всего 3-и конструктора: стандартный (как здесь),
                                                              //   принимающий Sysetm.Collections.Generic.List<T> и
        people.Add(25);                                       //   System.Collections.Generic.IEnumerable<T>


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemCollectionsObjectModelObservableCollectionANDItsStuff_Silent()");
    }
}