namespace FormalGrammarTask.Symbols
{
    public class NotTerminal : Symbol
    {
        public bool Achievable;
        public bool IsStartSymbol => Name == "S";
        public NotTerminal(string name) : base (name)
        {
        }
    }
}