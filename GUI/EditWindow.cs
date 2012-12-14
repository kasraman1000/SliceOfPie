using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SliceOfPie;
using System.Text.RegularExpressions;

namespace GUI
{
    public partial class EditWindow : Form
    {
        private Document currentDoc;
        private Project currentProj;
        private User currentUser;
        private bool modified; // Does this document have unsaved changes?
        public bool Modified
        {
            get { return modified; }
        }

        public EditWindow(Project proj, Document doc, User user)
        {
            currentProj = proj;
            currentDoc = doc;
            currentUser = user;
            InitializeComponent();
        }

        private void EditWindow_Load(object sender, EventArgs e)
        {   

            textField.Text = Regex.Replace(currentDoc.Text, "\n", Environment.NewLine);
            this.Text = currentDoc.Title;
            modified = false;
            saveButton.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            currentDoc.Text = textField.Text;

            Controller.SaveDocument(currentProj, currentDoc, currentUser);

            modified = false;
            saveButton.Enabled = false;
        }

        private void textField_TextChanged(object sender, EventArgs e)
        {
            modified = true;
            saveButton.Enabled = true;
        }

        /**
         * Make sure the user does not leave behind any unsaved changes
         */
        private void EditWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modified)
            {
                
                DialogResult result = MessageBox.Show(
                    "You have unsaved changes to this document. " +
                    "Closing this window now will discard these changes. " +
                    "\nAre you sure you want to close this window?",
                    "Unsaved Changes",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    modified = false;
                }

            }

        }

        private void viewLogButton_Click(object sender, EventArgs e)
        {
            new LogWindow(currentDoc).Show();
        }

    }
}
