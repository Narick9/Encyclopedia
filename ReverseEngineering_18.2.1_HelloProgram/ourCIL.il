
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib                                                   // .assembly extern .. - как видно, запрос у нас есть только к
{                                                                           //   mscorlib.dll
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly outCIL                                                            // .assembly outCIL - метаданные к нашей сборке (изначально здесь
{                                                                           //   было .assembly HelloProgram, но компилятор выдал
                                                                            //   outCIL.exe. значит, компилиер пользуется именем входного .il
                                                                            //   файла)
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module outCIL.exe                                                          // .module outCIL.exe - да, каким-то образом здесь замешана
// MVID: {0681584E-1419-424B-8370-B6A9150B0D9D}                             //   наша сборка (****а что не так?)
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x06DB0000


// =============== CLASS MEMBERS DECLARATION ===================

                                   
.class private auto ansi beforefieldinit Program                            // .class Program - собственно, наш класс и его атрибуты
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint                                                             // .entrypoint - эта директива говорит CLR о том, что с этого
    // Code size       19 (0x13)                                            //   метода стоит стартовать (кстати, сейчас произошёл ещё один
    .maxstack  8                                                            //   unlock в методе о Main()). Если эта директива не задана для
                                                                            //   метода, то ilasm.exe выставит своё стандартное значение -
                                                                            //   .maxstack 8 (этого хватает для подавляющего большинства
                                                                            //   методов. стёк не увеличится, если 8-ми не хватит).
                                                                            // Если ты запихнёшь в стёк больше значений, чем она может вместить
                                                                            //   - получишь ошибку о необработанном исключении от CLR
                                                                            //   /////////after reading: PEVerify.exe//////////////////////////
                                                                            //   // но предупредить тебя о таком изъяне кода может peverify.exe
                                                                            //   // , выдав тебе такую ошибку, как stack overflow
                                                                            //   //////////////////////////////////////////////////////////////
    IL_0000:  nop
    IL_0001:  ldstr      "Hello CIL code!"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)   
    IL_000b:  nop                                                           
    IL_000c:  call       string [mscorlib]System.Console::ReadLine()        
    IL_0011:  pop                                                           
    IL_0012:  ret                                                           
  } // end of method Program::Main                                            
                                                                            



  .method public hidebysig specialname rtspecialname
          instance void  .ctor() cil managed                                // .. .cotr() .. - стандартный конструктор
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:    ldarg.0                                                     // ldarg.0 - этот код операции загружает 0-ой аргумент в стёк.
    IL_0001:    call       instance void [mscorlib]System.Object::.ctor()   //   В объявлении конструктора нет параметров? На самом деле один
    Nothing_1:  nop                                                         //   есть, но спрятан. Из себя он представляет ссылку на текущий
    ismyday:    ret                                                         //   объект
  } // end of method Program::.ctor                                         // .. System.Object::ctor() - заметь, здесь явно вызывается
                                                                            //   конструктор базового класса (видимо, туда и посылается только
} // end of class Program                                                   //   что загруженный аргумент)
                                                                            // Nothing_1 / todayis.. - об этом рассказано в основном файле
                                                                            //   проекта
// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file D:\dev 10 feb 2020\c#_2020-31oct-11\chapter18\18.2\18.2.1\ourCIL.res
