using System.Collections.Generic;
using System.Text;

namespace FormalGrammarTask
{
    public class Rule
    {
        public readonly List<Chain> Chains;

        public Rule(List<Chain> chains)
        {
            Chains = chains;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < Chains.Count; i++)
            {
                var chain = Chains[i];
                builder.Append(chain);
                if (i != Chains.Count - 1)
                    builder.Append("|");
            }

            return builder.ToString();
        }
    }
}