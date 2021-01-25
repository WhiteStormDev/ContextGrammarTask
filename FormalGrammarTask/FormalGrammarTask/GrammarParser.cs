using System;
using System.Collections.Generic;
using System.IO;
using FormalGrammarTask.Symbols;
using GrammarDeleteSymbols;

namespace FormalGrammarTask
{
    public static class GrammarParser
    {
        private static readonly string Lower = "qwertyuiopasdfghjklzxcvbnm";
        
        public static bool Parse(string fileName, out Grammar grammar)
        {
            grammar = new Grammar();
            try
            {
                TextReader tr = new StreamReader(fileName);
                var all = tr.ReadToEnd();
                
                var strings = all.Split(new [] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length < 1)
                {
                    return ErrorResult();
                }
                var vt = strings[0].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length < 2)
                {
                    return ErrorResult();
                }
                var vn = strings[1].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (strings.Length < 3)
                {
                    return ErrorResult();
                }
                
                var vtList = new List<Terminal>();
                var vnList = new List<NotTerminal>();
                var rules = new Dictionary<NotTerminal, Rule>();
                
                foreach (var s in vt)
                {
                   vtList.Add(new Terminal(s)); 
                }
                
                foreach (var s in vn)
                {
                    vnList.Add(new NotTerminal(s));
                }

                for (var i = 2; i < strings.Length; i++)
                {
                    var str = strings[i];
                    
                    var sp = str.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                    if (sp.Length != 2)
                    {
                        return ErrorResult();
                    }

                    var keySymbol = new NotTerminal(sp[0]);

                    var chainStrings = sp[1].Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    var chains = new List<Chain>();
                    foreach (var chainString in chainStrings)
                    {
                        var chain = ParseSymbols(chainString);
                        chains.Add(new Chain(chain));
                    }
                    
                    rules.Add(keySymbol, new Rule(chains));
                }
                
                grammar = new Grammar(rules, vtList, vnList);
                return true;
            }
            catch (FileNotFoundException)
            {
                return ErrorResult();
            }
        }

        private static bool ErrorResult()
        {
            Console.WriteLine("Wrong file name!");
            return false;
        }

        private static HashSet<Symbol> ParseSymbols(string symbolsString)
        {
            var symbols = new HashSet<Symbol>();
            symbolsString = symbolsString.Trim();
            foreach (var ch in symbolsString)
            {
                var isTerminal = Lower.Contains(ch.ToString());
                if (isTerminal)
                    symbols.Add(new Terminal(ch.ToString()));
                else
                    symbols.Add(new NotTerminal(ch.ToString()));
            }

            return symbols;
        }
    }
}