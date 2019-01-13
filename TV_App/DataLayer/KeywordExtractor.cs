using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.EFModels;
using LemmaSharp;
using Microsoft.EntityFrameworkCore;

namespace TV_App.DataLayer
{
    public class KeywordExtractor
    {
        private static testContext DbContext = new testContext();
        private static Random r = new Random();
        ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);

        private IEnumerable<string> totalCorpus = DbContext.Description.Select(desc => desc.Content);

        public IEnumerable<string> ProcessKeywords(Programme p)
        {
            string description = p.Description.FirstOrDefault(d => d.Content != "")?.Content;
            if (description == null)
            {
                return new List<string>();
            }
            else
            {
                var featnames = DbContext.FeatureExample
                    .Include(fe => fe.Feature)
                    .Where(fe => fe.ProgrammeId == p.Id)
                    .Select(fe => lemmatizer.Lemmatize(fe.Feature.Value.ToLower()));
                var keywords = ExtractKeywords(description, 20);
                var keywords_pruned = keywords.Where(keyword => !featnames.Any(feat => feat.Contains(keyword)));
                return keywords_pruned
                    .Take(10);
            }
        }

        private IEnumerable<string> ExtractKeywords(string description, int corp_size)
        {
            IEnumerable<string> LemmatizedWords = LemmatizeDescription(description).Where(word => word.Length > 3 && !word.EndsWith('ć'));
            List<IEnumerable<string>> corpus = TemporaryCorpus(corp_size).ToList();
            corpus.Add(LemmatizedWords);

            IDictionary<string, double> TermFrequency = LemmatizedWords
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => LemmatizedWords.Count(w => w == word) / (double)LemmatizedWords.Count()
                );

            IDictionary<string, double> InverseDocumentFrequency = LemmatizedWords
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => Math.Log(corpus.Count() / (double)
                        corpus.Count(doc => doc.Contains(word))
                    )
                );


            return LemmatizedWords
                .Distinct()
                .OrderByDescending(lw => TermFrequency[lw] * InverseDocumentFrequency[lw]);
        }

        private IEnumerable<IEnumerable<string>> TemporaryCorpus(int size = 0)
        {
            List<IEnumerable<string>> ret = new List<IEnumerable<string>>();
            for(int i = 0; i < size; i++)
            {
                ret.Add(LemmatizeDescription(totalCorpus.ElementAt(r.Next(totalCorpus.Count()))));
            }
            return ret;
        }

        private IEnumerable<string> LemmatizeDescription(string description)
        {
            return description.Split(new char[] { ' ', ',', '.', ')', '(', '\n', '"', ':' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => lemmatizer.Lemmatize(word.ToLower()));
        }
    }
}
