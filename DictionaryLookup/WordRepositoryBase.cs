using System;

namespace DictionaryLookup
{
    public abstract class WordRepositoryBase : IWordRepository
    {
        public bool IsDefined(string word)
        {
            return LookupWord(word) != null;
        }

        public abstract WordReference LookupWord(string word);

        protected string Validate(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentNullException("word");
            }
            return word.Trim().ToUpper();
        }
    }
}