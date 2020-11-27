using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        public void UpdateChatWindow( string message, Color color, string alignment, Color backColor )
        {
            if ( MessageWindowRich.InvokeRequired )
            {
                Invoke( new Action( () => { UpdateChatWindow( message, color, alignment, backColor ); } ) );
            }
            else
            {
                MessageWindowRich.SelectionStart = MessageWindowRich.TextLength;
                MessageWindowRich.SelectionLength = 0;

                MessageWindowRich.SelectionColor = color;
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

        private void SubmitButton_Click( object sender, EventArgs e )
        {
            string message = InputField.Text;
            if ( message != "" )
            {
                client.TcpSendMessage( new ChatMessagePacket( message ) );
                UpdateChatWindow( "Me: " + InputField.Text, Color.Black, "right", Color.PowderBlue );
                InputField.Clear();
            }
        }

        private void NicknameButton_Click( object sender, EventArgs e )
        {
            client.TcpSendMessage( new NicknamePacket( ClientNameField.Text ) );
            client.clientName = ClientNameField.Text;

            if ( ClientNameField.Text != "" )
            { 
                UpdateChatWindow( "You updated your nickname. Hello " + client.clientName + "!", Color.Green, "left", Color.White );
                nicknameEntered = true;
            }
            else
            {
                UpdateChatWindow( "Please enter an appropriate nickname!", Color.Red, "left", Color.White );
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
                UpdateChatWindow( "You have connected to the server!", Color.Blue, "left", Color.White );
            }
            else if ( connected )
            {
                connected = false;
                disconnected = true;
                InputField.ReadOnly = true;
                SubmitButton.Enabled = false;
                ConnectButton.Text = "Connect";
                UpdateChatWindow( "You have disconnected from the server!", Color.Red, "left", Color.White );
            }

            if ( disconnected && !nicknameEntered )
                UpdateChatWindow( "Please enter a nickname to connect before trying to connect to the server!", Color.Red, "left", Color.White );
        }
    }
}