using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LemmaSharp;

namespace TV_App.Models
{
    public partial class Description
    {
        private static TvAppContext DbContext = new TvAppContext();
        private static Random r = new Random();
        private IEnumerable<string> totalCorpus = DbContext.Description.Select(desc => desc.Content);

        private static ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);


        public IEnumerable<string> GetKeywords()
        {
            List<IEnumerable<string>> corpus = TemporaryCorpus(20).ToList();

            List<string> LemmatizedWords = LemmatizeContent().ToList();

            Dictionary<string, double> TermFrequency = LemmatizedWords
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => LemmatizedWords.Count(w => w == word) / (double)LemmatizedWords.Count()
                );

            Dictionary<string, double> InverseDocumentFrequency = LemmatizedWords
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => Math.Log(corpus.Count() / (double)
                        corpus.Count(doc => doc.Contains(word))
                    )
                );

            return LemmatizedWords
                .Distinct()
                .OrderByDescending(lw => TermFrequency[lw] * InverseDocumentFrequency[lw])
                .Select(lw => $"{lw} : {TermFrequency[lw] * InverseDocumentFrequency[lw]}");
        }

        private IEnumerable<string> LemmatizeContent()
        {
            return Content
                .Split(new char[] { ' ', ',', '.', ')', '(', '\n', '"' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => lemmatizer.Lemmatize(word.ToLower()));
        }

        private IEnumerable<IEnumerable<string>> TemporaryCorpus(int size = 0)
        {
            List<IEnumerable<string>> ret = new List<IEnumerable<string>>();
            for (int i = 0; i < size; i++)
            {
                int index = r.Next(DbContext.Description.Count());
                ret.Add(DbContext.Description
                    .AsEnumerable()
                    .ElementAt(index)
                    .LemmatizeContent()
                );
            }
            return ret;
        }

    }
}
