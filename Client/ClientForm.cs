﻿using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Client
{
    public partial class ClientForm : Form
    {
        public List<string> mutedClients;
        private List<string> userNames;
        private Client client;
        private bool connected = false;
        private bool disconnected = true;
        private bool privateMessage = false;
        private bool nicknameEntered = false;
        private bool encryptMessages = false;
        private bool tcpMessages = true;

        public ClientForm( Client client )
        {
            InitializeComponent();
            this.client = client;
            mutedClients = new List<string>();
            userNames = new List<string>();

            ContextMenu blankContextMenu = new ContextMenu();
            MessageWindowRich.ContextMenu = blankContextMenu;
            CommandWindow.ContextMenu = blankContextMenu;
        }

        /*   UPDATE MESSAGE WINDOWS   */
        public void UpdateCommandWindow( string message, Color foreColor, Color backColor )
        {
            if ( CommandWindow.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateCommandWindow( message, foreColor, backColor ); } ) );
            }
            else
            {
                CommandWindow.SelectionStart = CommandWindow.TextLength;
                CommandWindow.SelectionLength = 0;

                CommandWindow.SelectionColor = foreColor;
                CommandWindow.SelectionBackColor = backColor;
                CommandWindow.AppendText( message + "\n" );
                CommandWindow.SelectionColor = CommandWindow.ForeColor;

                CommandWindow.SelectionStart = CommandWindow.Text.Length;
                CommandWindow.ScrollToCaret();
            }
        }

        public void UpdateChatWindow( string message, string alignment, Color foreColor, Color backColor )
        {
            if ( MessageWindowRich.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateChatWindow( message, alignment, foreColor, backColor ); } ) );
            }
            else
            {
                bool clientIsMuted = false;
                foreach ( string s in mutedClients )
                    if ( message.Contains( s.ToString() ) )
                        clientIsMuted = true;

                if ( !clientIsMuted )
                {
                    MessageWindowRich.SelectionStart = MessageWindowRich.TextLength;
                    MessageWindowRich.SelectionLength = 0;

                    MessageWindowRich.SelectionColor = foreColor;
                    if ( alignment == "left".ToLower() )
                        MessageWindowRich.SelectionAlignment = HorizontalAlignment.Left;
                    if ( alignment == "right".ToLower() )
                        MessageWindowRich.SelectionAlignment = HorizontalAlignment.Right;
                    MessageWindowRich.SelectionBackColor = backColor;
                    MessageWindowRich.AppendText( message + "\n" );
                    MessageWindowRich.SelectionColor = MessageWindowRich.ForeColor;

                    MessageWindowRich.SelectionStart = MessageWindowRich.Text.Length;
                    MessageWindowRich.ScrollToCaret();
                }
            }
        }

        public void UpdateClientList( string message, bool removeText )
        {
            if ( ClientListBox.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateClientList( message, removeText ); } ) );
            }
            else
            {
                // add or remove players from the list as they connect/disconnect
                if ( message != null )
                {
                    if ( removeText )
                    {
                        userNames.Clear();
                        ClientListBox.Items.Clear();
                    }
                    else
                    {
                        userNames.Add( message );
                        ClientListBox.Items.Add( message );
                    }
                }

                if ( userNames.Count != userNames.Distinct().Count() )
                {
                    userNames.Remove( message );
                    ClientListBox.Items.Remove( message );
                }
            }
        }

        private void UpdateFriendList( string message, bool removeFriend )
        {
            if ( FriendsListBox.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateFriendList( message, removeFriend ); } ) );
            }
            else
            {
                if ( !removeFriend )
                {
                    bool friendExists = false;
                    for ( int i = FriendsListBox.Items.Count - 1; i >= 0; --i )
                        if ( FriendsListBox.Items[i].ToString() == message )
                            friendExists = true;

                    if ( !friendExists )
                        FriendsListBox.Items.Add( message );
                }
                else
                {
                    for ( int i = FriendsListBox.Items.Count - 1; i >= 0; --i )
                        if ( FriendsListBox.Items[i].ToString().Contains( message ) )
                            FriendsListBox.Items.RemoveAt( i );
                }
            }
        }

        /*   SET USERNANME   */
        private void SetUsername()
        {
            client.TcpSendMessage( new NicknamePacket( ClientNameField.Text ) );
            client.clientName = ClientNameField.Text;

            if ( ClientNameField.Text != "" && ClientNameField.Text != "Enter username..." )
            { 
                UpdateCommandWindow( "You updated your nickname. Hello " + client.clientName + "!", Color.Black, Color.SkyBlue );
                ConnectButton.Enabled = true;
                nicknameEntered = true;
            }
            else
            {
                UpdateCommandWindow( "Please enter an appropriate nickname!", Color.Black, Color.LightCoral );
                nicknameEntered = false;
            }
        }

        private void NicknameButton_Click( object sender, EventArgs e )
        {
            SetUsername();
        }

        private void ClientNameField_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                e.SuppressKeyPress = true;
                SetUsername();
                ConnectButton.Focus();
            }
        }

        /*   CONNECT/DISCONNECT FROM THE SERVER   */
        private void ConnectButton_Click( object sender, EventArgs e )
        {
            if ( disconnected && nicknameEntered )
            {
                connected = true;
                disconnected = false;
                InputField.Enabled = true;
                SubmitButton.Enabled = true;

                InputField.ReadOnly = false;
                InputField.Focus();

                ConnectButton.Text = "Disconnect";
                UpdateCommandWindow( "You have connected to the server!", Color.Black, Color.LightGreen );
                client.TcpSendMessage( new ClientListPacket( ClientNameField.Text, false ) );
            }
            else if ( connected )
            {
                connected = false;
                disconnected = true;

                InputField.ReadOnly = true;
                InputField.Enabled = false;
                SubmitButton.Enabled = false;

                ConnectButton.Text = "Connect";
                UpdateCommandWindow( "You have disconnected from the server!", Color.Black, Color.LightCoral );
                client.TcpSendMessage( new ClientListPacket( ClientNameField.Text, true ) );
            }

            if ( disconnected && !nicknameEntered )
                UpdateCommandWindow( "Please enter a nickname to connect before trying to connect to the server!", Color.Black, Color.LightCoral );
        }

        /*   CONTEXT MENU OPTIONS   */
        private void AddFriend_Click( object sender, EventArgs e )
        {
            if ( ClientListBox.Items.Count > 0 && ClientListBox.SelectedItem != null )
            {
                if ( ClientListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                {
                    UpdateFriendList( ClientListBox.SelectedItem.ToString(), false );
                    UpdateCommandWindow( "You have added " + ClientListBox.SelectedItem + " to your friends list.", Color.Black, Color.SkyBlue );
                }
            }
        }

        private void RemoveFriend_Click( object sender, EventArgs e )
        {
            if ( FriendsListBox.Items.Count > 0 && FriendsListBox.SelectedItem != null )
            {
                if ( FriendsListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                {
                    UpdateFriendList( FriendsListBox.SelectedItem.ToString(), true );
                    UpdateCommandWindow( "You have removed " + FriendsListBox.SelectedItem + " from your friends list.", Color.Black, Color.LightCoral );
                }
            }
        }

        private void PrivateMessageMenu_Click( object sender, EventArgs e )
        {
            if ( ClientListBox.Items.Count > 0 && ClientListBox.SelectedItem != null )
            {
                privateMessage = true;
                UpdateCommandWindow( "You are now whispering to " + ClientListBox.SelectedItem.ToString() + ".", Color.Black, Color.LightPink );
            }
        }

        private void GlobalMessage_Click( object sender, EventArgs e )
        {
            privateMessage = false;
            UpdateCommandWindow( "You are now messaging everyone on the server.", Color.Black, Color.LightPink );
        }

        private void LocalMute_Click( object sender, EventArgs e )
        {
            bool alreadyMuted = false;
            string clientToMute = ClientListBox.SelectedItem.ToString();

            foreach ( string s in mutedClients )
                if( s.ToString() == clientToMute && s.ToString() != ClientNameField.Text )
                    alreadyMuted = true;

            if ( alreadyMuted  )
            {
                mutedClients.Remove( clientToMute );
                UpdateCommandWindow( "You have unmuted " + clientToMute + ".", Color.Black, Color.SkyBlue );
            }
            else if ( !alreadyMuted && clientToMute != ClientNameField.Text )
            {
                UpdateCommandWindow( "You have muted all incoming messages from " + clientToMute + ".", Color.Black, Color.LightCoral );
                mutedClients.Add( clientToMute );
            }    
        }

        /*   SEND MESSAGES   */
        private void SendMessage()
        {
            string message = InputField.Text;
            if ( message != "" )
            {
                if ( !privateMessage )
                {
                    if ( encryptMessages )
                        client.TcpSendMessage( new EncryptedMessagePacket( client.EncryptString( message ) ) );
                    else
                    {
                        if ( tcpMessages )
                            client.TcpSendMessage( new ChatMessagePacket( message ) );
                        else
                            client.UdpSendMessage( new ChatMessagePacket( message ) );
                    }
                    UpdateChatWindow( "To [Local]: " + InputField.Text, "right", Color.Black, Color.LightSteelBlue );
                }
                else
                {
                    client.TcpSendMessage( new PrivateMessagePacket( "[" + ClientNameField.Text + "]: " + message, ClientListBox.SelectedItem.ToString() ) );
                    UpdateChatWindow( "To [" + ClientListBox.SelectedItem.ToString() + "]: " + InputField.Text, "right", Color.Black, Color.LightPink );
                }
                InputField.Clear();
            }
        }

        private void SubmitButton_Click( object sender, EventArgs e )
        {
            SendMessage();
        }

        private void InputField_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }
    }
}