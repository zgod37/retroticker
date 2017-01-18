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

        private volatile bool isReadingFile = false;

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

        private void readMessagesFromChat() {
            //** MESSAGE READING THREAD **
            //reads messages from chat until user clicks the "stop reading" button
               
            while (bot.isReading()) {
                int messageCount = messageBin.Count;
                Console.WriteLine("number of messages in bin = " + messageCount);
                if (messageCount == 0) {
                    Thread.Sleep(3000);
                } else {
                    List<Message> copyOfMessageBin = new List<Message>(messageBin);
                    messageBin.Clear();
                    notifyMessageObservers(copyOfMessageBin);
                }
            }
            Console.WriteLine("Bot stopped reading.");
        }

        private void readMessagesFromFile() {
            //** MESSAGE READING THREAD **
            //reads messages from chat until user clicks the "stop reading" button
            //NOTE: fills messageBin before loop, clears it after

            fillMessageBinFromFile();
            int messageCount = messageBin.Count;
            if (messageCount > 0) {
                while (isReadingFile) {
                    notifyMessageObservers(messageBin);
                }
            } else {
                Console.WriteLine("No messages found in file!");
                isReadingFile = false;
            }
            messageBin.Clear();
            Console.WriteLine("Stopped reading from file.");
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

        private void addMessage(String text) {
            messageBin.Add(messageFactory.createMessage(text));
        }

        private void fillMessageBinFromFile() {
            using (StreamReader file = new StreamReader(getMessageBinFilePath())) {
                String line;
                while ((line = file.ReadLine()) != null) {
                    Console.WriteLine("adding generic message = " + line);
                    messageBin.Add(messageFactory.createMessage(line));
                }
            }
        }

        private List<Message> createDemoMessages() {

            List<Message> messages = new List<Message>();

            Message leo = messageFactory.createMessage("Leonardo");
            leo.displayType = (int)DisplayType.ScrollUp;
            leo.color = System.Drawing.Color.RoyalBlue;
            messages.Add(leo);


            Message raph = messageFactory.createMessage("Raphael");
            raph.displayType = (int)DisplayType.ScrollDown;
            raph.color = System.Drawing.Color.Red;
            messages.Add(raph);


            Message mikey = messageFactory.createMessage("Michelangelo");
            mikey.displayType = (int)DisplayType.Horizontal;
            mikey.color = System.Drawing.Color.Orange;
            messages.Add(mikey);

            Message don = messageFactory.createMessage("Donatello");
            don.displayType = (int)DisplayType.Vertical;
            don.color = System.Drawing.Color.Purple;
            messages.Add(don);

            /*
            Message scrollUpMessage = messageFactory.createMessage("Scroll Up Scroll Up");
            scrollUpMessage.displayType = (int)DisplayType.ScrollUp;
            messages.Add(scrollUpMessage);

            Message scrollDownMessage = messageFactory.createMessage("Scroll Down Scroll Down");
            scrollDownMessage.displayType = (int)DisplayType.ScrollDown;
            messages.Add(scrollDownMessage);

            Message horizontalMessage = messageFactory.createMessage("Horizontal Horizontal Horizontal");
            horizontalMessage.displayType = (int)DisplayType.Horizontal;
            messages.Add(horizontalMessage);

            Message verticalMessage = messageFactory.createMessage("Vertical Vertical Vertical");
            verticalMessage.displayType = (int)DisplayType.Vertical;
            messages.Add(verticalMessage);

            Message rainingMessage = messageFactory.createMessage("Raining Raining Raining");
            rainingMessage.displayType = (int)DisplayType.Raining;
            messages.Add(rainingMessage);

            Message randomMessage = messageFactory.createMessage("Random Random Random");
            randomMessage.displayType = (int)DisplayType.Random;
            messages.Add(randomMessage);
            */
            return messages;
        }

        public List<Message> createTestMessages() {
            //for debugging messages
            List<Message> messages = new List<Message>();
            messageFactory.enableDebugging();
            messages = createDemoMessages();
            //messages.Add(messageFactory.createMessage(messageFactory.getAlphabetAsString()));
            messageFactory.disableDebugging();
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
            //if bot is connected, read chat messages
            //otherwise read from file

            Thread messageReader = null;
            if (bot != null && bot.isRunning()) {
                Console.WriteLine("Bot is running -- reading messages from chat");
                bot.startReading();
                messageReader = new Thread(readMessagesFromChat);
            } else {
                Console.WriteLine("reading messages from file");
                isReadingFile = true;
                messageReader = new Thread(readMessagesFromFile);
            }
            messageReader.Start();
        }

        public void stopReading() {
            if (bot != null && bot.isRunning()) {
                bot.stopReading();
            } else {
                isReadingFile = false;
            }
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

        private String getMessageBinFilePath() {
            String baseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDi‌​rectory);
            return baseDir + "data\\messages.txt";
        }
    }
}
