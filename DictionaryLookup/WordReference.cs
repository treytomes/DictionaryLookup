using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryLookup
{
    public class WordReference : IEquatable<WordReference>
    {
        #region Fields

        private string[] _definitions;

        #endregion

        #region Constructors

        public WordReference(string word, IEnumerable<string> definitions)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentNullException("word");
            }

            if (definitions == null)
            {
                throw new ArgumentNullException("definitions");
            }

            Word = word.Trim().ToUpper();

            _definitions = definitions.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (_definitions.Length == 0)
            {
                throw new ArgumentException("You must have at least one definition for a word.", "definitions");
            }
        }

        #endregion

        #region Properties

        public string Word { get; private set; }

        public IEnumerable<string> Definitions
        {
            get
            {
                foreach (var definition in _definitions)
                {
                    yield return definition;
                }
                yield break;
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Word;
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj != null) && Equals(obj as WordReference);
        }

        public bool Equals(WordReference other)
        {
            return (other != null) && (string.Compare(Word, other.Word, true) == 0);
        }

        #endregion
    }
}
