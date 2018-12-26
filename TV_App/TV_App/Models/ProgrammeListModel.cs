using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class ProgrammeListModel
    {
        public string Title { get; set; }
        public IList<ProgrammeListElement> Listing { get; set; }


        public ProgrammeListModel()
        {
            Listing = new List<ProgrammeListElement>();
        }
    }
}
