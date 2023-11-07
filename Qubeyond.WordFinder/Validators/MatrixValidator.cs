using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qubeyond.WordFinder.Validators
{
    public class MatrixValidator : AbstractValidator<IEnumerable<string>>
    {
        private const int MATRIX_SIZE_LIMIT = 64;
        public MatrixValidator() 
        {
            RuleFor(x => x).NotEmpty().WithMessage("Provided matrix is empty");
            RuleFor(x => x).Must(x => !MatrixExceedsSizeLimit(x)).WithMessage("Provided matrix exceeds the matrix size limits");
            RuleFor(x => x).Must(x => !MatrixContainsWordsWithNonLetterCharacters(x)).WithMessage("One of the provided words contains one or more non letter character");
            RuleFor(x => x).Must(x => !MatrixContainsDifferentWordSizes(x)).WithMessage("Rows contained in matrix do not have the same length");
        }

        private bool MatrixContainsDifferentWordSizes(IEnumerable<string> words)
        {
            return words.Any(x => words.First().Length != x.Length);
        }

        private bool MatrixExceedsSizeLimit(IEnumerable<string> words)
        {
            return MatrixExceedsRowsLimit(words) || MatrixExceedsColumnLimit(words);
        }
        private bool MatrixContainsWordsWithNonLetterCharacters(IEnumerable<string> words)
        {
            return words.Any(x => StringContainsNonLetterCharacters(x));
        }

        private bool MatrixExceedsRowsLimit(IEnumerable<string> words)
        {
            return words.Count() > MATRIX_SIZE_LIMIT;
        }

        private bool MatrixExceedsColumnLimit(IEnumerable<string> words)
        {
            return words.Any(x => x.Length > MATRIX_SIZE_LIMIT);
        }


        private bool StringContainsNonLetterCharacters(string word)
        {
            return !Regex.IsMatch(word, @"^[a-zA-Z]+$");
        }
    }
}
