using Aemo_BenB.Interfaces;
using Aemo_BenB.Models;
using Aemo_BenB.Processors;
using System;
using System.Linq;
using Xunit;

namespace AemoTests
{
    public class TextMatchTests
    {
        private readonly string _defaultTextInput = "Word1 word2 word3";
        private readonly string _defaultTextMatch = "word";

        private ITextMatch _textMatch { get; }
        public TextMatchTests()
        {
            _textMatch = new TextMatch();
        }

        private TextMatchRequest GetDefaultTextMatchRequest()
        {
            return new TextMatchRequest()
            {
                Text = _defaultTextInput,
                Subtext = _defaultTextMatch,
                CaseSensitive = false,
                MultipleMatches = true
            };
        }

        [Fact]
        public void MatchMultipleCaseInsensetive()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { 0, 6, 12 };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void MatchMultipleCaseSensetive()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.CaseSensitive = true;

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { 6, 12 };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void MatchSingleCaseInsensetive()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.MultipleMatches = false;

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { 0 };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void MatchSingleCaseSensetive()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.CaseSensitive = true;
            matchRequest.MultipleMatches = false;

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { 6 };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void EmptySearchHasNoMatches()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.Subtext = "";

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void NonMatchingSearchHasNoMatches()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.Subtext = "nomatch";

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void EmptyTextSearchHasNoMatches()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.Text = "";

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void SearchWithSpacesMatches()
        {
            // arrange
            var matchRequest = GetDefaultTextMatchRequest();
            matchRequest.Text = "word word word";
            matchRequest.Subtext = "d w";

            // act
            var results = _textMatch.Match(matchRequest);

            // assert
            var expected = new int[] { 3, 8 };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void MissingRequestThrowsArgumentException()
        {
            // arrange
            var textMatch = new TextMatch();

            // act/assert
            Assert.Throws<ArgumentException>(() => textMatch.Match(null));
        }

    }
}
