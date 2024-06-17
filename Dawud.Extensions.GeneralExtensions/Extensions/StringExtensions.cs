using System;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Dawud.Extensions.GeneralExtensions.Extensions
{
    public static class StringExtensions
	{
		/// <summary>
		///     returns true if the string is null or "" using the <see cref="string.IsNullOrEmpty" /> function.
		/// </summary>
		public static bool IsNullOrEmpty(this string value) =>
			string.IsNullOrEmpty(value);

        /// <summary>
        ///     returns true if the string is null or "unbekannt" using the <see cref="string.IsNullOrEmpty" /> function.
        /// </summary>
        public static bool IsNullOrEmptyOrUnbekannt(this string value) =>
            string.IsNullOrEmpty(value) || string.Equals(value, "unbekannt");

        /// <summary>
        ///     returns true if the string is null or "" or the specified <paramref name="other"/>.
        /// </summary>
        public static bool IsNullOrEmptyOr(this string value, string other, StringComparison comparison = default(StringComparison)) =>
			string.IsNullOrEmpty(value) || string.Equals(value, other, comparison);


		/// <summary>
		///     returns true if the string is null or "" ignoring the <paramref name="whiteSpaceCharacters" /> using the
		///     <see cref="string.IsNullOrEmpty" /> function.
		/// </summary>
		public static bool IsNullOrWhiteSpace(this string s, string whiteSpaceCharacters = " \t") =>
			string.IsNullOrEmpty(s?.Trim(whiteSpaceCharacters.ToCharArray()));


		/// <summary>returns true if the string can be converted to an valid mail address.</summary>
		public static bool IsValidMailAddress(this string emailAddress)
		{
			emailAddress.MayNotBeNullOrEmpty(nameof(emailAddress));
			try
			{
				// ReSharper disable once ObjectCreationAsStatement
				new MailAddress(emailAddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}


		/// <summary>True if the provided string is a valid hexadecimal representation. Can start with 0x or 0X</summary>
		public static bool IsHex(this string value)
		{
			value.MayNotBeNullOrEmpty(nameof(value));
			if (value.StartsWith("0x") || value.StartsWith("0X"))
				value = value.Substring(2);
			return !value.Any(c => !(c >= '0' && c <= '9') && !(c >= 'a' && c <= 'f') && !(c >= 'A' && c <= 'F'));
		}


		/// <summary>True if the file name does not contain any invalid file name characters.</summary>
		public static bool IsValidFilename(this string filename)
		{
			filename.MayNotBeNullOrEmpty(nameof(filename));
			return !Path.GetInvalidFileNameChars().Any(filename.Contains);
		}


		/// <summary>True if the path does not contain any invalid path characters.</summary>
		public static bool IsValidPath(this string path)
		{
			path.MayNotBeNullOrEmpty(nameof(path));
			if (path.IsNullOrEmpty())
				return false;
			return !Path.GetInvalidPathChars().Any(path.Contains);
		}


		/// <summary>
		///     Converts the specified base64 encoded string back into a byte array.
		/// </summary>
		public static byte[] ToBytesFromBase64(this string value, int offset = 0, int? length = null)
		{
			value.MayNotBeNullOrEmpty(nameof(value));
			offset.MayNotBeNegative(nameof(offset));
			offset.MayNotBeGreaterThen(nameof(offset), value.Length);
			length?.MayNotBeGreaterThen(nameof(length), value.Length - offset);

			return Convert.FromBase64String(offset == 0 && (length == null || length == value.Length) ? value : value.Substring(offset, length ?? value.Length));
		}
		
		/// <summary>
		///     Converts the specified hex encoded string back into a byte array.
		/// </summary>
		public static byte[] ToBytesFromHex(this string value, int offset = 0, int? length = null)
		{
			value.MayNotBeNullOrEmpty(nameof(value));
			offset.MayNotBeNegative(nameof(offset));
			offset.MayNotBeGreaterThen(nameof(offset), value.Length);
			length?.MayNotBeGreaterThen(nameof(length), value.Length - offset);


			var bytes = new byte[length.GetValueOrDefault(value.Length)/2];
			for (var i = 0; i < bytes.Length; i++)
				bytes[i] = Convert.ToByte(value.Substring((i*2)+offset, 2), 16);
			return bytes;
		}
		/// <summary>Encodes the string into a valid html string.</summary>
		public static string ToEscapedHtml(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return SecurityElement.Escape(value);
		}


		/// <summary>
		///     Presents the string as html. Escapes the <paramref name="value" /> and replaces characters to specialiced html
		///     tags if available. '\n' > 'br/'
		/// </summary>
		public static string ToHtmlText(this string value) =>
			value.ToEscapedHtml().Replace("\n", "<br/>");


		/// <summary>
		///     Presents the string as html. Escapes the <paramref name="value" /> and replaces characters to specialiced html
		///     tags if available. '\n' > 'br/'
		/// </summary>
		public static string ToRawHtmlText(this string value) =>
			value.Replace("\n", "<br/>");


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using the provided <paramref name="encoding" />.
		/// </summary>
		public static byte[] ToBytes(this string value, Encoding encoding = null)
		{
			value.MayNotBeNull(nameof(value));
			return (encoding ?? Encoding.Default).GetBytes(value);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.ASCII" />.
		/// </summary>
		public static byte[] ToAsciiBytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.ASCII);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.UTF7" />.
		/// </summary>
		[Obsolete("Obsolete")]
		public static byte[] ToUtf7Bytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.UTF7);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.UTF8" />.
		/// </summary>
		public static byte[] ToUtf8Bytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.UTF8);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.UTF32" />.
		/// </summary>
		public static byte[] ToUtf32Bytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.UTF32);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.BigEndianUnicode" />.
		/// </summary>
		public static byte[] ToBigEndianUnicodeBytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.BigEndianUnicode);
		}


		/// <summary>
		///     Converts the specified <see cref="value" /> into a byte array using <see cref="Encoding.Unicode" />.
		/// </summary>
		public static byte[] ToUnicodeBytes(this string value)
		{
			value.MayNotBeNull(nameof(value));
			return value.ToBytes(Encoding.Unicode);
		}


		/// <summary>Returns a <see cref="FileInfo" /> from the <paramref name="input" />.</summary>
		public static FileInfo ToFileInfo(this string input)
		{
			input.MayNotBeNullOrEmpty(nameof(input));
			return new FileInfo(input);
		}


		/// <summary>Returns a <see cref="DirectoryInfo" /> from the <paramref name="input" />.</summary>
		public static DirectoryInfo ToDirectoryInfo(this string input)
		{
			input.MayNotBeNullOrEmpty(nameof(input));
			return new DirectoryInfo(input);
		}


		/// <summary>
		///     Removes all invalid characters and tries to pluralize the word.
		/// </summary>
		public static string ToValidSqlServerName(this string s, bool pluralize = false)
		{
			s.MayNotBeNullOrEmpty(nameof(s));
			s = s
			    .ReplaceByRegex("^[a-z]", match => match.Value.ToUpper())
			    .ReplaceByRegex("[^a-zA-Z0-9_\\-.]", match => "")
			    .ReplaceByRegex("^[0-9.]*", match => "")
			    .Trim();
			if (!s.EndsWith("s"))
				s = s + "s";
			return s;
		}


		/// <summary>
		///     Replaces all occurances of <see cref="pattern" /> using the <see cref="evaluater" />.
		/// </summary>
		public static string ReplaceByRegex(this string value, string pattern, MatchEvaluator evaluater)
		{
			value.MayNotBeNull(nameof(value));
			pattern.MayNotBeNullOrEmpty(nameof(pattern));
			evaluater.MayNotBeNull(nameof(evaluater));
			return Regex.Replace(value, pattern, evaluater);
		}


		/// <summary>Joins the sequence of strings using the <paramref name="delimiter" />.</summary>
		public static string Join(this IEnumerable<string> items, string delimiter = ", ")
		{
			items.MayNotBeNull(nameof(items));
			delimiter.MayNotBeNull(nameof(delimiter));
			return string.Join(delimiter, items);
		}


		/// <summary>
		///     Expands a string to a specific length by adding a text (<paramref name="expansiontext" />) to the beginning of the
		///     input or
		///     the end. If
		///     <paramref name="input" /> already exceeds or is equal to <paramref name="length" /> the input is returned.
		/// </summary>
		/// <param name="input">the text to be expanded</param>
		/// <param name="length">the destination length</param>
		/// <param name="expansiontext">the text which is used to expand the input.</param>
		/// <param name="insertBefore">Defines if expansion text is added at position 0</param>
		public static string Expand(this string input, int length = 40, string expansiontext = " ", bool insertBefore = false)
		{
			input.MayNotBeNull(nameof(input));
			expansiontext.MayNotBeNullOrEmpty(nameof(expansiontext));
			length.MayNotBeZeroOrNegative(nameof(length));

			if (input.Length > length)
				return input;


			var charsToAdd = length - input.Length;
			var sb         = new StringBuilder("", length);
			while (charsToAdd != 0)
				if (charsToAdd < expansiontext.Length)
				{
					sb.Append(expansiontext.Substring(0, charsToAdd));
					charsToAdd = charsToAdd - charsToAdd;
				}
				else
				{
					sb.Append(expansiontext);
					charsToAdd = charsToAdd - expansiontext.Length;
				}

			if (insertBefore)
				sb.Append(input);
			else
				sb.Insert(0, input);

			return sb.ToString();
		}


		/// <summary>
		///     Cuts a string to a specific length by removing characters from the end of a string. If input length is already
		///     appropriate
		///     the method returns the input.
		/// </summary>
		/// <param name="input">the text to be shortened</param>
		/// <param name="length">the destination length</param>
		/// <param name="appendix">the last characters of shortened string will be replaced with this sequence</param>
		public static string TruncateAtTheEnd(this string input, int length = 100, string appendix = "...")
		{
			input.MayNotBeNullOrEmpty(nameof(input));
			length.MayNotBeZeroOrNegative(nameof(length));
			appendix.Length.MayNotBeGreaterThen(nameof(appendix), length);

			if (input.Length < length)
				return input;
			return input.Substring(0, length - appendix.Length) + appendix;
		}


		/// <summary>
		///     Cuts a string to a specific length by removing characters from the beginning of a string. If input length is
		///     already
		///     appropriate the method returns the input.
		/// </summary>
		/// <param name="input">the text to be shortened</param>
		/// <param name="length">the destination length</param>
		/// <param name="appendix">the first characters of shortened string will be replaced with this sequence</param>
		public static string TruncateAtTheBeginning(this string input, int length = 100, string appendix = "...")
		{
			input.MayNotBeNullOrEmpty(nameof(input));
			length.MayNotBeZeroOrNegative(nameof(length));
			appendix.Length.MayNotBeGreaterThen(nameof(appendix), length);

			if (input.Length < length)
				return input;
			return appendix + input.Substring(input.Length - (length - appendix.Length));
		}


		/// <summary>
		///     Cuts a string to a specific length by removing characters from the middle of a string. If input length is already
		///     appropriate
		///     the method returns the input.
		/// </summary>
		/// <param name="input">the string to be shortened.</param>
		/// <param name="length">the destination length</param>
		/// <param name="insert">the character to be inserted in the middle</param>
		public static string TruncateAtTheMiddle(this string input, int length = 100, string insert = "~")
		{
			input.MayNotBeNullOrEmpty(nameof(input));
			length.MayNotBeZeroOrNegative(nameof(length));
			insert.Length.MayNotBeGreaterThen(nameof(insert), length);
			if (input.Length < length)
				return input;

			var centerInput  = input.Length / 2;
			var toRemove     = input.Length - length + insert.Length;
			var toRemoveHalf = toRemove / 2;
			var insertPoint  = centerInput - toRemoveHalf;

			return input.Remove(insertPoint, toRemove).Insert(insertPoint, insert);
		}


		/// <summary>Removes invalid characters from filename.</summary>
		public static string RemoveInvalidFileNameCharacters(this string filename)
		{
			if (filename.IsNullOrEmpty())
				return filename;
			return Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c.ToString(), string.Empty));
		}


		/// <summary>Removes invalid characters from path.</summary>
		public static string RemoveInvalidPathCharacters(this string path)
		{
			if (path.IsNullOrEmpty())
				return path;
			return Path.GetInvalidPathChars().Aggregate(path, (current, c) => current.Replace(c.ToString(), string.Empty));
		}


		/// <summary>
		///     Removes the <paramref name="leadingString" /> from the value until the <paramref name="value" /> does not start
		///     with
		///     <paramref name="leadingString" />.
		/// </summary>
		/// <param name="value">The string which could start with the <paramref name="leadingString" />.</param>
		/// <param name="leadingString">the string to remove from the beginning of the <paramref name="value" />.</param>
		public static string RemoveLeadingString(this string value, string leadingString)
		{
			leadingString.MayNotBeNullOrEmpty(nameof(leadingString));
			if (value.IsNullOrEmpty())
				return value;
			while (!value.IsNullOrEmpty() && value.StartsWith(leadingString, StringComparison.Ordinal))
				value = value.Substring(leadingString.Length);
			return value;
		}


		/// <summary>
		///     Removes the <paramref name="trailingString" /> from the value until the <paramref name="value" /> does not end with
		///     <paramref name="trailingString" />.
		/// </summary>
		/// <param name="value">The string which could end with the <paramref name="trailingString" />.</param>
		/// <param name="trailingString">the string to remove from the end of the <paramref name="value" />.</param>
		public static string RemoveTrailingString(this string value, string trailingString)
		{
			trailingString.MayNotBeNullOrEmpty(nameof(trailingString));
			if (value.IsNullOrEmpty())
				return value;
			while (!value.IsNullOrEmpty() && value.EndsWith(trailingString, StringComparison.Ordinal))
				value = value.Substring(0, value.Length - trailingString.Length);
			return value;
		}


		/// <summary>
		///     <see cref="Regex" /> matches the <paramref name="value" />.
		/// </summary>
		public static Match Match(this string value, string pattern, RegexOptions options = default(RegexOptions))
		{
			value.MayNotBeNull(nameof(value));
			pattern.MayNotBeNull(nameof(pattern));
			return Regex.Match(value, pattern, options);
		}

		/// <summary>
		///     <see cref="Regex" /> matches the <paramref name="value" />.
		/// </summary>
		public static IEnumerable<Match> Matches(this string value, string pattern, RegexOptions options = default(RegexOptions))
		{
			value.MayNotBeNull(nameof(value));
			pattern.MayNotBeNull(nameof(pattern));
			return Regex.Matches(value, pattern, options).OfType<Match>();
		}
		


		/// <summary>
		///     Returns the extension of the <paramref name="fileName" /> including the leading dot.
		/// </summary>
		public static string ToExtension(this string fileName)
		{
			fileName.MayNotBeNullOrEmpty(nameof(fileName));
			return new FileInfo(fileName).Extension;
		}

        public static string Substring(this string value, int startIndex, string endIndex)
        {
            return value.Substring(startIndex, value.LastIndexOf(endIndex, StringComparison.Ordinal));
        }
	}
}