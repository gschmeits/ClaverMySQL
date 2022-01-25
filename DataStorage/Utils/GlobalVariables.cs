using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public static class Globals_org
    {
        static Globals_org()
        {
            ServerName = "autotest";
            UserName1 = "autotest";
            Password1 = "autotest";
            Database1 = "autotest";
            Provider = "MySql.Data.MySqlClient";
        } 

        public static string Database1 { get; private set; }
        public static string Password1 { get; private set; }
        public static string Provider { get; private set; }
        public static string ServerName { get; private set; }
        public static string UserName1 { get; private set; }
        
        public static void SetDatabase1(string newDatabase)
        {
            Database1 = newDatabase;
        }
        
        public static void SetPassword1(string newPassword)
        {
            Password1 = newPassword;
        }

        public static void SetProvider(string newProvider)
        {
            Provider = newProvider;
        }

        public static void SetServerName(string newServerName)
        {
            ServerName = newServerName;
        }

        public static void SetUserName1(string newUserName)
        {
            UserName1 = newUserName;
        }
    }
}
