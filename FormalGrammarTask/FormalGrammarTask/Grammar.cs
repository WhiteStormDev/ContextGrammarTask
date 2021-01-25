using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormalGrammarTask.Symbols;
using GrammarDeleteSymbols;

namespace FormalGrammarTask
{
    public class Grammar
    {
        public Dictionary<NotTerminal, Rule> Rules { get; private set; }
        public List<Terminal> Terminals { get; private set; }
        public List<NotTerminal> NotTerminals { get; private set; }

        public Grammar()
        {
            
        }

        public Grammar(Dictionary<NotTerminal, Rule> rules)
        {
            var vn = rules.Keys.ToList();
            var vt = new List<Terminal>();
            
            foreach (var keyValuePair in rules)
            {
                foreach (var valueChain in keyValuePair.Value.Chains)
                {
                    var terminals = valueChain.GetTerminals();
                    foreach (var terminal in terminals)
                    {
                        if (!vt.Contains(terminal))
                            vt.Add(terminal);
                    }
                }
            }

            Rules = rules;
            Terminals = vt;
            NotTerminals = vn;
        }
        
        public Grammar(Dictionary<NotTerminal, Rule> rules, List<Terminal> terminals, List<NotTerminal> notTerminals)
        {
            Rules = rules;
            Terminals = terminals;
            NotTerminals = notTerminals;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            foreach (var keyValuePair in Rules)
            {
                builder.Append(keyValuePair.Key.Name);
                builder.Append("->");
                builder.Append(keyValuePair.Value);
                builder.Append("\n");
            }
            
            return builder.ToString();
        }
    }
}