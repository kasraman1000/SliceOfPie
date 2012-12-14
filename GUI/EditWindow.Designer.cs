namespace GUI
{
    partial class EditWindow
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
            this.titleField = new System.Windows.Forms.TextBox();
            this.textField = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.viewLogButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleField
            // 
            this.titleField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.titleField.Location = new System.Drawing.Point(12, 29);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(473, 22);
            this.titleField.TabIndex = 0;
            this.titleField.Text = "Input Document Title Here";
            this.titleField.TextChanged += new System.EventHandler(this.titleField_TextChanged);
            // 
            // textField
            // 
            this.textField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textField.Location = new System.Drawing.Point(12, 74);
            this.textField.Multiline = true;
            this.textField.Name = "textField";
            this.textField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textField.Size = new System.Drawing.Size(473, 251);
            this.textField.TabIndex = 1;
            this.textField.Text = "Input Document Contents Here";
            this.textField.TextChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(370, 331);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(115, 33);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Document Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Document Contents";
            // 
            // viewLogButton
            // 
            this.viewLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.viewLogButton.Location = new System.Drawing.Point(15, 331);
            this.viewLogButton.Name = "viewLogButton";
            this.viewLogButton.Size = new System.Drawing.Size(160, 33);
            this.viewLogButton.TabIndex = 2;
            this.viewLogButton.Text = "View document history";
            this.viewLogButton.UseVisualStyleBackColor = true;
            this.viewLogButton.Click += new System.EventHandler(this.viewLogButton_Click);
            // 
            // EditWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(497, 376);
            this.Controls.Add(this.viewLogButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.textField);
            this.Controls.Add(this.titleField);
            this.Name = "EditWindow";
            this.Text = "Edit document";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWindow_FormClosing);
            this.Load += new System.EventHandler(this.EditWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox titleField;
        private System.Windows.Forms.TextBox textField;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button viewLogButton;

    }
}