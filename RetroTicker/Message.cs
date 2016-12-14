using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public class Message {

        public String fullText { get; set; }
        public String visibleText { get; set; }
        public String user { get; set; }
        public bool isScrolling { get; set; }
        public Color color { get; set; }

        //determines how the message is displayed on ticker
        public int displayType { get; set; }

        //***** NOTE *****
        //depending on display type, get the appropriate instruction set
        //check MessageFactory methods for descriptions of each set
        //****************
        public List<String[]> rowInstructions { get; set; }
        public String[] columnInstructions { get; set; }
        public List<int[]> shuffledCoordinates { get; set; }

        public Message() { }
    }
}
