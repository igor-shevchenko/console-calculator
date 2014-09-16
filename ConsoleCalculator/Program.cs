using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new ContainerFactory().GetContainer())
            {
                var operatorLoader = container.Resolve<IOperatorLoader>();
                operatorLoader.Load();

                var calculator = container.Resolve<ICalculator>();
                const string prompt = "> ";
                Console.WriteLine("Type expression (e.g. \"1+2\") for calculation or \"exit\" for exit");
                while (true)
                {
                    Console.Write(prompt);
                    string command = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(command))
                        continue;
                    if (command == "exit")
                        break;
                    try
                    {
                        double result = calculator.Calculate(command);
                        Console.WriteLine(result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
