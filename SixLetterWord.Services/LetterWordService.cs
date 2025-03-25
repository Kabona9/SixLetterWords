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
        /// Return list of all the words in given file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>        
        public static List<string> GetAllWordsInFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                //throw new FileNotFoundException($"file {filePath} not found.");
                return [];
            }

            return File.ReadAllLines(filePath)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Return list of strings within a file, with given length
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>        
        public List<string> GetAllCandidates(string filePath)
        {
            return GetAllWordsInFile(filePath)
                .Where(x => x.Length == _wordLength)
                .ToList();
        }

        /// <summary>
        /// returns a list of words that are valid combinations of other words 
        /// </summary>
        /// <param name="words"></param>
        /// <param name="listOfCandidates"></param>
        /// <returns></returns>
        public List<List<string>> FindValidCombinations(List<string> words, List<string> listOfCandidates)
        {
            var results = new List<List<string>>();
            FindCombinations(words, new List<string>(), results, listOfCandidates);
            return results;
        }


        /// <summary>
        /// Recursive method that will find the right combinations of words
        /// </summary>
        /// <param name="words"></param>
        /// <param name="current"></param>
        /// <param name="results"></param>
        /// <param name="listOfCandidates"></param>
        private void FindCombinations(List<string> words, List<string> current, List<List<string>> results, List<string> listOfCandidates)
        {
            //should be combination of words, not just one
            if (current.Count > 1)
            {
                string combined = string.Join(string.Empty, current);
                if (combined.Length > _wordLength)
                    return;

                if (IsCandidate(combined, listOfCandidates))
                {
                    results.Add(new List<string>(current));
                }
            }

            foreach (var word in words)
            {
                //check to see if a word is not used more than allowed in the list of words
                if (current.Count(w => w == word) < words.Count(w => w == word))
                {
                    current.Add(word);
                    FindCombinations(words, current, results, listOfCandidates);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }

        /// <summary>
        /// Function that will check if a string of words is a match for a letterword of 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="listOfCandidates"></param>
        /// <returns></returns>
        public bool IsCandidate(string flattenString, List<string> listOfCandidates)
        {
            return flattenString.Length == _wordLength && listOfCandidates.Contains(flattenString);
        }

        /// <summary>
        /// dummy code to test everything
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public void RunCode(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} does not exists.");
            }

            var words = GetAllWordsInFile(filePath);
            var validWords = GetAllCandidates(filePath);
            
            if (words.Count == 0 && validWords.Count ==  0) return;

            var results = FindValidCombinations(words, validWords);
            foreach (var candidateString in results)
            {
               Console.WriteLine($"{string.Join("+", candidateString)}={string.Join(string.Empty, candidateString)}");
            }
        }

       

        
    }
}
