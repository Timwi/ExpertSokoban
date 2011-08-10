using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    sealed partial class AboutBox : Form
    {
        /// <summary>Main constructor.</summary>
        public AboutBox()
        {
            InitializeComponent();
            Lingo.TranslateControl(this, Program.Tr.AboutBox);

            lblProductName.Text = AssemblyProduct;
            lblVersion.Text = Program.Tr.AboutBox.Version.Fmt(Ut.VersionOfExe());
            lblCopyright.Text = AssemblyCopyright;
            btnOK.Text = Program.Tr.Dialogs_btnOK;
        }

#if DEBUG
        public AboutBox(bool dummy)
        {
            InitializeComponent();
        }
#endif

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public Version AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }
        #endregion

        private void clickUrl(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblURL.Text);
        }

        private void clickRummageUrl(object sender, RT.Util.Controls.LinkEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldaray.com/Rummage");
        }
    }
}
