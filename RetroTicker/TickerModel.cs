using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetroTicker {
    class TickerModel : ITickerModel {

        Bot bot;
        BotConfig config;

        MessageFactory messageFactory;
        List<Message> messageBin = new List<Message>();

        List<IMessageObserver> messageObservers = new List<IMessageObserver>();
        List<IBotObserver> botObservers = new List<IBotObserver>();

        public TickerModel() {
            Alphabet alphabet = new Alphabet();
            messageFactory = new MessageFactory(alphabet);
            config = new BotConfig();
        }

        public bool connect() {
            //starts bot and connects to server

            if (credentialsExist()) {
                config.load();
                bot = new Bot(this, config);
                Thread botConnection = new Thread(new ThreadStart(bot.Run));
                botConnection.Start();
                return true;
            } else {
                //alert user creds aren't set
                //open set creds window
                Console.WriteLine("Bot credentials are NOT set!");
                return false;
            }
        }

        public void Run() {
            //uses thread to read messages from bin
            while (bot.isReading()) {
                checkMessageBinForDisplay();
            }
            Console.WriteLine("Bot stopped reading.");
        }

        private void checkMessageBinForDisplay() {
            
            int messageCount = messageBin.Count;
            Console.WriteLine("number of messages in bin = " + messageCount);
            List<Message> copyOfMessageBin = new List<Message>(messageBin);
            messageBin.Clear();
            notifyMessageObservers(copyOfMessageBin);
        }

        public void addChatMessage(String rawText) {
            messageBin.Add(messageFactory.createChatMessage(rawText));
        }

        public List<Message> createTestMessages() {
            //for debugging

            List<Message> messages = new List<Message>();
            for (int i = 0; i < 2; i++) messages.Add(messageFactory.createMessage("MSDS Short message" + i));
            return messages;
        }

        public void registerMessageObserver(IMessageObserver observer) {
            messageObservers.Add(observer);
        }

        private void notifyMessageObservers(List<Message> messages) {
            foreach (IMessageObserver observer in messageObservers) {
                observer.displayMessages(messages);
            }
        }

        public void registerBotObserver(IBotObserver observer) {
            botObservers.Add(observer);
        }

        private void notifyBotObservers() {
            foreach (IBotObserver observer in botObservers) {
                if (bot.isReading()) {
                    observer.setBotStatus("Reading");
                } else if (bot.isRunning()) {
                    observer.setBotStatus("Connected");
                } else {
                    observer.setBotStatus("Disconnected");
                }
            }
        }

        public void startReading() {
            bot.startReading();
            Thread messageReader = new Thread(new ThreadStart(this.Run));
            messageReader.Start();
        }

        public void stopReading() {
            bot.stopReading();
        }

        public void disconnect() {
            bot.disconnect();
        }

        public void updateBotStatus() {
            notifyBotObservers();
        }

        public BotConfig getConfig() {
            return config;
        }

        public void switchChannel() {
            throw new NotImplementedException();
        }

        public void setCredentials(String server, int port, String nick, String twitchPass, String channel) {
            config.server = server;
            config.port = port;
            config.nick = nick;
            config.twitchPass = twitchPass;
            config.channel = channel;
            config.save();
        }

        public bool credentialsExist() {
            String filePath = BotConfig.getFilePath();
            Console.WriteLine("Checking for config file at path: " + filePath);
            return File.Exists(filePath);
        }
    }
}
