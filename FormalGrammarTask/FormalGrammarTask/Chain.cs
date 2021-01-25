using System.Collections.Generic;
using System.Text;
using FormalGrammarTask.Symbols;

namespace FormalGrammarTask
{
    public class Chain
    {
        public bool Added;
        
        private readonly HashSet<Symbol> _symbols;
        
        public Chain(HashSet<Symbol> symbols)
        {
            _symbols = symbols;
        }

        public HashSet<Terminal> GetTerminals()
        {
            var terminals = new HashSet<Terminal>();
            foreach (var symbol in _symbols)
            {
                if (symbol is Terminal s)
                {
                    terminals.Add(s);
                }
            }

            return terminals;
        }
        
        public HashSet<NotTerminal> GetNotTerminals()
        {
            var notTerminals = new HashSet<NotTerminal>();
            foreach (var symbol in _symbols)
            {
                if (symbol is NotTerminal s)
                {
                    notTerminals.Add(s);
                }
            }

            return notTerminals;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var symbol in _symbols)
            {
                str.Append(symbol.Name);
            }

            return str.ToString();
        }
    }
}