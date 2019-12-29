using System;
using System.Collections.Generic;

namespace OAcoding.ReorderLogFiles
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            /*

             Input: logs = [
                             "dig1 8 1 5 1",
                             "let1 art can",
                             "dig2 3 6",
                             "let2 own kit dig",
                             "let3 art zero"
                            ]



            Output: [
                      "let1 art can",
                      "let3 art zero",
                      "let2 own kit dig",
                      "dig1 8 1 5 1",
                      "dig2 3 6"
                     ]


             */
            var logs = new string[] {
                             "dig1 8 1 5 1",
                             "let1 art can",
                             "dig2 3 6",
                             "let2 own kit dig",
                             "let3 art zero"
             };
            var res = ReorderLogFiles(logs);
            Console.WriteLine(string.Join(", ",res));
            Console.ReadKey();
        }

        public static string[] ReorderLogFiles(string[] logs)
        {
            if (logs.Length == 0)
            {
                return new string[0];
            }

            var letterLogsOrdered = new SortedSet<string>(new LetterLogComparer());
            var digitLogs = new List<string>();
            for (int i = 0; i < logs.Length; i++)
            {
                var log = logs[i];

                var firstSpace = log.IndexOf(" ", StringComparison.CurrentCulture);
                var firstChar = Convert.ToString(log[firstSpace + 1]);

                int digit = -1;
                if (int.TryParse(firstChar, out digit))
                {
                    digitLogs.Add(log);
                }
                else
                {
                    letterLogsOrdered.Add(log);
                }
            }

            var result = new List<string>();

            foreach (var log in letterLogsOrdered)
            {
                result.Add(log);
            }

            foreach (var log in digitLogs)
            {
                result.Add(log);
            }
            
            return result.ToArray();
        }
    }


    public class LetterLogComparer : IComparer<string>
    {
        public int Compare(string one, string two)
        {
            var oneWithoutIdentifier = RemoveIdentifier(one);
            var twoWithoutIdentifier = RemoveIdentifier(two);

            if (oneWithoutIdentifier.Equals(twoWithoutIdentifier))
            {
                // a match was found, so compare the original strings with the identifier included
                return string.Compare(one, two);
            }

            return string.Compare(oneWithoutIdentifier, twoWithoutIdentifier);
        }

        private string RemoveIdentifier(string str)
        {
            var firstSpace = str.IndexOf(" ", StringComparison.Ordinal);
            return str.Substring(firstSpace + 1, str.Length - firstSpace - 1);
        }
    }

}
