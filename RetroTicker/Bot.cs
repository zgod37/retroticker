using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetroTicker {
    class Bot {

        ITickerModel model;

        private String server;
        private int port;
        private String nick;
        private String twitchPass;
        private String channel;

        public StreamWriter writer;
        private StreamReader reader;

        KeepAlive keepAlive;

        //thread-safe switches for connecting and reading chat
        private volatile bool _isRunning;
        private volatile bool _isReading;

        public Bot(ITickerModel model, BotConfig config) {

            this.model = model;

            this.server = config.server;
            this.port = config.port;
            this.nick = config.nick;
            this.twitchPass = config.twitchPass;
            this.channel = config.channel;
        }

        public override string ToString() {
            return "Twitch nick: " + nick + " channel: " + channel;
        }

        public void send(String message) {
            try {
                writer.WriteLine(message);
                writer.Flush();
            } catch (Exception e) {
                Console.WriteLine("SEND Error " + e.Message);
            }
        }

        public void say(String message, String chan) {
            try {
                writer.WriteLine("PRIVMSG " + chan + " :" + message);
                writer.Flush();
            } catch (Exception e) {
                Console.WriteLine("SAY Error " + e.Message);
            }
        }

        public void Run() {
            try {
                Console.WriteLine("Starting bot: " + this);
                TcpClient irc = new TcpClient(server, port);
                NetworkStream stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                send("PASS " + twitchPass);
                send("NICK " + nick);

                keepAlive = new KeepAlive(this);
                send("JOIN " + channel);

            } catch (Exception e) {
                Console.WriteLine("Error occured starting the bot " + e);
                return;
            }

            _isRunning = true;
            model.updateBotStatus();

            //**** MAIN LOOP ****
            //TODO: separate this into diff function for reconnecting
            String line = "";
            while (_isRunning) {
                try {
                    while ((line = reader.ReadLine()) != null) {
                        Console.WriteLine(line);

                        if (line.Contains("PING")) {
                            send("PONG " + line.Substring(5));
                        }

                        if (line.Contains("PRIVMSG")) {

                            if (_isReading) {
                                model.addChatMessage(line);
                            }
                        }

                        //***NOTE*** bot does NOT break out of inner loop
                        //***NOTE*** if_isRunning is set to false by other thread
                        //***NOTE*** so use this as temporary work-around (need to fix later)
                        if (!_isRunning) break;
                    }
                } catch (SocketException socketException) {
                    //if code reaches here, we lost connection to server
                    //TODO: add code to auto-reconnect
                    Console.WriteLine(socketException);
                    break;
                } catch (Exception e) {
                    Console.WriteLine("Error occured: " + e);
                }
            }

            Console.WriteLine("Main loop exited for bot <" + this + ">");
        }

        public bool isRunning() {
            return _isRunning;
        }

        public bool isReading() {
            return _isReading;
        }

        public void startReading() {
            _isReading = true;
            model.updateBotStatus();
        }

        public void stopReading() {
            _isReading = false;
            model.updateBotStatus();
        }

        public void disconnect() {
            _isRunning = false;
            _isReading = false;
            model.updateBotStatus();
        }

    }

    class KeepAlive {

        Bot bot;

        public KeepAlive(Bot bot) {
            this.bot = bot;
            Thread keepAlive = new Thread(new ThreadStart(this.Run));
            keepAlive.Start();
        }

        public void Run() {
            while (bot.isRunning()) {
                bot.send("ping irc.twitch.tv");
                Thread.Sleep(30000);
            }
            Console.WriteLine("KeepAlive loop exited for " + bot);
        }
    }
}
