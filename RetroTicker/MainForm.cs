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
    public partial class MainForm : Form, IBotObserver {

        ITickerController controller;
        ITickerModel model;

        CredentialsForm credentialsForm;
        TickerForm tickerForm;

        public MainForm() {
            InitializeComponent();
            this.model = new TickerModel();
            this.controller = new TickerController(model, this);
            credentialsForm = new CredentialsForm(model, controller);
            tickerForm = new TickerForm();

            model.registerBotObserver(this);
        }

        public void enableStartReadingButton() {
            startReadingButton.Enabled = true;
        }

        public void disableStartReadingButton() {
            startReadingButton.Enabled = false;
        }

        public void enableStopReadingButton() {
            stopReadingButton.Enabled = true;
        }

        public void disableStopReadingButton() {
            stopReadingButton.Enabled = false;
        }

        private void connectButton_Click(object sender, EventArgs e) {
            controller.connectBot();
        }
        
        public void setBotStatus(String status) {
            botStatusLabel.Text = status;
        }

        public void setStatusConnected() {
            botStatusLabel.Text = "Connected";
        }

        public void setStatusReading() {
            botStatusLabel.Text = "READING";
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e) {
            controller.showCredentialsForm();
        }

        public void showCredentialsForm() {
            credentialsForm.Show();
        }

        public void setCredentials() {
            credentialsForm.fillCredentialForm();
        }

        public void showTickerForm() {
            tickerForm.Show();
        }

        public void displayMessageTest(Message message) {
            tickerForm.displayMessageTest(message);
        }

        public void displayScrollingMessage(Message message) {
            tickerForm.displayScrollingMessage(message);
        }

        public void displayShortMessage(Message message) {
            tickerForm.displayShortMessage(message);
        }

        public void wipePanels(Message message) {
            tickerForm.wipePanels(message);
        }

        private void openTickerButton_Click(object sender, EventArgs e) {
            controller.showTickerForm();
        }

        private void testTickerButton_Click(object sender, EventArgs e) {
            controller.testTicker();
        }

        private void startReadingButton_Click(object sender, EventArgs e) {
            controller.startReading();
        }

        private void stopReadingButton_Click(object sender, EventArgs e) {
            controller.stopReading();
        }

        private void disconnectButton_Click(object sender, EventArgs e) {
            controller.disconnectBot();
        }

        private void MainForm_Load(object sender, EventArgs e) {

        }
    }
}
