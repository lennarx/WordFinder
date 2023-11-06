using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubeyond.WordFinder
{
    public interface IWordFinder
    {
        Task<IEnumerable<string>> Find(IEnumerable<string> wordstream);
    }
}
