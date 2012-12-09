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
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(12, 12);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(318, 300);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // createDocumentButton
            // 
            this.createDocumentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createDocumentButton.Location = new System.Drawing.Point(12, 318);
            this.createDocumentButton.Name = "createDocumentButton";
            this.createDocumentButton.Size = new System.Drawing.Size(159, 30);
            this.createDocumentButton.TabIndex = 11;
            this.createDocumentButton.Text = "New document";
            this.createDocumentButton.UseVisualStyleBackColor = true;
            this.createDocumentButton.Click += new System.EventHandler(this.createDocumentButton_Click);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openButton.Location = new System.Drawing.Point(177, 318);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(153, 30);
            this.openButton.TabIndex = 12;
            this.openButton.Text = "Open document";
            this.openButton.UseVisualStyleBackColor = true;
            // 
            // createFolderButton
            // 
            this.createFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createFolderButton.Location = new System.Drawing.Point(12, 354);
            this.createFolderButton.Name = "createFolderButton";
            this.createFolderButton.Size = new System.Drawing.Size(90, 30);
            this.createFolderButton.TabIndex = 16;
            this.createFolderButton.Text = "New Folder";
            this.createFolderButton.UseVisualStyleBackColor = true;
            this.createFolderButton.Click += new System.EventHandler(this.createFolderButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moveButton.Location = new System.Drawing.Point(108, 354);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(72, 30);
            this.moveButton.TabIndex = 13;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            // 
            // renameButton
            // 
            this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.renameButton.Location = new System.Drawing.Point(186, 354);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(72, 30);
            this.renameButton.TabIndex = 15;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(264, 354);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(66, 30);
            this.deleteButton.TabIndex = 14;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // userLabel
            // 
            this.userLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(12, 395);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(98, 17);
            this.userLabel.TabIndex = 17;
            this.userLabel.Text = "Logged in as: ";
            // 
            // syncButton
            // 
            this.syncButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.syncButton.Location = new System.Drawing.Point(12, 415);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(168, 30);
            this.syncButton.TabIndex = 18;
            this.syncButton.Text = "Synchronize with server";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 457);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.createDocumentButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.createFolderButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.treeView);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 45);
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
    }
}

