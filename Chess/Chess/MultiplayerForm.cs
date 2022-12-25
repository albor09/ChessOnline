using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class MultiplayerForm : Form
    {
        public ChessClient client;
        public MultiplayerForm()
        {
            InitializeComponent();
        }

        private async void connectToServer_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected)
                return;
            client = new ChessClient();
            if (await client.Connect(serverAdress.Text))
            {
                SetConnectionStatus(true);
                RefreshLobbyList();
            }
            else
            {
                SetConnectionStatus(false);
            }
        }

        public void SetConnectionStatus(bool connected)
        {
            if (connected)
            {
                connectionStatus.Text = "CONNECTED";
                connectionStatus.ForeColor = Color.Green;
                createLobbyBtn.Enabled = true;
                createLobbyInp.Enabled = true;
            }
            else
            {
                createLobbyBtn.Enabled = false;
                createLobbyInp.Enabled = false;
                connectionStatus.Text = "OFFLINE";
                connectionStatus.ForeColor = Color.Red;
            }
        }

        private void createLobbyBtn_Click(object sender, EventArgs e)
        {
            client.CreateLobby(createLobbyInp.Text);
            this.DialogResult = DialogResult.OK;
        }

        private async void RefreshLobbyList()
        {
            lobbyList.Items.Clear();
            List<string> res = await client.GetLobbies();
            lobbyList.Items.AddRange(res.ToArray());
        }

        private async void refreshLobList_Click(object sender, EventArgs e)
        {
            RefreshLobbyList();
        }

        private void lobbyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectToLobby.Enabled = true;
        }

        private void connectToLobby_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            client.ConnectLobby(lobbyList.Items[lobbyList.SelectedIndex].ToString());
        }
    }
}
