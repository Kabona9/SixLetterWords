namespace LetterWord.Services
{
    public class LetterWordService
    {
        private readonly int _wordLength;

        public LetterWordService(int wordLength)
        {
            _wordLength = wordLength;
        }

        /// <summary>
        /// Return List of all the words in given file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>        
        public static HashSet<string> GetAllWordsInFile(string filePath)
        {
            if (!File.Exists(filePath)) return new HashSet<string>();

            return File.ReadAllLines(filePath)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToHashSet();
        }

        /// <summary>
        /// Return List of strings within a file, with given length
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>        
        public HashSet<string> GetAllCandidates(string filePath)
        {
            return GetAllWordsInFile(filePath)
                .Where(x => x.Length == _wordLength)
                .ToHashSet();
        }

        /// <summary>
        /// returns a list of words that are valid combinations of other words 
        /// </summary>
        /// <param name="words"></param>
        /// <param name="listOfCandidates"></param>
        /// <returns></returns>
        public List<List<string>> FindValidCombinations(HashSet<string> words, HashSet<string> listOfCandidates)
        {
            var results = new List<List<string>>();
            FindCombinations(words, new List<string>(), results, listOfCandidates, string.Empty);
            return results;
        }

        /// <summary>
        /// Recursive method that will find the right combinations of words
        /// </summary>
        /// <param name="words"></param>
        /// <param name="current"></param>
        /// <param name="results"></param>
        /// <param name="listOfCandidates"></param>
        /// <param name="currentWord"></param>
        private void FindCombinations(HashSet<string> words, List<string> current, List<List<string>> results, HashSet<string> listOfCandidates, string currentWord)
        {
            //should be combination of words, not just one
            //the combination of word (currentword) should have the right length
            //and should be in the list of candidates
            if (current.Count > 1 && currentWord.Length == _wordLength && listOfCandidates.Contains(currentWord))
            {
                results.Add(new List<string>(current));
                return;
            }

            if (currentWord.Length > _wordLength) return;

            foreach (var word in words)
            {
                //check to see if a word is not yet uses                 
                if (!current.Any(w => w == word))
                {
                    current.Add(word);
                    FindCombinations(words, current, results, listOfCandidates, currentWord + word);
                    //continue with new combination
                    current.RemoveAt(current.Count - 1);
                }
            }
        }

        /// <summary>
        /// dummy code to test everything
        /// </summary>
        /// <param name="filePath"></param>
        public void RunCode(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} does not exists.");
            }

            var words = GetAllWordsInFile(filePath);
            var validWords = GetAllCandidates(filePath);

            if (words.Count == 0 && validWords.Count == 0) return;

            var results = FindValidCombinations(words, validWords);

            foreach (var candidateString in results)
            {
                Console.WriteLine($"{string.Join("+", candidateString)}={string.Join(string.Empty, candidateString)}");
            }
        }
    }
}
