/*
 * creation date  26 jun 2021
 * last change    26 jun 2021
 * author         artur
 */
using System;

class _SystemIComparableANDSystemCollectionsIComparer
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        SystemIComparableANDSystemCollectionsIComparer();

        Console.ReadLine();
    }
    static void SystemIComparableANDSystemCollectionsIComparer()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemIComparableANDSystemCollectionsIComparer()\n");


        // Интерфейс System.IComparable гарантиSystemOperatingSystemAndSystemPlatformIDрует, что экземпляры поддерживающих типов будут иметь
        //   метод, нацеленный для сравнения между собой (кто больше, кто меньше, равны?). Вот как он выглядит:
        //
        //       interface IComparable
        //       {
        //           int CompareTo(object o);  // CompareTo() - единственный метод, требуемый интерфейсом IComparable
        //       }
        //
        // У этого интерфейса также есть обобщённый вариант - IComparable<T>, не требующий конвертации твоего типа к object и обратно в методе


        Car[] myAutos = new Car[5];
        myAutos[0] = new Car("Rusty", 80, 1);
        myAutos[1] = new Car("Mary", 40, 234);
        myAutos[2] = new Car("Viper", 40, 34);
        myAutos[3] = new Car("Mel", 40, 4);
        myAutos[4] = new Car("Chucky", 40, 5);

        Array.Sort(myAutos);           // Array.Sort() - напомню, что этот статический метод требует наличия IComparable объектов

        Console.WriteLine("Sortered set of cars:");
        foreach (Car curr in myAutos)  // myAutos - System.Array поддерживает интерфейс IEnumerable
        {
            Console.WriteLine($"{curr}");
        }
        Console.WriteLine();




        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemIComparableANDSystemCollectionsIComparer()");
    }
    class Car : IComparable
    {
        public string PetName { get; set; }
        public int CurrentSpeed { get; set; }
        public uint CarID { get; set; }
        public Car() { }
        public Car(string petName, int currentSpeed, uint carID)
        {
            PetName = petName;
            CurrentSpeed = currentSpeed;
            CarID = carID;
        }
        int IComparable.CompareTo(object o)            // CompareTo() - выполняем требование для поддержки IComparable. Мы сами решаем
        {                                              //   каким будет механизм сравнивания
            if (!(o is Car car))
            {
                throw new ArgumentException("It isn't a Car type", "object o");  
            }                                                                    
            if (this.CarID < car.CarID)                // CarID - прекрасный кандидат для сравнения
                return -1;                             // car - да, это переменная объявляется в условии первого if
            if (this.CarID == car.CarID)
                return 0;
            return 1;
            //return this.CarID.CompareTo(car.CarID);  // int.Compare - т.к. мы сравниваем просто числа, мы могли воспользоваться их
        }                                              //   реализацией метода
        public override string ToString() => $"{CarID}  {PetName} is going {CurrentSpeed}";
    }
}