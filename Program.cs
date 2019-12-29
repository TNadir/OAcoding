using System;
using System.Collections;
using System.Collections.Generic;

namespace OAcoding
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var res = SortKMessedArray(new int[] { 1, 4, 5, 2, 3, 7, 8, 6, 10, 9 }, 2);
            Console.WriteLine(res);
            Console.ReadKey();
        }

        public static int[] SortKMessedArray(int[] arr, int k)
        {
            if (arr == null && arr.Length <= 1)
            {
                return arr;
            }
            //1, 4, 5, 2, 3, 7, 8, 6, 10, 9
            for (int i = 1; i < arr.Length; i++)
            {//O(N)
                int x = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > x)
                { //O(k)
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = x;
            }
            return arr;
        }

    }
}
