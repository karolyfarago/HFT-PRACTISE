using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HFT_ZH_A.Model {

    class Warplane {

        public string Type { get; set; }

        public double Wingspan { get; set; }

        public List<Engine> Engines { get; set; }
    }
}
