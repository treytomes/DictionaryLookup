using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DictionaryLookup
{
    public class LocalWordRepository : WordRepositoryBase
    {
        #region Fields

        /// <summary>
        /// If the local repository doesn't contain the word, then look it up in the source.
        /// </summary>
        private IWordRepository _source;

        private Dictionary<string, WordReference> _localWords;

        #endregion

        #region Constructors

        public LocalWordRepository(IWordRepository source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            _source = source;
            _localWords = new Dictionary<string, WordReference>();
            IsDirty = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Have any changes been made to the dictionary since the last save?
        /// </summary>
        public bool IsDirty { get; private set; }

        #endregion

        #region Methods

        public override WordReference LookupWord(string word)
        {
            word = Validate(word);

            if (!_localWords.ContainsKey(word))
            {
                var reference = _source.LookupWord(word);
                if (reference != null)
                {
                    _localWords.Add(reference.Word, reference);
                    IsDirty = true;
                }
                else
                {
                    return null;
                }
            }

            return _localWords[word];
        }

        public void Load(string dictionaryPath)
        {
            var document = XElement.Load(dictionaryPath);
            foreach (var referenceElem in document.Elements("WordReference"))
            {
                var word = referenceElem.Attribute("word").Value;
                foreach (var definitionElem in referenceElem.Elements("Definition"))
                {
                    var definition = definitionElem.Value;
                }
            }
        }

        public void Save(string dictionaryPath)
        {
            var document = new XElement("Dictionary");
            foreach (var reference in _localWords.Values)
            {
                document.Add(new XElement("WordReference", new XAttribute("word", reference.Word),
                    reference.Definitions.Select(definition => new XElement("Definition", definition))));
            }
            document.Save(dictionaryPath);
            IsDirty = false;
        }

        #endregion
    }
}