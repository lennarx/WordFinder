using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Qubeyond.WordFinder.Tests
{
    [Binding]
    public class WordFinderTestsStepDefinitions
    {
        private IList<string> _wordMatrix;
        private List<string> _wordsIncluded;
        private List<string> _wordsExcluded;

        private IEnumerable<string> _resultWords;

        private IWordFinder _wordFinder;

        private Exception _exception;

        [Given(@"I have a matrix to create the WordFinder")]
        public void GivenIHaveAMatrixToCreateTheWordFinder()
        {
            _wordMatrix = new List<string>()
            {
                { "adsgadsgadsfgadfghsfdghdfgjwn" },
                { "asdgsdfheadphoneshkjmsdfhdfoi" },
                { "dfgspeakergasdfgsdfgsdfbsdfrr" },
                { "notebookfhgjfghdfgjtukjouyodv" },
                { "gsoftwarefyonmyimopopsdxhgdia" },
                { "adgsdfhgsfhyuhoujoijgoijilfsn" },
                { "uimooauiehjslpukllusiueoeakfa" },
                { "afodglksjgitmabmiuuiasiousoro" },
                { "bovbabtickhialsjkdhgkjanrsjgn" },
                { "klijsoljdfycyunomkladjgkledjf" },
                { "klesayflkhjkdflkhjhorafdassdf" },
                { "lknjolempirelbjinlkuixhjouaae" }
            };
        }



        [Given(@"the matrix is empty")]
        public void GivenTheMatrixIsEmpty()
        {
            _wordMatrix = new List<string>();
        }

        [Given(@"the matrix has words with different sizes")]
        public void GivenTheMatrixHasWordsWithDifferentSizes()
        {
            _wordMatrix = new List<string>() { { "asidfhsdfh" }, { "sg" } };
        }

        [Given(@"the matrix contains non letter characters")]
        public void GivenTheMatrixContainsNonLetterCharacters()
        {
            _wordMatrix = new List<string> { { "pasdfl,ik2kj" } };
        }

        [Given(@"the matrix rows and columns exceeds the size limit")]
        public void GivenTheMatrixRowsAndColumnsExceedsTheSizeLimit()
        {
            _wordMatrix = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                _wordMatrix.Add("akdfjghlksdfjhlsfhlsdkjgfh");
            }
        }

        [Given(@"I have words included in the matrix")]
        public void GivenIHaveWordsIncludedInTheMatrix()
        {
            _wordsIncluded = new List<string>()
            {
                {"headphones" },
                {"joystick" },
                {"speaker" },
                {"glasses" },
                {"boy" },
                {"box" },
                {"nirvana" },
                {"software" },
                {"movie" },
                {"empire" },
                {"notebook" }
            };
        }

        [Given(@"I have exluded words in the matrix")]
        public void GivenIHaveExcludedWordsInTheMatrix()
        {
            _wordsExcluded = new List<string>()
            {
                {"lift" },
                {"bag" },
                {"chocolate" },
                {"guitar" },
                {"bass" },
                {"player" },
                {"cigarette" },
                {"smoke" },
                {"soccer" },
                {"car" },
                {"bike" }
            };
        }

        [Given(@"the Wordfinder is created")]
        public void GivenTheWordfinderIsCreated()
        {
            _wordFinder = new WordFinder(_wordMatrix);
        }

        [When(@"I try to create the matrix")]
        public void WhenITryToCreateTheMatrix()
        {
            try
            {
                _wordFinder = new WordFinder(_wordMatrix);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [When(@"I try to search the words in the matrix")]
        public void WhenITryToSearchTheWordsInTheMatrix()
        {
            var parameterWords = new List<string>();
            parameterWords.AddRange(_wordsExcluded);
            parameterWords.AddRange(_wordsIncluded);
            _resultWords = _wordFinder.Find(parameterWords);
        }

        [Then(@"I should the matrix created")]
        public void ThenIShouldTheMatrixCreated()
        {
            _wordFinder.Should().NotBeNull();
        }

        [Then(@"I should see an exception thrown")]
        public void ThenIShouldSeeAnExceptionThrown()
        {
            _exception.Should().NotBeNull();
        }

        [Then(@"it should contain the message (.*)")]
        public void ThenItShouldContainTheMessage(string message)
        {
            _exception.Message.Should().Contain(message);
        }

        [Then(@"I should see the results")]
        public void ThenIShouldSeeTheResults()
        {
            _resultWords.Should().NotBeEmpty();
        }

        [Then(@"the lists should match")]
        public void ThenTheListsShouldMatch()
        {
            _resultWords.Should().Contain(_wordsIncluded);
            _resultWords.Should().NotContain(_wordsExcluded);
        }
    }
}
