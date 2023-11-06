namespace Qubeyond.WordFinder
{
    public class WordFinder : IWordFinder
    {
        private readonly IEnumerable<string> _wordMatrix;
        public WordFinder(IEnumerable<string> matrix)
        {
            ValidateMatrix(matrix);
            _wordMatrix = matrix;
        }

        public async Task<IEnumerable<string>> Find(IEnumerable<string> wordStream)
        {
            var foundWordsTasks = wordStream.Distinct().Select(word => WordIsContainedInMatrix(word));

            await Task.WhenAll(foundWordsTasks);

            return foundWordsTasks.Select(x => x.Result); 
        }

        private async Task<string> WordIsContainedInMatrix(string word)
        {
            var wordFoundInRowTask = SearchWordInRow(word, _wordMatrix);
            var wordFoundInColTask = SearchWordInCol(word);

            await Task.WhenAll(wordFoundInColTask, wordFoundInRowTask);

            return wordFoundInColTask.Result ?? wordFoundInRowTask.Result;
        }

        private async Task<string> SearchWordInCol(string word)
        {
            var charColumns = _wordMatrix.Select(x => x.ToList()).ToList();

            var stringColumns = charColumns.Select(x => x.ToString()!);

            return await SearchWordInRow(word, stringColumns);

            //for (int colIndex = 0; colIndex < columns.Count; colIndex++)
            //{
            //    foreach (var column in columns)
            //    {
            //        if (column[colIndex].ToString().Contains(word))
            //        {
            //            return word;
            //        }
            //    }
            //}
        }

        private async Task<string> SearchWordInRow(string word, IEnumerable<string> wordRows)
        {
            if(await Task.Run(() => wordRows.Any(x => x.Contains(word))))
            {
                return word;
            }
            return null;
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

            if (matrix.Any(x => x.Length > 64 || matrix.First().Length != x.Length))
            {
                throw new Exception("Rows contained in matrix do not have the same length");
            }

        }
    }
}
