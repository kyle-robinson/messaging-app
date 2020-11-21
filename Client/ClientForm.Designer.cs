namespace Client
{
    partial class ClientForm
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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.InputField = new System.Windows.Forms.TextBox();
            this.ClientNameField = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.MessageWindowRich = new System.Windows.Forms.RichTextBox();
            this.NicknameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(444, 398);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(99, 40);
            this.SubmitButton.TabIndex = 0;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(12, 398);
            this.InputField.Multiline = true;
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(426, 40);
            this.InputField.TabIndex = 1;
            // 
            // ClientNameField
            // 
            this.ClientNameField.Location = new System.Drawing.Point(12, 12);
            this.ClientNameField.Name = "ClientNameField";
            this.ClientNameField.Size = new System.Drawing.Size(321, 20);
            this.ClientNameField.TabIndex = 3;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(444, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 20);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // MessageWindowRich
            // 
            this.MessageWindowRich.Location = new System.Drawing.Point(12, 38);
            this.MessageWindowRich.Name = "MessageWindowRich";
            this.MessageWindowRich.Size = new System.Drawing.Size(529, 354);
            this.MessageWindowRich.TabIndex = 5;
            this.MessageWindowRich.Text = "";
            // 
            // NicknameButton
            // 
            this.NicknameButton.Location = new System.Drawing.Point(339, 12);
            this.NicknameButton.Name = "NicknameButton";
            this.NicknameButton.Size = new System.Drawing.Size(99, 20);
            this.NicknameButton.TabIndex = 6;
            this.NicknameButton.Text = "Set Nickname";
            this.NicknameButton.UseVisualStyleBackColor = true;
            this.NicknameButton.Click += new System.EventHandler(this.NicknameButton_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 450);
            this.Controls.Add(this.NicknameButton);
            this.Controls.Add(this.MessageWindowRich);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ClientNameField);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.SubmitButton);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.TextBox ClientNameField;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.RichTextBox MessageWindowRich;
        private System.Windows.Forms.Button NicknameButton;
    }
}