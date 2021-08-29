/*
 * creation date  14 nov 2021
 * last change    19 aug 2021
 * author         artur
 */
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )
  .ver 4:0:0:0
}
.assembly extern System.Windows.Forms
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )
  .ver 4:0:0:0
}
.assembly CILCars
{
  .hash algorithm 0x00008004    // .hash algorithm - как ты помнишь, этой связкой директивы и атрибута можно задать нужный алгоритм шифрования.
  .ver 1:0:0:0                  //   Через C# это можно было сделать, задействуя атрибут уровня сборки
}                               //   System.Reflection.AssemblyAlgorithmIdAttribute (уверен, что это он) в ./Properties/AssemblyInfo.cs
                                //   (если ты сидишь на VS)



.namespace CILCars
{
  .class public auto ansi beforefieldinit CILCar                // auto - напомню, что этот атрибут говорит среде CLR о том, как размещать
    extends [mscorlib]System.Object                             //   экземпляры класса в памяти. Ещё есть sequential и explicit, но они могут
  {                                                             //   пригодится только при работе с P/Invoke, т.е. с неуправляемым кодом Си
    .field public string    petName
    .field public int32     currentSpeed

    .method public hidebysig specialname rtspecialname instance
      void .ctor(string _petName, int32 _currentSpeed) cil managed
    {
      .maxstack         2
      ldarg.0                                                   // ldarg.0 - загружаем this, не string
      call instance     void [mscorlib]System.Object::.ctor()   // call.. - вызываем конструктор от object и отправляем последнюю запись стёка

      ldarg.0
      ldarg.1
      stfld             string CILCars.CILCar::petName          // stfld string .. - вот как выглядит присваивание значение полю (в стёке
                                                                //   последними двумя записями должны быть ссылка на нужный объект и значение)
      ldarg.0
      ldarg.2
      stfld             int32 CILCars.CILCar::currentSpeed

      ret
    }
  }
  .class public auto ansi beforefieldinit CILCarInfo
    extends [mscorlib]System.Object
  {
    .method public hidebysig static
      void Display(class CILCars.CILCar car) cil managed
    {
      .maxstack 2
      .locals           init ([0] string caption)

      ldstr             "{0}'s speed is:"
      ldarg.0
      ldfld             string CILCars.CILCar::petName                          // ldfld string .. - не забываем всегда указывать возвращающий
      call              string [mscorlib]System.String::Format(string, object)  //   тип атрибутом
      stloc.0                                                                   // ..System.String::Format() - т.к. это статический метод, нам
                                                                                //   не стоит добавлять атрибут instance к коду операции call
      ldarg.0
      ldflda            int32 CILCars.CILCar::currentSpeed                      // ldflda - т.к. мы намерены дальше вызвать метод этого поля,
      call instance     string [mscorlib]System.Int32::ToString()               //   мы должны иметь в стёке его адрес (указатель)

      ldloc.0
      call valuetype [System.Windows.Forms]System.Windows.Forms.DialogResult
                        [System.Windows.Forms]System.Windows.Forms.MessageBox::Show(string, string)
      pop
      ret
    }
  }
}