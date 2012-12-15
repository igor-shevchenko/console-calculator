using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalculator
{
    interface ISplitter
    {
        IEnumerable<string> Split(string s);
    }
}
