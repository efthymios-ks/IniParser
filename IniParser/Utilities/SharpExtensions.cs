using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharpUtilities
{
    /// <summary>
    /// Linq extensions. 
    /// </summary>
    internal static partial class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> Source, Func<TSource, TKey> KeySelector)
        {
            return Source.DistinctBy(KeySelector, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> Source, Func<TSource, TKey> KeySelector, IEqualityComparer<TKey> Comparer)
        {
            if (Source == null)
                throw new ArgumentNullException(nameof(Source));
            if (KeySelector == null)
                throw new ArgumentNullException(nameof(KeySelector));

            var seenKeys = new HashSet<TKey>(Comparer);
            foreach (TSource element in Source)
                if (seenKeys.Add(KeySelector(element)))
                    yield return element;
        }
    }

    /// <summary>
    /// StringBuilder extensions.
    /// </summary>
    internal static partial class StringBuilderExtensions
    {
        /// <summary>
        /// Append format + new line. 
        /// </summary>
        public static void AppendFormatLine(this StringBuilder Builder, string Format, params object[] Parameters)
        {
            Builder.AppendFormat(Format + Environment.NewLine, Parameters);
        }

        /// <summary>
        /// Append format + new line. 
        /// </summary>
        public static void AppendFormatLine(this StringBuilder Builder, CultureInfo Culture, string Format, params object[] Parameters)
        {
            Builder.AppendFormat(Culture, Format + Environment.NewLine, Parameters);
        }
    }

    /// <summary>
    /// String extensions.
    /// </summary>
    internal static partial class StringExtensions
    {     
        public static bool Matches(this string Data, string CompareWith, StringComparison Comparer = StringComparison.InvariantCultureIgnoreCase)
        {
            return String.Equals(Data, CompareWith, Comparer);
        }
    }

    /// <summary>
    /// Generic extensions. 
    /// </summary>
    internal static partial class GenericExtensions
    {
        /// <summary>
        /// Check if value is contained in list. 
        /// </summary>
        public static bool IsIn<T>(this T Source, params T[] Collection)
        {
            if (Source == null)
                return false;
            if (Collection == null)
                return false;
            return Collection.Contains(Source);
        }
    }
}