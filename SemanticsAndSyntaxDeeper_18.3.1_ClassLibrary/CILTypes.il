/*
 * creation date  06 nov 2020 (not exactly)
 * last change    15 aug 2021
 * author         artur
 */
.assembly extern mscorlib                                   // mscorlib --- здесь было решено явно подключить эту сборку (а как же без неё?)
{
  .publickeytoken = (B7 7A 5C 56 19 34 e0 89 )
  .ver 4:0:0:0                                              // .publickeytoken / .ver --- без каждого из них всё также было бы хорошо, но автор
}                                                           //   решил их указать
.assembly CILTypes
{
  .ver 1:0:0:0                                              // .ver 1:0:0:0 --- как мы помним, по умолчанию ставится 0:0:0:0. Здесь это было
}                                                           //   изменено
.module CILType.dll                                         // .module --- это директива для обозначения официального имени двоичного файла,
                                                            //   что должен получиться (как пишет автор,  определение сборки завершается этим)
                                                            //
                                                            //
.namespace MyNamespace                                      // .namespace --- пространтсва имён тоже определяются довольно предсказуемо. Они
{                                                           //   (как и в C#) могут вкладываться друг в друга (вложение .namespace.A.B здесь
                                                            //   тоже работает). Иметь корневое пространство здесь также не обязательно
  .class public MyBaseClass                                 // .class --- эта простая директива может быть декорирована многочисленными
  {                                                         //   атрибутами, что уточнят природу типа. Здесь, как видишь, базовый класс не
  }                                                         //   указан, и этот класс автоматически станет производным от System.Object
                                                            //   (да, даже в MSIL).

  .class public MyDerivedClass
    extends MyNamespace.MyBaseClass                         // extends --- если тип должен расширять что-то, и это не System.Object (хотя для
  {                                                         //   него тоже можно), то должен быть применён этот атрибут. extends ставится после
  }                                                         //   имени класса
                                                            // Заметь, что полностью заданное имя применилось и к классу, что находится в том
                                                            //   же пространстве (этот канон CIL)


  .class public interface IMyInterface                      // interface --- здесь мы определяем интерфейс, и да, все типы (и не очень)
  {                                                         //   определяются директивой .class. Интерфейсы отличает их особый атрибут
  }                                                         //   interface
  .class public MyDerivedClass2
    extends MyNamespace.MyBaseClass
    implements MyNamespace.IMyInterface                     // implements --- и ещё интерфейсы записываются после этого атрибута
  {                                                         // extends, implements --- extands должен быть перед implements, т.к. за implements
  }                                                         //   может следовать список имён (реализуемых интерфейсов), разделённых запятой
  .class public interface IMyInterface2
    implements MyNamespace.IMyInterface                     // implements --- если один интерфейс расширяет другой, то снова
  {                                                         //   используется этот атрибут
  }


  .class public sealed MyStructure
    extends [mscorlib]System.ValueType                      // [mscorlib].System.ValueType --- в CIL (а значит и в C#) именно этот базовый
  {                                                         //   класс отличает структуры от других классов
  }                                                         // sealed --- как говорит автор, стркутуры в CIL должны быть всегда запечатаны явно
                                                            //   , иначе ошибка компиляции (хотя и без него всё ок, и peverify.exe считает
                                                            //   также)
  .class public sealed value MyStructure2                   // value --- в CIL для объявления структуры предусмотрели сокращённый синтаксис.
  {                                                         //   Классы с этим атрибутом становятся производным от [mscorlib]System.ValueType
  }


  .class public sealed MyEnumeration
    extends [mscorlib]System.Enum                           // [mscorlib]System.Enum --- перечисления определяются схоже со структурами. Их
  {                                                         //   тоже отличает запечатанность и наследование от спецефичного класса (компилятор
  }                                                         //   не заметит остуствия sealed, но peverify.exe выдаст ошибку)
  .class public sealed enum MyEnumeration2                  // enum --- и у перечислений в CIL также есть сокращённые синтаксис. Этот атрибут
  {                                                         //   автоматически делает классы производными от [mscorlib]System.Enum
  }


  .class public sealed MyDelegate
    extends [mscorlib]System.MulticastDelegate              // [mscorlib]System.MutlicastDelegate --- и делегаты - это всё те же классы (как
  {                                                         //   говорилось в методе Delegates), но со специфичныe предком. И ещё sealed.
  }                                                         //   Сокращённого синтаксиса для их определения уже нет (я не нашёл его в
                                                            //   спецификациях ECMA 335)
                                                            // sealed --- ilasm.exe и тут не заметит отсутсвия, но не peverify.exe
                                   

}
.class Program
{
  .method static void m()                                   // .method - как ты уже много раз мог заметить, этой директивой объявляются методы
  {                                                         //   (и функции тоже!)
    .entrypoint                                             
    .maxstack               2
    newobj instance void class [mscorlib]System.Collections.Generic.List`1<int32>::.ctor()
                                                            // newobj instance void class --- ****пока что можем только догадываться
                                                            // ..List`1<int32>.. --- в CIL коде для обобщённых типов нам нужно указывать число
                                                            //   их параметров-типов (и именно в таком виде)
                                                            // int32 --- снова особое обозначение стандартного типа
    newobj instance void class [mscorlib]System.Collections.Generic.List`1< class [mscorlib]System.Collections.Generic.List`1<int32> >::.ctor()
    pop                                                     // ..List`1< ..List`1<int32> >.. - а вот так монструозно выглядит список со
    pop                                                     //   списками чисел типа int32
    ret
  }
}