/*
 * creation date  06 nov 2020 (not exactly)
 * last change    16 aug 2021
 * author         artur
 */
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 e0 89 )
  .ver 4:0:0:0
}
.assembly CILTypes
{
  .ver 1:0:0:0
}
.module CILType.dll


.namespace MyNamespace
{

  .class public MyBaseClass
  {
    .field private string stringField = "hello!"            // .field --- эта директива используется для определения поля в классе (типе)
    .field private int32 intField = int32(42)               // private --- так атрибуты применяются к полям. Здесь у нас квалификатор доступа
                                                            // (42) --- на самом деле именно так и задаются значения. У string просто
                                                            //   используется литерал в коде, и поэтому в его случае не используется string().
                                                            //   При инициализации объекта эти поля получат такие стандартные значения (это мы
                                                            //   знаем по C#'у)
                                                            // Ещё значения можно задавать и в шестнадцатиричном формате (как 0x2A, так и 0x2a)


    .method public hidebysig specialname rtspecialname
      instance void .ctor(string s, int32 i) cil managed    // .ctor --- это достаточно интересная вещь, и о ней написано в основном файле
    {                                                       // hidebysig --- скрыть метод базового класса с такой же сигнатурой
    }                                                       // instance --- этим атрибутом помечаются методы (в том числе и конструкторы)
                                                            //   уровня экземпляра (у полей и так всё ясно, и к ним instance не применяется)
                                                            // cil managed --- этим указываем, что здесь у нас не неуправляемый код (что может
                                                            //   использоваться при выполнении запросов P/Invoke)
                                                            // specialname --- этим помечаются спец. методы


    .method public hidebysig specialname instance
      string get_TheString() cil managed
    {
    }
    .method public hidebysig specialname instance
      void set_TheString(string 'value') cil managed        // get_.. / set_.. --- (как уже когда-то говорилось в блоке read after), свойства в
    {                                                       //   CIL коде обрастают двумя методами - для получения и задания значения поля. В
    }                                                       //    этих методах, собственно, и происходит вся логика.
                                                            //    CLR просто дёргает эти методы из свойства в нужный момент
                                                            //   . Поле, с которым свойство будет связано, мы сделаем позже
                                                            // 'value' --- заметь, что входной параметр set_.. обёрнут в ''. Как говорит автор,
                                                            //   такая лексема (в '') должна применяться справа в операции присваивания внутри
                                                            //   метода
    .property instance string TheString()                   // .property --- директива для свойств. Почему у ствоства есть ()'бки? Просто
    {                                                       //   свойство в CIL выглядит как метод

      .get instance string MyNamespace.MyBaseClass::get_TheString()
      .set instance string MyNamespace.MyBaseClass::set_TheString(string)
    }
  }


  .class public MyDerivedClass
    extends MyNamespace.MyBaseClass
  {
  }


  .class public interface IMyInterface
  {
  }
  .class public MyDerivedClass2
  extends MyNamespace.MyBaseClass
  implements MyNamespace.IMyInterface
  {
  }
  .class public interface IMyInterface2
    implements MyNamespace.IMyInterface
  {
  }


  .class public sealed MyStructure
    extends [mscorlib]System.ValueType
  {
  }

  .class public sealed value MyStructure2
  {
  }


  .class public sealed MyEnumeration
    extends [mscorlib]System.Enum
  {
  }
  .class public sealed enum MyEnumeration2
  {
    .field public static literal valuetype MyNamespace.MyEnumeration2 A = int32(0)
    .field public static literal valuetype MyNamespace.MyEnumeration2 B = int32(43)
    .field public static literal valuetype MyNamespace.MyEnumeration2 C = int32(0xA)  // static literal --- как мы много раз видели, в .NET
  }                                                                                   //   значения перечислений статичны и постоянны


  .class public sealed MyDelegate
    extends [mscorlib]System.MulticastDelegate
  {
  }
  .class public sealed MyDelegate2
  {
  }


}
.class Program
{
  .method static hidebysig void MyMethod(int32 inputInt, int32& refInt, class [mscorlib]System.Collections.ArrayList arr, [out] int32& outInt)
  {                                                         // MyMethod() --- на самом деле этот метод просто демонстрирует вид параметров
  }                                                         //   в CIL. Если говорить кратко, то в C# всё более-менее как в CIL. Немного
                                                            //   интереснее с модификаторами параметров
                                                            // int32& --- в C# коде это заменили на ref int. Как ты понял, этим помечаются
                                                            //   параметры, передаваемые по ссылке. int32& здесь - это отдельный тип
                                                            //   (модификация простого int32 для возможности передать ссылку. Украдено у C++)
                                                            // class ..ArrayList --- если параметр относится к чистокровно ссылочному типу, то
                                                            //   перед ним ставится этот модификатор (не путать class с директивой .class)
                                                            // params --- его здесь нет (автор просто сказал, что он существует в CIL), но если
                                                            //   бы он был здесь, то он ничем не отличался бы от просто массива, вроде int32[]
                                                            //   в прототипе метода. Вся движуха с таким параметром происходит в кодах операций
                                                            //   - первым делом применяется директива .param [4], например (4 - номер
                                                            //   параметра). Дальше создаётся экземпляр атрибута типа
                                                            //   System.ParamArrayAttribute. Вот пример:
                                                            //
                                                            //       .method private hidebysig static void  FTest(int32[] arr) cil managed
                                                            //       {
                                                            //         .param [1]
                                                            //         .custom instance void [mscorlib]System.ParamArrayAttribute::.ctor() =
                                                            //                                                       ( 01 00 00 00 )
                                                            //       //..
                                                            //
                                                            //   Мы как-бы сделали пометку для CLR, сделав такое в коде. Ничего не напоминает?
                                                            //   Да, атрибуты C#'па применяются примерно также.
                                                            //   Именно по этому экземпляру CLR потом и поймёт, что все дальнейшие параметры,
                                                            //   которые были отправлены в метод, нужно запихнуть в int32[]
  .method static void m()
  {
    .entrypoint
    .maxstack               2
    pop
    ret
  }
}