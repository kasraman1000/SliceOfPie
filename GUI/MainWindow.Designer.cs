﻿namespace GUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.createDocumentButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.createFolderButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.userLabel = new System.Windows.Forms.Label();
            this.syncButton = new System.Windows.Forms.Button();
            this.projectBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.getProjectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(12, 47);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(319, 304);
            this.treeView.TabIndex = 1;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // createDocumentButton
            // 
            this.createDocumentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createDocumentButton.Location = new System.Drawing.Point(12, 358);
            this.createDocumentButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createDocumentButton.Name = "createDocumentButton";
            this.createDocumentButton.Size = new System.Drawing.Size(159, 30);
            this.createDocumentButton.TabIndex = 2;
            this.createDocumentButton.Text = "New document";
            this.createDocumentButton.UseVisualStyleBackColor = true;
            this.createDocumentButton.Click += new System.EventHandler(this.createDocumentButton_Click);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openButton.Location = new System.Drawing.Point(177, 358);
            this.openButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(153, 30);
            this.openButton.TabIndex = 3;
            this.openButton.Text = "Open document";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // createFolderButton
            // 
            this.createFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createFolderButton.Location = new System.Drawing.Point(12, 394);
            this.createFolderButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createFolderButton.Name = "createFolderButton";
            this.createFolderButton.Size = new System.Drawing.Size(99, 30);
            this.createFolderButton.TabIndex = 4;
            this.createFolderButton.Text = "New Folder";
            this.createFolderButton.UseVisualStyleBackColor = true;
            this.createFolderButton.Click += new System.EventHandler(this.createFolderButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moveButton.Location = new System.Drawing.Point(116, 394);
            this.moveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(55, 30);
            this.moveButton.TabIndex = 5;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.renameButton.Location = new System.Drawing.Point(176, 394);
            this.renameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(83, 30);
            this.renameButton.TabIndex = 6;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(264, 394);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(67, 30);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // userLabel
            // 
            this.userLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(12, 434);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(98, 17);
            this.userLabel.TabIndex = 17;
            this.userLabel.Text = "Logged in as: ";
            // 
            // syncButton
            // 
            this.syncButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.syncButton.Location = new System.Drawing.Point(12, 455);
            this.syncButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(168, 30);
            this.syncButton.TabIndex = 8;
            this.syncButton.Text = "Synchronize with server";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // projectBox
            // 
            this.projectBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectBox.FormattingEnabled = true;
            this.projectBox.Location = new System.Drawing.Point(124, 15);
            this.projectBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.projectBox.Name = "projectBox";
            this.projectBox.Size = new System.Drawing.Size(207, 24);
            this.projectBox.TabIndex = 0;
            this.projectBox.SelectedIndexChanged += new System.EventHandler(this.projectBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Select a project";
            // 
            // getProjectButton
            // 
            this.getProjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getProjectButton.Location = new System.Drawing.Point(186, 456);
            this.getProjectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getProjectButton.Name = "getProjectButton";
            this.getProjectButton.Size = new System.Drawing.Size(146, 30);
            this.getProjectButton.TabIndex = 21;
            this.getProjectButton.Text = "Download projects";
            this.getProjectButton.UseVisualStyleBackColor = true;
            this.getProjectButton.Click += new System.EventHandler(this.getProjectButton_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.openButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(344, 497);
            this.Controls.Add(this.getProjectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.projectBox);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.createDocumentButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.createFolderButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.treeView);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(358, 45);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slice of Pie";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button createDocumentButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button createFolderButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.ComboBox projectBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getProjectButton;
    }
}

