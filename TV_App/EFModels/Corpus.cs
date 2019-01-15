using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LemmaSharp;

namespace TV_App.EFModels
{
    public class Corpus
    {
        private static Corpus _instance = null;

        private static testContext DbContext = new testContext();
        private static ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);
        private static Random r = new Random();
        private static IEnumerable<string> totalCorpus = DbContext.Description.Select(desc => desc.Content);

        private readonly List<List<string>> lemmatizedContent;

        private Corpus()
        {
            lemmatizedContent = new List<List<string>>();
            for (int i = 0; i < 20; i++)
            {
                lemmatizedContent.Add(LemmatizeDescription(totalCorpus.ElementAt(r.Next(totalCorpus.Count()))));
            }
        }

        public static Corpus GetInstance()
        {
            if (_instance == null)
                _instance = new Corpus();
            return _instance;
        }

        public List<List<string>> GetLemmatizedContent()
        {
            return lemmatizedContent;
        }

        public static List<string> LemmatizeDescription(string description)
        {
            return description.Split(new char[] { ' ', ',', '.', ')', '(', '\n', '"', ':' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => lemmatizer.Lemmatize(word.ToLower()))
                .ToList();
        }

    }
}
