using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;

namespace ClaverMySQL.Utils
{
    class PrintControle
    {
        private PrintDocument m_PrintDocument = new PrintDocument();
        private PrintDocument m_PrintDocument1 = new PrintDocument();
        private PrintPreviewDialog m_PrintReview = new PrintPreviewDialog();
        private Int32 maand;
        private Int32 maand1;
        private Int32 jaar;
        private string tekst;
        private float lijnhoogteG;

        public int aantalRegelsPerPagina = 63;
        public int RegelsGeprint = 0;
        public int PrintRows = 0;
        public int RowsGeprint = 0;
        public int pagina = 1;

        public Func<string> GetMaand
        {
            get;
            set;
        }

        public Func<string> GetMaand1
        {
            get;
            set;
        }


        public Func<string> GetJaar
        {
            get;
            set;
        }

        public Func<string> GetText
        {
            get;
            set;
        }

        public PrintControle()
        {

        }

        public void PrintGegevens()
        {
            // Weghalen print button
            ToolStrip ts = new ToolStrip();
            ts.Name = "wrongToolStrip";
            foreach (Control ctl in m_PrintReview.Controls)
            {
                if (ctl.Name.Equals("toolStrip1"))
                {
                    ts = ctl as ToolStrip;
                    break;
                }
            }
            ToolStripButton printButton = new ToolStripButton();
            foreach (ToolStripItem tsi in ts.Items)
            {
                if (tsi.Name.Equals("printToolStripButton"))
                {
                    printButton = tsi as ToolStripButton;
                }
            }

            ts.Items.Remove(printButton);

            maand = Convert.ToInt32(GetMaand.Invoke());
            maand1 = Convert.ToInt32(GetMaand1.Invoke());
            jaar = Convert.ToInt32(GetJaar.Invoke());
            tekst = GetText.Invoke();

            m_PrintDocument.PrintPage += new PrintPageEventHandler(m_printDocument_PrintPage);
            m_PrintDocument1.PrintPage += new PrintPageEventHandler(m_printDocument1_PrintPage);
            m_PrintReview.Document = m_PrintDocument;

            m_PrintReview.Width = m_PrintDocument.DefaultPageSettings.PaperSize.Width - 200;
            m_PrintReview.Height = m_PrintDocument.DefaultPageSettings.PaperSize.Height - 200;


            m_PrintReview.ShowDialog();

            aantalRegelsPerPagina = 63;
            RegelsGeprint = 0;
            PrintRows = 0;
            RowsGeprint = 0;
            pagina = 1;
            PrintDialog pdi = new PrintDialog();


            if (pdi.ShowDialog() == DialogResult.OK)
            {
                m_PrintDocument1.Print();
            }
        }

        private void m_printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            m_PrintKopTekst(e);
            e.HasMorePages = PrintDetails(e.Graphics);
            m_PrintVoetNoot(e);
        }

