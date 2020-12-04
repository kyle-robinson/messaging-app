using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Client
{
    public partial class ClientForm : Form
    {
        private Client client;
        private bool connected = false;
        private bool disconnected = true;
        public List<string> mutedClients;
        private bool privateMessage = false;
        private bool nicknameEntered = false;
        private bool encryptMessages = false;
        private bool tcpMessages = true;

        public ClientForm( Client client )
        {
            InitializeComponent();
            this.client = client;
            mutedClients = new List<string>();

            ContextMenu blankContextMenu = new ContextMenu();
            MessageWindowRich.ContextMenu = blankContextMenu;
            CommandWindow.ContextMenu = blankContextMenu;
        }

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
                if ( removeText )
                {
                    try
                    {
                        for ( int i = ClientListBox.Items.Count - 1; i >= 0; --i )
                            if ( ClientListBox.Items[i].ToString().Contains( message ) )
                                ClientListBox.Items.RemoveAt( i );
                    }
                    catch ( Exception e )
                    {
                        Console.WriteLine( "ERROR:: " + e.Message );
                        ClientListBox.Items.RemoveAt( ClientListBox.Items.Count - 1 );
                    }
                }
                else
                {
                    ClientListBox.Items.Add( message );
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
                try
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
                catch ( Exception e )
                {
                    Console.WriteLine( "ERROR:: " + e.Message );
                }
            }
        }

        private void SubmitButton_Click( object sender, EventArgs e )
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
                    UpdateChatWindow( InputField.Text, "right", Color.Black, Color.PowderBlue );
                }
                else
                {
                    client.TcpSendMessage( new PrivateMessagePacket( "[Whisper] " + ClientNameField.Text + ": " + message, ClientListBox.SelectedItem.ToString() ) );
                    UpdateChatWindow( "[Whisper] " + ClientListBox.SelectedItem.ToString() + ": " + InputField.Text, "right", Color.Black, Color.LightYellow );
                }
                InputField.Clear();
            }
        }

        private void NicknameButton_Click( object sender, EventArgs e )
        {
            client.TcpSendMessage( new NicknamePacket( ClientNameField.Text ) );
            client.clientName = ClientNameField.Text;

            if ( ClientNameField.Text != "" )
            { 
                UpdateCommandWindow( "You updated your nickname. Hello " + client.clientName + "!", Color.Black, Color.Green );
                ConnectButton.Enabled = true;
                nicknameEntered = true;
            }
            else
            {
                UpdateCommandWindow( "Please enter an appropriate nickname!", Color.Black, Color.Red );
                nicknameEntered = false;
            }
        }

        private void ConnectButton_Click( object sender, EventArgs e )
        {
            if ( disconnected && nicknameEntered )
            {
                connected = true;
                disconnected = false;
                InputField.ReadOnly = false;
                InputField.Enabled = true;
                SubmitButton.Enabled = true;
                ConnectButton.Text = "Disconnect";
                UpdateCommandWindow( "You have connected to the server!", Color.Black, Color.Blue );
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
                UpdateCommandWindow( "You have disconnected from the server!", Color.Black, Color.Red );
                client.TcpSendMessage( new ClientListPacket( ClientNameField.Text, true ) );
            }

            if ( disconnected && !nicknameEntered )
                UpdateCommandWindow( "Please enter a nickname to connect before trying to connect to the server!", Color.Black, Color.Red );
        }

        private void AddFriend_Click( object sender, EventArgs e )
        {
            try
            {
                if ( ClientListBox.Items.Count > 0 )
                    if ( ClientListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                        UpdateFriendList( ClientListBox.SelectedItem.ToString(), false );
            }
            catch ( NullReferenceException exception )
            {
                Console.WriteLine( "ERROR:: ", exception.Message );
            }
        }

        private void RemoveFriend_Click( object sender, EventArgs e )
        {
            try
            {
                if ( FriendsListBox.Items.Count > 0 )
                    if ( FriendsListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                        UpdateFriendList( FriendsListBox.SelectedItem.ToString(), true );
            }
            catch ( NullReferenceException exception )
            {
                Console.WriteLine( "ERROR:: ", exception.Message );
            }
        }

        private void PrivateMessageMenu_Click( object sender, EventArgs e )
        {
            if ( ClientListBox.Items.Count > 0 )
            {
                privateMessage = true;
                UpdateCommandWindow( "You are now whispering to " + ClientListBox.SelectedItem.ToString() + "...", Color.Black, Color.Orange );
            }
        }

        private void GlobalMessage_Click( object sender, EventArgs e )
        {
            privateMessage = false;
            UpdateCommandWindow( "You are now messaging everyone on the server...", Color.Black, Color.Orange );
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
                UpdateCommandWindow( "You have unmuted " + clientToMute, Color.Black, Color.Blue );
            }
            else if ( !alreadyMuted && clientToMute != ClientNameField.Text )
            {
                UpdateCommandWindow( "You have muted all incoming messages from " + clientToMute, Color.Black, Color.Red );
                mutedClients.Add( clientToMute );
            }    
        }
    }
}