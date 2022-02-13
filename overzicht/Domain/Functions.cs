using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Windows;
using System.Xml.Linq;

namespace Domain
{
    public static class Functions
    {
        public static DatabaseConnection databaseConnection;

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

        public static string sDataSource(string xmlText)
        {
            string text = "";
            try
            {
                var path = System.Environment.CurrentDirectory + "\\";
                XmlDocument doc = new XmlDocument();
                doc.Load(path + xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/datasource");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex,
                    "Ophalen 'Data Source' uit xml bestand: 'ConfigData.xml'");
            }

            return text;
        }

        public static string sInitial(string xmlText)
        {
            string text = "";
            try
            {
                var path = System.Environment.CurrentDirectory + "\\";
                XmlDocument doc = new XmlDocument();
                doc.Load(path + xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode("/configdata/initial");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex,
                    "Ophalen 'Initial Catalog' uit xml bestand: 'ConfigData.xml'");
            }

            return text;
        }

        public static string sSecurity(string xmlText)
        {
            string text = "";
            try
            {
                var path = System.Environment.CurrentDirectory + "\\";
                XmlDocument doc = new XmlDocument();
                doc.Load(path + xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/security");
                text = node.InnerText;
            }
            catch (IOException ex)
            {
                errorBericht(ex,
                    "Ophalen 'Integrated Security' uit xml bestand: 'ConfigData.xml'");
                ;
            }

            return text;
        }


        public static string sDirectory(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/configdata/dir");
            string text = node.InnerText;
            return text;
        }

        public static string sFile(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/configdata/file");
            string text = node.InnerText;
            return text;
        }

        public static string ServerName(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(path + xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/serverdata/server");
            string text = node.InnerText;
            return text;
        }

        /// <summary>
        /// Users the name1.
        /// </summary>
        /// <param name="xmlText">The XML text.</param>
        /// <returns>System.String.</returns>
        public static string UserName1(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(path + xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/serverdata/name");
            string text = node.InnerText;
            return text;
        }

        /// <summary>
        /// Password1s the specified XML text.
        /// </summary>
        /// <param name="xmlText">The XML text.</param>
        /// <returns>System.String.</returns>
        public static string Password1(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(path + xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/serverdata/password");
            string text = node.InnerText;
            var sha =
                new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var encoder = new ASCIIEncoding();
            byte[] combined = encoder.GetBytes("bikebill");
            string hash = BitConverter.ToString(sha.ComputeHash(combined))
                .Replace("-", "");

            if (hash == text.ToUpper())
            {
                return "bikebill";
            }
            else
            {
                //WPFMessageBox.Show("Database connectie", "Kan geen connectie met database tot stand brengen.\r\nApplicatie wordt afgesloten!!!", WPFMessageBoxButtons.OK);
                return "";
            }
        }

        /// <summary>
        /// Database1s the specified XML text.
        /// </summary>
        /// <param name="xmlText">The XML text.</param>
        /// <returns>System.String.</returns>
        public static string Database1(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(path + xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode("/serverdata/database");
            string text = node.InnerText;
            return text;
        }

        /// <summary>
        /// Providers the specified XML text.
        /// </summary>
        /// <param name="xmlText">The XML text.</param>
        /// <returns>System.String.</returns>
        public static string Provider(string xmlText)
        {
            var path = System.Environment.CurrentDirectory + "\\";
            var doc = new XmlDocument();
            doc.Load(path + xmlText);

            XmlNode node =
                doc.DocumentElement.SelectSingleNode(
                    "/serverdata/providerName");
            string text = node.InnerText;
            return text;
        }

        /// <summary>
        /// Strip2s the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string Strip2(string s)
        {
            string sF = "";
            string sF1 = "";
            if (s.Length > 0)
            {
                foreach (char c in s)
                {
                    if (c == (char)45)
                    {
                        sF += c;
                    }

                    if (c > (char)47 && c < (char)58)
                    {
                        sF += c;
                    }
                    else
                    {
                        break;
                    }
                }

                sF1 = sF;
            }

            return sF1;
        }

        /// <summary>
        /// Strip1s the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string Strip1(string s)
        {
            int iPos = 0;
            int iCount = 0;
            string sF = "";
            string sF1 = "";
            if (s.Length > 0)
            {
                foreach (char c in s)
                {
                    if (c == (char)45)
                    {
                        sF += c;
                    }

                    if (c > (char)47 && c < (char)58)
                    {
                        sF += c;
                    }

                    if (c == (char)44)
                    {
                        iPos = iCount;
                    }

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
                //iPos = sF.Length;
                //if (iPos >= 2)
                //{
                //	sF = sF1.Insert(iPos - 2, ".");
                //	sF1 = sF;
                //}
                //else
                //{
                //	sF1 = sF;
                //}
            }

            return sF1;
        }

        /// <summary>
        /// Strips the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string Strip(string s)
        {
            int iPos = 0;
            int iCount = 0;
            string sF = "";
            string sF1 = "";
            foreach (char c in s)
            {
                if (c == (char)45)
                {
                    sF += c;
                }

                if (c > (char)47 && c < (char)58)
                {
                    sF += c;
                }

                if (c == (char)44)
                {
                    iPos = iCount;
                }

                iCount++;
            }

            iPos = sF.Length;
            if (iPos >= 2)
            {
                sF1 = sF.Insert(iPos - 2, ".");
            }
            else
            {
                sF1 = sF;
            }

            return sF1;
        }

        /// <summary>
        /// Punts the komma.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string PuntKomma(string s)
        {
            s.Replace(',', '.');
            return s;
        }

        /// <summary>
        /// Meldings the voltooid.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int MeldingVoltooid()
        {
            //string strCap = "Melding";
            //string strMelding = "Voltooid";
            //WPFMessageBox.Show(strCap, strMelding, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Information);
            return 0;
        }

        /// <summary>
        /// Meldings the geen gegevens.
        /// </summary>
        /// <param name="strTabel">The string tabel.</param>
        /// <returns>System.Int32.</returns>
        public static int MeldingGeenGegevens(string strTabel)
        {
            //string strCap = "Foutmelding";
            //string strMelding = "Er zijn geen gegevens gevonden\r\nVoeg eerst de benodigde gegevens toe in tabel: '" + strTabel + "'!!!";
            //WPFMessageBox.Show(strCap, strMelding, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Alert);
            return 0;
        }

        /// <summary>
        /// Verdelens the regels.
        /// </summary>
        /// <param name="sString">The s string.</param>
        /// <returns>System.String.</returns>
        public static string VerdelenRegels(string sString)
        {
            string strDetail = sString;
            string strMaak = "";
            string[] strDetail1 = strDetail.Split('\n');
            if (strDetail1.Count() == 1)
            {
                strDetail1 = strDetail.Split('\r');
            }

            if (strDetail1.GetLength(0) > 1)
            {
                foreach (string strT in strDetail1)
                {
                    strMaak += strT + "\r\n";
                }
            }

            if (strDetail1.Count() == 1)
            {
                strMaak += strDetail;
            }

            return strMaak;
        }

        public static string GetTempPath()
        {
            string path =
                System.Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\", StringComparison.Ordinal))
            {
                path += "\\";
            }

            return path;
        }

        public static string GetPathGer(object applicationObject)
        {
            Type applicationType = applicationObject.GetType();

            string propertyName = "";

            switch (applicationType.ToString())

            {
                case "Microsoft.Office.Interop.Word.ApplicationClass":

                    propertyName = "ActiveDocument";

                    break;

                case "Microsoft.Office.Interop.Excel.ApplicationClass":

                    propertyName = "ActiveWorkbook";

                    break;

                case "Microsoft.Office.Interop.Powerpoint.ApplicationClass":

                    propertyName = "ActivePresentation";

                    break;
            }

            object activeDocument = applicationType.InvokeMember(propertyName,
                BindingFlags.GetProperty, null, applicationObject, null);

            string documentPath = (string)activeDocument.GetType()
                .InvokeMember("FullName", BindingFlags.GetProperty, null,
                    activeDocument, null);

            return documentPath;
        }

        /// <summary>
        /// Logs the message to file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="niveau">The niveau.</param>
        public static void LogMessageToFile(string message, int niveau)
        {
            //MessageBox.Show(System.IO.Path.GetTempPath());
            System.IO.StreamWriter sw =
                System.IO.File.AppendText(
                    "e:\\Logfile.txt"); // Change filename
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

        public static void errorBericht(Exception ex, string function1 = "")
        {
            System.Text.StringBuilder errorMessages = new StringBuilder();

            errorMessages.Append(function1 + "\n" +
                                 "Message: " + ex.Message + "\n" +
                                 "Source: " + ex.Source + "\n" +
                                 "StrackTrace: " + ex.StackTrace + "\n");
            MessageBox.Show(errorMessages.ToString(), "Error");
            Functions.LogMessageToFile(errorMessages.ToString(), 4);

        }

        public static string sHost(string xmlText)
        {
            string text = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlText);
                XmlNode node =
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/connection/host");
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
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/connection/database");
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
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/connection/username");
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
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/connection/password");
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
                    doc.DocumentElement.SelectSingleNode(
                        "/configdata/connection/port");
                text = node.InnerText;
                return text;
            }
            catch (IOException ex)
            {
                var fout = ex.Message;
            }

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
    }
}