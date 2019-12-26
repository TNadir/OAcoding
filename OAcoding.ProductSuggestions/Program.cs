using System;
using System.Collections.Generic;

namespace OAcoding.ProductSuggestions
{

    public class Trie
    {
        public Trie[] m_tries = new Trie[26];
        public List<string> m_list = new List<string>();
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var products = new string[] { "bags", "baggage", "banner", "box", "cloths" };
            string searchWord = "cloths";

            var list = SuggestedProducts(products, searchWord);
            foreach (var item in list)
            {
                Console.WriteLine(string.Join(", ",item));
                //Console.WriteLine(" ");
            }
            Console.ReadKey();
        }

        static IList<IList<string>> SuggestedProducts(string[] products,
                                                      string searchWord)
        {
            Array.Sort(products);

            Trie head = new Trie();
            BuildTrie(head, products);

            List<IList<string>> llistResult = new List<IList<string>>();

            foreach (char lch in searchWord)
            {
                head = head?.m_tries[lch - 'a'];
                if (head != null)
                {
                    llistResult.Add(head.m_list.GetRange(0,
                                     Math.Min(3, head.m_list.Count)));
                }
                else
                {
                    llistResult.Add(new List<string>());
                }
            }

            return llistResult;
        }

        static void BuildTrie(Trie head, string[] products)
        {
            foreach (var word in products)
            {
                Trie phead = head;
                foreach (var c in word)
                {
                    var w = c - 'a';
                    if (phead.m_tries[w]==null)
                    {
                        phead.m_tries[w] = new Trie();
                    }
                    phead = phead.m_tries[w];
                    phead.m_list.Add(word);
                }
            }
        }
    }

    
    
}
