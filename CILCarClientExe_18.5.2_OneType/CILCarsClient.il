/*
 * creation date  14 nov 2021
 * last change    18 aug 2021
 * author         artur
 */
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )
  .ver 4:0:0:0
}
.assembly extern CILCars
{
  .ver 1:0:0:0
}
.assembly CILCarsClient
{
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module CILCarsClient.exe

.namespace CILCarsClient
{
  .class private auto ansi beforefieldinit Program
    extends [mscorlib]System.Object
  {
    .method private hidebysig static
      void startIt(string[] args) cil managed
    {
      .entrypoint
      .maxstack         3

      .locals           init ([0] class [CILCars]CILCars.CILCar)                        // .locals init .. - на самом деле здесь легко можно
      ldstr             "George"                                                        //   обойтись и без отдельной переменной
      ldc.i4            55
      newobj            instance void [CILCars]CILCars.CILCar::.ctor(string, int32)     // void - да, коснтруктор возвращает void, но всё-равно
      stloc.0                                                                           //   что-то загружается в стёк (и мы сохраняем это в
                                                                                        //   локальную переменную)
      ldloc.0
      call              void [CILCars]CILCars.CILCarInfo::Display(class [CILCars]CILCars.CILCar)
      ret
    }
  }
}