using System;
using System.Linq;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var arg in args)
            //    Console.WriteLine(arg);

            //InpcInjection.Inject(@"e:\Dev\Other\CleanArch\Core\bin\Debug\netcoreapp2.0\Core.dll", "Core.PropertyChangedAttribute", "NotifyPropertyChanged");

            if (args.Count() == 4)
            {
                if (args[0] == "InpcInjection")
                    InpcInjection.Inject(args[1], args[2], args[3]);
            }            
        }
    }
}
