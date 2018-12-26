using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class ProgrammeListBuilder
    {
        readonly QueryExecutor executor = new QueryExecutor();
        public ProgrammeListModel BuildForChannel(string channelname)
        {
            List<ProgrammeListElement> emissions = new List<ProgrammeListElement>();
            IEnumerable<int> ids = executor.Query<int>($"SELECT programme_id FROM Emission WHERE channel_id IN (SELECT id FROM Channel WHERE name like '{channelname}');"); 

            foreach(int id in ids)
            {
                emissions.Add(BuildElement(id));
            }

            return new ProgrammeListModel()
            {
                Title = $"Program dla {channelname}",
                Listing = emissions
            };

        }

        public ProgrammeListModel BuildForUser(string username)
        {
            List<ProgrammeListElement> emissions = new List<ProgrammeListElement>();
            IEnumerable<int> ids = executor.Query<int>($"SELECT programme_id FROM Rating WHERE user_login LIKE '{username}';");

            foreach (int id in ids)
            {
                emissions.Add(BuildElement(id));
            }

            return new ProgrammeListModel()
            {
                Title = $"Ulubione programy {username}",
                Listing = emissions
            };

        }

        private ProgrammeListElement BuildElement(int id)
        {
            return new ProgrammeListElement()
            {
                Title = executor.Query<string>($"SELECT title FROM Programme WHERE id = {id}").Single(),
                Emissions = executor.Query<EmissionModel>($"SELECT (SELECT name FROM Channel WHERE id = Emission.channel_id) ChannelName, start Start, stop Stop FROM Emission WHERE programme_id = {id};"),
                Features = executor.Query<FeatureModel>($"SELECT type Type, value Value FROM Feature WHERE id IN (SELECT feature_id FROM FeatureExample WHERE programme_id = {id});")
            };
        }
    }
}
