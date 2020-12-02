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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.InputField = new System.Windows.Forms.TextBox();
            this.ClientNameField = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.MessageWindowRich = new System.Windows.Forms.RichTextBox();
            this.NicknameButton = new System.Windows.Forms.Button();
            this.ClientListBox = new System.Windows.Forms.ListBox();
            this.ClientListBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.FriendsListBox = new System.Windows.Forms.ListBox();
            this.FriendsListBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.friendsLabel = new System.Windows.Forms.Label();
            this.clientListLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PrivateMessageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GlobalMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientListBoxMenu.SuspendLayout();
            this.FriendsListBoxMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(157)))), ((int)(((byte)(51)))));
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubmitButton.Location = new System.Drawing.Point(234, 410);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(99, 40);
            this.SubmitButton.TabIndex = 12;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // InputField
            // 
            this.InputField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.InputField.Location = new System.Drawing.Point(12, 410);
            this.InputField.Multiline = true;
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(216, 40);
            this.InputField.TabIndex = 1;
            // 
            // ClientNameField
            // 
            this.ClientNameField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ClientNameField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientNameField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientNameField.Location = new System.Drawing.Point(50, 16);
            this.ClientNameField.Name = "ClientNameField";
            this.ClientNameField.Size = new System.Drawing.Size(178, 13);
            this.ClientNameField.TabIndex = 3;
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(157)))), ((int)(((byte)(51)))));
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectButton.Location = new System.Drawing.Point(339, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 20);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // MessageWindowRich
            // 
            this.MessageWindowRich.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MessageWindowRich.Location = new System.Drawing.Point(12, 50);
            this.MessageWindowRich.Name = "MessageWindowRich";
            this.MessageWindowRich.Size = new System.Drawing.Size(321, 354);
            this.MessageWindowRich.TabIndex = 5;
            this.MessageWindowRich.Text = "";
            // 
            // NicknameButton
            // 
            this.NicknameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(157)))), ((int)(((byte)(51)))));
            this.NicknameButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NicknameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NicknameButton.Location = new System.Drawing.Point(234, 12);
            this.NicknameButton.Name = "NicknameButton";
            this.NicknameButton.Size = new System.Drawing.Size(99, 20);
            this.NicknameButton.TabIndex = 6;
            this.NicknameButton.Text = "Set Username";
            this.NicknameButton.UseVisualStyleBackColor = false;
            this.NicknameButton.Click += new System.EventHandler(this.NicknameButton_Click);
            // 
            // ClientListBox
            // 
            this.ClientListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientListBox.ContextMenuStrip = this.ClientListBoxMenu;
            this.ClientListBox.FormattingEnabled = true;
            this.ClientListBox.Location = new System.Drawing.Point(339, 277);
            this.ClientListBox.Name = "ClientListBox";
            this.ClientListBox.Size = new System.Drawing.Size(141, 173);
            this.ClientListBox.TabIndex = 8;
            // 
            // ClientListBoxMenu
            // 
            this.ClientListBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFriend});
            this.ClientListBoxMenu.Name = "contextMenuStrip1";
            this.ClientListBoxMenu.Size = new System.Drawing.Size(133, 26);
            // 
            // AddFriend
            // 
            this.AddFriend.Name = "AddFriend";
            this.AddFriend.Size = new System.Drawing.Size(132, 22);
            this.AddFriend.Text = "Add Friend";
            this.AddFriend.Click += new System.EventHandler(this.AddFriend_Click);
            // 
            // FriendsListBox
            // 
            this.FriendsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FriendsListBox.ContextMenuStrip = this.FriendsListBoxMenu;
            this.FriendsListBox.FormattingEnabled = true;
            this.FriendsListBox.Location = new System.Drawing.Point(339, 72);
            this.FriendsListBox.Name = "FriendsListBox";
            this.FriendsListBox.Size = new System.Drawing.Size(142, 173);
            this.FriendsListBox.TabIndex = 9;
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
            // RemoveFriend
            // 
            this.RemoveFriend.Name = "RemoveFriend";
            this.RemoveFriend.Size = new System.Drawing.Size(180, 22);
            this.RemoveFriend.Text = "Remove Friend";
            this.RemoveFriend.Click += new System.EventHandler(this.RemoveFriend_Click);
            // 
            // friendsLabel
            // 
            this.friendsLabel.AutoSize = true;
            this.friendsLabel.BackColor = System.Drawing.Color.Black;
            this.friendsLabel.ForeColor = System.Drawing.Color.White;
            this.friendsLabel.Location = new System.Drawing.Point(340, 50);
            this.friendsLabel.Name = "friendsLabel";
            this.friendsLabel.Size = new System.Drawing.Size(60, 13);
            this.friendsLabel.TabIndex = 10;
            this.friendsLabel.Text = "Friends List";
            // 
            // clientListLabel
            // 
            this.clientListLabel.AutoSize = true;
            this.clientListLabel.BackColor = System.Drawing.Color.Black;
            this.clientListLabel.ForeColor = System.Drawing.Color.White;
            this.clientListLabel.Location = new System.Drawing.Point(340, 255);
            this.clientListLabel.Name = "clientListLabel";
            this.clientListLabel.Size = new System.Drawing.Size(52, 13);
            this.clientListLabel.TabIndex = 11;
            this.clientListLabel.Text = "Client List";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.ClientNameField);
            this.panel1.Controls.Add(this.NicknameButton);
            this.panel1.Controls.Add(this.ConnectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 41);
            this.panel1.TabIndex = 0;
            // 
            // PrivateMessageMenu
            // 
            this.PrivateMessageMenu.Name = "PrivateMessageMenu";
            this.PrivateMessageMenu.Size = new System.Drawing.Size(180, 22);
            this.PrivateMessageMenu.Text = "Private Message";
            this.PrivateMessageMenu.Click += new System.EventHandler(this.PrivateMessageMenu_Click);
            // 
            // GlobalMessage
            // 
            this.GlobalMessage.Name = "GlobalMessage";
            this.GlobalMessage.Size = new System.Drawing.Size(180, 22);
            this.GlobalMessage.Text = "Global Message";
            this.GlobalMessage.Click += new System.EventHandler(this.GlobalMessage_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(489, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.clientListLabel);
            this.Controls.Add(this.friendsLabel);
            this.Controls.Add(this.FriendsListBox);
            this.Controls.Add(this.ClientListBox);
            this.Controls.Add(this.MessageWindowRich);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.SubmitButton);
            this.Name = "ClientForm";
            this.Text = "Client-Server Messaging App";
            this.ClientListBoxMenu.ResumeLayout(false);
            this.FriendsListBoxMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem PrivateMessageMenu;
        private System.Windows.Forms.ToolStripMenuItem GlobalMessage;
    }
}