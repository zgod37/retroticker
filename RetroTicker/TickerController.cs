using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroTicker {
    class TickerController : IMessageObserver, ITickerController {

        ITickerModel model;
        MainForm view;

        public TickerController(ITickerModel model, MainForm view) {
            this.model = model;
            this.view = view;
            model.registerMessageObserver(this);
        }

        public void connectBot() {
            if (model.connect()) {
                view.setStatusConnected();
            } else {
                MessageBox.Show("Error connecting bot!");
            }
        }

        public void disconnectBot() {
            model.disconnect();
        }

        public void startReading() {
            model.startReading();
        }

        public void stopReading() {
            model.stopReading();
        }

        public void showCredentialsForm() {
            if (model.credentialsExist()) {
                Console.WriteLine("config file found!");
                view.setCredentials();
            }
            view.showCredentialsForm();
        }

        public void setCredentials(String server, int port, String nick, String twitchPass, String channel) {
            model.setCredentials(server, port, nick, twitchPass, channel);
        }

        public void showTickerForm() {
            view.showTickerForm();
        }

        public void displayMessages(List<Message> messages) {
            //display each message in the ticker
            //if message is short, wipe ticker with given code
            //otherwise scroll it across the screen


            foreach (Message message in messages) {
                view.displayShortMessage(message);
                if (message.isScrolling) {
                    view.displayScrollingMessage(message);
                } else {
                    Thread.Sleep(500);
                    view.wipePanels(message);
                }
            }

        }

        public void testTicker() {
            //debug
            displayMessages(model.createTestMessages());

        }
    }
}
