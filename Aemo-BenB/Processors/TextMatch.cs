using Aemo_BenB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aemo_BenB.Processors
{
    public class TextMatch : ITextMatch
    {
        public IEnumerable<int> Match(ITextMatchRequest request)
        {
            // Precondition - request must not be null
            if (request == null)
            {
                throw new ArgumentException("ITextMatchRequest must be provided", "request");
            }

            var results = new List<int>();

            // Precondition - Can not match an empty or null string as input, return empty list
            if (string.IsNullOrEmpty(request.Text))
            {
                return results;
            }

            // Precondition - Can not match an empty or null string as search string, return empty list
            if (string.IsNullOrEmpty(request.Subtext))
            {
                return results;
            }

            // Set regex options for case if needed
            // Nothing in the specifications about culture invariance or others but could easily be added here if needed
            var regexOptions = RegexOptions.None;
            if (!request.CaseSensitive)
            {
                regexOptions |= RegexOptions.IgnoreCase;
            }

            // Search for the actual matche indexes using Regex
            var matches = Regex.Matches(request.Text, request.Subtext, regexOptions).Cast<Match>().Select(m => m.Index);

            // If we want multiple matches can just directly return the result from the regex match instead of adding it to our results variable
            if (request.MultipleMatches)
            {
                return matches;
            }

            // If there are no matches return the empty result list
            if (matches.Count() == 0)
            {
                return results;
            }

            // If we are here MultipleMatches is false and there is at least 1 match, add the first match to our results and return
            results.Add(matches.First());
            return results;
        }
    }
}
