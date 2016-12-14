using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    [Serializable]
    public class BotConfig {

        public String server { get; set; }
        public int port { get; set; }
        public String nick { get; set; }
        public String twitchPass { get; set; }
        public String channel { get; set; }

        public BotConfig() {

        }

        public override string ToString() {
            return "Config loaded <" + server + " " +
                                port + " " +
                                nick + " " +
                                twitchPass + " " +
                                channel +">";
        }

        public static String getFilePath() {
            String baseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDi‌​rectory);
            return baseDir + "data\\config.bin";
        }

        public void load() {
            Console.WriteLine("Loading config...");
            BotConfig savedConfig;
            using (Stream stream = File.Open(getFilePath(), FileMode.Open)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                savedConfig = (BotConfig)binaryFormatter.Deserialize(stream);
            }
            this.server = savedConfig.server;
            this.port = savedConfig.port;
            this.nick = savedConfig.nick;
            this.twitchPass = savedConfig.twitchPass;
            this.channel = savedConfig.channel;
            Console.WriteLine(this);
        }

        public void save() {
            try {
                using (Stream stream = File.Open(getFilePath(), FileMode.Create)) {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                }
            } catch (Exception e) {
                Console.WriteLine("Received error " + e.Message);
                Console.WriteLine("Be sure that the data folder exists in your project directory");
            }
        }
        
    }
}
