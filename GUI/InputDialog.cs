using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class InputDialog : Form
    {
        private bool canceled = false;
        public bool Canceled { get { return canceled; } }

        public string Input { get { return textBox.Text; } }
        
        private string question;
        private string def;
        private bool cancelPossible;

        public InputDialog(string question, string def = "", bool cancelPossible = true)
        {
            this.question = question;
            this.def = def;
            this.cancelPossible = cancelPossible;
            InitializeComponent();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            questionLabel.Text = question;
            textBox.Text = def;
            cancelButton.Enabled = cancelPossible;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            canceled = true;
            Close();
        }

        
    }
}
