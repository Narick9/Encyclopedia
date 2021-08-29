/*
 * creation date  10 nov 2021 (not exactly)
 * last change    17 aug 2021
 * author         artur
 */
.assembly extern mscorlib
{
  .ver 4:0:0:0
}
.assembly SomeCILInfo
{
}


.method void LocalVariables()
{
  .maxstack         1

  .locals           init ([0] string myStr, [1] int32 myInt, [2] object myObj)  // .locals init --- эта директива с этим атрибутом
  ldstr             "CIL code is fun!"                                          //   используеются в паре для размещения в методе локальных
  stloc.0                                                                       //   переменных. Как видишь, каждая переменная должна иметь
  ldc.i4 33                                                                     //   свой индекс в [], тип данных, имя (необязательно. их мы
  stloc.1                                                                       //   здесь задали, но использовать не будем)
                                                                                //
  newobj instance void [mscorlib]System.Object::.ctor()                         // stloc.0 --- эта вариация инструкции stloc, она берёт запись
  stloc.2                                                                       //   в стёке и сохраняет её в локальную переменную под индексом
                                                                                //   0 (всего есть stloc.<0-5>)
  ldloc.0                                                                       // ldc.i4 --- загружаем константу 33 (константа, т.к. литерал)
  call void [mscorlib]System.Console::WriteLine(string)                         // instance --- предположу, что этот атрибут говорит о
                                                                                //   экземплярной природе newobj в этой строке кода
  ret
}
.method int32 Add(int32 a, int32 b) cil managed                                 // Add() --- просто пример того, как выглядить небольшая
{                                                                               //   функция. Код вполне понятен
  .maxstack 2                                                                   // cil managed --- на самом деле здесь это не обязательно
  ldarg.0
  ldarg.1
  add
  ret
}
.method void CountToTen() cil managed                                           // CountToTen() --- для удобоства было сделано нормальное
{                                                                               //   оформление кода
  .entrypoint
  .maxstack             2
  .locals init          ([0] int32 i)

  ldc.i4.0                                                                      // ldc.i4.0 --- это одна из вариаций кода операции ldc.i4,
  stloc.0                                                                       //   записывающая в стёк число 0 типа int32 (всего есть
  br.s IS_END                                                                   //   ldc.i4.<0-8>). Ещё есть ldc.i4.M1 для загрузки числа -1 (M
                                                                                //   может быть m) и ldc.i4.s <int8 (num)>)
  BODY:     ldloc.0
            call        void [mscorlib]System.Console::WriteLine(int32)
            ldc.i4.1
            ldloc.0
            add
            stloc.0

  IS_END:   ldloc.0
            ldc.i4      10
            blt         BODY
  ret                                                                           // Вот так выглядит цикл со сложением и выводом результата
}