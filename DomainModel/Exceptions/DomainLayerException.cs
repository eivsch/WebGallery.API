using Serilog;
using System;

namespace DomainModel.Exceptions
{
    /// <summary>
    /// <para>Exception type for domain exceptions</para>
    /// <para>These exceptions occur if business rules (domain logic) are not respected</para>
    /// </summary>
    public sealed class DomainLayerException : Exception
    {
        public DomainLayerException()
        { }

        public DomainLayerException(string message) : base(message)
        {
            Log.Warning(message);
        }

        public DomainLayerException(string message, Exception innerException) : base(message, innerException)
        {
            Log.Error(innerException, message);
        }
    }
}