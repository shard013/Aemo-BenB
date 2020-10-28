using System.Collections.Generic;

namespace Aemo_BenB.Interfaces
{
    public interface ITextMatch
    {
        public IEnumerable<int> Match(ITextMatchRequest request);
    }
}
