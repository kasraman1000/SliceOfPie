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
            this.SuspendLayout();
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(15, 12);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(255, 22);
            this.titleField.TabIndex = 1;
            this.titleField.Text = "Input Document Title Here";
            this.titleField.TextChanged += new System.EventHandler(this.titleField_TextChanged);
            // 
            // textField
            // 
            this.textField.Location = new System.Drawing.Point(15, 41);
            this.textField.Multiline = true;
            this.textField.Name = "textField";
            this.textField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textField.Size = new System.Drawing.Size(254, 173);
            this.textField.TabIndex = 2;
            this.textField.Text = "Input Document Contents Here";
            this.textField.TextChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(155, 220);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(115, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save Changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // EditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.ControlBox = false;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.textField);
            this.Controls.Add(this.titleField);
            this.Name = "EditWindow";
            this.Text = "Edit Document";
            this.Load += new System.EventHandler(this.EditWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox titleField;
        private System.Windows.Forms.TextBox textField;
        private System.Windows.Forms.Button saveButton;

    }
}