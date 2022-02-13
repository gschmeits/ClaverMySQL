using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace Domain
{
    public static class General
    {
        public static DatabaseConnection _databaseConnection;

        public static void AddResultToDatabase(string commandText)
        {
            using (var objConn = new MySqlConnection(MySqlConnectionString()))
            {
                var command = objConn.CreateCommand();
                command.CommandText = commandText;
                objConn.Open();
                command.ExecuteNonQuery();
                objConn.Close();
            }
        }

        public static void ExecuteQueryCommand(string commandText)
        {
            using (var objConn = new MySqlConnection(MySqlConnectionString()))
            {
                var command = objConn.CreateCommand();
                command.CommandText = commandText;
                objConn.Open();
                command.ExecuteNonQuery();
                objConn.Close();
            }
        }

        public static string MySqlConnectionString()
        {
            var connectionstring = "server=" + _databaseConnection.Host + ";";
            connectionstring += "user id=" + _databaseConnection.User + ";";
            connectionstring +=
                "password=" + _databaseConnection.Password + ";";
            connectionstring +=
                "database=" + _databaseConnection.Database + ";";
            connectionstring += "port=" + _databaseConnection.Port + ";";
            connectionstring += "sslmode=None;";
            connectionstring += "characterset=" + _databaseConnection.Charset;
            return connectionstring;
        }

        public static void SetDatabaseConnection(
            DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public static void LogMessageDatabase(string message, int niveau,
            string testname = "", int testnr = 0,
            string testcase = "", string machinecode = "")
        {
            var query =
                "INSERT INTO log (log_text, log_type, log_testname, log_testnr, log_testcase, log_machine) ";
            query += "VALUES ('" + MySqlHelper.EscapeString(message) + "', '" +
                     niveau + "', '";
            query += MySqlHelper.EscapeString(testname) + "', " + testnr +
                     ", '" + MySqlHelper.EscapeString(testcase) +
                     "', '";
            query += machinecode += "');";
            ExecuteQueryCommand(query);
        }

        public static string GetTempPath()
        {
            var path = Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\", StringComparison.CurrentCulture))
                path += "\\";
            return path;
        }

        public static void LogMessage(string message, int niveau,
            string testname = "", int testnr = 0,
            string testcase = "", string testaction = "",
            string testattribute = "", string machinecode = "")
        {
            var DatumG = string.Empty;
            DatumG += DateTime.Now.Year.ToString();
            if (DateTime.Now.Month < 10) DatumG += "0";
            DatumG += DateTime.Now.Month.ToString();
            if (DateTime.Now.Day < 10) DatumG += "0";
            DatumG += DateTime.Now.Day.ToString();

            var dir1 = GetTempPath();

            if (dir1 != null)
            {
                var appendtext = dir1 + @"\Logfile_" + DatumG + ".txt";
                var sw = File.AppendText(appendtext); 
                try
                {
                    var niveau1 = string.Empty;
                    switch (niveau)
                    {
                        case 1:
                            niveau1 = "Info     ";
                            break;

                        case 2:
                            niveau1 = "Warning  ";
                            break;

                        case 3:
                            niveau1 = "Failure  ";
                            break;

                        case 4:
                            niveau1 = "Critical ";
                            break;

                        default:
                            niveau1 = "Info     ";
                            break;
                    }


                    var logLine = string.Format(
                        "{0:G}|{1}|TestStep|{3}|{2}|{4}|{5}|{6}", DateTime.Now,
                        niveau1,
                        message,
                        testcase, testnr, testattribute, testaction);

                    if (testnr == 0)
                        logLine = string.Format("{0:G}|{1}|{2}", DateTime.Now,
                            niveau1, message);

                    sw.WriteLine(logLine);
                }
                finally
                {
                    sw.Close();
                }
            }
        }
    }
}
