using Microsoft.Extensions.DependencyInjection;
using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            SetUpServices();

            Console.WriteLine("Hello World!");
        }

        private static void SetUpServices()
        {
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();
        }
    }


}
