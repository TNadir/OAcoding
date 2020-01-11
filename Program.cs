using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OAcoding
{


    public class Solution
    {
        // should contain the list of all words, or you can use any other data structure (e.g. a Trie)
        private HashSet<string> dictionary;

        public String parse(String s)
        {
            return parse(s, new Dictionary<String, String>());
        }

        public String parse(String s, Dictionary<string, string> map)
        {
            if (map.ContainsKey(s))
            {
                return map[s];
            }
            if (dictionary.Contains(s))
            {
                return s;
            }
            for (int left = 1; left < s.Length; left++)
            {
                String leftSub = s.Substring(0, left);
                if (!dictionary.Contains(leftSub))
                {
                    continue;
                }
                String rightSub = s.Substring(left);
                String rightParsed = parse(rightSub, map);
                if (rightParsed != null)
                {
                    String parsed = leftSub + " " + rightParsed;
                    map.Add(s, parsed);
                    return parsed;
                }
            }
            map.Add(s, null);
            return null;
        }
    }

    class TopWord
    {
        public string word;
        public int count;
        public HashSet<int> quotaIds;
        public TopWord(string word, int count)
        {
            this.word = word;
            this.count = count;
            quotaIds = new HashSet<int>();
        }


        public int GetCount()
        {
            return this.quotaIds.Count;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {

            /*
              elmo - 4
              elsa - 4



             */

            var quotes = new List<string> {
"elsa is the hottest of the season! elsa will be on every kid's wishlist!",
"The new elsa dolls are super high quality",
"Expect the elmo dolls to be very popular this year, elmo!",
"Elsa and elmo are the toys I'll be buying for my kids, elmo is good",
"For parents of older kids, look into buying them a drone",
"Warcraft is slowly rising in popularity ahead of the holiday season"};

            var toys = new List<string>
            {
              "elmo", "elsa", "legos", "drone", "tablet", "warcraft"
             };


            int topToys = 2;

            var orderedWords = new SortedSet<TopWord>(new LetterLogComparer());

            foreach (var item in toys)
            {
                string toy = item.ToLower();
                int quotaId = 0;
                var wordObject = new TopWord(toy, 0);
                foreach (var line in quotes)
                {
                    var lower = line.ToLower();
                    var count = lower.Split(new[] { ' ', ',', '.', ':', '!', '?', '\'' }, StringSplitOptions.RemoveEmptyEntries).Count(k => toy == k);
                    if (count > 0)
                    {
                        wordObject.count += count;
                        wordObject.quotaIds.Add(quotaId);
                    }
                    quotaId++;
                }
                orderedWords.Add(wordObject);
            }

            var res = orderedWords.Take(topToys);

            //var result = wrds.GroupBy(n => n)
            //    // Order by the frequency 
            //    .OrderByDescending(g => g.Count())
            //     .ThenBy(g => g.Key)
            //    .Select(k => new { word = k.Key, count = k.Count() })
            //    .Take(topToys).ToList();

            foreach (var entry in res)
            {
                Console.WriteLine(entry.word + " - " + entry.count);
            }

            Console.ReadKey();
        }

        public class LetterLogComparer : IComparer<TopWord>
        {
            public int Compare(TopWord one, TopWord two)
            {
                if (one.count!=two.count)
                {
                    return two.count.CompareTo(one.count);
                }

                if (one.GetCount()!=two.GetCount())
                {
                    return two.GetCount().CompareTo(one.GetCount());
                }

                return string.Compare(two.word, one.word, StringComparison.Ordinal);
            }
        }

        //public static List<string> decode(string prefix, string code)
        //{
        //    List<string> set = new List<string>();
        //    if (code.Length == 0)
        //    {
        //        set.Add(prefix);
        //        return set;
        //    }

        //    if (code[0] == '0')
        //        return set;
        //    char cal = (char)(code[0] - '1' + 'a');
        //    prefix = prefix + cal;
        //    set.AddRange(decode(prefix, code.Substring(1)));
        //    if (code.Length >= 2 && code[0] == '1')
        //    {
        //        prefix = prefix + (char)(10 + code[1] - '1' + 'a');
        //        set.AddRange(decode(prefix, code.Substring(2)));
        //    }
        //    if (code.Length >= 2 && code[0] == '2' && code[1] <= '6')
        //    {
        //        prefix = prefix + (char)(20 + code[1] - '1' + 'a');
        //        set.AddRange(decode(prefix, code.Substring(2)));
        //    }
        //    return set.ToList();
        //}


    }
}
