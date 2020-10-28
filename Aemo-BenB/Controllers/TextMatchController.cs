using System.Collections.Generic;
using Aemo_BenB.Interfaces;
using Aemo_BenB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aemo_BenB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextMatchController : ControllerBase
    {

        private readonly ITextMatch _textMatch;

        public TextMatchController(ITextMatch textMatch)
        {
            _textMatch = textMatch;
        }

        [HttpGet]
        public IEnumerable<int> Get(string text, string subtext, bool multiplematches, bool casesensitive)
        {
            var matchRequest = new TextMatchRequest()
            {
                Text = text,
                Subtext = subtext,
                MultipleMatches = multiplematches,
                CaseSensitive = casesensitive
            };

            var results = _textMatch.Match(matchRequest);
            return results;
        }
    }
}