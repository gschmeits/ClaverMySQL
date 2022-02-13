namespace overzicht
{
	partial class ClaverRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public ClaverRibbon()
			: base(Globals.Factory.GetRibbonFactory())
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            Microsoft.Office.Tools.Ribbon.RibbonTab Maandoverzicht;
            this.group1 = this.Factory.CreateRibbonGroup();
            this.cmbMaand = this.Factory.CreateRibbonComboBox();
            this.cmbJaar = this.Factory.CreateRibbonComboBox();
            this.buttonGroup1 = this.Factory.CreateRibbonButtonGroup();
            this.btnHalen = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.Bij = this.Factory.CreateRibbonButtonGroup();
            this.bijwerkenSaldo = this.Factory.CreateRibbonButton();
            this.btnLeegmaken = this.Factory.CreateRibbonButton();
            Maandoverzicht = this.Factory.CreateRibbonTab();
            Maandoverzicht.SuspendLayout();
            this.group1.SuspendLayout();
            this.buttonGroup1.SuspendLayout();
            this.Bij.SuspendLayout();
            this.SuspendLayout();
            // 
            // Maandoverzicht
            // 
            Maandoverzicht.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            Maandoverzicht.Groups.Add(this.group1);
            Maandoverzicht.Label = "Maandoverzicht";
            Maandoverzicht.Name = "Maandoverzicht";
            // 
            // group1
            // 
            this.group1.Items.Add(this.cmbMaand);
            this.group1.Items.Add(this.cmbJaar);
            this.group1.Items.Add(this.buttonGroup1);
            this.group1.Items.Add(this.separator1);
            this.group1.Items.Add(this.Bij);
            this.group1.Items.Add(this.btnLeegmaken);
            this.group1.Label = "Maandoverzicht";
            this.group1.Name = "group1";
            // 
            // cmbMaand
            // 
            this.cmbMaand.Label = "Maand";
            this.cmbMaand.Name = "cmbMaand";
            this.cmbMaand.Text = null;
            this.cmbMaand.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cmbMaand_TextChanged);
            // 
            // cmbJaar
            // 
            this.cmbJaar.Label = "Jaar";
            this.cmbJaar.Name = "cmbJaar";
            this.cmbJaar.Text = null;
            this.cmbJaar.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cmbJaar_TextChanged);
            // 
            // buttonGroup1
            // 
            this.buttonGroup1.Items.Add(this.btnHalen);
            this.buttonGroup1.Name = "buttonGroup1";
            // 
            // btnHalen
            // 
            this.btnHalen.Label = "Samenstellen";
            this.btnHalen.Name = "btnHalen";
            this.btnHalen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHalen_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // Bij
            // 
            this.Bij.Items.Add(this.bijwerkenSaldo);
            this.Bij.Name = "Bij";
            // 
            // bijwerkenSaldo
            // 
            this.bijwerkenSaldo.Enabled = false;
            this.bijwerkenSaldo.Label = "Bijwerken saldi";
            this.bijwerkenSaldo.Name = "bijwerkenSaldo";
            this.bijwerkenSaldo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.bijwerkenSaldo_Click);
            // 
            // btnLeegmaken
            // 
            this.btnLeegmaken.Label = "Cellen leegmaken";
            this.btnLeegmaken.Name = "btnLeegmaken";
            this.btnLeegmaken.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLeegmaken_Click);
            // 
            // ClaverRibbon
            // 
            this.Name = "ClaverRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(Maandoverzicht);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ClaverRibbon_Load);
            Maandoverzicht.ResumeLayout(false);
            Maandoverzicht.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.buttonGroup1.ResumeLayout(false);
            this.buttonGroup1.PerformLayout();
            this.Bij.ResumeLayout(false);
            this.Bij.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cmbMaand;
		internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cmbJaar;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup Bij;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHalen;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton bijwerkenSaldo;
		internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroup1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLeegmaken;
    }

    partial class ThisRibbonCollection
	{
		internal ClaverRibbon ClaverRibbon
		{
			get { return this.GetRibbon<ClaverRibbon>(); }
		}
	}
}
