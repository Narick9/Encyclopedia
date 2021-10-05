/*
 * creation date  11 jun 2021
 * last change    25 sep 2021
 * author         artur
 */
using System;
using System.Reflection;
using System.IO;
//using System.Windows.Forms;  //****System.Windows.Forms нету на Linux'е

class _ReflectionForAttributes_SystemIOPath
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        ReflectionForAttributes_Silent();
        SystemIOPath();

        Console.ReadLine();
    }
    static void ReflectionForAttributes_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   ReflectionForAttributes_Silent()\n");


        // Как мы знаем, суть атрибута в том, чтобы какая-то другая часть программы (или другая программа) могла прочитать его и, исходя из
        //   полученных данных (просто существование атирибута - это тоже данные) предпринять нужный образ действий. Здесь будут показаны
        //   два случая. Сначала рассмотрим тот, когда мы проводим рефлексию по получению атрибутов типа, что известен на этапе компиляции


        Type winnebagoType = typeof(Winnebago);
        object[] winnebagoAttributes = winnebagoType.GetCustomAttributes(false);  // weinnebago.GetCustomAttributes() - этот метод возвращает
        // false - одна из перегрузок метода принимает булевый параметр inherit   //   массив экземпляров атрибутов, что были применены в
        //   что задаёт, следует ли идти вверх по цепочке наследования, и         //   объявлении типа (они прямо при этом методе и создаются)
        //   получать атрибуты классов-предков (правда, этот параметр здесь       //   (почему выдаётся массив object[]? прост я не
        //   игнорируется. Он существует лишь для сохранения поддержки интерфейса //   переопределял этот метод в классе Winnebago. в BCL'овких
        //   ICustomAttributeProvider, которому почему-то должен следовать класс  //   типах этот метод переопределён. там даже дополнительные
        //   MemberInfo, который расширяет класс Type. В книге ни слова про это)  //   GetAttributes()-методы ставят)
        //   ****слов в скобках непонятны                                         //
                                                                                  //
        Console.WriteLine("There are few desribe strings for {0} type:", winnebagoType.Name);
        foreach (VehicleDescriptionAttribute current in winnebagoAttributes)
        {                                                      // VehicleDescription.. - здесь мы точно знаем, что у этого класса есть
            Console.WriteLine("-> {0}", current.Description);  //   атрибут этого типа и больше никаких, поэтому здесь всё хорошо. Да, если
        }                                                      //   добавить что-то другое, выйдет исключение. Автор решил пойти довольно
        Console.WriteLine();                                   //   странным путём (на мой взгляд)


        // Второй случай - это когда нам нужно получить атрибуты из типа, что находится в сборке, что будет подключеная в runtime поздним
        //   связыванием. На самом деле принцип тот же. Отличие только в том, что нужно проводить всё в семантике позднего связывания. Для
        //   эмуляции такого случая был создан проект 15.6.1, чья папка находится в папке текущего проекта (т.е. в папке энциклопедии)
        //
        try
        {
            System.Reflection.Assembly myAsm = System.Reflection.Assembly.Load("15.6.1");  // 15.6.1 - сборка помещена в подпапку с таким
            Type locoType = myAsm.GetType("AttributedTypes.Locomotive");                   //   же именем

            foreach (Type current in myAsm.GetTypes())
            {
                foreach (VehicleDescription_1Attribute currentAtt in current.GetCustomAttributes(typeof(VehicleDescription_1Attribute), false))
                {
                    Console.WriteLine("Type {0} has a VehicleDescription_1Attribute with the message \"{1}\"",
                                        current.Name, typeof(VehicleDescription_1Attribute).GetProperty("Description").GetValue(currentAtt));
                }
            }                 // ..GetCustomAttributes(..) - у этого метода есть ещё одна версия, принимающая параметр типа System.Type, с
        }                     //   которым возвращаемые атрибуты должны быть совместимы. И да, неиграющий роли параметр inherit здесь получает
        catch (Exception ex)  //   false (если бы он был true и при этом влиял на подбор атрибутов, вышло бы исключение, т.к. в foreach
        {                     //   попали бы атрибуты с System.Object - его класса предка, что не совместимы с VehicleDesc.._1... Да и не
                              //   понятно было бы что делать с возможно неизвестным типом-предком, т.к. о нём метод ничего знать не может)
                              // ..GetValue() - этот метод объектов System.Refltcion.PropertyInfo возвращает значение самого свойства в виде
                              //   типа object (автор говорит, что этот метод активизирует средство доступа к свойству)
                              //****GetProperty(..) и GetValue(..) ещё не известны. Зачем вообще здесь городить такая фигня?. Почему
                              //    просто не написать current.Description?
                              // Скорее всего кто-то скажет что я комкую код, но я скажу, что так понятнее

            Console.WriteLine(ex);  // ex - да, в этот раз будет выводиться полная трассировка исключения с максимально
        }                           //   информацией (для разнообразия)
        Console.WriteLine();


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   ReflectionForAttributesWith_Silent()");
    }
    static void SystemIOPath()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   SystemIOPath()");


        // System.IO.Path - статический (и поэтому запечатанный) класс, что
        //   хранит в себе методы для работы со строковыми путями


        System.IO.Path.GetDirectoryName("./14.2.dll");  // ..Path.GetDirectoryName() - тут всё просто. Посылаешь строку с файлом в метод, а он
                                                        //   выделяет полное имя его директории (без имени файла). Есть и вторая версия
                                                        //   /////////after reading:System.ReadOnlySpan<T>/////////////////////////////////////
                                                        //   // , принимающая System.ReadOnlySpan<T>
                                                        //   //////////////////////////////////////////////////////////////////////////////////


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   SystemIOPath()");
    }
}