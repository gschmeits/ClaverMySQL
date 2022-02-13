using System;
using Microsoft.Office.Interop.Excel;

namespace overzicht
{
    public partial class ThisWorkbook
    {
        public Application app;
        public Workbook workBook;

        private void ThisWorkbook_Startup(object sender, EventArgs e)
        {
            app = Application;
            workBook = app.ActiveWorkbook;
            Domain.Functions.GetCurrentDir(1);
            Domain.Functions.InitializeDatabaseConnection(false);
        }

        private void ThisWorkbook_Shutdown(object sender, EventArgs e)
        {
        }

        #region VSTO Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisWorkbook_Startup;
            Shutdown += ThisWorkbook_Shutdown;
        }

        #endregion
    }
}