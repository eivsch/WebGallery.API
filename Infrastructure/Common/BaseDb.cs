using DomainModel.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Common
{
    public abstract class BaseDb : IDataContext
    {
        private readonly string _connectionString;
        private readonly int _connectionTimeout;

        /// <summary>
        /// Initialises the base database class
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="connectionTimeout">Default connection timeout in seconds</param>
        public BaseDb(string connectionString, int connectionTimeout = 15)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InfrastructureLayerException($"{nameof(connectionString)} is empty");

            _connectionString = connectionString;
            _connectionTimeout = connectionTimeout;
        }

        public int DefaultTimeout => _connectionTimeout;

        public IDbConnection Connection
        {
            get
            {
                var conn = new SqlConnection
                {
                    ConnectionString = _connectionString
                };

                return conn;
            }
        }
    }
}
