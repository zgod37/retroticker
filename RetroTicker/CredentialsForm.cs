using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroTicker {
    public partial class CredentialsForm : Form {

        ITickerModel model;
        ITickerController controller;

        public CredentialsForm(ITickerModel model, ITickerController controller) {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
        }

        public void fillCredentialForm() {
            BotConfig config = model.getConfig();
            config.load();
            serverTextBox.Text = config.server;
            portTextBox.Text = config.port.ToString();
            nickTextBox.Text = config.nick;
            oauthTextBox.Text = config.twitchPass;
            channelTextBox.Text = config.channel;
        }

        private void saveButton_Click(object sender, EventArgs e) {
            //TODO: input validation here

            String server = serverTextBox.Text;
            int port = Int32.Parse(portTextBox.Text);
            String nick = nickTextBox.Text;
            String twitchPass = oauthTextBox.Text;
            String channel = channelTextBox.Text;
            controller.setCredentials(server, port, nick, twitchPass, channel);
            this.Hide();
        }
    }
}
