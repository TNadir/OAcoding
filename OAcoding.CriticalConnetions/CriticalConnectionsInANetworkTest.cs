using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OAcoding.CriticalConnetions
{
    [TestClass]
    public class CriticalConnectionsInANetworkTest
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
}
