namespace Dawud.Extensions.GeneralExtensions.Extensions;

using System;
using System.ComponentModel;
using System.Runtime.Serialization;


public class CsException : Exception
{
    public CsException()
    {
    }


    public CsException(string message) : base(message)
    {
    }


    public CsException(string message, Exception innerException) : base(message, innerException)
    {
    }


    public CsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }




    public sealed class InvalidArgument : CsException
    {
        public InvalidArgument(string argumentName, string message) : base(
            $"The argument [{argumentName}] is invalid: {message}")
        {
        }


        public InvalidArgument(string argumentName, Reasons reason) : base(
            $"The argument [{argumentName}] is invalid: {reason}")
        {
        }




        public enum Reasons
        {
            [Description("May not be [null].")] MayNotBeNull,
            [Description("May not be [empty].")] MayNotBeEmpty,

            [Description("May not be [null] or [empty].")]
            MayNotBeNullOrEmpty,
            [Description("May not be [0].")] MayNotBeZero,

            [Description("May not be [negative].")]
            MayNotBeNegative,

            [Description("May not be [0] or [negative].")]
            MayNotBeZeroOrNegative,
            [Description("Must exist.")] MustExist,
            [Description("May not exist.")] MayNotExist,
        }
    }

    public sealed class NotImplemented : CsException
    {
        public NotImplemented(string message) : base(message)
        {
        }
    }

    public sealed class NotFound : CsException
    {
        public NotFound(string message) : base(message)
        {
        }
    }
}
