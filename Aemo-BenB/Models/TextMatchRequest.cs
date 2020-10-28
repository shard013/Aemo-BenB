using Aemo_BenB.Interfaces;

namespace Aemo_BenB.Models
{
    public class TextMatchRequest : ITextMatchRequest
    {
        public string Text { get; set; }
        public string Subtext { get; set; }
        public bool MultipleMatches { get; set; }
        public bool CaseSensitive { get; set; }
    }
}
