using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroTicker {
    public partial class MainForm : Form {

        private static String versionString = "Retro Ticker v1.1 by zgod";

        //panel matrix that makes up the ticker
        List<List<Panel>> panels = new List<List<Panel>>();

        //matrix size
        int matrixWidth = 1025;
        int matrixHeight = 100;

        //panel size
        int panelWidth = 5;
        int panelHeight = 10;

        //character boundaries
        int upperCharacterRow = -1;
        int lowerCharacterRow = -1;

        //characters are fixed width and height
        const int CHARACTER_WIDTH = 5;
        const int CHARACTER_HEIGHT = 8;

        //dict containing row by row instructions for each letter in alphabet\
        //used for scrolling messages up and down
        Dictionary<Char, String[]> alphabetRows = new Dictionary<Char, String[]>();

        //dict containing column by column instruction for each letter in alphabet
        //used for scrolling messages left to right
        Dictionary<char, string[]> alphabetColumns = new Dictionary<char, string[]>();

        //irc bot stuff
        Bot bot;
        bool isReadingChat = false;
        static List<String> twitchMessageBin = new List<String>();
        static List<String> genericMessageBin = new List<String>();

        //colors
        Color[] colors = new Color[] { Color.BlueViolet, Color.DarkCyan, Color.DeepPink, Color.Yellow, Color.MediumVioletRed, Color.DeepSkyBlue, Color.DarkOrange };
        Color defaultBackColor = Color.FromArgb(40, 40, 40);

        public MainForm() {
            InitializeComponent();
            initializeAlphabetRows();
            createColumnInstructionSet();
            createPanelMatrix();
            colorizePanelsDefault();
            addGenericMessages();
            this.KeyPreview = true;
        }

        private void showControlsToolStripMenuItem_Click(object sender, EventArgs e) {
            //show controls
        }

        private void initializeAlphabetRows() {
            //each character will contain the row-by-row instructions
            //each row contains 0-5 numbers of 0, 1, 2, 3, or 4
            //the number correspondes to whether or not the square is colored

            //uppercase
            alphabetRows['A'] = new string[] { "123", "04", "04", "01234", "04", "04", "04", "04" };
            alphabetRows['B'] = new string[] { "0123", "04", "04", "0123", "04", "04", "04", "0123" };
            alphabetRows['C'] = new string[] { "1234", "0", "0", "0", "0", "0", "0", "1234" };
            alphabetRows['D'] = new string[] { "012", "03", "04", "04", "04", "04", "03", "012" };
            alphabetRows['E'] = new string[] { "01234", "0", "0", "0123", "0", "0", "0", "01234" };
            alphabetRows['F'] = new string[] { "01234", "0", "0", "0123", "0", "0", "0", "0" };
            alphabetRows['G'] = new string[] { "1234", "0", "0", "0", "0234", "04", "04", "123" };
            alphabetRows['H'] = new string[] { "04", "04", "04", "01234", "04", "04", "04", "04" };
            alphabetRows['I'] = new string[] { "01234", "2", "2", "2", "2", "2", "2", "01234" };
            alphabetRows['J'] = new string[] { "4", "4", "4", "4", "04", "04", "04", "123" };
            alphabetRows['K'] = new string[] { "04", "03", "02", "01", "01", "02", "03", "04" };
            alphabetRows['L'] = new string[] { "0", "0", "0", "0", "0", "0", "0", "01234" };
            alphabetRows['M'] = new string[] { "04", "0134", "0134", "024", "024", "04", "04", "04" };
            alphabetRows['N'] = new string[] { "04", "014", "014", "024", "024", "034", "034", "04" };
            alphabetRows['O'] = new string[] { "123", "04", "04", "04", "04", "04", "04", "123" };
            alphabetRows['P'] = new string[] { "0123", "04", "04", "04", "0123", "0", "0", "0" };
            alphabetRows['Q'] = new string[] { "123", "04", "04", "04", "04", "024", "123", "4" };
            alphabetRows['R'] = new string[] { "0123", "04", "04", "0123", "01", "02", "03", "04" };
            alphabetRows['S'] = new string[] { "1234", "0", "0", "123", "4", "4", "4", "0123" };
            alphabetRows['T'] = new string[] { "01234", "2", "2", "2", "2", "2", "2", "2" };
            alphabetRows['U'] = new string[] { "04", "04", "04", "04", "04", "04", "04", "123" };
            alphabetRows['V'] = new string[] { "04", "04", "04", "04", "13", "13", "13", "2" };
            alphabetRows['W'] = new string[] { "04", "04", "04", "04", "024", "024", "024", "13" };
            alphabetRows['X'] = new string[] { "04", "13", "13", "2", "2", "13", "13", "04" };
            alphabetRows['Y'] = new string[] { "04", "04", "13", "13", "2", "2", "2", "2" };
            alphabetRows['Z'] = new string[] { "01234", "4", "3", "2", "2", "1", "0", "01234" };

            //lower case
            alphabetRows['a'] = new string[] { "", "", "123", "4", "1234", "04", "04", "123" };
            alphabetRows['b'] = new string[] { "0", "0", "0", "0123", "04", "04", "04", "0123" };
            alphabetRows['c'] = new string[] { "", "", "", "123", "0", "0", "0", "123" };
            alphabetRows['d'] = new string[] { "4", "4", "4", "1234", "04", "04", "04", "1234" };
            alphabetRows['e'] = new string[] { "", "", "", "123", "04", "0123", "0", "123" };
            alphabetRows['f'] = new string[] { "34", "2", "2", "2", "01234", "2", "2", "2" };
            alphabetRows['g'] = new string[] { "", "", "123", "04", "1234", "4", "04", "123" };
            alphabetRows['h'] = new string[] { "0", "0", "0", "0", "0123", "04", "04", "04" };
            alphabetRows['i'] = new string[] { "", "", "2", "", "12", "2", "2", "01234" };
            alphabetRows['j'] = new string[] { "", "4", "", "4", "4", "4", "04", "123" };
            alphabetRows['k'] = new string[] { "0", "04", "03", "02", "01", "01", "02", "03" };
            alphabetRows['l'] = new string[] { " ", "12", "2", "2", "2", "2", "2", "01234" };
            alphabetRows['m'] = new string[] { "", "", "0", "013", "024", "024", "024", "024" };
            alphabetRows['n'] = new string[] { "", "", "", "0", "023", "014", "04", "04" };
            alphabetRows['o'] = new string[] { "", "", "", "123", "04", "04", "04", "123" };
            alphabetRows['p'] = new string[] { "", "", "123", "04", "04", "0123", "0", "0" };
            alphabetRows['q'] = new string[] { "", "", "123", "04", "04", "1234", "4", "4" };
            alphabetRows['r'] = new string[] { "", "", "", "023", "014", "0", "0", "0" };
            alphabetRows['s'] = new string[] { "", "", "", "123", "0", "123", "4", "123" };
            alphabetRows['t'] = new string[] { "", "2", "2", "01234", "2", "2", "2", "2" };
            alphabetRows['u'] = new string[] { "", "", "", "04", "04", "04", "04", "123" };
            alphabetRows['v'] = new string[] { "", "", "", "04", "04", "13", "13", "2" };
            alphabetRows['w'] = new string[] { "", "", "", "024", "024", "024", "024", "13" };
            alphabetRows['x'] = new string[] { "", "", "", "04", "13", "2", "13", "04" };
            alphabetRows['y'] = new string[] { "", "", "04", "04", "1234", "4", "4", "123" };
            alphabetRows['z'] = new string[] { "", "", "", "01234", "3", "2", "1", "01234" };

            //numerals
            alphabetRows['1'] = new string[] { "2", "12", "02", "2", "2", "2", "2", "01234" };
            alphabetRows['2'] = new string[] { "123", "04", "4", "3", "2", "1", "0", "01234" };
            alphabetRows['3'] = new string[] { "123", "04", "4", "23", "4", "4", "04", "123" };
            alphabetRows['4'] = new string[] { "3", "23", "13", "01234", "3", "3", "3", "3" };
            alphabetRows['5'] = new string[] { "01234", "0", "0", "0123", "4", "4", "4", "0123" };
            alphabetRows['6'] = new string[] { "123", "04", "0", "0123", "04", "04", "04", "123" };
            alphabetRows['7'] = new string[] { "01234", "04", "04", "4", "3", "2", "1", "0" };
            alphabetRows['8'] = new string[] { "123", "04", "04", "123", "04", "04", "04", "123" };
            alphabetRows['9'] = new string[] { "123", "04", "04", "1234", "4", "4", "4", "123" };
            alphabetRows['0'] = new string[] { "123", "034", "034", "024", "024", "014", "014", "123" };


            //symbols
            alphabetRows[' '] = new string[] { "", "", "", "", "", "", "", "" };
            alphabetRows['-'] = new string[] { "", "", "", "01234", "", "", "", "" };
            alphabetRows['_'] = new string[] { "", "", "", "", "", "", "", "01234" };
            alphabetRows['.'] = new string[] { "", "", "", "", "", "", "12", "12" };
            alphabetRows['?'] = new string[] { "123", "04", "04", "3", "2", "2", "", "2" };
            alphabetRows['!'] = new string[] { "23", "23", "23", "23", "23", "23", "", "23" };
            alphabetRows['@'] = new string[] { "123", "04", "04", "01234", "0134", "01234", "0", "123" };
            alphabetRows['#'] = new string[] { "13", "13", "01234", "13", "13", "01234", "13", "13" };
            alphabetRows['$'] = new string[] { "2", "1234", "02", "123", "24", "24", "0123", "2" };
            alphabetRows['%'] = new string[] { "014", "013", "3", "2", "2", "1", "134", "034" };
            alphabetRows['^'] = new string[] { "2", "13", "13", "04", "04", "", "", "" };
            alphabetRows['&'] = new string[] { "1", "02", "03", "02", "1", "024", "03", "124" };
            alphabetRows['*'] = new string[] { "2", "024", "01234", "123", "123", "01234", "024", "2" };
            alphabetRows['('] = new string[] { "4", "3", "3", "2", "2", "3", "3", "4" };
            alphabetRows[')'] = new string[] { "0", "1", "1", "2", "2", "1", "1", "0" };
            alphabetRows['['] = new string[] { "234", "2", "2", "2", "2", "2", "2", "234" };
            alphabetRows[']'] = new string[] { "012", "2", "2", "2", "2", "2", "2", "012" };
            alphabetRows['<'] = new string[] { "", "3", "2", "1", "0", "1", "2", "3" };
            alphabetRows['>'] = new string[] { "", "1", "2", "3", "4", "3", "2", "1" };
            alphabetRows['+'] = new string[] { "", "", "2", "2", "01234", "2", "2", "" };
            alphabetRows['='] = new string[] { "", "", "", "01234", "", "01234", "", "" };
            alphabetRows[':'] = new string[] { "", "12", "12", "", "", "12", "12", "" };
            alphabetRows['/'] = new string[] { "4", "3", "3", "2", "2", "1", "1", "0" };
            alphabetRows['~'] = new string[] { "", "", "124", "023", "", "", "", "" };
            alphabetRows[';'] = new string[] { "", "12", "12", "", "", "1", "1", "0" };
            alphabetRows[','] = new string[] { "", "", "", "", "", "1", "1", "0" };
            alphabetRows['"'] = new string[] { "0134", "0134", "0134", "", "", "", "", "" };
            alphabetRows['\''] = new string[] { "12", "12", "12", "", "", "", "", "" };
            alphabetRows['♪'] = new string[] { "1", "12", "13", "13", "13", "1", "01", "01" };
            alphabetRows['♫'] = new string[] { "1234", "14", "1234", "14", "14", "14", "0134", "0134" };

        }

        private void createColumnInstructionSet() {
            //create column instructions for each char based on their row instructions
            //each char correspondes to a column-by-column instruction set of 0-8 numbers 1-7
            //if the number appears in the string, it means that square gets colored

            foreach (Char ch in alphabetRows.Keys) {

                //hard code instructions based on character width
                string[] columnInstructions = new string[CHARACTER_WIDTH] { "", "", "", "", "" };
                string[] rowInstructions = alphabetRows[ch];

                //Console.WriteLine("ROWS for" + ch + " = " + String.Join(" ", rowInstructions));


                for (int col = 0; col < CHARACTER_WIDTH; col++) {
                    String columnInstruction = "";

                    for (int i = 0; i < rowInstructions.Length; i++) {
                        if (rowInstructions[i].Contains(col.ToString())) {
                            columnInstruction += i.ToString();
                        }
                    }
                    columnInstructions[col] = columnInstruction;
                }

                alphabetColumns[ch] = columnInstructions;
            }

            /*
            //display column alphabet
            foreach (char ch in alphabetColumns.Keys) {
                Console.WriteLine(ch + " = " + String.Join(" ", alphabetColumns[ch]));
            }
            */
        }

        private void createPanelMatrix() {

            //set form size
            this.Size = new Size(matrixWidth + (panelWidth * 2), matrixHeight + (panelHeight * 3));

            int rowCount = matrixHeight / panelHeight;
            int colCount = matrixWidth / panelWidth;

            for (int i = 0; i < rowCount; i++) {

                List<Panel> row = new List<Panel>();

                for (int j = 0; j < colCount; j++) {

                    Panel panel = new Panel();
                    panel.Size = new Size(panelWidth, panelHeight);
                    panel.Location = new Point(j * panelWidth, i * panelHeight);

                    this.Controls.Add(panel);
                    row.Add(panel);
                }

                panels.Add(row);
            }

            upperCharacterRow = 1;
            lowerCharacterRow = rowCount - 2;

        }

        private void colorizePanelsDefault() {
            foreach (List<Panel> row in panels) {
                foreach (Panel panel in row) {
                    panel.BackColor = defaultBackColor;
                }
            }
        }

        public static void addTwitchMessage(String user, String message) {
            String userMessage = "<" + user + "> " + message;
            twitchMessageBin.Add(userMessage);
        }

        private void randomizeColors() {

            Random random = new Random();
            foreach (List<Panel> row in panels) {
                foreach (Panel panel in row) {
                    panel.BackColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                    sleep(1);
                }
            }
        }

        private void wipePanelsHorizontally() {
            foreach (List<Panel> row in panels) {
                foreach (Panel panel in row) {
                    panel.BackColor = defaultBackColor;
                    sleep(1);
                }
            }
        }

        private void wipePanelsVertically() {
            for (int col = 0; col < panels[0].Count; col++) {
                for (int row = 0; row < panels.Count; row++) {
                    panels[row][col].BackColor = defaultBackColor;
                    sleep(1);
                }
            }
        }

        private void wipePanelsRandomly() {

            //flatten 2D list of panels into 1D list of coordinates
            List<int[]> coordinateList = new List<int[]>();
            for (int row = 0; row < panels.Count; row++) {
                for (int col = 0; col < panels[0].Count; col++) {
                    coordinateList.Add(new int[] { row, col });
                }
            }

            //shuffle 1D list in place
            shuffleList(coordinateList);

            for (int i = 0; i < coordinateList.Count; i++) {
                int[] coordinates = coordinateList[i];
                colorSinglePanel(coordinates[0], coordinates[1], defaultBackColor);
                if (i % 2 == 0) {
                    sleep(1);
                }
            }
        }

        private void addGenericMessages() {
            genericMessageBin.Add("Ghosts n' Goblins (NES) Firstplay - no hints pls :)");
            genericMessageBin.Add("Don't forget to *** that follow boys");
        }

        public void checkTwitchMessageBinForDisplay() {
            //*******************************************
            //MAIN LOOP for checking twitch chat messages
            //loops until command given by user, continuously checks the amount of messages in bin
            //depending on how many messages are queued up, calls diff methods accordingly

            isReadingChat = true;
            while (isReadingChat) {

                int messageCount = twitchMessageBin.Count;
                Console.WriteLine("Number of twitch messages in bin = " + messageCount);

                if (messageCount > 10) {
                    displayTwitchMessagesQuickly(messageCount, 3);
                } else if (messageCount > 5) {
                    displayTwitchMessagesQuickly(messageCount, 2);
                } else if (messageCount > 0) {
                    displayTwitchMessages();
                } else {
                    sleep(3000);
                }

                //if there aren't any messages after loop, wait extra 3 sec
                if (twitchMessageBin.Count == 0) {
                    sleep(3000);
                }
            }
        }

        private void displayTwitchMessages() {

            //copy over message bin to prevent new messages from entering during display
            List<String> messageBin = getCopyOfMessageBin();

            //clear the message bin to prevent repeated messages
            //AND to allow new messages to come in during display
            clearMessageBin();

            foreach (String message in messageBin) {
                prepareMessageForDisplay(message);
            }

        }

        private void displayTwitchMessagesQuickly(int messageCount, int step) {
            //method is called when there too many messages to display
            //displays one message for every x messages (x = step)

            //copy over message bin to prevent new messages from entering during display
            List<String> messageBin = getCopyOfMessageBin();

            //clear the message bin to prevent repeated messages
            //AND to allow new messages to come in during display
            clearMessageBin();

            //only get 1 message for each 3 messages in 'semi-random' way
            //grabs one message from 0-2, one from 3-5, etc.
            //this ensures that the message are still displayed chronologically
            int displayMessageCount = messageCount / step;
            int[] displayMessageIndices = new int[displayMessageCount];
            Random rng = new Random();
            int i = 0;
            for (int stepIndex = 0; stepIndex < messageCount - step; stepIndex += step) {
                displayMessageIndices[i] = stepIndex + rng.Next(step);
                i++;
            }

            //prepare message for display
            //*** NOTE: need to account for full message bin here
            foreach (int index in displayMessageIndices) {
                prepareMessageForDisplay(messageBin[index]);
            }

        }

        private void prepareMessageForDisplay(String message) {
            //method that decides how each message is displayed
            //if message is short, display  

            //first remove any un-charted characters
            String strippedMessage = stripMessage(message);
            int messageLength = strippedMessage.Length;

            Color color = getRandomColor();
            if (messageLength < 35) {
                int wipeCode = selectShortMessageDisplayMethod(strippedMessage, color);
                sleep(2000);
                selectWipeMethod(wipeCode);
            } else {
                //pad out message for scrolling
                strippedMessage += "                              ";
                selectLongMessageDisplayMethod(strippedMessage, color);
            }
        }

        private int selectShortMessageDisplayMethod(String strippedMessage, Color color) {
            //randomly selects a method to display a short message
            //returns back a wipe code depending on which message was chosen
            //returns 0 for horizontal wipe; 1 for vertical wipe; 2 for random
            //NOTE: message could be truncated long message, in which case wipe code is ignored

            Random random = new Random();
            int randomNum = random.Next(100);
            if (randomNum < 2) {
                scrollMessageDownFlipped(strippedMessage, color, defaultBackColor);
                sleep(1000);
                scrollMessageUp(strippedMessage, color);
                return 0;
            } else if (randomNum >= 2 && randomNum < 34) {
                if (randomNum < 18) {
                    scrollMessageUp(strippedMessage, color);
                    return 0;
                } else {
                    scrollMessageDown(strippedMessage, color, defaultBackColor);
                    return 1;
                }
            } else if (randomNum >= 34 && randomNum < 66) {
                if (randomNum < 50) {
                    displayShortMessageLayeredHorizontally(strippedMessage, color, defaultBackColor);
                    return 0;
                } else {
                    displayShortMessageLayeredVertically(strippedMessage, color, defaultBackColor);
                    return 1;
                }
            } else {
                displayShortMessageRandomly(strippedMessage, color);
                return 2;
            }
        }

        private void selectLongMessageDisplayMethod(String strippedMessage, Color color) {

            selectShortMessageDisplayMethod(strippedMessage.Substring(0, 34), color);
            scrollLongMessageLeftWrappingAroundStartingLeftJustified(strippedMessage, color, defaultBackColor);

        }

        private void selectWipeMethod(int wipeCode) {
            if (wipeCode == 0) {
                wipePanelsHorizontally();
            } else if (wipeCode == 1) {
                wipePanelsVertically();
            } else {
                wipePanelsRandomly();
            }
        }

        private void displayShortMessageLayeredHorizontally(String message, Color color, Color backColor) {

            int leftCol = 1;
            for (int row = upperCharacterRow; row <= lowerCharacterRow; row++) {
                for (int i = 0; i < message.Length; i++) {
                    Char ch = message[i];
                    String instruction = alphabetRows[ch][row - 1];
                    colorRowFromInstructionAnimated(instruction, row, leftCol + (i * 6), color, backColor);
                }

            }

        }

        private void displayShortMessageLayeredHorizontallyAlternating(String message, Color color1, Color color2, Color backColor) {

            int leftCol = 1;
            for (int row = upperCharacterRow; row <= lowerCharacterRow; row++) {
                for (int i = 0; i < message.Length; i++) {
                    Char ch = message[i];
                    String instruction = alphabetRows[ch][row - 1];
                    if (i % 2 == 0) {
                        colorRowFromInstructionAnimated(instruction, row, leftCol + (i * 6), color1, backColor);
                    } else {
                        colorRowFromInstructionAnimated(instruction, row, leftCol + (i * 6), color2, backColor);
                    }
                }

            }

        }

        private void displayShortMessageLayeredVertically(String message, Color color, Color backColor) {
            int panelsColCount = panels[0].Count;
            int messageLength = message.Length;
            int instructionCount = messageLength * CHARACTER_WIDTH + (messageLength * 2);

            String[] columnInstructions = getColumnInstructions(message, messageLength, instructionCount);

            int col = 1;
            foreach (String instruction in columnInstructions) {
                if (col >= panelsColCount) break;

                colorColumnFromInstructionAnimated(instruction, upperCharacterRow, col, color, backColor);
                col++;
            }
        }

        private void displayShortMessageRandomly(String message, Color color) {

            //first get a list of all coordinates to be colored from message
            List<int[]> coordinateList = getCoordinateList(message);

            //perform in-place shuffle of list
            shuffleList(coordinateList);

            //now color each panel one by one
            for (int i = 0; i < coordinateList.Count; i++) {
                int[] coordinates = coordinateList[i];
                colorSinglePanel(coordinates[0], coordinates[1], color);
                sleep(3);
            }

        }

        private void scrollLongMessageUpAndLeftWrappingAround(String message, Color color, Color backColor) {

            scrollMessageUp(message.Substring(0, 34), Color.DarkSeaGreen);
            scrollLongMessageLeftWrappingAroundStartingLeftJustified(message, color, backColor);
        }

        private void scrollShortMessageLeftWrappingAround(String message, Color color, Color backColor) {
            //only works correctly for "short messages" 
            //i.e. messages that can fit entirely in the form window
            //otherwise, the message will overlap itself

            int colCount = panels[0].Count;
            int messageLength = message.Length;

            //start initial left column based on length of message
            //i.e. message will start right justified
            // **** NOTE = need to come back and adjust for super long messages
            int leftCol = 1;

            //move left edge column to the left
            for (int a = 0; a < 300; a++) {

                //starting at top row (1) and moving down to bottom row (8)
                for (int row = upperCharacterRow; row <= lowerCharacterRow; row++) {

                    //color row from instructions for each character for each row
                    for (int i = 0; i < messageLength; i++) {
                        Char ch = message[i];
                        String instruction = alphabetRows[ch][row - 1];
                        colorRowFromInstruction(instruction, row, leftCol + (i * 6), color, backColor);
                    }
                }

                //because characters are scrolling left, far right column
                //after each character needs to be set back to background color
                for (int i = 0; i < messageLength; i++) {
                    colorColumn(leftCol + (((i + 1) * 6) - 1), backColor);
                }

                //call sleep method on thread so animation is visualized
                sleep(50);

                leftCol--;

                //to ensure proper wrapping, reset value at -(colCount)
                if (leftCol <= -colCount) {
                    leftCol = 1;
                }
            }

        }

        private void scrollLongMessageLeftFromOffScreen(String message, Color color, Color backColor) {
            //this method for long messages that wont fit entirely on shown panel

            int panelsColCount = panels[0].Count;
            int messageLength = message.Length;
            int instructionCount = messageLength * CHARACTER_WIDTH + (messageLength * 2);

            //get list of column instructions
            string[] columnInstructions = getColumnInstructions(message, messageLength, instructionCount);

            //STEP ONE:
            //start at rightmost column and scroll message to the far left column
            for (int col = panelsColCount - 1; col >= 0; col--) {

                //loop thru instruction set based on which column we're on
                for (int i = 0; i < panelsColCount - col; i++) {
                    String instruction = columnInstructions[i];
                    colorColumnFromInstruction(instruction, upperCharacterRow, col + i, color, backColor);
                }

                sleep(15);
            }

            //STEP TWO:
            //scroll rest of message on screen while pushing beginning of message off screen
            scrollLongMessageLeftWrappingAroundStartingLeftJustified(message, color, backColor);
        }

        private void scrollLongMessageLeftWrappingAroundStartingLeftJustified(String message, Color color, Color backColor) {
            //scrolls a message left, wrapping around panel
            //assumes start of message is already left-justified

            int panelsColCount = panels[0].Count;
            int messageLength = message.Length;
            int instructionCount = messageLength * CHARACTER_WIDTH + (messageLength * 2);

            int remainingInstructionCount = instructionCount - panelsColCount;
            String[] columnInstructions = getColumnInstructions(message, messageLength, instructionCount);
            for (int offset = 1; offset < remainingInstructionCount; offset++) {
                for (int col = 0; col < panels[0].Count; col++) {
                    String instruction = columnInstructions[col + offset];
                    colorColumnFromInstruction(instruction, upperCharacterRow, col, color, backColor);
                }
                if (offset % 60 == 0) sleep(500);
            }
        }

        private void scrollMessageDownFlipped(String message, Color color, Color backColor) {

            int leftCol = 1;

            for (int bottomRow = upperCharacterRow; bottomRow <= lowerCharacterRow; bottomRow++) {
                for (int instructionIndex = CHARACTER_HEIGHT - 1; instructionIndex >= (CHARACTER_HEIGHT - bottomRow); instructionIndex--) {
                    for (int charIndex = 0; charIndex < message.Length; charIndex++) {
                        Char ch = message[charIndex];
                        String instruction = alphabetRows[ch][instructionIndex];
                        colorRowFromInstruction(instruction, bottomRow, leftCol + (charIndex * (CHARACTER_WIDTH + 1)), color, backColor);
                    }
                }
                sleep(175);
            }

        }

        private void scrollMessageDown(String message, Color color, Color backColor) {

            int leftCol = 1;

            for (int bottomRow = upperCharacterRow; bottomRow <= lowerCharacterRow; bottomRow++) {
                for (int instructionIndex = (CHARACTER_HEIGHT - bottomRow); instructionIndex < CHARACTER_HEIGHT; instructionIndex++) {
                    for (int charIndex = 0; charIndex < message.Length; charIndex++) {
                        Char ch = message[charIndex];
                        String instruction = alphabetRows[ch][instructionIndex];
                        colorRowFromInstruction(instruction, instructionIndex + (bottomRow - (CHARACTER_HEIGHT - 1)), leftCol + (charIndex * (CHARACTER_WIDTH + 1)), color, backColor);
                    }
                }
                sleep(175);
            }

        }

        private void scrollMessageUp(String message, Color color) {

            //left-justify message
            int leftCol = 1;

            //start top row at lower character boundary
            for (int topRow = lowerCharacterRow; topRow >= upperCharacterRow; topRow--) {

                //get character instructions based on which row is top
                //e.g. if top row is 7, we need instructions 0 through 1 (aka the 2 top rows of each char)
                //so loop from 0 to (CHARACTER_HEIGHT-topRow)
                for (int instructionIndex = 0; instructionIndex <= (CHARACTER_HEIGHT - topRow); instructionIndex++) {
                    for (int charIndex = 0; charIndex < message.Length; charIndex++) {
                        Char ch = message[charIndex];
                        String instruction = alphabetRows[ch][instructionIndex];
                        colorRowFromInstruction(instruction, topRow + instructionIndex, leftCol + (charIndex * (CHARACTER_WIDTH + 1)), color, defaultBackColor);
                    }
                }

                sleep(175);
            }

        }

        private String stripMessage(String message) {
            //remove characters from message that are not charted

            String strippedMessage = "";
            foreach (Char ch in message) {
                if (alphabetRows.ContainsKey(ch)) {
                    strippedMessage += ch;
                }
            }
            return strippedMessage;
        }

        private string[] getColumnInstructions(String message, int messageLength, int instructionCount) {

            string[] columnInstructions = new String[instructionCount];
            int instructionIndex = 0;
            for (int charIndex = 0; charIndex < messageLength; charIndex++) {
                Char ch = message[charIndex];
                foreach (String instruction in alphabetColumns[ch]) {
                    columnInstructions[instructionIndex] = instruction;
                    instructionIndex++;
                }

                //add one column space between each char
                columnInstructions[instructionIndex] = "";
                instructionIndex++;
            }

            //pad out rest of insructions with blank columns
            while (instructionIndex < instructionCount) {
                columnInstructions[instructionIndex] = "";
                instructionIndex++;
            }

            return columnInstructions;
        }

        private List<int[]> getCoordinateList(String message) {
            //returns a list of coordinates [row,col] for given message
            //iterates through the row instructions to produces list of int[2] coordinates

            //***** CURRENTLY INCORRECT CODE: doesn't left-justify message - not sure why???????????

            List<int[]> coordinateList = new List<int[]>();
            for (int charIndex = 0; charIndex < message.Length; charIndex++) {
                Char ch = message[charIndex];
                for (int row = 0; row < CHARACTER_HEIGHT; row++) {
                    String rowInstructions = alphabetRows[ch][row];
                    for (int i = 0; i < rowInstructions.Length; i++) {
                        int col = (int)Char.GetNumericValue(rowInstructions[i]);
                        int[] coordinates = new int[] { row + 1, (col + 1) + ((CHARACTER_WIDTH + 1) * charIndex) };
                        coordinateList.Add(coordinates);
                    }
                }
            }
            return coordinateList;
        }

        private List<String> getCopyOfMessageBin() {
            return new List<String>(twitchMessageBin);
        }

        private void clearMessageBin() {
            twitchMessageBin.Clear();
        }

        public void stopReadingChat() {
            isReadingChat = false;
        }

        private void shuffleList<T>(List<T> list) {
            //in place shuffle of list
            //uses fischer-yates algorithm

            Random rng = new Random();
            for (int i = list.Count; i > 1; i--) {
                int randomIndex = rng.Next(i);
                T temp = list[randomIndex];
                list[randomIndex] = list[i - 1];
                list[i - 1] = temp;
            }
        }

        private Color getRandomColor() {
            Random rng = new Random();
            return colors[rng.Next(colors.Length)];
        }

        private void sleep(int waitMillis) {
            Thread.Sleep(waitMillis);
            Application.DoEvents();
        }

        private void colorSinglePanel(int row, int col, Color color) {
            panels[row][col].BackColor = color;
        }

        private void colorColumn(int col, Color backColor) {
            for (int i = 0; i < panels.Count; i++) {
                panels[i][wrapValue(col)].BackColor = backColor;
            }
        }

        private int wrapValue(int col) {
            int colCount = panels[0].Count;
            return col < 0 ? colCount - (-col % colCount) : col % colCount;
        }

        private void colorRowFromInstruction(String instruction, int row, int leftCol, Color color, Color backColor) {
            //color row based on given instruction string
            //if instruction == "04" then color panel[row][0] and panel[row][4]

            for (int i = 0; i <= CHARACTER_WIDTH; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[row][leftCol + i].BackColor = color;
                } else {
                    panels[row][leftCol + i].BackColor = backColor;
                }
            }

        }

        private void colorRowFromInstructionAnimated(String instruction, int row, int leftCol, Color color, Color backColor) {

            for (int i = 0; i <= CHARACTER_WIDTH; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[row][wrapValue(leftCol + i)].BackColor = color;
                } else {
                    panels[row][wrapValue(leftCol + i)].BackColor = backColor;
                }
                sleep(2);
            }

        }

        private void colorColumnFromInstruction(String instruction, int topRow, int col, Color color, Color backColor) {
            //color column based on given instruction string

            for (int i = 0; i <= CHARACTER_HEIGHT; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[topRow + i][col].BackColor = color;
                } else {
                    panels[topRow + i][col].BackColor = backColor;
                }
            }
        }

        private void colorColumnFromInstructionAnimated(String instruction, int topRow, int col, Color color, Color backColor) {
            //color column based on given instruction string

            for (int i = 0; i <= CHARACTER_HEIGHT; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[topRow + i][col].BackColor = color;
                } else {
                    panels[topRow + i][col].BackColor = backColor;
                }
                sleep(2);
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e) {
            //controls

            switch (e.KeyChar) {

                //debug commands
                case 't':
                    prepareMessageForDisplay(versionString);
                    break;

                //bot commands
                case 'c':
                    bot = new Bot();
                    Thread botConnection = new Thread(new ThreadStart(bot.Run));
                    botConnection.Start();
                    break;
                case 'r':
                    isReadingChat = true;
                    bot.setReadingStatus(true);
                    checkTwitchMessageBinForDisplay();
                    break;
                case 'y':
                    Console.WriteLine("Number of twitch messages = " + twitchMessageBin.Count);
                    break;
                case '9':
                    stopReadingChat();
                    break;

                default:
                    break;
            }

        }
    }
}
