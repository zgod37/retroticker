using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    class Alphabet {

        public const int CHARACTER_WIDTH = 5;
        public const int CHARACTER_HEIGHT = 8;

        private Dictionary<Char, String[]> alphabetRowInstructions = new Dictionary<Char, String[]>();
        private Dictionary<Char, String[]> alphabetColumnInstructions = new Dictionary<Char, String[]>();

        public Alphabet() {
            createRowInstructionsForAlphabet();
            createColumnInstructionsForAlphabet();
        }

        private void createRowInstructionsForAlphabet() {
            // each character will contain the row - by - row instructions
            //each row contains 0-5 numbers of 0, 1, 2, 3, or 4
            //the number correspondes to whether or not the square is colored

            //uppercase
            alphabetRowInstructions['A'] = new string[] { "123", "04", "04", "01234", "04", "04", "04", "04" };
            alphabetRowInstructions['B'] = new string[] { "0123", "04", "04", "0123", "04", "04", "04", "0123" };
            alphabetRowInstructions['C'] = new string[] { "1234", "0", "0", "0", "0", "0", "0", "1234" };
            alphabetRowInstructions['D'] = new string[] { "012", "03", "04", "04", "04", "04", "03", "012" };
            alphabetRowInstructions['E'] = new string[] { "01234", "0", "0", "0123", "0", "0", "0", "01234" };
            alphabetRowInstructions['F'] = new string[] { "01234", "0", "0", "0123", "0", "0", "0", "0" };
            alphabetRowInstructions['G'] = new string[] { "1234", "0", "0", "0", "0234", "04", "04", "123" };
            alphabetRowInstructions['H'] = new string[] { "04", "04", "04", "01234", "04", "04", "04", "04" };
            alphabetRowInstructions['I'] = new string[] { "01234", "2", "2", "2", "2", "2", "2", "01234" };
            alphabetRowInstructions['J'] = new string[] { "4", "4", "4", "4", "04", "04", "04", "123" };
            alphabetRowInstructions['K'] = new string[] { "04", "03", "02", "01", "01", "02", "03", "04" };
            alphabetRowInstructions['L'] = new string[] { "0", "0", "0", "0", "0", "0", "0", "01234" };
            alphabetRowInstructions['M'] = new string[] { "04", "0134", "0134", "024", "024", "04", "04", "04" };
            alphabetRowInstructions['N'] = new string[] { "04", "014", "014", "024", "024", "034", "034", "04" };
            alphabetRowInstructions['O'] = new string[] { "123", "04", "04", "04", "04", "04", "04", "123" };
            alphabetRowInstructions['P'] = new string[] { "0123", "04", "04", "04", "0123", "0", "0", "0" };
            alphabetRowInstructions['Q'] = new string[] { "123", "04", "04", "04", "04", "024", "123", "4" };
            alphabetRowInstructions['R'] = new string[] { "0123", "04", "04", "0123", "01", "02", "03", "04" };
            alphabetRowInstructions['S'] = new string[] { "1234", "0", "0", "123", "4", "4", "4", "0123" };
            alphabetRowInstructions['T'] = new string[] { "01234", "2", "2", "2", "2", "2", "2", "2" };
            alphabetRowInstructions['U'] = new string[] { "04", "04", "04", "04", "04", "04", "04", "123" };
            alphabetRowInstructions['V'] = new string[] { "04", "04", "04", "04", "13", "13", "13", "2" };
            alphabetRowInstructions['W'] = new string[] { "04", "04", "04", "04", "024", "024", "024", "13" };
            alphabetRowInstructions['X'] = new string[] { "04", "13", "13", "2", "2", "13", "13", "04" };
            alphabetRowInstructions['Y'] = new string[] { "04", "04", "13", "13", "2", "2", "2", "2" };
            alphabetRowInstructions['Z'] = new string[] { "01234", "4", "3", "2", "2", "1", "0", "01234" };

            //lower case
            alphabetRowInstructions['a'] = new string[] { "", "", "123", "4", "1234", "04", "04", "123" };
            alphabetRowInstructions['b'] = new string[] { "0", "0", "0", "0123", "04", "04", "04", "0123" };
            alphabetRowInstructions['c'] = new string[] { "", "", "", "123", "0", "0", "0", "123" };
            alphabetRowInstructions['d'] = new string[] { "4", "4", "4", "1234", "04", "04", "04", "1234" };
            alphabetRowInstructions['e'] = new string[] { "", "", "", "123", "04", "0123", "0", "123" };
            alphabetRowInstructions['f'] = new string[] { "34", "2", "2", "2", "01234", "2", "2", "2" };
            alphabetRowInstructions['g'] = new string[] { "", "", "123", "04", "1234", "4", "04", "123" };
            alphabetRowInstructions['h'] = new string[] { "0", "0", "0", "0", "0123", "04", "04", "04" };
            alphabetRowInstructions['i'] = new string[] { "", "", "2", "", "12", "2", "2", "01234" };
            alphabetRowInstructions['j'] = new string[] { "", "4", "", "4", "4", "4", "04", "123" };
            alphabetRowInstructions['k'] = new string[] { "0", "04", "03", "02", "01", "01", "02", "03" };
            alphabetRowInstructions['l'] = new string[] { " ", "12", "2", "2", "2", "2", "2", "01234" };
            alphabetRowInstructions['m'] = new string[] { "", "", "0", "013", "024", "024", "024", "024" };
            alphabetRowInstructions['n'] = new string[] { "", "", "", "0", "023", "014", "04", "04" };
            alphabetRowInstructions['o'] = new string[] { "", "", "", "123", "04", "04", "04", "123" };
            alphabetRowInstructions['p'] = new string[] { "", "", "123", "04", "04", "0123", "0", "0" };
            alphabetRowInstructions['q'] = new string[] { "", "", "123", "04", "04", "1234", "4", "4" };
            alphabetRowInstructions['r'] = new string[] { "", "", "", "023", "014", "0", "0", "0" };
            alphabetRowInstructions['s'] = new string[] { "", "", "", "123", "0", "123", "4", "123" };
            alphabetRowInstructions['t'] = new string[] { "", "2", "2", "01234", "2", "2", "2", "2" };
            alphabetRowInstructions['u'] = new string[] { "", "", "", "04", "04", "04", "04", "123" };
            alphabetRowInstructions['v'] = new string[] { "", "", "", "04", "04", "13", "13", "2" };
            alphabetRowInstructions['w'] = new string[] { "", "", "", "024", "024", "024", "024", "13" };
            alphabetRowInstructions['x'] = new string[] { "", "", "", "04", "13", "2", "13", "04" };
            alphabetRowInstructions['y'] = new string[] { "", "", "04", "04", "1234", "4", "4", "123" };
            alphabetRowInstructions['z'] = new string[] { "", "", "", "01234", "3", "2", "1", "01234" };

            //numerals
            alphabetRowInstructions['1'] = new string[] { "2", "12", "02", "2", "2", "2", "2", "01234" };
            alphabetRowInstructions['2'] = new string[] { "123", "04", "4", "3", "2", "1", "0", "01234" };
            alphabetRowInstructions['3'] = new string[] { "123", "04", "4", "23", "4", "4", "04", "123" };
            alphabetRowInstructions['4'] = new string[] { "3", "23", "13", "01234", "3", "3", "3", "3" };
            alphabetRowInstructions['5'] = new string[] { "01234", "0", "0", "0123", "4", "4", "4", "0123" };
            alphabetRowInstructions['6'] = new string[] { "123", "04", "0", "0123", "04", "04", "04", "123" };
            alphabetRowInstructions['7'] = new string[] { "01234", "04", "04", "4", "3", "2", "1", "0" };
            alphabetRowInstructions['8'] = new string[] { "123", "04", "04", "123", "04", "04", "04", "123" };
            alphabetRowInstructions['9'] = new string[] { "123", "04", "04", "1234", "4", "4", "4", "123" };
            alphabetRowInstructions['0'] = new string[] { "123", "034", "034", "024", "024", "014", "014", "123" };


            //symbols
            alphabetRowInstructions[' '] = new string[] { "", "", "", "", "", "", "", "" };
            alphabetRowInstructions['-'] = new string[] { "", "", "", "01234", "", "", "", "" };
            alphabetRowInstructions['_'] = new string[] { "", "", "", "", "", "", "", "01234" };
            alphabetRowInstructions['.'] = new string[] { "", "", "", "", "", "", "12", "12" };
            alphabetRowInstructions['?'] = new string[] { "123", "04", "04", "3", "2", "2", "", "2" };
            alphabetRowInstructions['!'] = new string[] { "23", "23", "23", "23", "23", "23", "", "23" };
            alphabetRowInstructions['@'] = new string[] { "123", "04", "04", "01234", "0134", "01234", "0", "123" };
            alphabetRowInstructions['#'] = new string[] { "13", "13", "01234", "13", "13", "01234", "13", "13" };
            alphabetRowInstructions['$'] = new string[] { "2", "1234", "02", "123", "24", "24", "0123", "2" };
            alphabetRowInstructions['%'] = new string[] { "014", "013", "3", "2", "2", "1", "134", "034" };
            alphabetRowInstructions['^'] = new string[] { "2", "13", "13", "04", "04", "", "", "" };
            alphabetRowInstructions['&'] = new string[] { "1", "02", "03", "02", "1", "024", "03", "124" };
            alphabetRowInstructions['*'] = new string[] { "2", "024", "01234", "123", "123", "01234", "024", "2" };
            alphabetRowInstructions['('] = new string[] { "4", "3", "3", "2", "2", "3", "3", "4" };
            alphabetRowInstructions[')'] = new string[] { "0", "1", "1", "2", "2", "1", "1", "0" };
            alphabetRowInstructions['['] = new string[] { "234", "2", "2", "2", "2", "2", "2", "234" };
            alphabetRowInstructions[']'] = new string[] { "012", "2", "2", "2", "2", "2", "2", "012" };
            alphabetRowInstructions['<'] = new string[] { "", "3", "2", "1", "0", "1", "2", "3" };
            alphabetRowInstructions['>'] = new string[] { "", "1", "2", "3", "4", "3", "2", "1" };
            alphabetRowInstructions['+'] = new string[] { "", "", "2", "2", "01234", "2", "2", "" };
            alphabetRowInstructions['='] = new string[] { "", "", "", "01234", "", "01234", "", "" };
            alphabetRowInstructions[':'] = new string[] { "", "12", "12", "", "", "12", "12", "" };
            alphabetRowInstructions['/'] = new string[] { "4", "3", "3", "2", "2", "1", "1", "0" };
            alphabetRowInstructions['~'] = new string[] { "", "", "124", "023", "", "", "", "" };
            alphabetRowInstructions[';'] = new string[] { "", "12", "12", "", "", "1", "1", "0" };
            alphabetRowInstructions[','] = new string[] { "", "", "", "", "", "1", "1", "0" };
            alphabetRowInstructions['"'] = new string[] { "0134", "0134", "0134", "", "", "", "", "" };
            alphabetRowInstructions['\''] = new string[] { "12", "12", "12", "", "", "", "", "" };
            alphabetRowInstructions['♪'] = new string[] { "1", "12", "13", "13", "13", "1", "01", "01" };
            alphabetRowInstructions['♫'] = new string[] { "1234", "14", "1234", "14", "14", "14", "0134", "0134" };
        }

        private void createColumnInstructionsForAlphabet() {
            //create column instructions for each char based on their row instructions
            //each char correspondes to a column-by-column instruction set of 0-8 numbers 1-7
            //if the number appears in the string, it means that square gets colored

            foreach (Char ch in alphabetRowInstructions.Keys) {

                //hard code instructions based on character width
                string[] columnInstruction = new string[CHARACTER_WIDTH] { "", "", "", "", "" };
                string[] rowInstruction = alphabetRowInstructions[ch];

                //Console.WriteLine("ROWS for" + ch + " = " + String.Join(" ", rowInstructions));


                for (int col = 0; col < CHARACTER_WIDTH; col++) {
                    String colInstruction = "";

                    for (int i = 0; i < rowInstruction.Length; i++) {
                        if (rowInstruction[i].Contains(col.ToString())) {
                            colInstruction += i.ToString();
                        }
                    }
                    columnInstruction[col] = colInstruction;
                }

                alphabetColumnInstructions[ch] = columnInstruction;
            }
        }

        public bool containsCharacter(Char ch) {
            return alphabetRowInstructions.ContainsKey(ch);
        }

        public List<String[]> getRowInstructionsForMessage(String messageText) {
            //returns list of sets of string instructions for each character
            //**** DIFFERS FROM COLUMN INSTRUCTIONS, USES 2-DIMENSIONAL LIST OF INSTRUCTIONS SETS ****

            List<String[]> rowInstructions = new List<String[]>();
            foreach (Char ch in messageText) {
                rowInstructions.Add(alphabetRowInstructions[ch]);
            }
            return rowInstructions;
        }

        public String[] getColumnInstructionsForMessage(String messageText) {
            //returns array of column-by-column instructions for message
            //**** DIFFERS FROM ROW INSTRUCTIONS, FLATTENS INSTRUCTIONS SETS INTO ON-DIMENSIONAL ARRAY ****
            //extra computation required here due to the spacing between characters

            int messageLength = messageText.Length;
            int instructionCount = messageLength * CHARACTER_WIDTH + (messageLength * 2);
            String[] columnInstructions = new String[instructionCount];

            int instructionIndex = 0;
            foreach (Char ch in messageText) {
                foreach (String instruction in alphabetColumnInstructions[ch]) {
                    columnInstructions[instructionIndex] = instruction;
                    instructionIndex++;
                }
                columnInstructions[instructionIndex] = "";
                instructionIndex++;
            }

            //pad out rest of instructions with blank columns
            while (instructionIndex < instructionCount) {
                columnInstructions[instructionIndex] = "";
                instructionIndex++;
            }

            return columnInstructions;
        }

        public List<int[]> getShuffledCoordinates(String message) {
            //returns a shuffled list of coordinates [row,col] for given message
            //iterates through the row instructions to produces list of int[2] coordinates
            //then calls helper method to shuffle the list in-place

            List<int[]> coordinateList = new List<int[]>();
            for (int charIndex = 0; charIndex < message.Length; charIndex++) {
                Char ch = message[charIndex];
                for (int row = 0; row < CHARACTER_HEIGHT; row++) {
                    String rowInstructions = alphabetRowInstructions[ch][row];
                    for (int i = 0; i < rowInstructions.Length; i++) {
                        int col = (int)Char.GetNumericValue(rowInstructions[i]);
                        int[] coordinates = new int[] { row + 1, (col + 1) + ((CHARACTER_WIDTH + 1) * charIndex) };
                        coordinateList.Add(coordinates);
                    }
                }
            }
            //shuffle list in place
            shuffleList(coordinateList);
            return coordinateList;
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
    }
}
