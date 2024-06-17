namespace Dawud.Extensions.GeneralExtensions.Extensions;

using System.IO;

public static class CsExceptionExtension
{
	public static void MustExist(this DirectoryInfo obj, string argumentName)
	{
		if (obj.Exists)
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MustExist);
	}

	public static void MustExist(this FileInfo obj, string argumentName)
	{
		if (obj.Exists)
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MustExist);
	}

	public static void MayNotExist(this FileInfo obj, string argumentName)
	{
		if (!obj.Exists)
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotExist);
	}

	public static void DirectoryMayNotBeNull(this FileInfo obj, string argumentName)
	{
		if (obj.Directory != null)
			return;
		throw new CsException.InvalidArgument(argumentName, "The directory of the file may not be null.");
	}

	public static void MayNotBeNull(this object obj, string argumentName)
	{
		if (!obj.IsNull())
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotBeNull);
	}

	public static string MayNotBeNullOrEmpty(this string obj, string argumentName)
	{
		if (!obj.IsNullOrEmpty())
			return obj;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotBeNullOrEmpty);
	}

	public static void MayNotBeZero(this int obj, string argumentName)
	{
		if (!obj.IsZero())
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotBeZero);
	}

	public static void MayNotBeNegative(this int obj, string argumentName)
	{
		if (!obj.IsNegative())
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotBeNegative);
	}

	public static void MayNotBeZeroOrNegative(this int obj, string argumentName)
	{
		if (!obj.IsZeroOrNegative())
			return;
		throw new CsException.InvalidArgument(argumentName, CsException.InvalidArgument.Reasons.MayNotBeZeroOrNegative);
	}

	public static void MayNotBeGreaterThen(this int obj, string argumentName, int maximum)
	{
		if (obj <= maximum)
			return;
		throw new CsException.InvalidArgument(argumentName, $"May not be greater then [{maximum}].");
	}

	public static void MayNotBeSmallerThen(this int obj, string argumentName, int minimum)
	{
		if (obj >= minimum)
			return;
		throw new CsException.InvalidArgument(argumentName, $"May not be smaller then [{minimum}].");
	}
}