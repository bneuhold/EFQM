using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Library.DataAccess
{
    public class DataBridge
    {
        /// <summary>
        /// main database object
        /// </summary>
        protected Database _dbView;

        public static object SimpleIDValueRetrieve(int? id, DataTable data, String columnName)
        {
            object result = null;
            if ((id.HasValue) && (data != null) && (data.Rows != null))
            {
                foreach (DataRow row in data.Rows)
                {
                    int rowID = (int)row["ID"];
                    if (rowID == id.Value)
                    {
                        return row[columnName];
                    }
                }
            }
            return result;
        }

        public static object SimpleIDValueRetrieve(int? id, IDataReader data, String columnName)
        {
            object result = null;
            while (data.NextResult())
            {
                int rowID = (int)data["ID"];
                if (rowID == id.Value)
                {
                    return data[columnName];
                }
            }
            return result;
        }

        public int ExecuteInsert(DbCommand command)
        {
            int identity = -1;
            identity = _dbView.ExecuteNonQuery(command);
            identity = (int)_dbView.GetParameterValue(command, "@return_value");
            return identity;
        }


        protected DataTable ExecuteDataTable(string storedProcedure, params object[] paramValues)
        {
            DataSet data = _dbView.ExecuteDataSet(storedProcedure, paramValues);
            return data.Tables[0];
        }

        protected DataTable ExecuteDataTable(DbCommand command)
        {
            DataSet data = _dbView.ExecuteDataSet(command);
            return data.Tables[0];
        }

        protected DataTable ExecuteDataTable(IDbCommand command)
        {
            DataSet data = _dbView.ExecuteDataSet((DbCommand)command);
            return data.Tables[0];
        }

        protected int ExecuteNonQuery(string storedProcedure, params object[] paramValues)
        {
            return _dbView.ExecuteNonQuery(storedProcedure, paramValues);
        }

        protected int ExecuteNonQuery(DbTransaction transaction, string storedProcedure, params object[] paramValues)
        {
            return _dbView.ExecuteNonQuery(transaction, storedProcedure, paramValues);
        }

        protected object ExecuteNonQueryWithOutputValue(string storedProcedure, string outputParamName, params object[] paramValues)
        {
            object ret_val = -1;
            DbCommand command = _dbView.GetStoredProcCommand(storedProcedure, paramValues);
            ret_val = _dbView.ExecuteNonQuery(command);
            ret_val = _dbView.GetParameterValue(command, "@" + outputParamName);
            return ret_val;
        }

        protected int? ExecuteNonQueryWithReturnValue(string storedProcedure, params object[] paramValues)
        {
            int? ret_val = -1;
            DbCommand command = _dbView.GetStoredProcCommand(storedProcedure, paramValues);
            ret_val = _dbView.ExecuteNonQuery(command);
            ret_val = (int?)_dbView.GetParameterValue(command, "@return_value");
            return ret_val;
        }

        protected IDataReader ExecuteReader(string storedProcedure, params object[] paramValues)
        {
            return _dbView.ExecuteReader(storedProcedure, paramValues);
        }

        protected IDataReader ExecuteReader(DbCommand command)
        {
            return _dbView.ExecuteReader(command);
        }

        protected DbCommand GetCommand(string procedureName)
        {
            DbCommand cmd = _dbView.GetStoredProcCommand(procedureName);
            _dbView.DiscoverParameters(cmd);
            return cmd;
        }

        protected DbParameter AddParameterValue(DbCommand command, string name, object param)
        {
            DbParameter p = GetParameter(command, name);
            p.Value = param;
            return p;
        }

        protected DbParameter SetParameterValue(DbCommand command, string name, object param)
        {
            _dbView.SetParameterValue(command, name, param);
            return GetParameter(command, name);
        }

        protected object GetParameterValue(DbCommand command, string name)
        {
            return _dbView.GetParameterValue(command, name);
        }

        protected DbParameter GetParameter(DbCommand command, string name)
        {
            
            return command.Parameters[name];
        }

        protected String listParams(object[] paramValues)
        {
            String result = "";
            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    if (paramValues[i] == null)
                    {
                        result += "null,";
                    }
                    else
                    {
                        result += paramValues[i] + ",";
                    }
                }
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
        private SqlParameter CreateParameter(string naziv, object val, Type objType, int length)
        {
            SqlParameter parameter = new SqlParameter();

            parameter.ParameterName = naziv;
            parameter.Value = val == null ? Convert.DBNull : val;

            //  Type type = val.GetType();

            switch (objType.FullName)
            {
                case ("System.String"):
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Size = length;
                    break;

                case ("System.DateTime"):
                    parameter.SqlDbType = SqlDbType.DateTime;
                    break;

                case ("System.Decimal"):
                    parameter.SqlDbType = SqlDbType.Decimal;
                    break;

                case ("System.Int32"):
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Size = length;
                    break;

                case ("System.Boolean"):
                    parameter.SqlDbType = SqlDbType.Bit;
                    break;

                case ("System.Int16"):
                    parameter.SqlDbType = SqlDbType.SmallInt;
                    parameter.Size = length;
                    break;

                case ("System.Byte"):
                    parameter.SqlDbType = SqlDbType.TinyInt;
                    break;
            }
            return parameter;
        }
    }
}