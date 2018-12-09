using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class StringUtilities
    {
        public static string MatchingBeginnings(string str1, string str2)
        {
            string[] str_split1 = Clean(str1);
            string[] str_split2 = Clean(str2);
            List<string> matching = new List<string>();



            for (int i = 0; i < Math.Min(str_split1.Length, str_split2.Length) && str_split1[i] == str_split2[i]; i++)
            {
                if (str_split1[i].Length > 0)
                    matching.Add(str_split1[i]);
            }
            string ret = string.Join(" ", matching).TrimEnd(' ', '-', ':', '.');
            return ret;
        }

        public static string[] Clean(string title)
        {
            title = title.Replace('-', ' ');
            string[] title_split = title.Split();
            for (int i = 0; i < title_split.Length; i++)
            {
                title_split[i] = title_split[i].ToLower().Trim(' ', '-', ':', '.', ',');
            }

            return title_split;
        }


    }
}
