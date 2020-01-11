using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace OAcoding.CriticalConnetions
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            #region Connectio Test-1
            /*
                Input: n = 5, edges = [[1, 2], [1, 3], [3, 4], [1, 4], [4, 5]]
                Output: [[1, 2], [4, 5]]

             */
            List<PairInt> edges = new List<PairInt> {
                new PairInt(1,2),
                new PairInt(1,3),
                new PairInt(3,4),
                new PairInt(1,4),
                new PairInt(4,5)
            };

            CriticalConnection ccClass = new CriticalConnection();
            var res = ccClass.criticalConnections(5, edges.Count, edges);
            foreach (var item in res)
            {
                Console.WriteLine("[ {0} {1} ]", item.first, item.second);
            }
            #endregion

            Console.WriteLine("\n Connection Test 2");
            #region Connection Test-2
            /*
                Input: n = 6, edges = [[1, 2], [1, 3], [2, 3], [2, 4], [2, 5], [4, 6], [5, 6]]
                Output: []

             */
            List<PairInt> edges2 = new List<PairInt> {
                new PairInt(1,2),
                new PairInt(1,3),
                new PairInt(2,3),
                new PairInt(2,4),
                new PairInt(2,5),
                new PairInt(4,6),
                new PairInt(5,6)
            };

            var res2 = ccClass.criticalConnections(6, edges2.Count, edges2);
            foreach (var item in res2)
            {
                Console.WriteLine("[ {0} {1} ]", item.first, item.second);
            }
            #endregion

            Console.WriteLine("\n Connection Test 3");
            #region Connection Test-3
            /*
                Input: n = 9, edges = [[1, 2], [1, 3], [2, 3], [3, 4], [3, 6],
                                       [4, 5], [6, 7], [6, 9], [7, 8], [8, 9]]
                Output: [[3, 4], [3, 6], [4, 5]]

             */
            List<PairInt> edges3 = new List<PairInt> {
                new PairInt(1,2),
                new PairInt(1,3),
                new PairInt(2,3),
                new PairInt(3,4),
                new PairInt(3,6),
                new PairInt(4,5),
                new PairInt(6,7),
                new PairInt(6,9),
                new PairInt(7,8),
                new PairInt(8,9)
            };

            var res3 = ccClass.criticalConnections(9, edges3.Count, edges3);
            foreach (var item in res3)
            {
                Console.WriteLine("[ {0} {1} ]", item.first, item.second);
            }
            #endregion


            Console.WriteLine("\n Points Test 1");
            #region Connection Points Test-1
            /*
                Input: numNodes = 7, numEdges = 7,
                       edges = [[0, 1], [0, 2], [1, 3], [2, 3], [2, 5], [5, 6], [3, 4]]
                Output: [2, 3, 5]

             */
            int numRouters1 = 7;
            int numLinks1 = 7;
            int[][] links1 = { new int[]{ 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 3 },
                               new int[]{ 2, 3 }, new int[] { 2, 5 }, new int[] { 5, 6 },
                                                                      new int[] { 3, 4 } };
            CriticalPoints cp = new CriticalPoints();
            var res4 = cp.criticalPoints(links1, numLinks1, numRouters1);
            foreach (var item in res4)
            {
                Console.Write("{0} , ", item);
            }
            #endregion

            Console.WriteLine("\n Points Test 2");
            #region Connection Points Test-2
            /*
                Input: numNodes = 5, numEdges = 5,
                       edges = [1, 2], [1, 3], [3, 4], [1, 4], [4, 5]]
                Output: [[1, 2], [4, 5]]

             */
            int numRouters2 = 5;
            int numLinks2 = 5;
            int[][] links2 = { new int[]{ 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 },
                               new int[]{ 2, 3 }, new int[] { 3, 4 }};
            var res5 = cp.criticalPoints(links2, numLinks2, numRouters2);
            foreach (var item in res5)
            {
                Console.Write("{0} , ", item);
            }
            #endregion


            Console.ReadKey();
        }
    }

    #region Critical Points

    public class CriticalPoints
    {
        public  IList<int> criticalPoints(int[][] links, int numLinks, int numRouters)
        {
          //  int[][] e = new int[][] { { 0, 1 }, { 0, 2 }, { 1, 3 }, { 2, 3 }, { 2, 5 }, { 5, 6 }, { 3, 4 } };
            //int v = 7;

            List<int> list = new List<int>();

            for (int i = 0; i < numLinks; i++)
            {
                //i is the edge I'm ignoring
                if (!Helper(links, i, numRouters))
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public bool Helper(int[][] e, int v, int total)
        {
            HashSet<int> set = new HashSet<int>();
            bool firstAdd = false;

            for (int i = 0; i < e.Length; i++)
            {
                //ignores edges that have v as starting or ending point
                if (e[i][0] == v || e[i][1] == v)
                    continue;

                //adding any edge as the starting point, both vertices
                //will fail if the edges are self edges
                if (!firstAdd)
                {
                    set.Add(e[i][0]);
                    set.Add(e[i][1]);
                    firstAdd = true;
                }

                //if the next edge has one of the edges already in the set, I can visit it
                if (set.Contains(e[i][0]) || set.Contains(e[i][1]))
                {
                    set.Add(e[i][0]);
                    set.Add(e[i][1]);
                }
            }

            //if total visited elements equals all vertices - removed vertice
            return set.Count == total - 1;
        }

    }


    #endregion

    #region Critical Connections
    public class PairInt
    {

        public int first;
        public int second;

        public PairInt(int first, int second)
        {
            this.second = second;
            this.first = first;
        }
    }

    public class CriticalConnection
    {

        

       
        Dictionary<int, bool> visited;
        public List<PairInt> criticalConnections(int numOfServers,
                                          int numOfConnections,
                                          List<PairInt> connections)
        {
            Dictionary<int, HashSet<int>> adj = new Dictionary<int, HashSet<int>>();
            List<PairInt> list = new List<PairInt>();
            foreach (PairInt connection in connections)
            {
                int u = connection.first;
                int v = connection.second;
                if (!adj.ContainsKey(u))
                {
                    adj.Add(u, new HashSet<int>());
                }
                adj[u].Add(v);
                if (!adj.ContainsKey(v))
                {
                    adj.Add(v, new HashSet<int>());
                }
                adj[v].Add(u);
            }

            
            for (int i = 0; i < numOfConnections; i++)
            {
                visited = new Dictionary<int, bool>();
                PairInt p = connections[i];
                int x = p.first;
                int y = p.second;
                adj[x].Remove(y);
                adj[y].Remove(x);
                DFS(adj, 1);
                if (visited.Count != numOfServers)
                {
                    if (p.first > p.second)
                        list.Add(new PairInt(p.second, p.first));
                    else
                        list.Add(p);
                }
                adj[x].Add(y);
                adj[y].Add(x);
            }
            return list;
        }

        public void DFS(Dictionary<int, HashSet<int>> adj, int u)
        {
            visited.Add(u, true);
            if (adj[u].Count() != 0)
            {
                foreach (var v in adj[u])
                {
                    bool isContain = false;

                    if (visited.ContainsKey(v)) {
                        isContain = visited[v];
                    }

                    if (isContain==false)
                    {
                        DFS(adj, v);
                    }
                }
            }
        }
    }

    #endregion
    //public class CriticalConnectionsClass
    //{
    //    //time when discovered the vertex
    //    private int time = 0;
    //    public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
    //    {
    //        int[] low = new int[n];
    //        List<IList<int>> result = new List<IList<int>>();
    //        //we init the visited array to -1 for all vertices
    //        int[] visited = Enumerable.Repeat(-1, n).ToArray();

    //        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
    //        //the graph is connected two ways
    //        foreach (var list in connections)
    //        {
    //            if (!dict.ContainsKey(list[0]))
    //            {
    //                dict.Add(list[0], new List<int>());
    //            }
    //            dict[list[0]].Add(list[1]);


    //            if (!dict.ContainsKey(list[1]))
    //            {
    //                dict.Add(list[1], new List<int>());
    //            }
    //            dict[list[1]].Add(list[0]);

    //        }

    //        for (int i = 0; i < n; i++)
    //        {
    //            if (visited[i] == -1)
    //            {
    //                DFS(i, low, visited, dict, result, i);
    //            }
    //        }
    //        return result;
    //    }

    //    private void DFS(int u, int[] low, int[] visited, Dictionary<int, List<int>> dict, List<IList<int>> result, int pre)
    //    {
    //        visited[u] = low[u] = ++time; // discovered u;
    //        for (int j = 0; j < dict[u].Count; j++) //iterate all of the nodes connected to u
    //        {
    //            int v = dict[u][j];
    //            if (v == pre)
    //            {
    //                //if parent vertex ignore
    //                continue;
    //            }

    //            if (visited[v] == -1) // if not visited
    //            {
    //                DFS(v, low, visited, dict, result, u);
    //                low[u] = Math.Min(low[u], low[v]);
    //                if (low[v] > visited[u])
    //                {
    //                    //u-v is critical there is no path for v to reach back to u or previous vertices of u
    //                    result.Add(new List<int> { u, v });
    //                }
    //            }
    //            else // if v is already visited put the minimum into low for vertex u
    //            {
    //                low[u] = Math.Min(low[u], visited[v]);
    //            }
    //        }
    //    }
    //}

}
