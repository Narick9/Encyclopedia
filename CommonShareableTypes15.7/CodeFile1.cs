/*
 * creation date  06 oct 2020
 * last change    21 jun 2021
 * author         artur
 */
using System;

namespace CommonShareableTypes
{
    public interface IAppFunctionality  // IAppFunctionality - этот интерфейс будет служить полиморфной основой для всех оснасток, что могут
    {                                   //   быть применены расширяемым приложением
                                        // Другими словами, расширяемое приложение сможет пользоваться только теми оснатсками, что совместимы с
                                        //   этим интерфейсом
        void DoIt();
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class CompanyInfoAttribute : System.Attribute  // CompanyInfo.. - этот атрибут будет описывать типы оснастки, храня инфо о месте
    {                                                     //   их происхождения (компании). С этим атрибутом также будут знакомы и расширяемое
                                                          //   приложение, и оснастки
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
    }
}
