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

        public ClientForm( Client client )
        {
            InitializeComponent();
            this.client = client;
            InputField.ReadOnly = true;
            SubmitButton.Enabled = false;
            ConnectButton.Enabled = false;
            mutedClients = new List<string>();
            MessageWindowRich.SelectionProtected = true;
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
                    client.TcpSendMessage( new ChatMessagePacket( message ) );
                    UpdateChatWindow( "[You]: " + InputField.Text, "right", Color.Black, Color.PowderBlue );
                }
                else
                {
                    client.TcpSendMessage( new PrivateMessagePacket( "[Whisper] " + ClientNameField.Text + ": " + message, ClientListBox.SelectedItem.ToString() ) );
                    UpdateChatWindow( "[Whisper] " + ClientListBox.SelectedItem.ToString() + ": " + InputField.Text, "right", Color.Black, Color.LightYellow );
                }
                //client.UdpSendMessage( new ChatMessagePacket( message ) );
                InputField.Clear();
            }
        }

        private void NicknameButton_Click( object sender, EventArgs e )
        {
            client.TcpSendMessage( new NicknamePacket( ClientNameField.Text ) );
            client.clientName = ClientNameField.Text;

            if ( ClientNameField.Text != "" )
            { 
                UpdateChatWindow( "You updated your nickname. Hello " + client.clientName + "!", "left", Color.Green, Color.White );
                ConnectButton.Enabled = true;
                nicknameEntered = true;
            }
            else
            {
                UpdateChatWindow( "Please enter an appropriate nickname!", "left", Color.Red, Color.White );
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
                SubmitButton.Enabled = true;
                ConnectButton.Text = "Disconnect";
                UpdateChatWindow( "You have connected to the server!", "left", Color.Blue, Color.White );
                client.TcpSendMessage( new ClientListPacket( ClientNameField.Text, false ) );
            }
            else if ( connected )
            {
                connected = false;
                disconnected = true;
                InputField.ReadOnly = true;
                SubmitButton.Enabled = false;
                ConnectButton.Text = "Connect";
                UpdateChatWindow( "You have disconnected from the server!", "left", Color.Red, Color.White );
                client.TcpSendMessage( new ClientListPacket( ClientNameField.Text, true ) );
            }

            if ( disconnected && !nicknameEntered )
                UpdateChatWindow( "Please enter a nickname to connect before trying to connect to the server!", "left", Color.Red, Color.White );
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
                UpdateChatWindow( "You are now whispering to " + ClientListBox.SelectedItem.ToString() + "...", "left", Color.Orange, Color.White );
            }
        }

        private void GlobalMessage_Click( object sender, EventArgs e )
        {
            privateMessage = false;
            UpdateChatWindow( "You are now messaging everyone on the server...", "left", Color.Orange, Color.White );
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
                UpdateChatWindow( "You have unmuted " + clientToMute, "left", Color.Blue, Color.White );
            }
            else
            {
                UpdateChatWindow( "You have muted all incoming messages from " + clientToMute, "left", Color.Red, Color.White );
                mutedClients.Add( clientToMute );
            }    
        }
    }
}