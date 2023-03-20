using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace myConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //создаем домен приложения с произвольным именем
            AppDomain domain = AppDomain.CreateDomain("My Domain");
            
            //загружаем в созданный домен приложения нашу dll библиотеку
            Assembly asm = domain.Load(AssemblyName.
                GetAssemblyName("DomainLibraryApp.dll")); //здесь выцепляет библ. по имени в debug папке 

            //получаем модуль, из которого будем осуществлять вызов
            Module module = asm.GetModule("DomainLibraryApp.dll");

            //получаем тип данных, содержащий искомый метод
            Type type = module.GetType("DomainLibraryApp.SampleClass"); //здесь была ошибка-не указана биб.

            //получаем метод (статический) из типа (объекта типа) данны
            MethodInfo method = type.GetMethod("DoSome"); 

            //вызываем метод который не принимает входных аргументов и параметров в массиве
            method.Invoke(null, null);

            //однострочный вариант вызова того же метода через анонимные объекты
            //(и без загрузки модуля)
            asm.GetType("DomainLibraryApp.SampleClass"). //получаем тип данных вызываемого класса
                GetMethod("DoSome"). //получаем метод класса 
                Invoke(null, null); //вызываем метод

            //вывод значения статической переменной библиотеки после использования ее функции
            Console.WriteLine(asm.GetType("DomainLibraryApp.SampleClass")
                .GetField("a").GetValue(null).ToString());

            //выгружаем домен приложения через его стат.метод - выгружаем из CLR домен прил.
            AppDomain.Unload(domain);
            Console.ReadLine();
        }
    }
}
