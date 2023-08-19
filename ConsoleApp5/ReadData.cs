using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmallWorld;



namespace SmallWorld
{
    class ReadData
    {

        public static Dictionary<string, List<ActorsEdges>> adj = new Dictionary<string, List<ActorsEdges>>();  //O(1)
        public static Dictionary<string, int> sharedMovies = new Dictionary<string, int>();                     //O(1)
        public List<string> actors = new List<string>();                                                        //O(1)
        public void ReadSample(int option)   //O(movies*(actors^2))
        {
            // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\Sample\movies1.txt";//O(1)

            string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\small\Case1\Movies193.txt"; //O(1)
            // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\small\Case2\Movies187.txt";//O(1)

            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case1\Movies967.txt";//O(1)
            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case2\Movies4736.txt";//O(1)

            // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\large\Movies14129.txt";//O(1)

            // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\extreme\Movies122806.txt";//O(1)
            string movie = "";                       //O(1)
            using (StreamReader sr = File.OpenText(filename))
            {
                string line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] subs = line.Split('/');
                    movie = subs[0];
                    for (int i = 1; i < subs.Length; i++)   //O(subs.Length)
                    {
                        actors.Add(subs[i]);    //O(1)
                    }
                    for (int i = 0; i < actors.Count; i++) //O( line->actors^2 )
                    {
                        if (!adj.ContainsKey(actors[i]))   //O(1)
                        {
                            adj.Add(actors[i], new List<ActorsEdges>());
                        }

                        for (int j = 0; j < actors.Count; j++)  //O(actors)
                        {
                            if (i != j)       //O(1)
                            {
                                ActorsEdges AE = new ActorsEdges(actors[i], actors[j], movie);//O(1)
                                adj[actors[i]].Add(AE);
                                string stest = actors[i] + actors[j];//O(1)
                                string stest2 = actors[j] + actors[i];//O(1)
                                if (sharedMovies.ContainsKey(stest) && sharedMovies.ContainsKey(stest2))
                                {
                                    sharedMovies[stest]++;//O(1)
                                    sharedMovies[stest2]++;//O(1)
                                }
                                else
                                {
                                    sharedMovies.Add(stest, 1);
                                    sharedMovies.Add(stest2, 1);
                                }
                            }
                        }
                    }
                    actors = new List<string>();
                }
            }
            Console.WriteLine("Done Reading Movie File!");    //O(1)

            if (option == 3)
            {
                BuildGraph BG = new BuildGraph(adj, sharedMovies);  //O(1)
                BG.Bonuse();
            }
        }
        public void ReadQueries(int opt) //O(queries*(AdjList^2))
        {

           // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\Sample\queries1.txt";//O(1)

            string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\small\Case1\queries110.txt";//O(1)
            // string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\small\Case2\queries50.txt";//O(1)

            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case1\queries85.txt";//O(1)
            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case1\queries4000.txt";//O(1)
            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case2\queries110.txt";//O(1)
            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\medium\Case2\queries2000.txt";//O(1)

            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\large\queries26.txt";//O(1)
            //  string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\large\queries600.txt";//O(1)

            //string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\extreme\queries22.txt";//O(1)
            //string filename = @"C:\Users\green\Desktop\SmallWorldPhenomenon\extreme\queries200.txt";//O(1)
            using (StreamReader sr = File.OpenText(filename))
            {
                string line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] subs = line.Split('/');    //O(1)
                    Console.WriteLine();                    //O(1)          
                    BuildGraph BG = new BuildGraph(adj, sharedMovies); //O(1)
                    BG.CalculateDeg(subs[0], subs[1], opt);//O(AdjList)
                }
            }
            Console.WriteLine("done reading queries");   //O(1)
        }
    }
}
