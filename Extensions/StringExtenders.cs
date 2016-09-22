using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.Extenders
{
    public static class StringExtenders
    {
        public static int GetWordCount(this string s, string ignoreRegexPattern = null)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            //remove anything that matches the ignoreRegexPattern so we don't count it
            if (!string.IsNullOrEmpty(ignoreRegexPattern))
            {
                Regex r = new Regex(ignoreRegexPattern, RegexOptions.None);
                s = r.Replace(s, string.Empty);
            }
            
            //passing in null as delimiter since we want all whitespaces
            return s.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string RemoveWords(this string s, string removeRegexPattern)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            //remove anything that matches the ignoreRegexPattern so we don't count it
            if (!string.IsNullOrEmpty(removeRegexPattern))
            {
                Regex r = new Regex(removeRegexPattern, RegexOptions.None);
                s = r.Replace(s, string.Empty);
            }

            return s;
        }

        /// <summary>
        /// removes unicode that is not valid for xml writes
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveInvalidXml(this string text)
        {
            return Regex.Replace(text, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
        }

        /// <summary>
        /// counts the amount of times a string starts with a substring
        /// </summary>
        /// <param name="text">the text that has the substring</param>
        /// <param name="compare">the substring to look for in the text</param>
        /// <param name="comparer">the comparison type for the count</param>
        /// <returns>the count</returns>
        public static int StartWithCount(this string text, string compare, StringComparison comparer = StringComparison.Ordinal)
        {
            int count = 0;
            string teststring = text;
            while (teststring.Length >= compare.Length)
            {
                if (teststring.StartsWith(compare, comparer))
                {
                    count++;
                    teststring = teststring.Substring(compare.Length);
                }
                else
                {
                    return count;
                }
            }
            return count;
        }

        #region singular/plural word extension
        /// <summary>
        /// Generic equals that takes in extra pluralized boolean to check for boolean or not
        /// </summary>
        /// <param name="text"></param>
        /// <param name="compare"></param>
        /// <param name="comparer"></param>
        /// <param name="pluralcheck"></param>
        /// <returns></returns>
        public static bool Equals(this string text, string compare, StringComparison comparer = StringComparison.Ordinal, bool pluralcheck = false)
        {
            if(pluralcheck)
                return PluralEquals(text, compare, comparer);
                
            else
                return string.Equals(text, compare, comparer);
        }

        /// <summary>
        /// compares two strings to see if they are equal if you add 's' or 'es' (english only)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="compare"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool PluralEquals(string text, string compare, StringComparison comparer = StringComparison.Ordinal)
        {
            //initial check to see if the base values are equal
            if (string.Equals(text, compare, comparer))
                return true;

            //get 's' instances
            var texts = text ?? string.Empty;
            texts = texts + "s";

            var compares = compare ?? string.Empty;
            compares = compares + "s";

            //check 's' variants
            if (string.Equals(text, compares, comparer))
                return true;

            if (string.Equals(texts, compare, comparer))
                return true;

            //get 'es' instances
            var textes = text ?? string.Empty;
            textes = textes + "es";

            var comparees = compare ?? string.Empty;
            comparees = comparees + "es";

            //check 'es' varients
            if (string.Equals(text, comparees, comparer))
                return true;

            if (string.Equals(textes, compare, comparer))
                return true;

            return false;
        }
        #endregion

        /// <summary>
        /// removes ending non alphanumeric character from text
        /// </summary>
        /// <param name="text">the text we want the characters removed</param>
        /// <returns>the string without ending symbols</returns>
        public static string RemoveEndingSymbol(this string text)
        {
            int i = text.Length - 1;
            while (i >= 0 && !char.IsLetterOrDigit(text[i]))
            {
                i--;
            }

            return i <= -1 ? string.Empty : text.Substring(0, i + 1);
        }

        #region Phrase checks
        /// <summary>
        /// Checks if the text contains the same words as the subtext while punctuation
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <param name="comparer"></param>
        /// <param name="pluralcheck"></param>
        /// <returns></returns>
        public static bool ContainsWords(string text, string subtext, StringComparison comparer = StringComparison.Ordinal, bool pluralcheck = false, bool removeendsymbol = false)
        {
            string[] orgtext = text.Split();
            string[] checktext = subtext.Split();

            Stack<int> comparesubtextindex = new Stack<int>();

            for (int i = 0; i < orgtext.Length; i++)
            {
                //if the first word for the check matches continue check at this index
                if (StringExtenders.Equals(removeendsymbol == true ? orgtext[i].RemoveEndingSymbol() : orgtext[i], removeendsymbol == true ? checktext[0].RemoveEndingSymbol() : checktext[0], comparer, pluralcheck))
                {
                    comparesubtextindex.Push(0);
                }

                Stack<int> newcompairsubtext = new Stack<int>();
                //loop through the ones that have matched so far
                while (comparesubtextindex.Count != 0)
                {
                    int comparesubtextstartindex = comparesubtextindex.Pop();

                    if (StringExtenders.Equals(removeendsymbol == true ? orgtext[i].RemoveEndingSymbol() : orgtext[i], removeendsymbol == true ? checktext[comparesubtextstartindex].RemoveEndingSymbol() : checktext[comparesubtextstartindex], comparer, pluralcheck))
                    {
                        comparesubtextstartindex++;
                        //there is at least one occurance for matching since we reached the end of the subtext that we're checking for
                        if (comparesubtextstartindex >= checktext.Length)
                            return true;

                        //continue to check the subtext on the next word
                        newcompairsubtext.Push(comparesubtextstartindex);
                    }
                }

                //set our comparestack for the next word iteration
                comparesubtextindex = newcompairsubtext;
            }
            return false;
        }

        /// <summary>
        /// Checks if the text contains the same words as the subtext with all words removing 's' and 'es'
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool ContainsPluralWords(this string text, string subtext, StringComparison comparer = StringComparison.Ordinal)
        {
            return StringExtenders.ContainsWords(text, subtext, comparer, true, false);
        }

        /// <summary>
        /// Checks if the text contains the same words as the subtext
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool ContainsWords(this string text, string subtext, StringComparison comparer = StringComparison.Ordinal)
        {
            return StringExtenders.ContainsWords(text, subtext, comparer, false, false);
        }
        #endregion

        #region wildcardsearches
        private const char SINGLE_WILDCARD = '?';
        private const char MULTI_WILDCARD = '*';

        private static bool isMatch(string pattern, string input, char singleWildcard, char multipleWildcard)
        {
            string escapedSingle = Regex.Escape(new string(singleWildcard, 1));
            string escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace(escapedSingle, ".");
            pattern = pattern.Replace("\\\\.", "\\?");
            pattern = "^" + pattern.Replace(escapedMultiple, ".*") + "$";
            pattern = pattern.Replace("\\\\.*", "\\*");
            pattern = Regex.Replace(pattern, @"(?<!\\)\\\\n", @"(\n|\r\n)", RegexOptions.None);
            pattern = pattern.Replace("\\\\", "\\");
            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }

        public static string Replace(this string input, Dictionary<string, string> replacepairs, StringComparison compare = StringComparison.Ordinal)
        {
            StringBuilder newstring = new StringBuilder();

            Dictionary<string, Stack<int>> comparesubtextindices = new Dictionary<string, Stack<int>>();
            
            for (int i = 0; i < input.Length; i++)
            {
                string replacestring = string.Empty;
                foreach (var replace in replacepairs)
                {
                    if (!comparesubtextindices.ContainsKey(replace.Key))
                    {
                        comparesubtextindices.Add(replace.Key, new Stack<int>());
                    }

                    //if the first word for the check matches continue check at this index
                    if (string.Equals(input[i].ToString(), replace.Key[0].ToString(), compare))
                    {
                        comparesubtextindices[replace.Key].Push(0);
                    }

                    Stack<int> newcompairsubtext = new Stack<int>();
                    //loop through the ones that have matched so far
                    while (comparesubtextindices[replace.Key].Count != 0)
                    {
                        int comparesubtextstartindex = comparesubtextindices[replace.Key].Pop();

                        if (string.Equals(input[i].ToString(), replace.Key[comparesubtextstartindex].ToString(), compare))
                        {
                            comparesubtextstartindex++;
                            //there is at least one occurance for matching since we reached the end of the subtext that we're checking for
                            if (comparesubtextstartindex >= replace.Key.Length)
                            {
                                replacestring = replace.Key;
                                //now that we found a match, reset all other searches and continue
                                foreach (var clear in comparesubtextindices)
                                {
                                    clear.Value.Clear();
                                }
                            }
                            else
                            {
                                //continue to check the subtext on the next word
                                newcompairsubtext.Push(comparesubtextstartindex);
                            }
                        }
                    }

                    //set our comparestack for the next word iteration if we haven't found a match already
                    if(string.IsNullOrEmpty(replacestring))
                        comparesubtextindices[replace.Key] = newcompairsubtext;
                }
                newstring.Append(input[i]);
                if (!string.IsNullOrEmpty(replacestring))
                {
                    newstring.Remove(newstring.Length - replacestring.Length, replacestring.Length);
                    newstring.Append(replacepairs[replacestring]);
                }
            }

            return newstring.ToString();
        }

        /// <summary>
        /// Used for filtering with wildcards
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="useWildcards"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool Contains(this string input, string pattern, bool useWildcards, RegexOptions options)
        {
            bool result = true;

            if (!useWildcards)
            {
                var escapedpattern = Regex.Escape(pattern);
                escapedpattern = Regex.Replace(escapedpattern, @"\\\\n", @"(\r|\n|\r\n)", options);
                Regex reg = new Regex(escapedpattern, options);

                result = reg.IsMatch(input);
            }
            else
            {
                result = isMatch("*" + pattern + "*", input, SINGLE_WILDCARD, MULTI_WILDCARD);
            }

            return result;
        }

        /// <summary>
        /// Used for filtering with wildcards
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="useWildcards"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool NotContains(this string input, string pattern, bool useWildcards, RegexOptions options)
        {
            bool result = true;

            if (!useWildcards)
            {
                pattern = Regex.Escape(pattern);
                pattern = Regex.Replace(pattern, @"(?<!\\)\\\\n", @"(\n|\r\n)", options);
                pattern = pattern.Replace("\\\\", "\\");
                Regex reg = new Regex(pattern, options);
                
                result = !reg.IsMatch(input);
            }
            else
            {
                result = !isMatch("*" + pattern + "*", input, SINGLE_WILDCARD, MULTI_WILDCARD);
            }

            return result;
        }

        /// <summary>
        /// Used for filtering with wildcards
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="useWildcards"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool BeginsWith(this string input, string pattern, bool useWildcards, RegexOptions options)
        {
            bool result = true;

            if (!useWildcards)
            {
                pattern = Regex.Escape(pattern);
                pattern = Regex.Replace(pattern, @"(?<!\\)\\\\n", @"(\n|\r\n)", options);
                pattern = pattern.Replace("\\\\", "\\");
                Regex reg = new Regex("\\A" + pattern, options);
                result = reg.IsMatch(input);
            }
            else
            {
                result = isMatch(pattern + "*", input, SINGLE_WILDCARD, MULTI_WILDCARD);
            }

            return result;
        }

        /// <summary>
        /// Used for filtering with wildcards
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="useWildcards"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool EndsWith(this string input, string pattern, bool useWildcards, RegexOptions options)
        {
            bool result = true;

            if (!useWildcards)
            {
                pattern = Regex.Escape(pattern);
                pattern = Regex.Replace(pattern, @"(?<!\\)\\\\n", @"(\n|\r\n)", options);
                pattern = pattern.Replace("\\\\", "\\");
                Regex reg = new Regex(pattern + "\\Z", options);
                result = reg.IsMatch(input);
            }
            else
            {
                result = isMatch("*" + pattern, input, SINGLE_WILDCARD, MULTI_WILDCARD);
            }

            return result;
        }

        public static bool Equals(this string input, string pattern, bool useWildcards, RegexOptions options)
        {
            bool result = true;

            if (!useWildcards)
            {
                pattern = Regex.Escape(pattern);
                pattern = Regex.Replace(pattern, @"(?<!\\)\\\\n", @"(\n|\r\n)", options);
                pattern = pattern.Replace("\\\\", "\\");
                Regex reg = new Regex("\\A" + pattern + "\\Z", options);
                result = reg.IsMatch(input);
            }
            else
            {
                result = isMatch(pattern, input, SINGLE_WILDCARD, MULTI_WILDCARD);
            }

            return result;
        }

        public static List<Match> FindMatches(this string text, string tofind, int startindex = 0, RegexOptions options = RegexOptions.CultureInvariant)
        {
            List<Match> indices = new List<Match>();

            //nullchecks
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(tofind))
                return indices;

            if (startindex >= text.Length)
                return indices;

            var matches = Regex.Matches(text, tofind, options);
            foreach (var m in matches)
            {
                Match match = m as Match;
                if (match.Index >= startindex)
                {
                    indices.Add(match);
                }
            }
            return indices;
        }

        public static T GetEnum<T>(this string enumstring) where T : struct, IComparable, IConvertible, IFormattable
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException(string.Format("{0} is not a type of enum", type.Name));

            return (T)Enum.Parse(typeof(T), enumstring);
        }
        #endregion
    }
}
