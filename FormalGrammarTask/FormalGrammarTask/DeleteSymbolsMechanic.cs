using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormalGrammarTask;
using FormalGrammarTask.Symbols;

namespace GrammarDeleteSymbols
{
    public static class DeleteSymbolsMechanic
    {
        private static readonly HashSet<NotTerminal> UsedSymbols = new HashSet<NotTerminal>();

        public static Grammar ProcessGrammar(Grammar grammar)
        {
            UsedSymbols.Clear();
            var firstStageRules = FirstStage(grammar);
            
            var sortedRules = SortRulesByStartSymbol(firstStageRules);
            Console.WriteLine("First Stage Result: ");
            PrintRules(sortedRules);
            
            var finalRules = SecondStage(sortedRules);
            
            return new Grammar(finalRules);
        }

        private static Dictionary<NotTerminal, Rule> FirstStage(Grammar grammar)
        {
            var changed = true;

            var firstStageRules = new Dictionary<NotTerminal, Rule>();
            
            while (changed)
            {
                changed = false;
                foreach (var keyToRule in grammar.Rules)
                {
                    foreach (var chain in keyToRule.Value.Chains)
                    {
                        var notTerminals = chain.GetNotTerminals();
                        if (notTerminals.Intersect(UsedSymbols).Count() != notTerminals.Count) 
                            continue;
                        
                        if (!UsedSymbols.Contains(keyToRule.Key))
                        {
                            UsedSymbols.Add(keyToRule.Key);
                            changed = true;
                        }
                            
                        if (chain.Added)
                            continue;

                        AddToDictionary(keyToRule.Key, chain, firstStageRules);
                        chain.Added = true;
                    }
                }
            }

            return firstStageRules;
        }

        private static Dictionary<NotTerminal, Rule> SecondStage(Dictionary<NotTerminal, Rule> sortedRules)
        {
            var secondStageRules = new Dictionary<NotTerminal, Rule>();
            var firstAchieved = false;
            
            foreach (var keyToRule in sortedRules)
            {
                if (!firstAchieved)
                {
                    keyToRule.Key.Achievable = true;
                    firstAchieved = true;
                }
                
                foreach (var notTerminals in keyToRule.Value.Chains
                    .Select(chain => chain.GetNotTerminals()))
                {
                    foreach (var nt in notTerminals)
                    {
                        if (sortedRules.ContainsKey(nt))
                        {
                            if (keyToRule.Key.Equals(nt))
                                break;
                        }
                        else break;

                        var key = sortedRules.Keys.FirstOrDefault(k => Equals(k, nt));
                        if (key != null)
                            key.Achievable = true;
                    }
                }
                
                if (!keyToRule.Key.Achievable)
                    continue;

                AddToDictionary(keyToRule, secondStageRules);
            }

            return secondStageRules;
        }
        
        private static void AddToDictionary(KeyValuePair<NotTerminal, Rule> pair, Dictionary<NotTerminal, Rule> dictionary)
        {
            foreach (var chain in pair.Value.Chains)
            {
                if (dictionary.ContainsKey(pair.Key))
                {
                    dictionary[pair.Key].Chains.Add(chain);
                }
                else
                {
                    dictionary.Add(pair.Key, new Rule(new List<Chain> { chain }));
                }
            }
        }
        
        private static void AddToDictionary(NotTerminal key, Chain chain, Dictionary<NotTerminal, Rule> dictionary)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Chains.Add(chain);
            }
            else
            {
                dictionary.Add(key, new Rule(new List<Chain> { chain }));
            }
        }
        
        private static void PrintRules(IDictionary<NotTerminal, Rule> rules)
        {
            var builder = new StringBuilder();
            
            foreach (var keyValuePair in rules)
            {
                builder.Append(keyValuePair.Key.Name);
                builder.Append("->");
                builder.Append(keyValuePair.Value);
                builder.Append("\n");
            }
            
            Console.WriteLine(builder.ToString());
        }

        private static Dictionary<NotTerminal, Rule> SortRulesByStartSymbol(IDictionary<NotTerminal, Rule> rules)
        {
            var sortedPairs = rules.OrderByDescending(pair => pair.Key.Name == "S");
            return new Dictionary<NotTerminal, Rule>(sortedPairs
                .ToDictionary(p => p.Key, p => p.Value));
        }
    }
}