using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public class ConfigHandler {

        //bot info
        public String server = "irc.twitch.tv";
        public int port = 6667;
        public String nick { get; set; }
        public String twitchPass { get; set; }
        public String channel { get; set; }

        //ticker info
        public int matrixWidth { get; set; }
        public int matrixHeight { get; set; }
        public int panelWidth { get; set; }
        public int panelHeight { get; set; }
        public int maxVisibleCharacters { get; set; }

        public ConfigHandler() {

        }

        public String getFilePath() {
            String baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDi‌​rectory, "..\\..\\"));
            return baseDir + "data\\config.bin";
        }



    }
}
