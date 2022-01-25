using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace DataStorage
{
    internal class GenericDataConnection
    {
        private static readonly string dbConnectionString;
        private static readonly string dbProviderName;

        static GenericDataConnection()
        {
            //var sServerName = "SCHMEITSNAS.SYNOLOGY.ME";
            //var sPort = "3306";
            //var sUserName = "autotest";
            //var sPassword = "NiFSi1fZjbJ4zXE8";
            ///var sDatabase = "autotest";
            //var sProvider = "MySql.Data.MySqlClient";

            var sServerName = "";
            var sPort = "";
            var sUserName = "";
            var sPassword = "";
            var sDatabase = "";
            var sProvider = "";

            sServerName = string.Empty;
            sPort = string.Empty;
            sUserName = string.Empty;
            sPassword = string.Empty;
            sDatabase = string.Empty;
            sProvider = string.Empty;

            XElement xelement = XElement.Load(GetCurrentDir(0) + "connect.xml");

            IEnumerable<XElement> testsElements = xelement.Elements();
            foreach (var testElement in testsElements)
            {
                sServerName = testElement.Element("host").Value;
                sDatabase = testElement.Element("database").Value;
                sPort = testElement.Element("port").Value;
                sUserName = testElement.Element("username").Value;
                sPassword = testElement.Element("password").Value;
            }

            dbConnectionString = "server=" + sServerName + ":" + sPort + ";";
            dbConnectionString += "User ID=" + sUserName + ";";
            dbConnectionString += "Password=" + sPassword + ";";
            dbConnectionString += "database=" + sDatabase + ";";
            dbConnectionString += "charset=utf8;";

            dbConnectionString = General.MySqlConnectionString();
            dbProviderName = sProvider;
        }

        public static string GetCurrentDir(int niveau)
        {
            var ValueList = new List<string>();
            if (File.Exists("settings.xml"))
            {
                var xDoc = new XmlDocument();
                xDoc.Load("settings.xml");
                try
                {
                    var xelement = XElement.Load("settings.xml");
                    var elementVars = xelement.Elements();
                    foreach (var elementVar in elementVars) ValueList.Add(elementVar.Value);
                }
                catch (Exception ex)
                {
                    General.LogMessage(ex.Message + ex.StackTrace, 4);
                    MessageBox.Show(
                        "The 'settings.xml' file is not found!!!\r\nPlease fill in the correct drive and directory.\r\n"
                        + Environment.CurrentDirectory,
                        "Settings Error",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    ValueList[0] = Environment.CurrentDirectory;
                }
            }
            else
            {
                MessageBox.Show(
                    "The 'settings.xml' file is not found!!!\r\nPlease fill in the correct drive and directory.\r\n"
                    + Environment.CurrentDirectory,
                    "Settings Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return ValueList[niveau];
        }

        public static string DbConnectionString => dbConnectionString;
        public static string DbProviderName => dbProviderName;
    }
}
