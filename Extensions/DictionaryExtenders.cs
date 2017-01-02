using System.Collections.Generic;

namespace Extenders
{
    /// <summary>DictionaryExtenders</summary>
    /// <author creationDate="10/11/2012 4:14 PM">jjohannes</author>
    /// <revisions>
    /// 	<revision build="3.1.0.28448" user="jjohannes" revisionDate="10/11/2012 4:14 PM">Original Implementation</revision>
    /// </revisions>
    /// <remarks>None</remarks>
    public static class DictionaryExtenders
    {
        /// <summary>ValueOf</summary>
        /// <author creationDate="10/11/2012 4:14 PM">jjohannes</author>
        /// <revisions>
        /// 	<revision build="3.1.0.28448" user="jjohannes" revisionDate="10/11/2012 4:14 PM">Original Implementation</revision>
        /// </revisions>
        /// <param name="dict" type="System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;"></param>
        /// <param name="key" type="TKey"></param>
        /// <returns type="TValue"></returns>
        /// <remarks>None</remarks>
        public static TValue ValueOf<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            TValue val;
            if (!dict.TryGetValue(key, out val)) return default(TValue);
            return val;
        }
    }
}
