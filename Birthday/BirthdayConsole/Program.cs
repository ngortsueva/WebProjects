using System;
using BirthdayConsole.Domain;
using BirthdayConsole.Commands;
using static System.Console;

namespace BirthdayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string strCmd = "";            

            var repository = new PersonRepository();
            var factory = new CommandFactory(repository);
            
            WriteLine("Birthday. Version 0.2 [2019]");
            WriteLine("Copyright N.Gortsuev");

            while (strCmd != "exit")
            {
                Console.Write(">");
                strCmd = Console.ReadLine();

                var command = factory.GetCommand(strCmd);

                command.Execute();
            }
        }
    }
}
