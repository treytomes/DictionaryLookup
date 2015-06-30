namespace DictionaryLookup
{
    public interface IWordRepository
    {
        bool IsDefined(string word);

        /// <summary>
        /// Look up a word from some dictionary source.
        /// </summary>
        /// <returns>A word reference if the word exists, or null if it is not a word.</returns>
        WordReference LookupWord(string word);
    }
}