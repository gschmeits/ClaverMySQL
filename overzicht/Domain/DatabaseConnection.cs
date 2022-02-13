using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DatabaseConnection
    {
        public DatabaseConnection(string host, string database, string port, string user, string password)
        {
            this.Host = host;
            this.Database = database;
            this.Port = port;
            this.User = user;
            this.Password = password;
            this.Charset = "utf8;";
        }
        public string Host { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Charset { get; set; }
    }
}