        private void m_printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            m_PrintKopTekst(e);
            e.HasMorePages = PrintDetails(e.Graphics);
            m_PrintVoetNoot(e);
        }

        private void m_PrintKopTekst(PrintPageEventArgs e)
        {
            int margeLinks = 30;
            int margeRechts = m_PrintDocument.DefaultPageSettings.PaperSize.Width - 30;
            int margeBoven = 0;
            var kopregelhoogte = 230;
            int printBreedte = 0;

            margeBoven = 20;
            printBreedte = m_PrintDocument.DefaultPageSettings.PaperSize.Width - m_PrintDocument.DefaultPageSettings.Margins.Left - m_PrintDocument.DefaultPageSettings.Margins.Right;

            Font Kopletter = new Font("Times New Roman", 20, FontStyle.Bold | FontStyle.Italic);
            Font Subletter = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic);
            Font Normaal = new Font("Times New Roman", 10, FontStyle.Regular);

            float lijnbreedte = 0;
            float lijnhoogte = 0;
            float lijnbreedte1 = 0;
            float lijnhoogte1 = 0;
            float positieLinks = 0;
            float positieBoven = 0;
            int aantalRegels = 2;
            string KopTekst = "Controle Overzicht St. Petrus Claver";
            string maandS = "";
            string maandS1 = "";
            switch (maand)
            {
                case 1:
                    maandS = "Januari";
                    break;
                case 2:
                    maandS = "Februari";
                    break;
                case 3:
                    maandS = "Maart";
                    break;
                case 4:
                    maandS = "April";
                    break;
                case 5:
                    maandS = "Mei";
                    break;
                case 6:
                    maandS = "Juni";
                    break;
                case 7:
                    maandS = "Juli";
                    break;
                case 8:
                    maandS = "Augustus";
                    break;
                case 9:
                    maandS = "September";
                    break;
                case 10:
                    maandS = "Oktober";
                    break;
                case 11:
                    maandS = "November";
                    break;
                case 12:
                    maandS = "December";
                    break;
            }

            switch (maand1)
            {
                case 1:
                    maandS1 = "Januari";
                    break;
                case 2:
                    maandS1 = "Februari";
                    break;
                case 3:
                    maandS1 = "Maart";
                    break;
                case 4:
                    maandS1 = "April";
                    break;
                case 5:
                    maandS1 = "Mei";
                    break;
                case 6:
                    maandS1 = "Juni";
                    break;
                case 7:
                    maandS1 = "Juli";
                    break;
                case 8:
                    maandS1 = "Augustus";
                    break;
                case 9:
                    maandS1 = "September";
                    break;
                case 10:
                    maandS1 = "Oktober";
                    break;
                case 11:
                    maandS1 = "November";
                    break;
                case 12:
                    maandS1 = "December";
                    break;
            }

            string SubTekst = "";
            if (maandS1 == String.Empty)
            {
                SubTekst = maandS + " " + jaar;
            }
            else
            {
                SubTekst = maandS + " - " + maandS1 + " " + jaar;
            }
           
            lijnbreedte = e.Graphics.MeasureString(KopTekst, Kopletter).Width;
            lijnhoogte = Kopletter.GetHeight(e.Graphics);

            // Bereken zelf de coördinaat van de eerste regel linksboven
            positieLinks = (e.PageBounds.Width - lijnbreedte) / 2;
            positieBoven = (e.PageBounds.Height - (lijnhoogte * aantalRegels)) / 2;

            // Print de tekst
            e.Graphics.DrawString(KopTekst, Kopletter, Brushes.Black, positieLinks, margeBoven);

            // Plaats de tekst 'Tweewielerservice' horizontaal op de pagina
            // Bereken de breedte van de tekst, alsmede de hoogte
            lijnbreedte1 = e.Graphics.MeasureString(SubTekst, Subletter).Width;
            lijnhoogte1 = Subletter.GetHeight(e.Graphics);

            // Bereken zelf de coördinaat van de eerste regel linksboven
            positieLinks = (e.PageBounds.Width - lijnbreedte1) / 2;
            positieBoven = (e.PageBounds.Height - (lijnhoogte1 * aantalRegels)) / 2;

            // Print de tekst
            e.Graphics.DrawString(SubTekst, Subletter, Brushes.Black, positieLinks, margeBoven + lijnhoogte);

            kopregelhoogte = Convert.ToInt32(lijnhoogte) + Convert.ToInt32(lijnhoogte1);
            Pen blackPen = new Pen(Color.Black, 2);
            Point point1 = new Point(margeLinks, kopregelhoogte + 20);
            Point point2 = new Point(margeRechts, kopregelhoogte + 20);
            e.Graphics.DrawLine(blackPen, point1, point2);
            Font NormaalG = new Font("Courier New", 10, FontStyle.Regular);
            lijnhoogteG = NormaalG.GetHeight(e.Graphics);
        }

        private bool PrintDetails(Graphics g)
        {
            int margeLinks = 30;
            int margeRechts = m_PrintDocument.DefaultPageSettings.PaperSize.Width - 30;
            int margeBoven = 60;
            int printBreedte = 0;

            printBreedte = m_PrintDocument.DefaultPageSettings.PaperSize.Width - m_PrintDocument.DefaultPageSettings.Margins.Left - m_PrintDocument.DefaultPageSettings.Margins.Right;
            Font Normaal = new Font("Courier New", 10, FontStyle.Regular);
            bool booT = false;

            string[] s = tekst.Split('\r');
            for (int intX = RowsGeprint; intX < s.Length; intX++)
            {
                margeBoven += Convert.ToInt32(lijnhoogteG);
                g.DrawString(s[intX], Normaal, Brushes.Black, margeLinks, margeBoven);
                RegelsGeprint += 1;
                if (RegelsGeprint > aantalRegelsPerPagina)
                {
                    booT = true;
                    RegelsGeprint = 0;
                    RowsGeprint = intX;
                    PrintRows = intX + 1;
                    return booT;
                }
            }
            return booT;
        }

        private void m_PrintVoetNoot(PrintPageEventArgs e)
        {
            Font Normaal = new Font("Times New Roman", 10, FontStyle.Regular);
            int margeLinks = 30;
            int margeRechts = m_PrintDocument.DefaultPageSettings.PaperSize.Width - 30;
            int margeOnder = m_PrintDocument.DefaultPageSettings.PaperSize.Height - 50;
            Pen blackPen = new Pen(Color.Black, 2);
            Point point1 = new Point(margeLinks, margeOnder);
            Point point2 = new Point(margeRechts, margeOnder);
            e.Graphics.DrawLine(blackPen, point1, point2);
            string melding = "pagina - " + pagina + " -";
            var midden = (m_PrintDocument.DefaultPageSettings.PaperSize.Width - melding.Length) / 2;
            e.Graphics.DrawString(melding, Normaal, Brushes.Black, midden, m_PrintDocument.DefaultPageSettings.PaperSize.Height - 40);
            pagina++;
        }
    }
}
