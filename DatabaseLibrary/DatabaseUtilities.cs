using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace DatabaseLibrary
{
    public static class DatabaseUtilities
    {       
        private static string _dbConnectionString = "Data Source=den1.mssql8.gear.host;Initial Catalog=eventbear2;Persist Security Info=True;User ID=eventbear2;Password=Zb21996D-~TH";//ConfigurationManager.ConnectionStrings["DBConnectionString"].ToSafeString();
        private static int _dbExecuteTimeout = 30;//ConfigurationManager.AppSettings["DBExecuteTimeout"].ToInteger();
        public static DataTable ExecuteQuery(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes)
        {
            DataTable dataTable = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommand(sql, conn, parameterNames, parameterValues, parameterTypes);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    dataTable = new DataTable();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
        public static DataTable ExecuteQuery(string sql, List<DBParameter> paramList = null)
        {
            DataTable dataTable = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommand(sql, conn, paramList);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
        public static List<T> ExecuteQuery<T>(StringBuilder sql, List<DBParameter> paramList = null) where T : new()
        {
            return ExecuteQuery<T>(sql.ToSafeString(), paramList);
        }
        public static List<T> ExecuteQuery<T>(string sql, List<DBParameter> paramList = null) where T : new()
        {
            List<T> dataItems = new List<T>();
            DataTable dataTable = ExecuteQuery(sql, paramList);
            if (dataTable != null)
            {
                dataItems = FillBusinessEntity<T>(dataTable);
            }

            return dataItems;
        }
        public static List<T> ExecuteQuery<T>(StringBuilder sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes) where T : new()
        {
            return ExecuteQuery<T>(sql.ToSafeString(), parameterNames, parameterValues, parameterTypes);
        }
        public static List<T> ExecuteQuery<T>(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes) where T : new()
        {
            List<T> dataItems = new List<T>();
            DataTable dataTable = ExecuteQuery(sql, parameterNames, parameterValues, parameterTypes);
            if (dataTable != null)
            {
                dataItems = FillBusinessEntity<T>(dataTable);
            }
            return dataItems;
        }
        public static int ExecuteNonQuery(StringBuilder sql, List<DBParameter> paramList = null, bool beginTrans = false)
        {
            return ExecuteNonQuery(sql.ToSafeString(), paramList, beginTrans);
        }
        public static int ExecuteNonQuery(string sql, List<DBParameter> paramList = null, bool beginTrans = false)
        {
            int affectedRow = 0;

            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlTransaction tran = null;

                try
                {
                    conn.Open();
                    if (beginTrans)
                    {
                        tran = conn.BeginTransaction();
                    }

                    SqlCommand command = GenerateSqlCommand(sql, conn, paramList, tran);
                    command.CommandType = CommandType.Text;
                    affectedRow = command.ExecuteNonQuery();

                    if (beginTrans)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (beginTrans)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
            }
            return affectedRow;
        }
        public static int ExecuteNonQuery(StringBuilder sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes, bool beginTrans = false)
        {
            return ExecuteNonQuery(sql.ToSafeString(), parameterNames, parameterValues, parameterTypes, beginTrans);
        }
        public static int ExecuteNonQuery(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes, bool beginTrans = false)
        {
            int affectedRow = 0;
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlTransaction tran = null;
                try
                {
                    conn.Open();
                    if (beginTrans)
                    {
                        tran = conn.BeginTransaction();
                    }

                    SqlCommand command = GenerateSqlCommand(sql, conn, parameterNames, parameterValues, parameterTypes, tran);
                    command.CommandType = CommandType.Text;
                    affectedRow = command.ExecuteNonQuery();

                    if (beginTrans)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (beginTrans)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
            }
            return affectedRow;
        }
        public static string ExecuteScalar(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes)
        {
            string value = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommand(sql, conn, parameterNames, parameterValues, parameterTypes);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    value = command.ExecuteScalar().ToSafeString();
                }
            }
            catch (NullReferenceException)
            {
                value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }
        public static string ExecuteScalar(string sql, List<DBParameter> paramList = null)
        {
            string value = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommand(sql, conn, paramList);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    value = command.ExecuteScalar().ToSafeString();
                }
            }
            catch (NullReferenceException)
            {
                value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }
        public static T ExecuteScalar<T>(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes) where T : new()
        {
            T value = new T();
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    SqlCommand command = GenerateSqlCommand(sql, conn, parameterNames, parameterValues, parameterTypes);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    conn.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                    if (dataTable != null
                        && dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            PropertyInfo property = value.GetType().GetProperty(column.ColumnName);
                            if (row[column] != DBNull.Value && property != null)
                            {
                                property.SetValue(value, row[column], null);
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                value = new T();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }
        public static T ExecuteScalar<T>(string sql, List<DBParameter> paramList = null) where T : new()
        {
            T value = new T();
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {

                    SqlCommand command = GenerateSqlCommand(sql, conn, paramList);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    conn.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                    if (dataTable != null
                        && dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            PropertyInfo property = value.GetType().GetProperty(column.ColumnName);
                            if (row[column] != DBNull.Value && property != null)
                            {
                                property.SetValue(value, row[column], null);
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                value = default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        public static DataTable ExecuteQueryStore(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes)
        {
            DataTable dataTable = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommandStore(sql, conn, parameterNames, parameterValues, parameterTypes);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    dataTable = new DataTable();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
        public static DataTable ExecuteQueryStore(string sql, List<DBParameter> paramList = null)
        {
            DataTable dataTable = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommandStore(sql, conn, paramList);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
        public static List<T> ExecuteQueryStore<T>(StringBuilder sql, List<DBParameter> paramList = null) where T : new()
        {
            return ExecuteQueryStore<T>(sql.ToSafeString(), paramList);
        }
        public static List<T> ExecuteQueryStore<T>(string sql, List<DBParameter> paramList = null) where T : new()
        {
            List<T> dataItems = new List<T>();
            DataTable dataTable = ExecuteQueryStore(sql, paramList);
            if (dataTable != null)
            {
                dataItems = FillBusinessEntity<T>(dataTable);
            }

            return dataItems;
        }
        public static List<T> ExecuteQueryStore<T>(StringBuilder sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes) where T : new()
        {
            return ExecuteQueryStore<T>(sql.ToSafeString(), parameterNames, parameterValues, parameterTypes);
        }
        public static List<T> ExecuteQueryStore<T>(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes) where T : new()
        {
            List<T> dataItems = new List<T>();
            DataTable dataTable = ExecuteQueryStore(sql, parameterNames, parameterValues, parameterTypes);
            if (dataTable != null)
            {
                dataItems = FillBusinessEntity<T>(dataTable);
            }
            return dataItems;
        }
        public static int ExecuteNonQueryStore(StringBuilder sql, List<DBParameter> paramList = null, bool beginTrans = false)
        {
            return ExecuteNonQueryStore(sql.ToSafeString(), paramList, beginTrans);
        }
        public static int ExecuteNonQueryStore(string sql, List<DBParameter> paramList = null, bool beginTrans = false)
        {
            int affectedRow = 0;

            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlTransaction tran = null;

                try
                {
                    conn.Open();
                    if (beginTrans)
                    {
                        tran = conn.BeginTransaction();
                    }

                    SqlCommand command = GenerateSqlCommandStore(sql, conn, paramList, tran);
                    affectedRow = command.ExecuteNonQuery();

                    if (beginTrans)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (beginTrans)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
            }
            return affectedRow;
        }
        public static int ExecuteNonQueryStore(StringBuilder sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes, bool beginTrans = false)
        {
            return ExecuteNonQueryStore(sql.ToSafeString(), parameterNames, parameterValues, parameterTypes, beginTrans);
        }
        public static int ExecuteNonQueryStore(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes, bool beginTrans = false)
        {
            int affectedRow = 0;
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlTransaction tran = null;
                try
                {
                    conn.Open();
                    if (beginTrans)
                    {
                        tran = conn.BeginTransaction();
                    }

                    SqlCommand command = GenerateSqlCommandStore(sql, conn, parameterNames, parameterValues, parameterTypes, tran);
                    affectedRow = command.ExecuteNonQuery();

                    if (beginTrans)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (beginTrans)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
            }
            return affectedRow;
        }
        public static T ExecuteScalarStore<T>(string sql, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes)
        {
            T value = default(T);
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommandStore(sql, conn, parameterNames, parameterValues, parameterTypes);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    value = (T)command.ExecuteScalar();
                }
            }
            catch (NullReferenceException)
            {
                value = default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }
        public static string ExecuteScalarStore(string commandText, string[] parameterNames, object[] parameterValues, SqlDbType[] parameterTypes)
        {
            return "";//ExecuteScalar<string>(commandText, parameterNames, parameterValues, parameterTypes);
        }
        public static string ExecuteScalarStore(string commandText, List<DBParameter> paramList = null)
        {
            return ExecuteScalarStore<string>(commandText, paramList);
        }

        public static T ExecuteScalarStore<T>(string sql, List<DBParameter> paramList = null)
        {
            T value = default(T);
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    SqlCommand command = GenerateSqlCommandStore(sql, conn, paramList);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    value = (T)command.ExecuteScalar();
                }
            }
            catch (NullReferenceException)
            {
                value = default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }
        #region "InternalProcess"

        private static SqlCommand GenerateSqlCommand(string sql, SqlConnection conn, List<DBParameter> paramList = null, SqlTransaction tran = null)
        {
            SqlCommand command;
            if (tran != null)
            {
                command = new SqlCommand(sql, conn, tran);
            }
            else
            {
                command = new SqlCommand(sql, conn);
            }

            if (paramList != null
                      && paramList.Count > 0)
            {
                foreach (var item in paramList)
                {
                    SqlParameter parameter = new SqlParameter(item.Name, item.Type);
                    parameter.Value = item.Value;
                    command.Parameters.Add(parameter);
                }
            }
            command.CommandTimeout = _dbExecuteTimeout;
            command.CommandType = CommandType.Text;

            return command;
        }
        private static SqlCommand GenerateSqlCommand(string sql, SqlConnection conn, string[] parameterNames = null, object[] parameterValues = null, SqlDbType[] parameterTypes = null, SqlTransaction tran = null)
        {
            SqlCommand command;
            if (tran != null)
            {
                command = new SqlCommand(sql, conn, tran);
            }
            else
            {
                command = new SqlCommand(sql, conn);
            }

            if (parameterNames != null
                      && parameterNames.Length > 0)
            {
                for (int i = 0; i < parameterNames.Length; i++)
                {
                    if (parameterValues[i] != null)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameterNames[i];
                        sqlParameter.Value = parameterValues[i];
                        sqlParameter.SqlDbType = parameterTypes[i];
                        command.Parameters.Add(sqlParameter);
                    }
                }
            }
            command.CommandTimeout = _dbExecuteTimeout;
            command.CommandType = CommandType.Text;

            return command;
        }

        private static SqlCommand GenerateSqlCommandStore(string sql, SqlConnection conn, List<DBParameter> paramList = null, SqlTransaction tran = null)
        {
            SqlCommand command;
            if (tran != null)
            {
                command = new SqlCommand(sql, conn, tran);
            }
            else
            {
                command = new SqlCommand(sql, conn);
            }

            if (paramList != null
                      && paramList.Count > 0)
            {
                foreach (var item in paramList)
                {
                    SqlParameter parameter = new SqlParameter(item.Name, item.Type);
                    parameter.Value = item.Value;
                    command.Parameters.Add(parameter);
                }
            }
            command.CommandTimeout = _dbExecuteTimeout;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }
        private static SqlCommand GenerateSqlCommandStore(string sql, SqlConnection conn, string[] parameterNames = null, object[] parameterValues = null, SqlDbType[] parameterTypes = null, SqlTransaction tran = null)
        {
            SqlCommand command;
            if (tran != null)
            {
                command = new SqlCommand(sql, conn, tran);
            }
            else
            {
                command = new SqlCommand(sql, conn);
            }

            if (parameterNames != null
                      && parameterNames.Length > 0)
            {
                for (int i = 0; i < parameterNames.Length; i++)
                {
                    if (parameterValues[i] != null)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameterNames[i];
                        sqlParameter.Value = parameterValues[i];
                        sqlParameter.SqlDbType = parameterTypes[i];
                        command.Parameters.Add(sqlParameter);
                    }
                }
            }
            command.CommandTimeout = _dbExecuteTimeout;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }


        //private static List<T> ConverToList<T>(DataTable dt)
        //{
        //    List<T> data = new List<T>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        T item = GetItem<T>(row);
        //        data.Add(item);
        //    }
        //    return data;
        //}
        //private static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
        private static List<T> FillBusinessEntity<T>(DataTable dataTable) where T : new()
        {
            List<T> businessEntityList = new List<T>();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    T businessEntity = new T();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PropertyInfo property = businessEntity.GetType().GetProperty(column.ColumnName);
                        if (row[column] != DBNull.Value && property != null)
                        {
                            property.SetValue(businessEntity, row[column], null);
                        }
                    }
                    businessEntityList.Add(businessEntity);
                }
            }
            return businessEntityList;
        }
        private static List<T> FillBusinessEntity<T>(IDataReader reader) where T : new()
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            return FillBusinessEntity<T>(dataTable);
        }

        #endregion


        #region "test"
        //public string LogNote(string user, string value, ActionType action)
        //{
        //    return this.LogNote(user, value, action.Value());
        //}

        //public string LogNote(string user, string value, string action)
        //{
        //    var request = HttpContext.Current.Request;
        //    var date = DateTime.Now.ToDbDate();
        //    var time = DateTime.Now.ToString("HH:mm:ss");
        //    var result = string.Empty;
        //    switch (action.ToLower())
        //    {
        //        case "add":
        //            result = string.Format("({0}, {1}, {2}, {3}, {4})",
        //                user, request.UserHostAddress, date, time, action);
        //            break;
        //        default:
        //            if (value == null) value = string.Empty;
        //            result = string.Format("({0}, {1}, {2}, {3}, {4})\n{5}",
        //               user, request.UserHostAddress, date, time, action, value);
        //            if (result.Length > ConfigurationManager.AppSettings.DbManager.MaxCharOfLogNote)
        //                result = result.Substring(0, ConfigurationManager.AppSettings.DbManager.MaxCharOfLogNote);
        //            break;
        //    }
        //    return result;
        //}


        #endregion
    }
}
