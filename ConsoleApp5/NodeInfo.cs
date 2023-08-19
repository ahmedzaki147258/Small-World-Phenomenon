using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class NodeInfo
    {
        public int deg;
        public int rel;
        public string src;
        public string movie;
        public NodeInfo(int degi, int reli, string srci, string moviei)
        {
            deg = degi;
            rel = reli;
            src = srci;
            movie = moviei;
        }

    }
}
