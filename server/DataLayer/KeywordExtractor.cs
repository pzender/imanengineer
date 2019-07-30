using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;
using LemmaSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TV_App.DataLayer
{
    public class KeywordExtractor
    {
        private readonly ILogger logger;

        private static TvAppContext DbContext = new TvAppContext();
        private readonly static Random r = new Random();
        ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);

        public List<string> ProcessKeywords(Programme p)
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
                    .Where(fe => fe.ProgrammeId == p.Id && fe.Feature.Type != 8)
                    .Select(fe => lemmatizer.Lemmatize(fe.Feature.Value.ToLower()))
                    .ToList();
                var keywords = ExtractKeywords(description);

                var keywords_pruned = keywords
                    .Where(keyword => !featnames.Any(feat => feat.Contains(keyword)))
                    .Take(10)
                    .ToList();

                return keywords_pruned;
            }
        }

        private IEnumerable<string> ExtractKeywords(string description)
        {
            List<string> LemmatizedDescription = Corpus.LemmatizeDescription(description);
            List<List<string>> corpus = new List<List<string>>(Corpus.GetInstance().GetLemmatizedContent());
            corpus.Add(LemmatizedDescription);

            IDictionary<string, double> TermFrequency = LemmatizedDescription
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => LemmatizedDescription.Count(w => w == word) / (double)LemmatizedDescription.Count()
                );

            IDictionary<string, double> InverseDocumentFrequency = LemmatizedDescription
                .Distinct()
                .ToDictionary(
                    word => word,
                    word => Math.Log(corpus.Count() / (double)
                        corpus.Count(doc => doc.Contains(word))
                    )
                );


            return LemmatizedDescription
                .Distinct()
                .OrderByDescending(lw => TermFrequency[lw] * InverseDocumentFrequency[lw]);
        }


        public KeywordExtractor(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
