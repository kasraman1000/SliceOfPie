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
using System.Diagnostics;
using System.IO;

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

            // Add document pictures to imagelist
            // and show images in listview
            int i = 0;
            foreach (Picture p in currentDoc.Images)
            {
                imageList.Images.Add(p.Image);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = i++;
                item.Tag = p;
                listView.Items.Add(item);
            }

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

        private void addImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            // Only find compatible images
            fileDialog.Filter = "PNG(*.PNG)|*.PNG|" +
                "JPEG (*.JPG;*.JPEG;*.JPE;*.JFIF)|*.JPG;*.JPEG;*.JPE;*.JFIF|"+
                "GIF (*.GIF)|*.GIF|"+
                "BMP (*.BMP;*.DIB;*.RLE)|*.BMP;*.DIB;*.RLE|" +
                "TIFF (*.TIF;*.TIFF)|*.TIF;*.TIFF";

            
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Picture pic = new Picture(new Bitmap(fileDialog.FileName));
                currentDoc.Images.Add(pic);
                imageList.Images.Add(pic.Image);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = imageList.Images.Count -1;
                item.Tag = pic;
                listView.Items.Add(item);

            }


        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                Picture pic = (Picture)listView.SelectedItems[0].Tag;
                new ImageViewer(pic.Image).Show(); ;

            }
        }

    }
}
