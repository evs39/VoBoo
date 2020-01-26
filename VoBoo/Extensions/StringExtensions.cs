using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Extensions
{
	public static class StringExtensions
	{
		public static string TrimAndToLower(this string input) => input.Trim().ToLower();
		public static string FirstCharToUpper(this string input) => input.Substring(0, 1).ToUpper() + input.Substring(1);
		public static string EachSentenceWordTrimAndOnlyFirstCharToUpper(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			var result = input.TrimAndToLower();
			var words = result.Split('\t', '\x20');

			result = words[0].FirstCharToUpper();
			for (int i = 1; i < words.Length; i++)
			{
				result += '\x20' + words[i];
			}

			return result;
		}
	}
}
