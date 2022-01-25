using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Windows;

namespace DataStorage
{
    public class GenericDataRead
    {
        static GenericDataRead()
        {
        }

        public static DataTable GetData(string strQue)
        {
            var comm = GenericDataAccess.CreateCommandText();
            comm.CommandText = strQue;
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        public static bool INUPDEL(string strQue)
        {
            DbCommand comm = GenericDataAccess.CreateCommandText();
            comm.CommandText = strQue;

            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch (MySqlException ex)
            {
                var str = ex;
                string message = "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
                message += "\r\n\r\n" + ex.Message + ex.StackTrace + "\r\n\r\n" + strQue;
                MessageBox.Show(message);
                General.LogMessage(
                    "INUPDEL query: '" + strQue + "'.",
                    4,
                    strQue,
                    0,
                    string.Empty,
                    "");
            }
            finally
            {
                comm.Connection.Close();
            }
            return (result != -1);
        }
    }
}
