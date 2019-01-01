using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class GuideUpdate
    {
        public long Id { get; set; }
        public string Source { get; set; }
        public string Posted { get; set; }
    }
}
