using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private Client client;
        private bool connected = false;
        private bool disconnected = true;
        private bool nicknameEntered = false;

        public ClientForm( Client client )
        {
            InitializeComponent();
            this.client = client;
            InputField.ReadOnly = true;
            SubmitButton.Enabled = false;
            ConnectButton.Enabled = false;
        }

        public void UpdateChatWindow( string message, string alignment, Color foreColor, Color backColor )
        {
            if ( MessageWindowRich.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateChatWindow( message, alignment, foreColor, backColor); } ) );
            }
            else
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
            if ( ClientListBox.InvokeRequired )
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
                        {
                            if ( FriendsListBox.Items[i].ToString() == message )
                            {
                                friendExists = true;
                            }
                        }
                        if ( !friendExists )
                        {
                            FriendsListBox.Items.Add( message );
                        }
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
                client.TcpSendMessage( new ChatMessagePacket( message ) );
                //client.UdpSendMessage( new ChatMessagePacket( message ) );
                UpdateChatWindow( "Me: " + InputField.Text, "right", Color.Black, Color.PowderBlue );
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
                {
                    if ( ClientListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                        UpdateFriendList( ClientListBox.SelectedItem.ToString(), false );
                }
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
                {
                    if ( FriendsListBox.SelectedItem.ToString() != ClientNameField.Text.ToString() )
                        UpdateFriendList( FriendsListBox.SelectedItem.ToString(), true );
                }
            }
            catch ( NullReferenceException exception )
            {
                Console.WriteLine( "ERROR:: ", exception.Message );
            }
        }
    }
}