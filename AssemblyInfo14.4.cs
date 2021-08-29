/////////after reading///////////////////////////////////////////////////////////////////////
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
/////////////////////////////////////////////////////////////////////////////////////////////

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("_14._4.Properties")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("_14._4.Properties")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]                              // [..AssemblyCultrue..] - как оказалось, задавать атрибут по нескольку раз
                                                               //   нельзя

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c5cd7d90-cb77-4280-8770-3e7d3f6c805c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
//[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


//[assembly: AssemblyKeyFile(@".\MyTestKeyPair\myNewKeys.snk")]  // assembly - насколько я понял, так устанавливаются атрибуты самой сборки
//[assembly: AssemblyVersion("1.0.*")]                         // .\ - да, можно использовать и относительный путь
// Жизненноважные атрибуты (вроде номера версии, имени сборки  // Как говорит автор, при установке этого атрибута вручную, VS выдаст
//   и т.д.), конечно, появятся и без явного прописания тут    //   предупреждение о том, что нужно или установить параметр /keyfile для
                                                               //   csc.exe (это компилятор), или установить файл ключей в свойствах проекта.
                                                               //   У меня warning'а не было
// 1.0.0.0 - каждая часть номера сборки может быть числом от 0 // [..AssemblyVersion("1.0.*")] - хотя ты и сам можешь выставить любую версию
//   до 35535 (видимо, всего данные о версии занимают 8 байт)  //   сборки, можно заставить VS постоянно инкрементировать её номера при каждой
                                                               //   компиляции, используя символ * (пришедший из мира регулярных выражений)
[assembly: AssemblyCulture("")]                                // Видимо, в VS 2019 что изменилось, т.к. он выдаёт ошибку, говорящую о том,
// [..AssemblyCulture("")] - т.к. строка здесь оставлена       //   что строка версии хранит подставляемые знаки, которые не совместимы
//   пустая, компилятор будет использовать значение культуры   //   с детерминизмом (т.е. с определённостью, однозначностью
//   самой машины                                              //   ****изначально я написал причинностью). Для её отмены
//                                                             //   пришлось изменить файл 14.4.csproj