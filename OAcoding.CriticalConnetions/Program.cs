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
            List<IList<int>> input = new List<IList<int>>();
            input.Add(new List<int> { 0, 1 });
            input.Add(new List<int> { 1, 2 });
            input.Add(new List<int> { 2, 0 });
            input.Add(new List<int> { 1, 3 });
            CriticalConnectionsClass ccClass = new CriticalConnectionsClass();
            var res = ccClass.CriticalConnections(input.Count, input);
            foreach (var r in res)
            {
                    Console.WriteLine(string.Join(", ",r));
            }

            Console.ReadKey();
        }
    }

    [TestClass]
    public class CriticalConnectionsInANetwork
    {
        [TestMethod]
        public void TestMethod1()
        {
            //[[0,1],[1,2],[2,0],[1,3]]
            List<IList<int>> input = new List<IList<int>>();
            input.Add(new List<int> { 0, 1 });
            input.Add(new List<int> { 1, 2 });
            input.Add(new List<int> { 2, 0 });
            input.Add(new List<int> { 1, 3 });
            CriticalConnectionsClass ccClass = new CriticalConnectionsClass();
            var res = ccClass.CriticalConnections(4, input);
            List<IList<int>> expcted = new List<IList<int>>();
            expcted.Add(new List<int> { 1, 3 });
            for (int i = 0; i < res.Count; i++)
            {
                CollectionAssert.AreEqual(expcted[i].ToList(), res[i].ToList());
            }
        }
    }

    public class CriticalConnectionsClass
    {
        //time when discovered the vertex
        private int time = 0;
        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            int[] low = new int[n];
            List<IList<int>> result = new List<IList<int>>();
            //we init the visited array to -1 for all vertices
            int[] visited = Enumerable.Repeat(-1, n).ToArray();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            //the graph is connected two ways
            foreach (var list in connections)
            {
                if (!dict.ContainsKey(list[0]))
                {
                    dict.Add(list[0], new List<int>());
                }
                dict[list[0]].Add(list[1]);


                if (!dict.ContainsKey(list[1]))
                {
                    dict.Add(list[1], new List<int>());
                }
                dict[list[1]].Add(list[0]);

            }

            for (int i = 0; i < n; i++)
            {
                if (visited[i] == -1)
                {
                    DFS(i, low, visited, dict, result, i);
                }
            }
            return result;
        }

        private void DFS(int u, int[] low, int[] visited, Dictionary<int, List<int>> dict, List<IList<int>> result, int pre)
        {
            visited[u] = low[u] = ++time; // discovered u;
            for (int j = 0; j < dict[u].Count; j++) //iterate all of the nodes connected to u
            {
                int v = dict[u][j];
                if (v == pre)
                {
                    //if parent vertex ignore
                    continue;
                }

                if (visited[v] == -1) // if not visited
                {
                    DFS(v, low, visited, dict, result, u);
                    low[u] = Math.Min(low[u], low[v]);
                    if (low[v] > visited[u])
                    {
                        //u-v is critical there is no path for v to reach back to u or previous vertices of u
                        result.Add(new List<int> { u, v });
                    }
                }
                else // if v is already visited put the minimum into low for vertex u
                {
                    low[u] = Math.Min(low[u], visited[v]);
                }
            }
        }
    }

}
