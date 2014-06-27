using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using WeiXin.Config;

namespace WeiXin.DAL
{
    public sealed class SqlHelper
    {
        static SqlHelper()
        {
            ConnectionString = ConfigProperty.DataBase_ConnectionString;
        }

        static string ConnectionString { get; set; }

        public static int CommandTimeout = 600;

        #region private utility methods & constructors

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandParameters != null)
            {
                foreach (var p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input)
                            && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }

                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">
        /// Array of SqlParameters to be assigned values
        /// </param>
        /// <param name="dataRow">
        /// The dataRow used to hold the stored procedure's parameter values
        /// </param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            int i = 0;

            // Set the parameters values
            foreach (var commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1)
                {
                    throw new Exception(
                        string.Format(
                            "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                            i,
                            commandParameter.ParameterName));
                }

                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                {
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                }

                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">
        /// Array of SqlParameters to be assigned values
        /// </param>
        /// <param name="parameterValues">
        /// Array of objects holding the values to be assigned
        /// </param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the SqlParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues[i] is IDbDataParameter)
                {
                    var paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        ///     to the provided command
        /// </summary>
        /// <param name="command">
        /// The SqlCommand to be prepared
        /// </param>
        /// <param name="connection">
        /// A valid SqlConnection, on which to execute this command
        /// </param>
        /// <param name="transaction">
        /// A valid SqlTransaction, or 'null'
        /// </param>
        /// <param name="commandType">
        /// The CommandType (stored procedure, text, etc.)
        /// </param>
        /// <param name="commandText">
        /// The stored procedure name or T-SQL command
        /// </param>
        /// <param name="commandParameters">
        /// An array of SqlParameters to be associated with the command or 'null' if no parameters are required
        /// </param>
        /// <param name="mustCloseConnection">
        /// <c>true</c> if the connection was opened by the method, otherwose is false.
        /// </param>
        private static void PrepareCommand(
            SqlCommand command,
            SqlConnection connection,
            SqlTransaction transaction,
            CommandType commandType,
            string commandText,
            SqlParameter[] commandParameters,
            out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException(
                        "The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }

                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(commandType, commandText, null);
        }

        public static int ExecuteNonQuery(
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(ConnectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(CommandType.StoredProcedure, spName);
            }
        }

        private static int ExecuteNonQuery(
             SqlConnection connection,
             CommandType commandType,
             string commandText,
             params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = 0;
            try
            {
                retval = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }

            return retval;
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataset

        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static DataSet ExecuteDataset(string spName, params object[] parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(ConnectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(CommandType.StoredProcedure, spName);
            }
        }

        private static DataSet ExecuteDataset(
            SqlConnection connection,
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (var da = new SqlDataAdapter(cmd))
            {
                var ds = new DataSet();
                ds.Locale = System.Globalization.CultureInfo.InvariantCulture;

                // Fill the DataSet using default values for DataTable names, etc

                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                {
                    connection.Close();
                }

                // Return the dataset
                return ds;
            }
        }

        #endregion ExecuteDataset

        #region ExecuteDataTable

        public static DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataTable(commandType, commandText, null);
        }

        public static DataTable ExecuteDataTable(
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataTable(connection, commandType, commandText, commandParameters);
            }
        }

        public static DataTable ExecuteDataTable(string spName, params object[] parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(ConnectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataTable(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataTable(CommandType.StoredProcedure, spName);
            }
        }

        private static DataTable ExecuteDataTable(
            SqlConnection connection,
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (var da = new SqlDataAdapter(cmd))
            {
                var ds = new DataSet();
                ds.Locale = System.Globalization.CultureInfo.InvariantCulture;

                // Fill the DataSet using default values for DataTable names, etc

                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                {
                    connection.Close();
                }

                // Return the dataset
                return ds.Tables[0];
            }
        }

        #endregion ExecuteDataTable

        #region ExecuteReader

        /// <summary>
        ///     This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        ///     we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,

            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        private static SqlDataReader ExecuteReader(
            SqlConnection connection,
            SqlTransaction transaction,
            CommandType commandType,
            string commandText,
            SqlParameter[] commandParameters,
            SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            bool mustCloseConnection = false;

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            try
            {
                PrepareCommand(
                    cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                // Create a reader
                SqlDataReader dataReader;

                // Call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                // Detach the SqlParameters from the command object, so they can be used again.
                // when the reader is closed, so if the parameters are detached from the command
                // then the SqlReader can磘 set its values. 
                // When this happen, the parameters can磘 be used again in other command.
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }

                throw;
            }
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteReader(commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(
                    connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null)
                {
                    connection.Close();
                }

                throw;
            }
        }

        public static SqlDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(ConnectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(commandType, commandText, null);
        }

        public static object ExecuteScalar(
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public static object ExecuteScalar(string spName, params object[] parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(ConnectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(CommandType.StoredProcedure, spName);
            }
        }

        private static object ExecuteScalar(
            SqlConnection connection,
            CommandType commandType,
            string commandText,
            params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                connection.Close();
            }

            return retval;
        }

        #endregion ExecuteScalar

        public static bool SqlBulkToDB(string desTable, DataTable dt)
        {
            bool flag = false;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var bulkCopy = new SqlBulkCopy(connection);
                bulkCopy.DestinationTableName = desTable;
                bulkCopy.BatchSize = dt.Rows.Count;
                try
                {
                    connection.Open();
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(dt);
                    }

                    flag = true;
                }
                catch (Exception)
                {
                    flag = false;
                }
                finally
                {
                    dt.Dispose();
                    connection.Close();
                    if (bulkCopy != null)
                    {
                        bulkCopy.Close();
                    }
                }
            }

            return flag;
        }
    }

    /// <summary>
    ///     SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    ///     ability to discover parameters for stored procedures at run-time.
    /// </summary>
    public sealed class SqlHelperParameterCache
    {
        #region private methods, variables, and constructors

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlHelperParameterCache()"
        private SqlHelperParameterCache()
        {
        }

        private static readonly Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Resolve at run time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <param name="connection">
        /// A valid SqlConnection object
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <param name="includeReturnValueParameter">
        /// Whether or not to include their return value parameter
        /// </param>
        /// <returns>
        /// The parameter array discovered.
        /// </returns>
        private static SqlParameter[] DiscoverSpParameterSet(
            SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            var cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            var discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (var discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }

            return discoveredParameters;
        }

        /// <summary>
        /// Deep copy of cached SqlParameter array
        /// </summary>
        /// <param name="originalParameters">
        /// </param>
        /// <returns>
        /// </returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            var clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">
        /// A valid connection string for a SqlConnection
        /// </param>
        /// <param name="commandText">
        /// The stored procedure name or T-SQL command
        /// </param>
        /// <param name="commandParameters">
        /// An array of SqlParamters to be cached
        /// </param>
        public static void CacheParameterSet(
            string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">
        /// A valid connection string for a SqlConnection
        /// </param>
        /// <param name="commandText">
        /// The stored procedure name or T-SQL command
        /// </param>
        /// <returns>
        /// An array of SqlParamters
        /// </returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }

            string hashKey = connectionString + ":" + commandText;

            var cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">
        /// A valid connection string for a SqlConnection
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <returns>
        /// An array of SqlParameters
        /// </returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">
        /// A valid connection string for a SqlConnection
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <param name="includeReturnValueParameter">
        /// A bool value indicating whether the return value parameter should be included in the results
        /// </param>
        /// <returns>
        /// An array of SqlParameters
        /// </returns>
        public static SqlParameter[] GetSpParameterSet(
            string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            using (var connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">
        /// A valid SqlConnection object
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <returns>
        /// An array of SqlParameters
        /// </returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">
        /// A valid SqlConnection object
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <param name="includeReturnValueParameter">
        /// A bool value indicating whether the return value parameter should be included in the results
        /// </param>
        /// <returns>
        /// An array of SqlParameters
        /// </returns>
        internal static SqlParameter[] GetSpParameterSet(
            SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            using (var clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">
        /// A valid SqlConnection object
        /// </param>
        /// <param name="spName">
        /// The name of the stored procedure
        /// </param>
        /// <param name="includeReturnValueParameter">
        /// A bool value indicating whether the return value parameter should be included in the results
        /// </param>
        /// <returns>
        /// An array of SqlParameters
        /// </returns>
        private static SqlParameter[] GetSpParameterSetInternal(
            SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            string hashKey = connection.ConnectionString + ":" + spName
                             + (includeReturnValueParameter ? ":include ReturnValue Parameter" : string.Empty);

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions
    }
}