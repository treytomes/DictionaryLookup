using HtmlAgilityPack;
using System.IO;
using System.Linq;
using System.Net;

namespace DictionaryLookup
{
    public class RemoteWordRepository : WordRepositoryBase
    {
        #region Constants

        private const string BROWSE_URL = "http://dictionary.reference.com/browse/{0}";
        private const string MISSPELLING_URL = "/misspelling";
        private const string XPATH_DEFINITIONS = @"//*[contains(@class, ""def-content"")]";

        #endregion

        #region Methods

        public override WordReference LookupWord(string word)
        {
            word = Validate(word);

            try
            {
                var request = WebRequest.Create(string.Format(BROWSE_URL, word));
                using (var response = request.GetResponse())
                {
                    if (response.ResponseUri.LocalPath.Equals(MISSPELLING_URL))
                    {
                        return null;
                    }
                    else
                    {
                        var responseStream = response.GetResponseStream();
                        var reader = new StreamReader(responseStream);

                        var html = new HtmlDocument();
                        html.Load(new StreamReader(responseStream));

                        reader.Close();

                        var definitions = html.DocumentNode.SelectNodes(XPATH_DEFINITIONS)
                            .Select(node =>
                                string.Join("; ", node.InnerText
                                    .Trim()
                                    .Replace("\r", string.Empty)
                                    .Split('\n')
                                    .Where(x => !string.IsNullOrWhiteSpace(x))));

                        return new WordReference(word, definitions);
                    }
                }
            }
            catch (WebException)
            {
                return null;
            }
        }

        #endregion
    }
}