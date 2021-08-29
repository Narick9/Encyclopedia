'
'  creation date  11 jun 2021
'  last change    21 jun 2021
'  author         artur
'
Imports CommonShareableTypes
' CommonShareableTypes - этот проект я привязал через Reference Manager (т.е. так, как это было с проектом CSharpSnareIn)
' Комментарии автоформатируются by VS, и я не нашёл решение этому

<CompanyInfo(CompanyName:="Chucky's Software", CompanyUrl:="www.ChuckySoft.com")>
Public Class VbSnareIn                                           ' <> - этим в VB помечаются атрибуты (как [] в C#)
    Implements IAppFunctionality                                 ' Implements - этим в VB помечаются интерфейсы перед типом
    Public Sub DoIt() Implements IAppFunctionality.DoIt
        Console.WriteLine("You have just used the VB snap-in!")  ' DoIt() - интересно, но как только я нажал на Tab для завершения записи
                                                                 '   IAppFunctionality, VS сама добавила загатовку для этого метода
    End Sub
End Class
