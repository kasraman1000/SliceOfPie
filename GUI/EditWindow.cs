using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SliceOfPie;

namespace GUI
{
    public partial class EditWindow : Form
    {
        private Document currentDoc;
        
        private bool modified; // Does this document have unsaved changes?
        public bool Modified
        {
            get { return modified; }
        }

        public EditWindow(Document doc)
        {
            currentDoc = doc;
            InitializeComponent();
        }

        private void EditWindow_Load(object sender, EventArgs e)
        {
            titleField.Text = currentDoc.Title;
            textField.Text = currentDoc.Text;
            modified = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            currentDoc.Title = titleField.Text;
            currentDoc.Text = textField.Text;

            new Controller().SaveDocument(currentDoc);

            modified = false;
        }

        private void titleField_TextChanged(object sender, EventArgs e)
        {
            modified = true;
        }

        private void textField_TextChanged(object sender, EventArgs e)
        {
            modified = true;
        }
    }
}
