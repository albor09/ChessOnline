
namespace Chess
{
    partial class MultiplayerForm
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
            this.serverAdress = new System.Windows.Forms.TextBox();
            this.lobbyList = new System.Windows.Forms.ListBox();
            this.connectToServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createLobbyInp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.createLobbyBtn = new System.Windows.Forms.Button();
            this.refreshLobList = new System.Windows.Forms.Button();
            this.connectToLobby = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverAdress
            // 
            this.serverAdress.Location = new System.Drawing.Point(12, 38);
            this.serverAdress.Name = "serverAdress";
            this.serverAdress.Size = new System.Drawing.Size(195, 20);
            this.serverAdress.TabIndex = 0;
            // 
            // lobbyList
            // 
            this.lobbyList.FormattingEnabled = true;
            this.lobbyList.Location = new System.Drawing.Point(213, 12);
            this.lobbyList.Name = "lobbyList";
            this.lobbyList.Size = new System.Drawing.Size(324, 277);
            this.lobbyList.TabIndex = 1;
            this.lobbyList.SelectedIndexChanged += new System.EventHandler(this.lobbyList_SelectedIndexChanged);
            // 
            // connectToServer
            // 
            this.connectToServer.Location = new System.Drawing.Point(12, 64);
            this.connectToServer.Name = "connectToServer";
            this.connectToServer.Size = new System.Drawing.Size(195, 23);
            this.connectToServer.TabIndex = 2;
            this.connectToServer.Text = "connect";
            this.connectToServer.UseVisualStyleBackColor = true;
            this.connectToServer.Click += new System.EventHandler(this.connectToServer_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server IP adress";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectionStatus
            // 
            this.connectionStatus.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStatus.ForeColor = System.Drawing.Color.Red;
            this.connectionStatus.Location = new System.Drawing.Point(12, 177);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(195, 23);
            this.connectionStatus.TabIndex = 5;
            this.connectionStatus.Text = "OFFLINE";
            this.connectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 48);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server connection status";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createLobbyInp
            // 
            this.createLobbyInp.Enabled = false;
            this.createLobbyInp.Location = new System.Drawing.Point(213, 361);
            this.createLobbyInp.Name = "createLobbyInp";
            this.createLobbyInp.Size = new System.Drawing.Size(324, 20);
            this.createLobbyInp.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Create lobby";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createLobbyBtn
            // 
            this.createLobbyBtn.Enabled = false;
            this.createLobbyBtn.Location = new System.Drawing.Point(213, 388);
            this.createLobbyBtn.Name = "createLobbyBtn";
            this.createLobbyBtn.Size = new System.Drawing.Size(324, 23);
            this.createLobbyBtn.TabIndex = 9;
            this.createLobbyBtn.Text = "Create";
            this.createLobbyBtn.UseVisualStyleBackColor = true;
            this.createLobbyBtn.Click += new System.EventHandler(this.createLobbyBtn_Click);
            // 
            // refreshLobList
            // 
            this.refreshLobList.Location = new System.Drawing.Point(462, 295);
            this.refreshLobList.Name = "refreshLobList";
            this.refreshLobList.Size = new System.Drawing.Size(75, 23);
            this.refreshLobList.TabIndex = 10;
            this.refreshLobList.Text = "Refresh";
            this.refreshLobList.UseVisualStyleBackColor = true;
            this.refreshLobList.Click += new System.EventHandler(this.refreshLobList_Click);
            // 
            // connectToLobby
            // 
            this.connectToLobby.Enabled = false;
            this.connectToLobby.Location = new System.Drawing.Point(213, 295);
            this.connectToLobby.Name = "connectToLobby";
            this.connectToLobby.Size = new System.Drawing.Size(109, 23);
            this.connectToLobby.TabIndex = 11;
            this.connectToLobby.Text = "Connect to lobby";
            this.connectToLobby.UseVisualStyleBackColor = true;
            this.connectToLobby.Click += new System.EventHandler(this.connectToLobby_Click);
            // 
            // MultiplayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 450);
            this.Controls.Add(this.connectToLobby);
            this.Controls.Add(this.refreshLobList);
            this.Controls.Add(this.createLobbyBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createLobbyInp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectToServer);
            this.Controls.Add(this.lobbyList);
            this.Controls.Add(this.serverAdress);
            this.Name = "MultiplayerForm";
            this.Text = "MultiplayerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverAdress;
        private System.Windows.Forms.ListBox lobbyList;
        private System.Windows.Forms.Button connectToServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label connectionStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox createLobbyInp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button createLobbyBtn;
        private System.Windows.Forms.Button refreshLobList;
        private System.Windows.Forms.Button connectToLobby;
    }
}