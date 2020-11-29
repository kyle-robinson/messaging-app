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

        private void DeleteLine( int a_line )
        {
            int start_index = ClientList.GetFirstCharIndexFromLine( a_line );
            int count = ClientList.Lines[a_line].Length;

            // eat new line chars
            if ( a_line < ClientList.Lines.Length - 1 )
                count += ClientList.GetFirstCharIndexFromLine( a_line + 1 ) -
                    ( ( start_index + count - 1 ) + 1 );

            ClientList.Text = ClientList.Text.Remove( start_index, count );
        }

        public void UpdateClientList( string message, Color foreColor, Color backColor, bool removeText )
        {
            if ( ClientList.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateClientList( message, foreColor, backColor, removeText ); } ) );
            }
            else
            {
                if ( removeText )
                {
                    try
                    {
                        DeleteLine( ClientList.Find( message ) );
                    }
                    catch ( Exception e )
                    {
                        // catch errors removing client name and use different method
                        Console.WriteLine( e.Message );
                        int currentLine = ClientList.Find( message );
                        ClientList.SelectedText = String.Empty;
                    }
                }
                else
                {
                    ClientList.SelectionStart = ClientList.TextLength;
                    ClientList.SelectionLength = 0;

                    ClientList.SelectionColor = foreColor;
                    ClientList.SelectionBackColor = backColor;
                    ClientList.AppendText( message + "\n" );
                    ClientList.SelectionColor = ClientList.ForeColor;

                    ClientList.SelectionStart = ClientList.Text.Length;
                    ClientList.ScrollToCaret();
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
    }
}