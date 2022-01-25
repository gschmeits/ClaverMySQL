using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_Logo.xaml
    /// </summary>
    public partial class pg_Logo : Page
    {
        public pg_Logo()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            versie.Text = "Versie " + version;
        }
    }
}
