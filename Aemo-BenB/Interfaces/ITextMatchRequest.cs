namespace Aemo_BenB.Interfaces
{
    public interface ITextMatchRequest
    {
        public string Text { get; }
        public string Subtext { get; }
        public bool MultipleMatches { get; }
        public bool CaseSensitive { get; }
    }
}
