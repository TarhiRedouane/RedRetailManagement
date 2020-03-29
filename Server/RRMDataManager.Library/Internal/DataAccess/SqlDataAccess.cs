using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RRMDataManager.Library.Internal.DataAccess
{
    public class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        /// <summary>
        /// Method that Load data from dataBase using dapper framework 
        /// </summary>
        /// <typeparam name="T">generic type that will be returned in a list</typeparam>
        /// <typeparam name="TV">type of parameters that will be passed to the stored procedure</typeparam>
        /// <param name="storedProcedure">the name of the stored procedure in sql</param>
        /// <param name="parameters">the parameters that will be passed to the stored procedure</param>
        /// <param name="connectionStringName">the name of the connection string</param>
        /// <returns></returns>
        public List<T> LoadData<T, TV>(string storedProcedure, TV parameters, string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection cnx = new SqlConnection(connectionString))
            {
                var rows = cnx.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }

        }

        /// <summary>
        /// Method that gona save data in sql 
        /// </summary>
        /// <typeparam name="TV">type of parameters that gonna be passed to store procedure</typeparam>
        /// <typeparam name="T">the return type </typeparam>
        /// <param name="storedProcedure">the stored procedure name located in sql</param>
        /// <param name="parameters">the parameters that gonna bed passed to the stored procedure</param>
        /// <param name="connectionStringName">the name of the connection string</param>
        /// <returns>returns something from the stored procedure if we explicitly says so</returns>
        public T SaveData<T, TV>(string storedProcedure, TV parameters, string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection cnx = new SqlConnection(connectionString))
            {
                return cnx.ExecuteScalar<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            }

        }

        #region Transaction

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }
        public void Dispose()
        {
            CommitTransaction();
        }
        /// <summary>
        /// Method that Load data from dataBase using dapper framework 
        /// </summary>
        /// <typeparam name="T">generic type that will be returned in a list</typeparam>
        /// <typeparam name="TV">type of parameters that will be passed to the stored procedure</typeparam>
        /// <param name="storedProcedure">the name of the stored procedure in sql</param>
        /// <param name="parameters">the parameters that will be passed to the stored procedure</param>
        /// <returns></returns>
        public List<T> LoadDataInTransaction<T, TV>(string storedProcedure, TV parameters)
        {
            var rows = _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }
        /// <summary>
        /// Method that gona save data in sql 
        /// </summary>
        /// <typeparam name="TV">type of parameters that gonna be passed to store procedure</typeparam>
        /// <typeparam name="T">the return type </typeparam>
        /// <param name="storedProcedure">the stored procedure name located in sql</param>
        /// <param name="parameters">the parameters that gonna bed passed to the stored procedure</param>
        /// <returns>returns something from the stored procedure if we explicitly says so</returns>
        public T SaveDataInTransaction<T, TV>(string storedProcedure, TV parameters)
        {
            return _connection.ExecuteScalar<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        #endregion


    }
}
