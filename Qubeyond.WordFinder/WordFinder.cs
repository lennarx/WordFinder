using Qubeyond.WordFinder.Extensions;
using Qubeyond.WordFinder.Validators;
using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;

namespace Qubeyond.WordFinder
{
    public class WordFinder : IWordFinder
    {
        private readonly IEnumerable<string> _wordMatrix;
        public WordFinder(IEnumerable<string> matrix)
        {
            ValidateMatrix(matrix);
            _wordMatrix = matrix.Select(x => x.ToLower());
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var foundWords = new ConcurrentBag<string>();
            //Decided to execute a parallel foreach in order to search for all the words in parallel at the same time
            Parallel.ForEach(wordStream.Distinct().Select(x => x.ToLower()), word =>
            {
                if (!string.IsNullOrEmpty(WordIsContainedInMatrix(word)))
                {
                    foundWords.Add(word);
                }
            });

            return foundWords.ToList();
        }

        private string WordIsContainedInMatrix(string word)
        {
            var wordFoundInRow = SearchWordInRow(word, _wordMatrix);
            var wordFoundInCol = SearchWordInCol(word);

            return wordFoundInCol ?? wordFoundInRow;
        }

        private string SearchWordInCol(string word)
        {
            var columnWords = new ConcurrentBag<string>();

            //In this approach, I decided to afford the trade-off of iterate all the colunmns, but doing it at the same time for the sake of the performance.
            //I could have iterated the matrix sequencially and stop the loop as soon as it found the word, but if the word is not found, it would have been very expensive.
            Parallel.For(0, _wordMatrix.First().Length,
                   index => {
                       var columnWord = new string(_wordMatrix.Select(x => x[index]).ToArray());
                       columnWords.Add(columnWord);
                   });
            return SearchWordInRow(word, columnWords);
        }

        private string SearchWordInRow(string word, IEnumerable<string> wordRows)
        {
            return wordRows.Any(x => x.Contains(word)) ? word : null;
        }

        private void ValidateMatrix(IEnumerable<string> matrix)
        {
            var validator = new MatrixValidator();
            var validationResult = validator.Validate(matrix);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ConcatErrors());
            }
        }
    }
}
