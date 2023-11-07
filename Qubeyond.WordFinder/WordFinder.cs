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

            Parallel.ForEach(wordStream.Distinct(), word =>
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
            if (!matrix.Any())
            {
                throw new Exception("Provided matrix is empty");
            }

            if (matrix.Count() > 64)
            {
                throw new Exception("Provided rows exceeds the limit of 64 rows");
            }

            if(matrix.Any(x => StringContainsNonLetterCharacters(x))) 
            {
                throw new Exception("One of the provided words contains one or more non letter character");
            }

            if (matrix.Any(x => x.Length > 64 || matrix.First().Length != x.Length))
            {
                throw new Exception("Rows contained in matrix do not have the same length");
            }

        }

        private bool StringContainsNonLetterCharacters(string word)
        {
            return !Regex.IsMatch(word, @"^[a-zA-Z]+$");
        }
    }
}
