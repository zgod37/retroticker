using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public interface ITickerModel {

        bool connect();
        void switchChannel();
        void startReading();
        void stopReading();
        void updateBotStatus();
        void disconnect();
        void addChatMessage(String rawText);
        List<Message> createTestMessages();
        BotConfig getConfig();
        void setCredentials(String server, int port, String nick, String twitchPass, String channel);
        bool credentialsExist();
        void registerBotObserver(IBotObserver observer);
        void registerMessageObserver(IMessageObserver observer);
    }
}
