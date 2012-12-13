﻿using System;
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
    public partial class LogWindow : Form
    {
        private Document currentDocument;

        public LogWindow(Document doc)
        {
            currentDocument = doc;


            InitializeComponent();
        }

        /**
         * When window is opened, load in all the log entries and
         * fill the list.
         */
        private void LogWindow_Load(object sender, EventArgs e)
        {
            int i = 0;

            foreach(Document.DocumentLog.Entry entry in currentDocument.Log.entries)
            {
                // ListViewItem item = new ListViewItem(entry.ToString());
                listView.Items.Insert(i, entry.ToString()).Tag = entry;
            }
            listView.Items[i].Selected = true;
        }

        /**
         * Show the selected log entry in the textBox
         */
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Document.DocumentLog.Entry entry = (Document.DocumentLog.Entry) listView.SelectedItems[0].Tag;
            textBox.Text = entry.ToStringWithLog();
        }
    }
}
