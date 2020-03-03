using Serilog;
using System;

namespace DomainModel.Exceptions
{
    /// <summary>
    /// Exception type for infrastructure exceptions
    /// </summary>
    public sealed class InfrastructureLayerException : Exception
    {
        public InfrastructureLayerException()
        { }

        public InfrastructureLayerException(string message) : base(message)
        {
            Log.Warning(message);
        }

        public InfrastructureLayerException(string message, Exception innerException) : base(message, innerException)
        {
            Log.Error(innerException, message);
        }

        public InfrastructureLayerException(Exception innerException) : base(innerException.Message, innerException)
        {
            Log.Error(innerException, innerException.Message);
        }
    }
}