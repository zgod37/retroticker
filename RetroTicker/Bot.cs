using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    internal class Bot {

        //Bot credentials
        BotConfig config;

        //TODO: put these in JSON file
        public static String server;
        private int port;
        private String nick;
        private String twitchPass;
        private String channel;

        private static bool isRunning = false;
        private bool isReading = false;

        static StreamWriter writer;
        static StreamReader reader;

        KeepAlive keepAlive;
        
        public Bot() {
            config = new BotConfig();
            server = config.getServer();
            port = config.getPort();
            nick = config.getNick();
            twitchPass = config.getTwitchPass();
            channel = config.getChannel();
        }

        public static void send(String message) {
            try {
                writer.WriteLine(message);
                writer.Flush();
            } catch (Exception e) {
                Console.WriteLine("SEND Error " + e.Message);
            }
        }

        public static void say(String message, String chan) {
            try {
                writer.WriteLine("PRIVMSG " + chan + " :" + message);
                writer.Flush();
            } catch (Exception e) {
                Console.WriteLine("SAY Error " + e.Message);
            }
        }

        public void disconnect() {
            Console.WriteLine("Bot disconnecting!");
            isRunning = false;
        }

        public void Run() {

            //connect to server with credentials
            try {

                TcpClient irc = new TcpClient(server, port);
                NetworkStream stream = irc.GetStream();

                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                //log in with credentials
                send("PASS " + twitchPass);
                send("NICK " + nick);

                //start thread to stay connected
                keepAlive = new KeepAlive();

                //join channels and start message listener
                send("JOIN " + channel);

                isRunning = true;

            } catch (Exception e) {
                Console.WriteLine("START Error " + e.Message);
            }

            //**** MAIN LOOP ****
            //process each line as its received from server
            String line = "";
            while (isRunning) {
                try {
                    while ((line = reader.ReadLine()) != null) {

                        Console.WriteLine(line);

                        if (line.Contains("PING")) {
                            send("PONG " + line.Substring(5));
                        }

                        if (line.Contains("PRIVMSG")) {

                            String[] breakup = line.Split(' ');

                            String user = breakup[0].Substring(1, breakup[0].IndexOf('!') - 1);

                            String message = breakup[3].Substring(1) + " ";
                            for (int i = 4; i < breakup.Length; i++) {
                                message += breakup[i];
                                message += " ";
                            }

                            if (isReading == true) {
                                MainForm.addTwitchMessage(user, message);
                            }
                        }
                    }
                } catch (Exception e) {
                    Console.WriteLine("onMessage error " + e);
                }
            }
        }

        public void setReadingStatus(bool status) {
            isReading = status;
        }

        public bool isReadingChat() {
            return isReading;
        }

        public static bool running() {
            return isRunning;
        }

    }

    internal class KeepAlive {

        public KeepAlive() {
            Thread keepAlive = new Thread(new ThreadStart(this.Run));
            keepAlive.Start();
        }

        public void Run() {
            while (Bot.running()) {
                Bot.send("ping " + Bot.server);
                Thread.Sleep(30000);
            }
        }
    }
}
