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

        static GenericDataConnection()
        {
            dbConnectionString = General.MySqlConnectionString();
            //dbProviderName = sProvider;
            General.LogMessage(dbConnectionString, 1);
        }

        public static string DbConnectionString => dbConnectionString;
    }
}
