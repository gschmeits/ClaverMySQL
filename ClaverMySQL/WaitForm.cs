using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetrusClaver
{
	public partial class WaitForm : Form
	{
		public int YPOS;

		public WaitForm(int intPosY)
		{
			InitializeComponent();
			YPOS = intPosY;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void test_Load(object sender, EventArgs e)
		{
			lblTekst.Text = "Een ogenblik geduld aub...";
			this.Location = new Point(this.Location.X, this.Location.Y-YPOS);
			//MessageBox.Show(this.Location.X.ToString() + ", " + this.Location.Y.ToString());
		}
	}
}
