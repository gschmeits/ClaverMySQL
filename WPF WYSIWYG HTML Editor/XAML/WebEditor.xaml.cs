using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using DataStorage;

namespace WPF_WYSIWYG_HTML_Editor
{
    public partial class WebEditor : Window
    {
        public WebEditor()
        {
            InitializeComponent();            
        }

        private void SettingsBold_Click(object sender, RoutedEventArgs e)
        {
            Format.bold();            
        }

        private void SettingsItalic_Click(object sender, RoutedEventArgs e)
        {
            Format.Italic();
        }

        private void SettingsUnderLine_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsRightAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsLeftAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyLeft();
        }

        private void SettingsCenter2_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyCenter();
        }

        private void SettingsJustifyRight_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyRight();
        }

        private void SettingsJustifyFull_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyFull();
        }

        private void SettingsInsertOrderedList_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertOrderedList();
        }

        private void SettingsBullets_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertUnorderedList();
        }

        private void SettingsOutIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Outdent();            
        }

        private void SettingsIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Indent();  
        }

        private void RibbonButtonNew_Click(object sender, RoutedEventArgs e)
        {
            TextBoxId.Text = string.Empty;
            TextBoxNaam.Text = string.Empty;
            webBrowserEditor.Visibility = Visibility.Visible;
            Gui.newdocument();
        }

        private void RibbonButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            webBrowserEditor.Visibility = Visibility.Hidden;
            StackPanelOpslaanAls.Visibility = Visibility.Hidden;
            StackPanelOphalen.Visibility = Visibility.Visible;
            Gui.newdocumentFile();
            lblCursorPosition.Text = TextBoxNaam.Text;
        }

        private void RibbonButtonOpenweb_Click(object sender, RoutedEventArgs e)
        {
           // webBrowserEditor.newWb(@"http://www.codeproject.com/");


        }

        private void SettingsFontColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsFontColor();
        }

        private void SettingsBackColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsBackColor();
        }

        private void SettingsAddLink_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddLink();
        }

        private void SettingsAddImage_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddImage();
        }

        private void RibbonButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxNaam.Text == string.Empty &&
                TextBoxId.Text == string.Empty)
            {
                webBrowserEditor.Visibility = Visibility.Hidden;
                StackPanelOpslaanAls.Visibility = Visibility.Visible;
            }
            else
            {
                Gui.RibbonButtonSave(TextBoxId.Text, TextBoxNaam.Text);
                ComboBoxTemplates.SelectedIndex = -1;
            }
        }

        private void RibbonComboboxFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFonts(RibbonComboboxFonts);            
        }

        private void RibbonComboboxFontHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFontHeight(RibbonComboboxFontHeight);
        }

        private void RibbonComboboxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFormat(RibbonComboboxFormat);
        }

        private void EditWeb_Click(object sender, RoutedEventArgs e)
        {
            Gui.EditWeb();
        }

        private void ViewHTML_Click(object sender, RoutedEventArgs e)
        {
            Gui.ViewHTML();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gui.webBrowser = webBrowserEditor;
            Gui.htmlEditor = HtmlEditor1;
            Initialisation.webeditor = this;
            Gui.newdocument();
            
            Initialisation.RibbonComboboxFontsInitialisation();
            Initialisation.RibbonComboboxFontSizeInitialisation();
            Initialisation.RibbonComboboxFormatInitionalisation();

            HaalTemplates();
            TextBoxId.Text = string.Empty;
            TextBoxNaam.Text = string.Empty;
        }

        private void HaalTemplates()
        {
            var sSQL = "SELECT * FROM mailtemplates";
            DataTable dt = GenericDataRead.GetData(sSQL);
            ComboBoxTemplates.ItemsSource = dt.DefaultView;
            ComboBoxTemplates.SelectedValuePath = "mailID";
            ComboBoxTemplates.DisplayMemberPath = "name";
        }

        private void SettingsInserLine_Click(object sender, RoutedEventArgs e)
        {
            Gui.horizontalLine();
        }

        private void ComboBoxTemplates_DropDownClosed(object sender, System.EventArgs e)
        {
            var sSQL = "SELECT * FROM mailtemplates WHERE mailID = " + ComboBoxTemplates.SelectedValue;
            DataTable dt = GenericDataRead.GetData(sSQL);

            if (dt.Rows.Count > 0)
            {
                var string1 = dt.Rows[0]["template"].ToString().Replace((char)34, (char)39);

                TextBoxId.Text = dt.Rows[0]["mailID"].ToString();
                TextBoxNaam.Text = dt.Rows[0]["name"].ToString();
                webBrowserEditor.doc.body.innerHTML = string1;
                lblCursorPosition.Text = TextBoxNaam.Text;
            }

            StackPanelOphalen.Visibility = Visibility.Hidden;
            webBrowserEditor.Visibility = Visibility.Visible;
        }

        private void TextBoxNaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxNaam.Text != String.Empty)
            {
                ButtonOplaan.IsEnabled = true;
            }
            else
            {
                ButtonOplaan.IsEnabled = false;
            }
        }

        private void ButtonOplaan_Click(object sender, RoutedEventArgs e)
        {
            Gui.RibbonButtonSave(TextBoxId.Text, TextBoxNaam.Text);
            StackPanelOpslaanAls.Visibility = Visibility.Hidden;
            webBrowserEditor.Visibility = Visibility.Visible;
            HaalTemplates();
        }
    }
}
