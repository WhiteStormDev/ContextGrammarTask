using System;
using FormalGrammarTask;

namespace GrammarDeleteSymbols
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter task char or \"q\" for quit");
                var q = Console.ReadLine();
                if (q == null || q.Equals("q", StringComparison.CurrentCultureIgnoreCase))
                    break;

                if (!GrammarParser.Parse("Tasks/" + q + ".txt", out var grammar)) 
                    continue;
                
                Console.WriteLine("File Content: ");
                Console.WriteLine(grammar);
                var processedGrammar = DeleteSymbolsMechanic.ProcessGrammar(grammar);
                Console.WriteLine("Final Result: ");
                Console.WriteLine(processedGrammar);
            }
        }
    }
}