using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    class ActorsEdges
    {
        public string from;
        public string to;
        public string movie;
        public int Edgecost;

        public ActorsEdges(string f, string t, string m)
        {
            from = f;
            to = t;
            movie = m;
            Edgecost = 1;
        }
    }

}
