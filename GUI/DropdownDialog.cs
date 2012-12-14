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
    public partial class DropdownDialog<T> : Form
    {
        private bool canceled = false;
        public bool Canceled { get { return canceled; } }

        public T Selected { get { return  (T) comboBox.SelectedItem; } }
        
        private string question;
        private List<T> items;
        private bool cancelPossible;

        public DropdownDialog(string question, List<T> items, bool cancelPossible = true)
        {
            this.question = question;
            this.items = items;
            this.cancelPossible = cancelPossible;
            
            InitializeComponent();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            questionLabel.Text = question;

            foreach (Object item in items)
            {
                comboBox.Items.Add(item);
            }

            if (cancelPossible == false)
            {
                cancelButton.Enabled = false;
                cancelButton.Visible = false;
            }
             
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
