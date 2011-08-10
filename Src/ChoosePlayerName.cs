using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RT.Util.Forms;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    partial class ChoosePlayerNameForm : ManagedForm
    {
        public enum FormType
        {
            Standard,
            FirstRun,
            SolvedLevel
        }

        private FormType _type;

        public ChoosePlayerNameForm(FormType type, string defaultValue)
            : base(Program.Settings.ChoosePlayerNameFormSettings)
        {
            InitializeComponent();
            Lingo.TranslateControl(this, Program.Tr.ChoosePlayerName);

            txtPlayerName.Text = defaultValue ?? "";
            _type = type;

            switch (type)
            {
                case FormType.Standard:
                    lblLanguage.Visible = false;
                    cmbLanguage.Visible = false;
                    lblPrompt.Text = Program.Tr.Mainform_ChooseName;
                    break;

                case FormType.FirstRun:
                    lblLanguage.Visible = true;
                    cmbLanguage.Visible = true;
                    lblPrompt.Text = Program.Tr.Mainform_ChooseName_FirstRun;
                    break;

                case FormType.SolvedLevel:
                    lblLanguage.Visible = false;
                    cmbLanguage.Visible = false;
                    lblPrompt.Text = Program.Tr.Mainform_ChooseName_SolvedLevel;
                    break;

                default:
                    throw new InvalidOperationException("562394");
            }

            btnOK.Text = Program.Tr.Dialogs_btnOK;
            btnCancel.Text = Program.Tr.Dialogs_btnCancel;
        }

#if DEBUG
        public ChoosePlayerNameForm(bool dummy)
            : base(Program.Settings.ChoosePlayerNameFormSettings)
        {
            InitializeComponent();
        }
#endif

        public static string GetPlayerName(FormType type, string defaultValue, LanguageMenuHelper<Translation> translationHelper = null)
        {
            using (var dlg = new ChoosePlayerNameForm(type, defaultValue))
            {
                if (translationHelper != null)
                    translationHelper.PopulateComboBox(dlg.cmbLanguage, tr =>
                    {
                        Lingo.TranslateControl(dlg, tr.ChoosePlayerName);
                        dlg.lblPrompt.Text = tr.Mainform_ChooseName_FirstRun;
                        dlg.btnOK.Text = tr.Dialogs_btnOK;
                        dlg.btnCancel.Text = tr.Dialogs_btnCancel;
                    });

                var result = dlg.ShowDialog();
                if (result == DialogResult.Cancel)
                    return null;
                return dlg.txtPlayerName.Text;
            }
        }
    }
}
