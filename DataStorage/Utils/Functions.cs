using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace DataStorage
{
    public static class Functions
    {
        public static DatabaseConnection databaseConnection;

        public static string Database1(string xmlText)
        {
            var doc = new XmlDocument();
            doc.Load(xmlText);

            var node =
                doc.DocumentElement.SelectSingleNode("/serverdata/database");
            var text = node.InnerText;
            return text;
        }

        public static string GetTempPath()
        {
            var path = Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\", StringComparison.CurrentCulture))
                path += "\\";
            return path;
        }

        public static void LogMessage(string message, int niveau,
            int testnr = 0, string testcase = "")
        {
            var DatumG = string.Empty;

            DatumG += DateTime.Now.Year.ToString();
            if (DateTime.Now.Month < 10) DatumG += "0";
            DatumG += DateTime.Now.Month.ToString();
            if (DateTime.Now.Day < 10) DatumG += "0";
            DatumG += DateTime.Now.Day.ToString();

            var sw =
                File.AppendText(GetTempPath() + "Logfile_" + DatumG +
                                ".txt"); // Change filename
            try
            {
                var niveau1 = string.Empty;
                switch (niveau)
                {
                    case 1:
                        niveau1 = "Info    ";
                        break;

                    case 2:
                        niveau1 = "Warning ";
                        break;

                    case 3:
                        niveau1 = "Failure ";
                        break;

                    case 4:
                        niveau1 = "Critical";
                        break;

                    default:
                        niveau1 = "Info    ";
                        break;
                }

                var logLine = string.Format("{0:G}:\t{1}\t{2}.", DateTime.Now,
                    niveau1, message);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }

        public static int MeldingGeenGegevens(string strTabel)
        {
            var strCap = "Foutmelding";
            var strMelding =
                "Er zijn geen gegevens gevonden\r\nVoeg eerst de benodigde gegevens toe in tabel: '" +
                strTabel
                + "'!!!";
            MessageBox.Show(
                strMelding,
                strCap,
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return 0;
        }

        public static int MeldingVoltooid()
        {
            var strCap = "Melding";
            var strMelding = "Voltooid";
            MessageBox.Show(
                strMelding,
                strCap,
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
            return 0;
        }

        public static string Password1(string xmlText)
        {
            var doc = new XmlDocument();
            doc.Load(xmlText);

            var node =
                doc.DocumentElement.SelectSingleNode("/serverdata/password");
            var text = node.InnerText;
            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                var encoder = new ASCIIEncoding();
                var combined = encoder.GetBytes("bikebill");
                var hash = BitConverter.ToString(sha.ComputeHash(combined))
                    .Replace("-", string.Empty);

                if (hash == text.ToUpper()) return "bikebill";

                MessageBox.Show(
                    "Kan geen connectie met de database tot stand brengen.\r\nApplicatie wordt afgesloten!!!",
                    "Database connectie",
                    MessageBoxButton.OK);
                return string.Empty;
            }
        }

        public static string Provider(string xmlText)
        {
            var doc = new XmlDocument();
            doc.Load(xmlText);

            var node =
                doc.DocumentElement.SelectSingleNode(
                    "/serverdata/providerName");
            var text = node.InnerText;
            return text;
        }

        public static string PuntKomma(string s)
        {
            s.Replace(',', '.');
            return s;
        }

        public static string ServerName(string xmlText)
        {
            var doc = new XmlDocument();
            doc.Load(xmlText);

            var node =
                doc.DocumentElement.SelectSingleNode("/serverdata/server");
            var text = node.InnerText;
            return text;
        }

        public static string Strip(string s)
        {
            var sF = string.Empty;
            var sF1 = string.Empty;
            foreach (var c in s)
            {
                if (c == (char)45) sF += c;

                if (c == (char)46) sF += c;
                if (c > (char)47 && c < (char)58) sF += c;
                if (c == (char)44) sF += '.';
            }

            sF1 = sF;
            return sF1;
        }

        public static string Strip1(string s)
        {
            var iPos = 0;
            var iCount = 0;
            var sF = string.Empty;
            var sF1 = string.Empty;
            if (s.Length > 0)
            {
                foreach (var c in s)
                {
                    if (c == (char)45) sF += c;
                    if (c > (char)47 && c < (char)58) sF += c;
                    if (c == (char)44) iPos = iCount;
                    iCount++;
                }

                if (iPos == 0)
                {
                    iPos = sF.Length;
                    sF1 = sF.Insert(iPos, "00");
                }
                else
                {
                    sF1 = sF;
                }

            }

            return sF1;
        }

        public static string Strip2(string s)
        {
            var sF = string.Empty;
            var sF1 = string.Empty;
            if (s.Length > 0)
            {
                foreach (var c in s)
                {
                    if (c == (char)45) sF += c;
                    if (c > (char)47 && c < (char)58) sF += c;
                    else break;
                }

                sF1 = sF;
            }

            return sF1;
        }

        public static string Strip4(string s)
        {
            var iPos = 0;
            var iCount = 0;
            var sF = string.Empty;
            var sF1 = string.Empty;
            if (s.Length > 0)
            {
                foreach (var c in s)
                {
                    if (c == (char)45) sF += c;
                    if (c > (char)47 && c < (char)58) sF += c;
                    if (c == (char)44) iPos = iCount;
                    iCount++;
                }

                if (iPos == 0)
                {
                    iPos = sF.Length;
                    sF1 = sF.Insert(iPos, "00");
                }
                else
                {
                    sF1 = sF;
                }
            }

            return sF1;
        }

        public static string Strip5(string s)
        {
            var sF = string.Empty;
            var sF1 = string.Empty;
            if (s.Length > 0)
            {
                foreach (var c in s)
                    if (c == (char)46)
                        sF += c;
                    else if (c > (char)47 && c < (char)58) sF += c;
                    else if (c == (char)44) sF += (char)46;
                    else break;
                sF1 = sF;
            }

            return sF1;
        }

        public static string Strip6(string s)
        {
            var sF = string.Empty;
            var sF1 = string.Empty;
            short iTeller = 0;
            short iLengte = 0;
            short iPos = 0;
            iLengte = Convert.ToInt16(s.Length);
            foreach (var c in s)
            {
                if (c == (char)46 && iPos >= iLengte - 2)
                {
                    sF += c;
                    if (iPos == iLengte - 2) iTeller = 2;
                }

                if (c > (char)47 && c < (char)58) sF += c;
                if (c == (char)44)
                {
                    sF += '.';
                    iTeller++;
                }

                iPos++;
            }

            if (iTeller == 0)
            {
                sF += '.';
                sF += '0';
                sF += '0';
            }

            if (iTeller == 2) sF += '0';
            sF1 = sF;
            return sF1;
        }

        public static string UserName1(string xmlText)
        {
            var doc = new XmlDocument();
            doc.Load(xmlText);
            var node = doc.DocumentElement.SelectSingleNode("/serverdata/name");
            var text = node.InnerText;
            return text;
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
                    foreach (var elementVar in elementVars)
                        ValueList.Add(elementVar.Value);
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

        public static void InitializeDatabaseConnection(bool loginU)
        {
            var host = "";
            var database = "";
            var port = "";
            var user = "";
            var password = "";
            General.LogMessage("InitializeDatabaseConnection(" + loginU + ");",
                1);

            host = sHost(GetCurrentDir(0) + "ConfigData.xml");
            database = sDatabase(GetCurrentDir(0) + "ConfigData.xml");
            user = sUsername(GetCurrentDir(0) + "ConfigData.xml");
            password = sPassword(GetCurrentDir(0) + "ConfigData.xml");
            port = sPort(GetCurrentDir(0) + "ConfigData.xml");

            databaseConnection = new DatabaseConnection(
                host,
                database,
                port,
                user,
                password);

            General.SetDatabaseConnection(databaseConnection);
        }

        public static string sHost(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/connection/host");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sDatabase(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/connection/database");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sUsername(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/connection/username");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sPassword(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/connection/password");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sPort(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/connection/port");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }


        public static string sBedragBedankjes(string xmlText)
        {
            string text = "";
            try
            {
                var path = System.Environment.CurrentDirectory + "\\";
                XmlDocument doc = new XmlDocument();
                doc.Load(path + xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/bedrag");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sAantalBedankjes(string xmlText)
        {
            string text = "";
            try
            {
                var path = System.Environment.CurrentDirectory + "\\";
                XmlDocument doc = new XmlDocument();
                doc.Load(path + xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/aantal");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

            return text;
        }

        public static string sDirectory(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/dir");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex);
            }

            return text;
        }

        public static string sDirectoryTemps(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/configdata/tempdir");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex);
            }
            return text;
        }

        public static string sFile(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);

                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/file");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex);
            }

            return text;
        }

        public static void errorBericht(Exception ex, string function1 = "")
        {
            System.Text.StringBuilder errorMessages = new StringBuilder();

            errorMessages.Append(function1 + "\n" +
                                 "Message: " + ex.Message + "\n" +
                                 "Source: " + ex.Source + "\n" +
                                 "StrackTrace: " + ex.StackTrace + "\n");
            MessageBox.Show(errorMessages.ToString(), "Error");
            //WPFMessageBox.Show("Error", errorMessages.ToString());
            Functions.LogMessageToFile(errorMessages.ToString(), 4);

        }

        public static void LogMessageToFile(string message, int niveau)
        {
            //MessageBox.Show(System.IO.Path.GetTempPath());
            //WPFMessageBox.Show(System.Environment.SystemDirectory);
            System.IO.StreamWriter sw =
                System.IO.File.AppendText(
                    GetTempPath() + "Logfile.txt"); // Change filename
            try
            {
                string niveau1 = "";
                switch (niveau)
                {
                    case 1:
                        niveau1 = "Info      : ";
                        break;
                    case 2:
                        niveau1 = "Warning   : ";
                        break;
                    case 3:
                        niveau1 = "Failure   : ";
                        break;
                    case 4:
                        niveau1 = "Critical  : ";
                        break;
                    default:
                        niveau1 = "Info      : ";
                        break;
                }

                string logLine =
                    System.String.Format(
                        "{0:G}:\t{1}\t{2}.", System.DateTime.Now, niveau1,
                        message);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
