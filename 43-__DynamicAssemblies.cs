/*
 * creation date  18 aug 2021
 * last change    20 aug 2021
 * author         artur
 */
using System;
using System.Reflection.Emit;

class __DynamicAssemblies
{
    static void Main()
    {
        Console.WriteLine("***** _ *****");

        DynamicAssembliesANDItsStuffANDCallingConvensions_Silent();

        Console.ReadLine();
    }
    static void DynamicAssembliesANDItsStuffANDCallingConvensions_Silent()
    {
        Console.WriteLine(">->->->->->->->->->->->->->->->->->->   DynamicAssembliesANDItsStuffANDCallingConvensions_Silent()\n");


        // Review
        //
        //   Есть ещё одна весьма интеренсая тема - построение "динамических сборок" (раньше мы видели только "статические сборки".
        //     динамические - это их противоположность)
        //   Чем же динамические сборки отличаются от статических? А тем, что они создаются в памяти на лету другим приложением, в то время,
        //     как статические сборки запускаются с жётского диска
        //
        //   Создаются динамические сборки посредством типов из пространства
        //     System.Reflection.Emit, что создано для этой цели. Это пространтво даёт возможность проектирования сборки и её модулей,
        //     определений типов и логики, и всё это на языке CIL "во время выполнения". Затем эту сборку можно сохранить на диск, получив
        //     в результате статическую сборку
        //
        //   Да, ты будешь отличаться от основной массы C# программистов этим умением, т.к. ты уже освоил
        //     природу CIL
        //
        //   Несмотря на то, что создание динамических сборок занятие также не лёгкое (и тоже довольно редкое), есть несколько
        //     разнообразных обстоятельств, когда оно будет подходящим:
        //  
        //       > Ты строишь инструмент программирования .NET, что должно генерировать сборки на ходу, основываясь, например, на
        //         пользовательском вводе
        //       > Ты создаёшь приложение, что нуждается в генерации прокси не лету для удалённых типов, основываясь на полученных метаданных
        //       > Тебе нужна возможность загрузки статической сборки и вставки в её двоичный образ новые элементы
        //  
        //   Давай же приступим к исследованию System.Reflection.Emit


        // SystemReflectionEmit
        //  
        //   Как и говорилось, создание динамической сборки требует некоторых знаний CIL, но типы из пространства имён System.Reflection.Emit
        //     максимально возможно скрывают сложность языка CIL. Скажем, вместо непосредственного добавлению всех нужных атрибутов для
        //     определиния типа класса можно просто применить класс TypeBuilder (если точнее, то нужно отправить все нужные параметры в его
        //     конструктор). Аналогично, если нужно создать новый конструктор уровня экземпляра, то не придётся использовать лексемы
        //     specialname, rtspecialname или .ctor. Взамен достаточно просто применить класс ConstructorBuilder (у него также атрибуты
        //     хранятся как поля)
        //   Вот все основные типы System.Reflection.Emit (и всё здесь - классы):
        //  
        //         System.Reflection.Emit.ModuleBuilder            - используется для определения набора модулей в проектируемой сборке
        //         System.Reflection.Emit.EnumBuilder              - для создания типа перечисления .NET
        //         System.Reflection.Emit.TypeBuilder              - используется для создания классов, интерфейсов, структур и делегатов
        //                                                           внутри модуля сборки
        //         System.Reflection.Emit.AssemblyBuilder          - применяется для создания сборки в runtime. Если ты хочешь получить .exe
        //                                                           сборку, то тебе нужно вызывать метод ..ModuleBuilder.SetEntryPoint(). Если
        //                                                           ни одной точки входа не указано, будет генерироваться .dll
        //  
        //         System.Reflection.Emit.MethodBuilder            - эти классы применяются для создания членов типов
        //         System.Reflection.Emit.LocalBuilder
        //         System.Reflection.Emit.PropertyBuilder
        //         System.Reflection.Emit.FieldBuilder
        //         System.Reflection.Emit.ConstructorBuilder
        //         System.Reflection.Emit.CustomAttributeBuilder
        //         System.Reflection.Emit.ParameterBuilder
        //         System.Reflection.Emit.EventBuilder
        //  
        //         System.Reflection.Emit.ILGenerator              - это класс для генерации кодов операций в указанном члене типа (только там
        //                                                           эти инструкции и могут находится)
        //         System.Reflection.Emit.OpCodes                  - этот класс имеет многочисленные поля, что представляют сбой коды операций
        //                                                           CIL (все они - структуры Systme.Reflection.Emit.OpCode)(все они read-only)
        //                                                           . Он используется в паре с разнообразными членами типа
        //                                                           System.Relfection.Emit.ILGenerator
        //  
        //     В целом, типы из System.Reflection.Emit представляют различные низкоуровневые лексемы CIL. Далее ты увидишь многие из них в
        //     действии
        //   Стоит сказать, что почти все (****а может и все) эти классы не имеют конструкторов (кроме стандартного)


        // SystemReflectionEmitILGenerator
        //  
        //   Роль класса System.Reflection.Emit.ILGenerator заключается во вставке кодов операций в заданный член типа. Как было сказано,
        //     создавать экземпляры этого класса не следует. Вместо этого
        //     объекты этого класса (а точнее его предков) получаются через специальные методы классов-строителей (методы класса
        //     System.Reflection.Emit.MethodBuilder и ..ConstructorBuilder). Полученный объект будет привязан к своему методу/конструктору
        //   Имея экземпляр ..ILGenerator в каком-нибудь методе/конструкторе, ты можешь создать коды операций через любой его метод. Вот
        //     некоторые его методы:
        //  
        //         BeginCatchBlock()      - начинает блок catch
        //         BeginExceptionBlock()  - начинает блок исключения
        //         BeginFinallyBlock()    - начинает finally блок
        //         BeginScope()           - начинает блок (т.е. {)
        //         DeclareLocal()         - объявляет локальную переменную
        //         DefineLabel()          - определяет новую метку
        //         Emit()                 - выпускает один код операции. Многократно перегружен
        //         EmitCall()             - выпускает call или callvirt
        //         EmitWriteLine()        - выпускает вызов Console.WriteLine() со значениями разных типов
        //         EndExceptionBlock()    - завершает блок ислючения
        //         EndScope()             - завершает блок (т.е. })
        //         ThrowException()       - выпускает инструкцию для выбпроски исключения
        //         UsingNamespace()       - указывает пространство имён, что должно учавствовать в данной области (блоке)
        //  
        //     Основной метод здесь - Emit(), что работает в сочетании с System.Reflection.Emit.OpCodes. Полный набор полей этого класса
        //     прекрасно документирован в официальной документации, ну а здесь ты неоднократно увидишь примеры их использования


        // DynamicAssemblyEmission
        //
        //   Для начала давай попробуем создать простенькую однофайловую сборку MyAssembly.dll с мелким открытым классом HelloWorld.
        //     Фактически, мы сделаем вот этот C# класс:
        //  
        //         public class HelloWorld
        //         {
        //             private string theMessage;
        //             public HelloWorld() {}
        //             public HelloWorld(string _theMessage) { theMessage = _theMessage; }
        //             public string GetMessage() { return theMessage; }
        //             public void SayHello()
        //             {
        //                 Console.WriteLine("Hello from the HelloWorld class!);
        //             }
        //         }
        //  
        //
        //
            System.Reflection.AssemblyName asmName = new System.Reflection.AssemblyName();
            asmName.Name = "MyAssembly";                     // asmName - динамические сборки, как, собственно, и статические, имеют имена (и
            asmName.Version = new Version("1.0.0.0");        //   вообще они одинаковы в плане внутреннего строения)
        //                                                   // System.Reflection - я не использовал using для это пространства, чтобы не было
        //                                                   //   путаницы
        //                                                   // .Name = .. - имя можно было задать прямо в конструкторе System..AssemblyName()
        //
            AssemblyBuilder asm = System.Threading.Thread.GetDomain().DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Save);
        //                                                   // System.Threading.Thread.GetDomain() - этой командой мы получим объект AppDomain
        //                                                   //   текущего домена. На самом деле гораздо проще было использовать более
        //                                                   //   привычное System.AppDomain.CurrentDomain
        //                                                   // ..DefineDynamicAssembly() - именно из объектов доменов и можно получить объект
        //                                                   //   ..AssemblyBuilder. Как видишь, наша динамическая сборка будет находится прямо
        //                                                   //   в текущем домене (но, как мы помним, в домен работает только с одним
        //                                                   //   исполняемым файлом), и все наши действия с asm отразятся именно в этом домене
        //                                                   // asmName - на самом деле дальше нам негде применять эту переменную
        //                                                   // System.Reflection.Emit.AssemblyBuilderAccess - это перечисление для задания
        //                                                   //   режима возможностей для динамических сборок. 
        //                                                   //   Вообще, в System.Reflection.Emit.AssemblyBuilderAccess есть:
        //                                                   //
        //                                                   //       ReflectionOnly  - динамическая сборка предназначена только для рефлексии
        //                                                   //       Run             - может только выполнятся, но не сохранятся на диске
        //                                                   //       RunAndSave      - эта сборка может выполнятся и сохранятся на диске
        //                                                   //       RunAndCollect   - сборка будет автоматически выгружена, и память с ней
        //                                                   //                         будет очищена, как только она перестанет быть доступна
        //                                                   //       Save            - динамическая сборка может быть сохранена, но не
        //                                                   //                         запущена
        //
            ModuleBuilder module = asm.DefineDynamicModule("MyAssembly", "MyAssembly.dll");
        //                                                   // asm.DefineDynamicModule() - определяет модуль в сборке
        //                                                   // "MyAssembly.dll" - т.к. мы планируем получить сборку только из одного модуля,
        //                                                   //   этот модуль (и его файл) будет иметь то же имя, что и у всей сборки
        //                                                   // По правде говоря, методы создания сборок/модулей очень напоминают те, что мы
        //                                                   //   рассматривали в пространстве System.Reflection
        //                                                   // Как говорит автор, модули играют ключевую роль в создании динамических сборок.
        //                                                   //   именно в них и создаются все классы/структуры/перечисления/.., а также
        //                                                   //   встроенные ресурсы
            TypeBuilder helloWorldClass = module.DefineType("MyNamespace.HelloWorld", System.Reflection.TypeAttributes.Public);
        //                                                   // module.DefineType() - определяем новый тип (класс MyNamespace.HelloWorld). Этот
        //                                                   //   метод создаёт только типы для классов (класс и интерфейс). Для перечисления
        //                                                   //   есть отдельный метод DefineEnum()
        //                                                   //   Заметь, что мы также создали новое пространство имён - MyNamespace
        //                                                   // ..TypeAttributes.Public - задаём модификатор доступа public. Вот избранные
        //                                                   //   члены этого перечисления:
        //                                                   //
        //                                                   //       Abstract           - указывает, что тип является абстрактным
        //                                                   //       Class              - тип должен быть классом
        //                                                   //       Interface          - тип должен быть интерфейсом
        //                                                   //       NestedAssembly     - этот тип должен быть виден только внутри сборки
        //                                                   //       NestedFamANDAssem  - этот тип должен быть виден только внутри семейства
        //                                                   //                            (иерархии классов) этой сборки (т.е. классы, другой
        //                                                   //                            сборки, что наследовали кого-то из семейства, не
        //                                                   //                            будут видеть этот вложенный тип)
        //                                                   //       NestedFamily       - этот тип должен быть виден только внутри семейства
        //                                                   //       NestedFamORAssem   - этот тип должен быть виден только внутри семейства
        //                                                   //       NestedPrivate      - этит тип будет доступен из сборки + из потомков из
        //                                                   //                            других сборок
        //                                                   //       NestedPublic       - указывает, что тип является вложенным, но открытым
        //                                                   //       NestedPrivate      - указывает, что тип является вложенным, но закрытым
        //                                                   //       NotPublic          - указывает, что тип не является открытым
        //                                                   //       Public             - указывает, что тип является открытым
        //                                                   //       Sealed             - указывает, что тип является запечатанным (конкретным
        //                                                   //                            , и не может быть уточнён ещё больше)
        //                                                   //       Serializable       - указывает, что тип может быть сериализирован
        //                                                   //
        //                                                   //   Если тебе нужен больше, чем один атрибут, можешь применить побитывую операцию
        //                                                   //   | (перечисление это разрешает, т.к. оно имеет [System.FlagAttribute])
        //                                                   //   ****что за System.FlagAttribute?
        //
            FieldBuilder theMsgField = helloWorldClass.DefineField("theMessage", Type.GetType("System.String"),
                System.Reflection.FieldAttributes.Private);  // helloWorldClass.DefineField() - определяет новое поле. Мы определяем поле
        //                                                   //   theMessage тип string
        //                                                   // System.Type.GetType() - можно было задействовать и typeof(string)
        //                                                   // System.Reflection.FieldAttributes - перечисление с набором атрибутов для поля
        //
            Type[] ctorArgs = new Type[1];
            ctorArgs[0] = typeof(string);
            ConstructorBuilder ctor = helloWorldClass.DefineConstructor(System.Reflection.MethodAttributes.Public,
                System.Reflection.CallingConventions.Standard, ctorArgs);  // helloWorldClass.DefineConstructor() - определяет конструктор. Мы
            ILGenerator ctorILGenerator = ctor.GetILGenerator();           //   определяем такой, что принимает параметр типа string
            ctorILGenerator.Emit(OpCodes.Ldarg_0);                         //
            System.Reflection.ConstructorInfo superCtor = typeof(object).GetConstructor(new Type[0]);
            ctorILGenerator.Emit(OpCodes.Call, superCtor);                 // System.Reflection.CallingConventions - перечисление. Соглашения -
            ctorILGenerator.Emit(OpCodes.Ldarg_0);                         //   это довольно низкоуровневая тема. Соглашения - это набор правил
            ctorILGenerator.Emit(OpCodes.Ldarg_1);                         //   , говорящих о том, как именно передаются аргументы (через
            ctorILGenerator.Emit(OpCodes.Stfld, theMsgField);              //   регистры процессора/стёк/смешанно), в каком порядке передаются
            ctorILGenerator.Emit(OpCodes.Ret);                             //   (слева направо / справа налево), кто должен очищать стёк от
        //                                                                 //   задействованных объектов (вызывающих код / вызываемый код),
        //                                                                 //   какие инструкции использовать для возврата значения метода
        //                                                                 // ctor.GetILGenerator() - получаем генератор кодов операций. Этот
        //                                                                 //   Define.. метод имеет лишь 2 версии
        //                                                                 // В типе System.Reflection.Emit.MethodBuilder
        //                                                                 //   также имеется GetILGenerator()
        //                                                                 // typeof(..).GetConstructor() - выдаст объект
        //                                                                 //   System.Reflection.ConstructorInfo с методанными конструктора
        //                                                                 //   (что подходит по схеме расположения параметров)
        //                                                                 // ctorILGenerator.Emit(..) - выпускает инструкции (коды операций)
        //                                                                 //   . Этот метод очень гибок,
        //                                                                 //   и он пригоден под использование для генерации всяких инструкций
        //                                                                 //   CIL (он имеет 16 перегрузок!)
        //
            helloWorldClass.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public);
        //                                                                 // helloWorldClass.DefineDefaultConstructor() - определяет готовый
        //                                                                 //   простенький конструктор по умолчанию. Создатели решили
        //                                                                 //   выделить отдельный (сильно упрощённый) метод для него
        //                                                                 //   . Метод создаёт конструктор, что просто вызывает
        //                                                                 //   конструктор от базового класса:
        //                                                                 //
        //                                                                 //       .method public hidebysig specialname rtspecialname
        //                                                                 //       instance void .ctor() cil managed
        //                                                                 //       {
        //                                                                 //         .maxstack 1
        //                                                                 //         ldarg.0
        //                                                                 //         call instance void [mscorlib]System.Object::.ctor()
        //                                                                 //         ret
        //                                                                 //       }
        //                                                                 //
        //                                                                 //   Только не нужно добавлять в него что-то своё через
        //                                                                 //   ..ILGenerator. Это приведёт к генерации икслючения, когда ты
        //                                                                 //   вызовешь helloWorldClass.CreateType() (этот метод выпускает
        //                                                                 //   класс в модуль, возвращая итоговый объект Type)
        //                                                                 // На самом деле если ты не создал какой-то свой
        //                                                                 //   конструктор в классе, то в нём всё-равно автоматически
        //                                                                 //   добавится конструктор по умолчанию (добавиться компилятором
        //                                                                 //   ilasm.exe)
        //
            MethodBuilder getMsgMethod = helloWorldClass.DefineMethod("GetMessage", System.Reflection.MethodAttributes.Public,
                typeof(string), null);
            ILGenerator getMsgMethodILGenerator = getMsgMethod.GetILGenerator();
            getMsgMethodILGenerator.Emit(OpCodes.Ldarg_0);                 // null - как видишь, вместо отправления пустого массива Type[0],
            getMsgMethodILGenerator.Emit(OpCodes.Ldfld, theMsgField);      //   можно просто отправить null
            getMsgMethodILGenerator.Emit(OpCodes.Ret);
        //                                                                 
        //
            MethodBuilder sayHelloMethod = helloWorldClass.DefineMethod("SayHello", System.Reflection.MethodAttributes.Public);
            ILGenerator sayHelloMethodILGenerator = sayHelloMethod.GetILGenerator();
            sayHelloMethodILGenerator.EmitWriteLine("Hello from the HelloWorld class");
            sayHelloMethodILGenerator.Emit(OpCodes.Ret);                   // helloWorldClass.DefineMethod(..) - можно было задать null и null
        //                                                                 //   для возвращаемого типа и массива аргументов, а можно вызвать
        //                                                                 //   эту перегрузку
        //
            helloWorldClass.CreateType();                                  // helloWorldClass.CreateType() - выпускаем класс в модуль. Ещё этот
        //                                                                 //   метод (как уже говорилось) возвращает нам финальный Type
        //
            asm.Save("MyAssembly.dll");                                    // asm.Save() - сохраняем нашу сборку в файл (наконец-то)



        // UsingDynamicAssembly
        //
        //   Чтобы было намного интереснее, эта сборка будет использоваться здесь посредством позднего связывания (она будет создана, сохранена
        //     на диск и тут же подхвачена)
        //
        //
            System.Reflection.Assembly theAsm = System.Reflection.Assembly.Load("MyAssembly");
        //
            Type helloWorldClass = theAsm.GetType("MyNamespace.HelloWorld");
        //
            Console.Write("Enter the message to pass to HelloWorld class: ");
            object theHelloWorld = Activator.CreateInstance(helloWorldClass, new string[] { Console.ReadLine() });
        //
            System.Reflection.MethodInfo sayHelloMethodInfo = helloWorldClass.GetMethod("SayHello");
            sayHelloMethodInfo.Invoke(theHelloWorld, null);
        //
            System.Reflection.MethodInfo getMessageMethodInfo = helloWorldClass.GetMethod("GetMessage");
            Console.WriteLine("{0}\n", getMessageMethodInfo.Invoke(theHelloWorld, null));
        //
        //
        //   И, наконец, мы создали сборку, что создаёт другую сборку, и даже использовали её. Да, ты можешь сказать, что примерно того же
        //     может добится и человек, незнакомый с CIL, просто строя исходники с кодом C# как обычные файлы текста. А я отвечу, что здесь
        //     стоит вопрос стандартизации и гибкости. Лучше использовать то, что уже было создано и протестировано множество раз, а CIL-код
        //     всё-таки самый гибкий язык .NET
        //   Попутно мы ещё немного глубже поняли устройство C#, а точнее то, что именно он из себя представляет, если отбросить все аспекты
        //     CIL (к примеру, мы видели, что стандартный конструктор появляется именно из компилятора ilasm.exe, и C# здесь не причём)


        Console.WriteLine("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<   DynamicAssembliesANDItsStuffANDCallingConvensions_Silent()");
    }
}  