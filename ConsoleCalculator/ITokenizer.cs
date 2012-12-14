using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalculator
{
    interface ITokenizer
    {
        IEnumerable<string> GetTokensFrom(string s);
    }
}
