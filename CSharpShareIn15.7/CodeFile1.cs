/*
 * creation date  06 oct 2020
 * last change    21 jun 2021
 * author         artur
 */
using System;
using CommonShareableTypes; // CommonSnareable - т.к. этот проект получил ссылку на CommonSnarableTypes, мы можем пользоваться его начинкой


namespace CSharpShareIn
{
    [CompanyInfo(CompanyName = "FooBar", CompanyUrl = "www.FooBar.com")]
    public class CSharpModule : IAppFunctionality
    {
        void IAppFunctionality.DoIt()                             // CommonShareableTypes.IAppFucntio.. - почему я явно указал этот интерфейс?
        {                                                         //   На самом деле это не обязательно (ты и сам знаешь), но зато так мы
            Console.WriteLine("You just have used C# snap-in!");  //   добъёмся того, что этот метод будет вызываться только из
                                                                  //   IAppFucntionality-объектов (объектов, что совместимых с этим
                                                                  //   интерфейсом)
                                                                  // .. void - напомню, что явнореализованные методы всегдя получают неявный
                                                                  //   public, и изменить это нельзя
        }
    }
}
