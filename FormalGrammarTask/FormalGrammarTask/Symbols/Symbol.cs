namespace FormalGrammarTask.Symbols
{
    public abstract class Symbol
    {
        public readonly string Name;

        protected Symbol(string name)
        {
            Name = name;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Symbol sym)
            {
                return sym.Name == Name;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            var hash = Name != null ? Name.GetHashCode() : 0;
            return hash;
        }
    }
}