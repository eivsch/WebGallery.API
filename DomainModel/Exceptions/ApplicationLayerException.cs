using Serilog;
using System;

namespace DomainModel.Exceptions
{
    /// <summary>
    /// Exception type for application exceptions
    /// </summary>
    public sealed class ApplicationLayerException : Exception
    {
        public ApplicationLayerException()
        { }

        public ApplicationLayerException(string message) : base(message)
        {
            Log.Warning(message);
        }

        public ApplicationLayerException(string message, Exception innerException) : base(message, innerException)
        {
            Log.Error(innerException, message);
        }
    }
}