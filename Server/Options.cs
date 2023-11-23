﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Options
    {
        public bool Debug { get; set; }
        public string Suffix { get; set; }
        public string ParserLocation { get; set; }
        public List<Tuple<string, string>> ClassesAndClassifiers { get; set; }
    }
}
