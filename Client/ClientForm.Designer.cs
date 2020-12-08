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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.SubmitButton = new System.Windows.Forms.Button();
            this.InputField = new System.Windows.Forms.TextBox();
            this.ClientNameField = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.MessageWindowRich = new System.Windows.Forms.RichTextBox();
            this.NicknameButton = new System.Windows.Forms.Button();
            this.ClientListBox = new System.Windows.Forms.ListBox();
            this.ClientListBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.LocalMute = new System.Windows.Forms.ToolStripMenuItem();
            this.FriendsListBox = new System.Windows.Forms.ListBox();
            this.FriendsListBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GlobalMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.PrivateMessageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.friendsLabel = new System.Windows.Forms.Label();
            this.clientListLabel = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.UsernameUnderline = new System.Windows.Forms.Panel();
            this.StaffsLogo = new System.Windows.Forms.PictureBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.FriendsLabelUnderline = new System.Windows.Forms.Panel();
            this.ClientLabelUnderline = new System.Windows.Forms.Panel();
            this.CommandWindow = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientListBoxMenu.SuspendLayout();
            this.FriendsListBoxMenu.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaffsLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.SubmitButton.Enabled = false;
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubmitButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Location = new System.Drawing.Point(234, 495);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(99, 40);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Send Message";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // InputField
            // 
            this.InputField.BackColor = System.Drawing.Color.White;
            this.InputField.Enabled = false;
            this.InputField.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputField.Location = new System.Drawing.Point(12, 495);
            this.InputField.Multiline = true;
            this.InputField.Name = "InputField";
            this.InputField.ReadOnly = true;
            this.InputField.Size = new System.Drawing.Size(216, 40);
            this.InputField.TabIndex = 5;
            this.InputField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputField_KeyDown);
            // 
            // ClientNameField
            // 
            this.ClientNameField.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientNameField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientNameField.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientNameField.Location = new System.Drawing.Point(107, 43);
            this.ClientNameField.MaxLength = 20;
            this.ClientNameField.Name = "ClientNameField";
            this.ClientNameField.Size = new System.Drawing.Size(226, 14);
            this.ClientNameField.TabIndex = 0;
            this.ClientNameField.Text = "Enter username...";
            this.ClientNameField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientNameField_KeyDown);
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.ConnectButton.Enabled = false;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.ForeColor = System.Drawing.Color.White;
            this.ConnectButton.Location = new System.Drawing.Point(107, 74);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(374, 30);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // MessageWindowRich
            // 
            this.MessageWindowRich.BackColor = System.Drawing.Color.White;
            this.MessageWindowRich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessageWindowRich.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageWindowRich.Location = new System.Drawing.Point(12, 251);
            this.MessageWindowRich.Name = "MessageWindowRich";
            this.MessageWindowRich.ReadOnly = true;
            this.MessageWindowRich.Size = new System.Drawing.Size(321, 238);
            this.MessageWindowRich.TabIndex = 4;
            this.MessageWindowRich.Text = "";
            // 
            // NicknameButton
            // 
            this.NicknameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.NicknameButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NicknameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NicknameButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NicknameButton.ForeColor = System.Drawing.Color.White;
            this.NicknameButton.Location = new System.Drawing.Point(344, 39);
            this.NicknameButton.Name = "NicknameButton";
            this.NicknameButton.Size = new System.Drawing.Size(137, 27);
            this.NicknameButton.TabIndex = 1;
            this.NicknameButton.Text = "Set Username";
            this.NicknameButton.UseVisualStyleBackColor = false;
            this.NicknameButton.Click += new System.EventHandler(this.NicknameButton_Click);
            // 
            // ClientListBox
            // 
            this.ClientListBox.BackColor = System.Drawing.Color.White;
            this.ClientListBox.ContextMenuStrip = this.ClientListBoxMenu;
            this.ClientListBox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientListBox.FormattingEnabled = true;
            this.ClientListBox.ItemHeight = 15;
            this.ClientListBox.Location = new System.Drawing.Point(339, 362);
            this.ClientListBox.Name = "ClientListBox";
            this.ClientListBox.Size = new System.Drawing.Size(141, 169);
            this.ClientListBox.TabIndex = 8;
            // 
            // ClientListBoxMenu
            // 
            this.ClientListBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFriend,
            this.LocalMute});
            this.ClientListBoxMenu.Name = "contextMenuStrip1";
            this.ClientListBoxMenu.Size = new System.Drawing.Size(134, 48);
            // 
            // AddFriend
            // 
            this.AddFriend.Name = "AddFriend";
            this.AddFriend.Size = new System.Drawing.Size(133, 22);
            this.AddFriend.Text = "Add Friend";
            this.AddFriend.Click += new System.EventHandler(this.AddFriend_Click);
            // 
            // LocalMute
            // 
            this.LocalMute.Name = "LocalMute";
            this.LocalMute.Size = new System.Drawing.Size(133, 22);
            this.LocalMute.Text = "Local Mute";
            this.LocalMute.Click += new System.EventHandler(this.LocalMute_Click);
            // 
            // FriendsListBox
            // 
            this.FriendsListBox.BackColor = System.Drawing.Color.White;
            this.FriendsListBox.ContextMenuStrip = this.FriendsListBoxMenu;
            this.FriendsListBox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FriendsListBox.FormattingEnabled = true;
            this.FriendsListBox.ItemHeight = 15;
            this.FriendsListBox.Location = new System.Drawing.Point(339, 157);
            this.FriendsListBox.Name = "FriendsListBox";
            this.FriendsListBox.Size = new System.Drawing.Size(142, 169);
            this.FriendsListBox.TabIndex = 7;
            // 
            // FriendsListBoxMenu
            // 
            this.FriendsListBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GlobalMessage,
            this.PrivateMessageMenu,
            this.RemoveFriend});
            this.FriendsListBoxMenu.Name = "FriendsListBoxMenu";
            this.FriendsListBoxMenu.Size = new System.Drawing.Size(160, 70);
            // 
            // GlobalMessage
            // 
            this.GlobalMessage.Name = "GlobalMessage";
            this.GlobalMessage.Size = new System.Drawing.Size(159, 22);
            this.GlobalMessage.Text = "Global Message";
            this.GlobalMessage.Click += new System.EventHandler(this.GlobalMessage_Click);
            // 
            // PrivateMessageMenu
            // 
            this.PrivateMessageMenu.Name = "PrivateMessageMenu";
            this.PrivateMessageMenu.Size = new System.Drawing.Size(159, 22);
            this.PrivateMessageMenu.Text = "Private Message";
            this.PrivateMessageMenu.Click += new System.EventHandler(this.PrivateMessageMenu_Click);
            // 
            // RemoveFriend
            // 
            this.RemoveFriend.Name = "RemoveFriend";
            this.RemoveFriend.Size = new System.Drawing.Size(159, 22);
            this.RemoveFriend.Text = "Remove Friend";
            this.RemoveFriend.Click += new System.EventHandler(this.RemoveFriend_Click);
            // 
            // friendsLabel
            // 
            this.friendsLabel.AutoSize = true;
            this.friendsLabel.BackColor = System.Drawing.Color.Transparent;
            this.friendsLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.friendsLabel.ForeColor = System.Drawing.Color.Black;
            this.friendsLabel.Location = new System.Drawing.Point(336, 135);
            this.friendsLabel.Name = "friendsLabel";
            this.friendsLabel.Size = new System.Drawing.Size(65, 15);
            this.friendsLabel.TabIndex = 9;
            this.friendsLabel.Text = "Friends List";
            // 
            // clientListLabel
            // 
            this.clientListLabel.AutoSize = true;
            this.clientListLabel.BackColor = System.Drawing.Color.Transparent;
            this.clientListLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientListLabel.ForeColor = System.Drawing.Color.Black;
            this.clientListLabel.Location = new System.Drawing.Point(336, 340);
            this.clientListLabel.Name = "clientListLabel";
            this.clientListLabel.Size = new System.Drawing.Size(57, 15);
            this.clientListLabel.TabIndex = 11;
            this.clientListLabel.Text = "Client List";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.UsernameUnderline);
            this.TopPanel.Controls.Add(this.StaffsLogo);
            this.TopPanel.Controls.Add(this.TitleLabel);
            this.TopPanel.Controls.Add(this.ClientNameField);
            this.TopPanel.Controls.Add(this.NicknameButton);
            this.TopPanel.Controls.Add(this.ConnectButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(492, 121);
            this.TopPanel.TabIndex = 0;
            // 
            // UsernameUnderline
            // 
            this.UsernameUnderline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.UsernameUnderline.Location = new System.Drawing.Point(107, 63);
            this.UsernameUnderline.Name = "UsernameUnderline";
            this.UsernameUnderline.Size = new System.Drawing.Size(225, 3);
            this.UsernameUnderline.TabIndex = 15;
            // 
            // StaffsLogo
            // 
            this.StaffsLogo.BackColor = System.Drawing.Color.Transparent;
            this.StaffsLogo.Image = ((System.Drawing.Image)(resources.GetObject("StaffsLogo.Image")));
            this.StaffsLogo.Location = new System.Drawing.Point(12, 11);
            this.StaffsLogo.Name = "StaffsLogo";
            this.StaffsLogo.Size = new System.Drawing.Size(89, 95);
            this.StaffsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.StaffsLogo.TabIndex = 13;
            this.StaffsLogo.TabStop = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.Black;
            this.TitleLabel.Location = new System.Drawing.Point(106, 12);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(188, 24);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Messaging App";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FriendsLabelUnderline
            // 
            this.FriendsLabelUnderline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.FriendsLabelUnderline.Location = new System.Drawing.Point(339, 149);
            this.FriendsLabelUnderline.Name = "FriendsLabelUnderline";
            this.FriendsLabelUnderline.Size = new System.Drawing.Size(61, 3);
            this.FriendsLabelUnderline.TabIndex = 13;
            // 
            // ClientLabelUnderline
            // 
            this.ClientLabelUnderline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(9)))), ((int)(((byte)(38)))));
            this.ClientLabelUnderline.Location = new System.Drawing.Point(339, 354);
            this.ClientLabelUnderline.Name = "ClientLabelUnderline";
            this.ClientLabelUnderline.Size = new System.Drawing.Size(53, 3);
            this.ClientLabelUnderline.TabIndex = 14;
            // 
            // CommandWindow
            // 
            this.CommandWindow.BackColor = System.Drawing.Color.White;
            this.CommandWindow.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandWindow.Location = new System.Drawing.Point(13, 136);
            this.CommandWindow.Name = "CommandWindow";
            this.CommandWindow.ReadOnly = true;
            this.CommandWindow.Size = new System.Drawing.Size(317, 109);
            this.CommandWindow.TabIndex = 3;
            this.CommandWindow.Text = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(294, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "[by Kyle Robinson]";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(492, 549);
            this.Controls.Add(this.CommandWindow);
            this.Controls.Add(this.ClientLabelUnderline);
            this.Controls.Add(this.FriendsLabelUnderline);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.clientListLabel);
            this.Controls.Add(this.friendsLabel);
            this.Controls.Add(this.FriendsListBox);
            this.Controls.Add(this.ClientListBox);
            this.Controls.Add(this.MessageWindowRich);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.SubmitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ClientForm";
            this.Text = "Client-Server Messaging App";
            this.ClientListBoxMenu.ResumeLayout(false);
            this.FriendsListBoxMenu.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaffsLogo)).EndInit();
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
        private System.Windows.Forms.ListBox ClientListBox;
        private System.Windows.Forms.ListBox FriendsListBox;
        private System.Windows.Forms.Label friendsLabel;
        private System.Windows.Forms.Label clientListLabel;
        private System.Windows.Forms.ContextMenuStrip ClientListBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem AddFriend;
        private System.Windows.Forms.ContextMenuStrip FriendsListBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveFriend;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.ToolStripMenuItem PrivateMessageMenu;
        private System.Windows.Forms.ToolStripMenuItem GlobalMessage;
        private System.Windows.Forms.ToolStripMenuItem LocalMute;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Panel UsernameUnderline;
        private System.Windows.Forms.PictureBox StaffsLogo;
        private System.Windows.Forms.Panel FriendsLabelUnderline;
        private System.Windows.Forms.Panel ClientLabelUnderline;
        private System.Windows.Forms.RichTextBox CommandWindow;
        private System.Windows.Forms.Label label1;
    }
}