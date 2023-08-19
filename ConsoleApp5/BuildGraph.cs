using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Diagnostics;
using ConsoleApplication1;
namespace SmallWorld
{
    class BuildGraph
    {

        static Dictionary<string, List<ActorsEdges>> AdjList;           //O(1)
        static Dictionary<string, NodeInfo> NodeData;
        static Dictionary<string, int> SHAREDMOVIES;                        //O(1)
        int[,] visited;
        public BuildGraph(Dictionary<string, List<ActorsEdges>> adj, Dictionary<string, int> sharedMovies)   //O(1)
        {
            visited = new int[5000, 5000];
            AdjList = adj;
            NodeData = new Dictionary<string, NodeInfo>();//O(1)
            SHAREDMOVIES = sharedMovies;            //O(1)
        }
        public void CalculateDeg(string actor1, string actor2, int opt)   //O(AdjList)
        {
            Console.Write(actor1 + "/" + actor2);   //O(1)
            NodeInfo res = BFS(actor1, actor2, opt);    //O(AdjList)
            Console.Write("\t  " + res.deg + " \t \t ");     //O(1)
            Console.Write(res.rel + "   \t");              //O(1)
            BuildChain(actor1, actor2);  //O(movieChain)
        }

        public NodeInfo BFS(string actor1, string actor2, int opt)
        {
            NodeInfo ni = new NodeInfo(0, 0, " ", " ");
            NodeData.Add(actor1, ni);

            Queue<ActorsEdges> pq = new Queue<ActorsEdges>();
            pq.Enqueue(new ActorsEdges("", actor1, ""));        //O(1)

            while (pq.Count != 0)      //O(AdjList)
            {
                ActorsEdges edge = pq.Peek();      //O(1)
                if (edge.to == actor2 && opt == 2)        //O(1)
                {
                    return NodeData[actor2];
                }
                pq.Dequeue();    //O(1)
                int f = 0; int t = 0, temp = 0;

                foreach (var c in edge.from) //O(actor1.Length)
                {
                    temp = (int)c;//O(1)
                    char x1 = 'A', x2 = 'Z', x3 = 'a', x4 = 'z';//O(1)
                    if ((temp >= (int)x1 && temp <= (int)x2) || (temp >= (int)x3 && temp <= (int)x4))
                    {
                        f += temp;//O(1)
                    }
                }

                foreach (var c in edge.to)//O(actor2.Length)
                {
                    temp = (int)c;//O(1)
                    char x1 = 'A', x2 = 'Z', x3 = 'a', x4 = 'z';//O(1)

                    if ((temp >= (int)x1 && temp <= (int)x2) || (temp >= (int)x3 && temp <= (int)x4))
                    {
                        t += temp;//O(1)
                    }
                }

                if (visited[f, t] == 1 || visited[t, f] == 1)//O(1)
                {
                    continue;
                }
                else
                {
                    visited[f, t] = 1;//O(1)
                    visited[t, f] = 1;//O(1)
                }

                for (int i = 0; i < AdjList[edge.to].Count; i++)//O(AdjList[edge.to])
                {

                    ActorsEdges neighbour = AdjList[edge.to][i];//O(1)

                    if (!NodeData.ContainsKey(neighbour.to))
                    {
                        ni = new NodeInfo(int.MaxValue, -1, " ", " ");//O(1)
                        NodeData.Add(neighbour.to, ni);
                    }

                    if (NodeData[edge.to].deg + neighbour.Edgecost < NodeData[neighbour.to].deg)
                    {

                        int moviesCount = 0;//O(1)
                        string s = edge.to + neighbour.to;//O(1)
                        moviesCount = SHAREDMOVIES[s] / 2;//O(1)

                        NodeData[neighbour.to] = new NodeInfo(NodeData[edge.to].deg + neighbour.Edgecost, NodeData[edge.to].rel + moviesCount, neighbour.from, neighbour.movie);//O(1)
                    }
                    else if (NodeData[edge.to].deg + neighbour.Edgecost == NodeData[neighbour.to].deg)
                    {
                        int moviesCount = 0;//O(1)
                        string s = edge.to + neighbour.to;//O(1)
                        moviesCount = SHAREDMOVIES[s] / 2;//O(1)

                        if (NodeData[edge.to].rel + moviesCount > NodeData[neighbour.to].rel)
                        {
                            NodeData[neighbour.to] = new NodeInfo(NodeData[neighbour.to].deg, NodeData[edge.to].rel + moviesCount, neighbour.from, neighbour.movie);//O(1)
                        }
                    }
                    pq.Enqueue(neighbour);//O(1)
                }
            }
            if (opt == 3) { return NodeData[actor1]; }    //O(1)

            return NodeData[actor2];      //O(1)
        }

        public void BuildChain(string actor1, string actor2)     //O(AdjList)
        {
            Stack<string> movieChain = new Stack<string>();    //O(1)
            string test = actor2;                              //O(1)

            while (test != actor1)
            {
                movieChain.Push(NodeData[test].movie);
                test = NodeData[test].src;
            }

            int i = 0;

            foreach (var element in movieChain)     //O(movieChain)
            {
                i++;
                if (i == movieChain.Count)
                {
                    Console.Write(element);
                }
                else
                    Console.Write(element + " -> ");
            }
            Console.WriteLine();
        }

        public void Bonuse()      //O(AdjList^2)
        {
            string src, dest = "";   //O(1)
            int maxrs = -1;          //O(1)
            int[] frequancy = new int[13];   //O(1)
            frequancy[0] = 1;    //O(1)
            Console.WriteLine("Enter Actor name: ");    //O(1)
            src = Console.ReadLine();      //O(1)

            BFS(src, "", 3);     //O(AdjList^2)

            for (int index = 0; index < NodeData.Count; index++)   //O(VertexInfo.Count)
            {
                var item = NodeData.ElementAt(index); //<string , NodeInfo> 
                var actor = item.Key;   //string
                var deg = item.Value.deg;   //int deg
                var rs = item.Value.rel;  //int rs

                int dos = deg;
                if (dos < 12) frequancy[dos]++;
                else frequancy[12]++;

                if (rs > maxrs)
                {
                    maxrs = rs;
                    dest = actor;
                }
            }

            Console.WriteLine("Deg. of Separ.  \t Frequency.");
            Console.WriteLine("--------------------------------------");

            for (int i = 0; i < 13; i++)      //O(1)
            {
                //print distribution of the degree of separation
                if (i == 12) Console.WriteLine(">" + (i - 1) + " \t\t\t " + (frequancy[i]));
                else Console.WriteLine(i + "\t\t\t " + frequancy[i]);
            }


            //print The strongest path (based on the relation strength)
            BuildChain(src, dest);          //O(AdjList)   
            Console.WriteLine("The strongest path (based on the relation strength): " + maxrs);
            //Console.ReadLine();
        }
    }
}
