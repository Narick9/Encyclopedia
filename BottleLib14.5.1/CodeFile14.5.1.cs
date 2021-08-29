/*
 * date    23 sep 20
 * author  artur
 */
using System;


namespace Assembly14_5_1  // Сначала в GAC была развёрнута версия 1.0.0.0 этой сборки, затем версия 2.0.0.0
{                         // В AssemblyInfo.cs ты также можешь найти мои комментарии
    public class Bottle   // Политика сборки для этой библиотеки кода находится прямо в папке проекта под именем Policy14.5.1.xml (там также
    {                     //   есть немного комментариев)
        public string Content { get; set; } = "Water";
        public float Capacity { get; set; } = 0.5f;
        public string Description { get; set; } = "Just bottle with pure water";


        public Bottle()
        {
            Console.WriteLine("version 2.0.0.0");
        }
        public Bottle(string content, float capacity, string description = null) : this()
        {
            Content = content;
            Capacity = capacity;
            Description = description;
        }


        public override string ToString() => $"[ Content={Content}, Capacity={Capacity}, Description={Description ?? "none"} ]";
    }
}