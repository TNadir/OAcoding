using System;
using System.Collections.Generic;
using System.Linq;
namespace OAcoding.OptimalUtilization
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*

               a = [[1, 3], [2, 5], [3, 7], [4, 10]]
               b = [[1, 2], [2, 3], [3, 4], [4, 5]]
               target = 10

               1.  i,j [1, 3]+[4, 5]=8<=10  =>F i++
               2.  i,j [2, 5]+[4, 5]=10<=10 =>T add to list [[2,4]] j--
               3.  

             */

            var a = new List<List<int>>();
            a.Add(new List<int> { 1, 2 });
            a.Add(new List<int> { 2, 4 });
            a.Add(new List<int> { 3, 5 });

            var b = new List<List<int>>();
            b.Add(new List<int> { 1, 2 });
            b.Add(new List<int> { 2, 4 });
            //b.Add(new List<int> { 2, 11 });
            //b.Add(new List<int> { 3, 12 });


            var res = pairsWithTarget(a, b, 9);
            foreach (var item in res)
            {
                Console.Write("[ {0} ]  ",string.Join(", ", item));
            }

        }


        public static List<int[]> pairsWithTarget(List<List<int>> firstList, List<List<int>> secList, int target)
        {
            List<int[]> pairsSumToTarget = new List<int[]>();

            Dictionary<int, List<int[]>> optimalPair = new Dictionary<int, List<int[]>>();
            Dictionary<int, List<int[]>> nearestPair = new Dictionary<int, List<int[]>>();

            foreach (List<int> a in firstList)
            {
                foreach (List<int> b in secList)
                {
                    int sum = a[1] + b[1];
                    if (sum == target)
                    {
                        if (!optimalPair.ContainsKey(sum))
                        {
                            optimalPair.Add(sum, new List<int[]>());
                        }
                        optimalPair[sum].Add(new int[] { a[0], b[0] });
                    }
                    else if (sum < target)
                    {
                        if (!nearestPair.ContainsKey(sum))
                        {
                            nearestPair.Add(sum, new List<int[]>());
                        }
                        nearestPair[sum].Add(new int[] { a[0], b[0] });
                    }
                }
            }
            if (optimalPair.ContainsKey(target))
            {
                pairsSumToTarget = optimalPair[target];
            }
            else
            {
                foreach (var item in nearestPair.OrderByDescending(x => x.Key))
                {
                    pairsSumToTarget = item.Value;
                    break;
                }
            }
            return pairsSumToTarget;
        }


    }
}
