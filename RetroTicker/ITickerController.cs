using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public interface ITickerController {

        void connectBot();
        void disconnectBot();
        void showTickerForm();
        void startReading();
        void stopReading();
        void testTicker();
        void showCredentialsForm();
        void setCredentials(String server, int port, String nick, String twitchPass, String channel);
    }
}
