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
    public partial class TickerForm : Form {

        //retro ticker is comprised of matrix of panels
        //TODO: move to dimension data storage file and retrieve
        List<List<Panel>> panels;
        int panelWidth = 4;
        int panelHeight = 4;
        int matrixWidth = 820;
        int matrixHeight = 40;

        //character display properties
        //calculated based on matrix/panel dimensions
        int upperCharacterRow;
        int lowerCharacterRow;
        int maxVisibleCharacters;
        Color backColor { get; set; }

        public TickerForm() {
            InitializeComponent();
            this.Size = new Size(matrixWidth + (panelWidth * 2), matrixHeight + (panelHeight * 4) - 1);
            //this.FormBorderStyle = FormBorderStyle.None;
            panels = new List<List<Panel>>();
            backColor = Color.FromArgb(40, 40, 40);
            createPanelMatrix();
            wipePanelsInstantly();
        }

        private void createPanelMatrix() {

            //create panel matrix
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

            //set boundaries
            upperCharacterRow = 1;
            lowerCharacterRow = rowCount - 2;
            maxVisibleCharacters = (matrixWidth / panelWidth) / (Alphabet.CHARACTER_WIDTH + 1);

            Console.WriteLine("maxVisibleCharacters = " + maxVisibleCharacters);
        }

        public void displayShortMessage(Message message) {
            switch ((DisplayType)message.displayType) {
                case DisplayType.Random:
                    displayMessageRandomly(message);
                    break;
                case DisplayType.ScrollDown:
                    scrollMessageDown(message);
                    break;
                case DisplayType.ScrollUp:
                    scrollMessageUp(message);
                    break;
                case DisplayType.Horizontal:
                    layerMessageHorizontally(message);
                    break;
                case DisplayType.Vertical:
                    layerMessageVertically(message);
                    break;
                case DisplayType.Raining:
                    displayMessageRaining(message);
                    break;
                default:
                    scrollMessageUp(message);
                    break;
            }

        }

        public void wipePanels(Message message) {
            switch ((DisplayType)message.displayType) {
                case DisplayType.Random:
                    wipePanelsRandomly(message);
                    break;
                case DisplayType.Horizontal:
                    wipePanelsHorizontally();
                    break;
                case DisplayType.Vertical:
                    wipePanelsVertically();
                    break;
                case DisplayType.ScrollDown:
                    wipeMessageDown(message);
                    break;
                case DisplayType.ScrollUp:
                    wipeMessageUp(message);
                    break;
                case DisplayType.Raining:
                    wipeMessageRaining(message);
                    break;
                default:
                    wipePanelsInstantly();
                    break;
            }
        }

        public void displayMessageTest(Message message) {
            //scrollMessageUp(message);
            //wipeMessageUp(message);
            //sleep(1);
            //wipePanelsInstantly();
            displayMessageRaining(message);
            sleep(100);
            wipeMessageRaining(message);
        }

        public void displayScrollingMessage(Message message) {
            scrollMessageLeft(message);
        }

        private void displayMessageRandomly(Message message) {
            List<int[]> shuffledCoordinates = message.shuffledCoordinates;
            for (int i = 0; i < shuffledCoordinates.Count; i++) {
                int[] coordinates = shuffledCoordinates[i];
                colorSinglePanel(coordinates[0], coordinates[1], message.color);
                sleep(3);
            }
        }

        private void layerMessageHorizontally(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int row = upperCharacterRow; row <= lowerCharacterRow; row++) {
                for (int charIndex = 0; charIndex < rowInstructions.Count; charIndex++) {
                    if (charIndex >= maxVisibleCharacters) break;
                    String instruction = rowInstructions[charIndex][row-1];
                    colorRowFromInstructionAnimated(instruction, row, leftCol + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                }
            }
        }

        private void layerMessageVertically(Message message) {
            int panelsColCount = panels[0].Count;
            int col = 1;
            foreach (String instruction in message.columnInstructions) {
                if (col >= panelsColCount) break;
                colorColumnFromInstructionAnimated(instruction, upperCharacterRow, col, message.color);
                col++;
            }
        }

        private void displayMessageRaining(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int row = lowerCharacterRow; row >= upperCharacterRow; row--) {
                for (int charIndex=0; charIndex < rowInstructions.Count; charIndex++) {
                    if (charIndex >= maxVisibleCharacters) break;
                    String instruction = rowInstructions[charIndex][row-1];
                    for (int instructionIndex=0; instructionIndex<instruction.Length; instructionIndex++) {
                        int col = (int) Char.GetNumericValue(instruction[instructionIndex]);
                        colorSinglePanelAnimatedDescending(row, leftCol + col + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }
            }
        }

        private void scrollMessageUp(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int topRow = lowerCharacterRow; topRow >= upperCharacterRow; topRow--) {
                for (int instructionIndex = 0; instructionIndex <= (Alphabet.CHARACTER_HEIGHT - topRow); instructionIndex++) {
                    for (int charIndex = 0; charIndex < rowInstructions.Count; charIndex++) {
                        if (charIndex >= maxVisibleCharacters) break;
                        String instruction = rowInstructions[charIndex][instructionIndex];
                        colorRowFromInstruction(instruction, topRow + instructionIndex, leftCol + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }

                sleep(175);
            }

        }

        private void scrollMessageDown(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int bottomRow = upperCharacterRow; bottomRow <= lowerCharacterRow; bottomRow++) {
                for (int instructionIndex = (Alphabet.CHARACTER_HEIGHT - bottomRow); instructionIndex < Alphabet.CHARACTER_HEIGHT; instructionIndex++) {
                    for (int charIndex = 0; charIndex < rowInstructions.Count; charIndex++) {
                        if (charIndex >= maxVisibleCharacters) break;
                        String instruction = rowInstructions[charIndex][instructionIndex];
                        colorRowFromInstruction(instruction, instructionIndex + (bottomRow - (Alphabet.CHARACTER_HEIGHT - 1)), leftCol + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }
                sleep(175);
            }
        }

        private void scrollMessageLeft(Message message) {
            //scrolls a message left starting at left edge

            String[] columnInstructions = message.columnInstructions;
            int panelsColumnCount = panels[0].Count;
            int remainingInstructionCount = columnInstructions.Length - panelsColumnCount;
            for (int offset = 1; offset < remainingInstructionCount; offset++) {
                for (int col = 0; col < panelsColumnCount; col++) {
                    String instruction = columnInstructions[col + offset];
                    colorColumnFromInstruction(instruction, upperCharacterRow, col, message.color);
                }
                if (offset % 60 == 0) sleep(500);
            }
        }

        private void scrollMessageLeftFromOffscreen(Message message) {
            //scrolls a message left starting from offscreen stopping at left edge

            String[] columnInstructions = message.columnInstructions;
            int panelsColumnCount = panels[0].Count;
            for (int col = panelsColumnCount - 1; col > 0; col--) {
                for (int instructionIndex=0; instructionIndex < panelsColumnCount - col; instructionIndex++) {
                    if (instructionIndex >= columnInstructions.Length) break;
                    colorColumnFromInstruction(columnInstructions[instructionIndex], upperCharacterRow, col + instructionIndex, message.color);
                }
                if (col % 60 == 0) sleep(500);
            }
        }

        private void wipePanelsInstantly() {
            foreach (List<Panel> row in panels) {
                foreach (Panel panel in row) {
                    panel.BackColor = backColor;
                }
            }
        }

        private void wipeMessageDown(Message message) {
            //**** NOTE: currently incorrect algo -> doubles the first row of instructions as it scrolls down

            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int topRow = upperCharacterRow; topRow <= lowerCharacterRow+1; topRow++) {
                for (int instructionIndex = 0; instructionIndex < Alphabet.CHARACTER_HEIGHT-topRow; instructionIndex++) {
                    for (int charIndex = 0; charIndex < rowInstructions.Count; charIndex++) {
                        String instruction = rowInstructions[charIndex][instructionIndex];
                        int row = instructionIndex + topRow + 1;
                        colorRowFromInstruction(instruction, row, leftCol + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }
                wipeSingleRow(topRow - 1);
                sleep(175);
            }
        }

        private void wipeMessageUp(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int bottomRow = lowerCharacterRow; bottomRow >= upperCharacterRow-1; bottomRow--) {
                for (int instructionIndex = (Alphabet.CHARACTER_HEIGHT - bottomRow); instructionIndex < Alphabet.CHARACTER_HEIGHT; instructionIndex++) {
                    for (int charIndex = 0; charIndex < rowInstructions.Count; charIndex++) {
                        String instruction = rowInstructions[charIndex][instructionIndex];
                        int row = instructionIndex + (bottomRow - (Alphabet.CHARACTER_HEIGHT - 1));
                        colorRowFromInstruction(instruction, row, leftCol + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }
                wipeSingleRow(bottomRow + 1);
                sleep(175);
            }
        }

        private void wipeMessageRaining(Message message) {
            int leftCol = 1;
            List<String[]> rowInstructions = message.rowInstructions;
            for (int bottomRow = lowerCharacterRow; bottomRow >= upperCharacterRow; bottomRow--) {
                for (int charIndex=0; charIndex<rowInstructions.Count; charIndex++) {
                    String instruction = rowInstructions[charIndex][bottomRow-1];
                    for (int instructionIndex = 0; instructionIndex < instruction.Length; instructionIndex++) {
                        int col = (int)Char.GetNumericValue(instruction[instructionIndex]);
                        wipeSinglePanelDescending(bottomRow, leftCol + col + (charIndex * (Alphabet.CHARACTER_WIDTH + 1)), message.color);
                    }
                }
            }
        }

        private void wipePanelsHorizontally() {
            foreach (List<Panel> row in panels) {
                foreach (Panel panel in row) {
                    panel.BackColor = backColor;
                    sleep(1);
                }
            }
        }

        private void wipePanelsVertically() {
            for (int col = 0; col < panels[0].Count; col++) {
                for (int row = 0; row < panels.Count; row++) {
                    panels[row][col].BackColor = backColor;
                    sleep(1);
                }
            }
        }

        private void wipePanelsRandomly(Message message) {
            List<int[]> coordinateList = message.shuffledCoordinates;
            for (int i = 0; i < coordinateList.Count; i++) {
                int[] coordinates = coordinateList[i];
                colorSinglePanel(coordinates[0], coordinates[1], backColor);
                sleep(2);
            }
        }

        private void wipeSingleRow(int row) {
            for (int col=0; col<panels[0].Count; col++) {
                panels[row][col].BackColor = backColor;
            }
        }

        private void wipeSinglePanelDescending(int row, int col, Color color) {
            for (int i=row+1; i<panels.Count; i++) {
                panels[i][col].BackColor = color;
                panels[i - 1][col].BackColor = backColor;
                sleep(2);
            }
            panels[panels.Count-1][col].BackColor = backColor;
        }

        private void colorSinglePanel(int row, int col, Color color) {
            panels[row][col].BackColor = color;
        }

        private void colorSinglePanelAnimatedDescending(int row, int col, Color color) {
            for (int i=0; i<=row; i++) {
                panels[i][col].BackColor = color;
                if (i>0) {
                    panels[i - 1][col].BackColor = backColor;
                }
                sleep(2);
            }
        }

        private void colorRowFromInstruction(String instruction, int row, int leftCol, Color color) {
            //color row based on given instruction string
            //if instruction == "04" then color panel[row][0] and panel[row][4]

            for (int i = 0; i <= Alphabet.CHARACTER_WIDTH; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[row][leftCol + i].BackColor = color;
                } else {
                    panels[row][leftCol + i].BackColor = backColor;
                }
            }

        }

        private void colorRowFromInstructionAnimated(String instruction, int row, int leftCol, Color color) {
            for (int i = 0; i <= Alphabet.CHARACTER_WIDTH; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[row][leftCol + i].BackColor = color;
                } else {
                    panels[row][leftCol + i].BackColor = backColor;
                }
                sleep(2);
            }

        }

        private void colorColumnFromInstruction(String instruction, int topRow, int col, Color color) {
            for (int i = 0; i <= Alphabet.CHARACTER_HEIGHT; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[topRow + i][col].BackColor = color;
                } else {
                    panels[topRow + i][col].BackColor = backColor;
                }
            }
        }

        private void colorColumnFromInstructionAnimated(String instruction, int topRow, int col, Color color) {
            for (int i = 0; i <= Alphabet.CHARACTER_HEIGHT; i++) {
                if (instruction.Contains(i.ToString())) {
                    panels[topRow + i][col].BackColor = color;
                } else {
                    panels[topRow + i][col].BackColor = backColor;
                }
                sleep(2);
            }
        }

        private void sleep(int millis) {
            Thread.Sleep(millis);
            Application.DoEvents();
        }

    }
}
