using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    class MessageFactory {

        bool debugging = false;

        Alphabet alphabet;
        Random rng;

        //TODO: currently hardcoded; need to get this from the matrix panels calculation
        int maxVisibleCharacters = 34;

        //TODO: currently picks *all* known colors which is bad b/c u cant see some against the background
        KnownColor[] allColors;

        public MessageFactory(Alphabet alphabet) {
            this.rng = new Random();
            this.alphabet = alphabet;

            Array colorsArray = Enum.GetValues(typeof(KnownColor));
            allColors = new KnownColor[colorsArray.Length];
            Array.Copy(colorsArray, allColors, colorsArray.Length);
        }
        
        public Message createChatMessage(String rawText) {
            //takes rawText from an irc PRIVMSG and creates Message object

            String user = rawText.Substring(1, rawText.IndexOf('!') - 1);
            String message = rawText.Substring(rawText.IndexOf(':', 1));
            return createMessage("<" + user + "> " + message);
        }

        public Message createMessage(String text) {
            //main method for creating messages

            Message message = new Message();

            //remove uncharted characters and choose random display type
            message.fullText = stripText(text);
            message.displayType = rng.Next((int)DisplayType.TypeCount);

            //set properties based on message length
            //pad out scrolling messages with extra spaces
            if (message.fullText.Length < maxVisibleCharacters) {
                message.visibleText = message.fullText;
                message.isScrolling = false;
            } else {
                message.visibleText = message.fullText.Substring(0, maxVisibleCharacters);
                message.isScrolling = true;
                message.fullText += "                                                   ";
                message.columnInstructions = alphabet.getColumnInstructionsForMessage(message.fullText);
            }

            //grab all instruction sets for debugging,
            //otherwise only grab whats needed based on displayCode
            if (debugging) {
                message.shuffledCoordinates = alphabet.getShuffledCoordinates(message.visibleText);
                message.columnInstructions = alphabet.getColumnInstructionsForMessage(message.fullText);
                message.rowInstructions = alphabet.getRowInstructionsForMessage(message.fullText);
            } else {
                if (message.displayType == (int)DisplayType.Random) {
                    message.shuffledCoordinates = alphabet.getShuffledCoordinates(message.visibleText);
                } else if ((message.displayType == (int)DisplayType.Vertical) && !message.isScrolling) {
                    message.columnInstructions = alphabet.getColumnInstructionsForMessage(message.fullText);
                } else {
                    message.rowInstructions = alphabet.getRowInstructionsForMessage(message.fullText);
                }
            }

            //choose color
            message.color = Color.FromKnownColor(allColors[rng.Next(allColors.Length)]);

            return message;
        }

        private String stripText(String rawText) {
            //remove characters from message that are not charted

            String strippedText = "";
            foreach (Char ch in rawText) {
                if (alphabet.containsCharacter(ch)) {
                    strippedText += ch;
                }
            }
            return strippedText;
        }
    }
}
