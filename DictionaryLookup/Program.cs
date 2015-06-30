using System;
using System.IO;

namespace DictionaryLookup
{
    /// <summary>
    /// Enter a word to look up in the local dictionary, and view it's definitions.
    /// If the word is not defined in the local dictionary, it will be looked up at dictionary.reference.com.
    /// If the word exists there, it will be added to the local dictionary and saved.
    /// </summary>
    public static class Program
    {
        private const string DICTIONARY_PATH = "dictionary.xml";

        public static void Main(string[] args)
        {
            var repo = new LocalWordRepository(new RemoteWordRepository());
            try
            {
                repo.Load(DICTIONARY_PATH);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to load the dictionary file.");
            }

            while (true)
            {
                Console.Write("Enter a word: ");
                var word = Console.ReadLine();
                Console.WriteLine();

                var reference = repo.LookupWord(word);
                if (reference != null)
                {
                    Console.WriteLine("Definitions:");

                    var index = 1;
                    foreach (var definition in reference.Definitions)
                    {
                        Console.WriteLine("\t{0}. {1}", index++, definition);
                    }

                    if (repo.IsDirty)
                    {
                        repo.Save(DICTIONARY_PATH);
                    }
                }
                else
                {
                    Console.WriteLine("{0} is not a word!", word);
                }

                Console.WriteLine();
            }
        }
    }
}
