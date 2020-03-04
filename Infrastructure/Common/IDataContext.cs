using System.Data;

namespace Infrastructure.Common
{
    public interface IDataContext
    {
        /// <summary>
        /// Timeout in seconds
        /// </summary>
        int DefaultTimeout { get; }

        IDbConnection Connection { get; }
    }
}
