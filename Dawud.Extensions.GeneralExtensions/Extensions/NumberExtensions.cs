namespace Dawud.Extensions.GeneralExtensions.Extensions;

public static class NumberExtensions
	{

		/// <summary>
		///     true if <paramref name="val" /> is zero.
		/// </summary>
		public static bool IsZero(this int val) =>
			val == 0;


		/// <summary>
		///     true if <paramref name="val" /> is negative.
		/// </summary>
		public static bool IsNegative(this int val) =>
			val < 0;


		/// <summary>
		///     true if <paramref name="val" /> is zero or negative.
		/// </summary>
		public static bool IsZeroOrNegative(this int val) =>
			val <= 0;


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this ulong value)
		{
			if (value < 1024)
				return value + " B";

			var val = value / 1024.0;

			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " KB";
			val = val / 1024.0;
			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " MB";
			val = val / 1024.0;
			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " GB";
			val = val / 1024.0;
			return Math.Round(val, 3).ToString("0.000") + " TB";
		}


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this ulong? value) =>
			(value == null ? null : ToByteSizeString(value.Value))!;


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this uint value) =>
			ToByteSizeString((ulong) value);


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this uint? value) =>
			(value == null ? null : ToByteSizeString(value.Value))!;


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this long value) =>
			ToByteSizeString((ulong) value);


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this long? value) =>
			(value == null ? null : ToByteSizeString((ulong) value))!;


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this int value) =>
			ToByteSizeString((ulong) value);


		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this int? value) =>
			(value == null ? null : ToByteSizeString((ulong) value))!;
	}