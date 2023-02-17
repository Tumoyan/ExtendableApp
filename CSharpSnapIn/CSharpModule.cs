using CommonSnappableTypes;
using System;

namespace CSharpSnapIn
{
    public class CSharpModule : IAppFunctionality
    {
        void IAppFunctionality.DoIt()
        {
            Console.WriteLine("You have just used C# snap-in!");
        }
    }
}
