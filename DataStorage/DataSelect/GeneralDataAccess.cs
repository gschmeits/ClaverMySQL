using System.Windows;
using MySql.Data.MySqlClient;

namespace DataStorage
{
    using System;
    using System.Data;
    using System.Data.Common;

    public static class GenericDataAccess
    {
        public static DbCommand CreateCommand()
        {
            var connectionString = GenericDataConnection.DbConnectionString;
            DbConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            var comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }

        public static DbCommand CreateCommandStoredProcedure()
        {
            var connectionString = GenericDataConnection.DbConnectionString;
            DbConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            var comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }

        public static DbCommand CreateCommandText()
        {
            var connectionString = General.MySqlConnectionString();
            DbConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            var comm = conn.CreateCommand();
            comm.CommandType = CommandType.Text;
            Functions.LogMessage("CreateCommandText(): " + connectionString,1);
            return comm;
        }

        public static int ExecuteNonQuery(DbCommand command)
        {
            var affectedRows = -1;
            try
            {
                command.Connection.Open();
                affectedRows = command.ExecuteNonQuery();
                Functions.LogMessage("ExecuteNonQuery(): " + command, 1);
            }
            catch (MySqlException ex)
            {
                var str = ex;
                var message =
                    "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return affectedRows;
        }

        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            DataTable table;
            table = new DataTable();

            try
            {
                command.Connection.Open();
                var reader = command.ExecuteReader();
                table.Clear();
                table.Load(reader);
                reader.Close();
            }
            catch (Exception ex) when (ex is MySqlException || ex is ConstraintException || ex is Exception)
            {
                var str = ex;
                var message =
                    "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
                message += "\r\n\r\n" + ex.Message + ex.StackTrace;
                message += "\r\n\r\n" + "sql commando:\r\n" + command.CommandText;
                MessageBox.Show(message,"Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                //Application.Current.Shutdown();
            }
            return table;
        }

        public static void ExecuteSelectNonQuery(DbCommand command)
        {
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (MySqlException ex)
            {
                var str = ex;
                var message =
                    "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public static string strScalar(DbCommand command)
        {
            var strWaarde = "";
            try
            {
                command.Connection.Open();
                strWaarde = command.ExecuteScalar().ToString();
            }
            catch (MySqlException ex)
            {
                var str = ex;
                var message =
                    "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return strWaarde;
        }
    }
}
