using System;
using System.Linq;
using System.Reflection;
using CommonSnappableTypes;

namespace MyExtendableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Welcome to MyTypeViewer *****");
            string typeName = "";
            do
            {
                Console.WriteLine("\nEnter a snapin to load");
                Console.Write("or enter Q to quit: ");

                // Получить имя типа.
                typeName = Console.ReadLine();

                // Желает ли пользователь завершить работу?
                if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                try
                {
                    LoadExternalModule(typeName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sorry, can't find snapin: {ex.Message}");
                }

            } while (true);


            static void LoadExternalModule(string assemblyName)
            {
                Assembly theSnapInAsm = null;
                try
                {
                    // Динамически загрузить выбранную сборку.
                    theSnapInAsm = Assembly.LoadFrom(assemblyName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred loading the snapin: {ex.Message}");
                    return;
                }

                // Получить все совместимые c IAppFunctionality классы в сборке,
                var theClassTypes = theSnapInAsm.GetTypes().Where(t => t.IsClass && (t.GetInterface("IAppFunctionality") != null)).ToList();

                if (!theClassTypes.Any())
                {
                    Console.WriteLine("Nothing implements IAppFunctionality!");
                }

                //Создать объект и вызвать метод DoIt().
                foreach (var item in theClassTypes)
                {
                    // Использовать позднее связывание для создания экземпляра типа.
                    IAppFunctionality itfApp = (IAppFunctionality)theSnapInAsm.CreateInstance(item.FullName, true);
                    itfApp.DoIt();

                    // Отобразить информацию о компании.
                    DisplayCompanyData(item);
                }
            }

            static void DisplayCompanyData(Type t)
            {
                // Получить данные [Companylnfo].
                var compInfo = t.GetCustomAttributes(false).Where(ci => (ci is CompanylnfoAttribute));

                // Отобразить данные.
                foreach (CompanylnfoAttribute item in compInfo)
                {
                    Console.WriteLine($"More info about {item.CompanyName} can be found at {item.CompanyUrl}");
                }
            }
        }
    }
}
