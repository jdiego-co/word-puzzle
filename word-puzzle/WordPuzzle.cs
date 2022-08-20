using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_puzzle
{
    public class WordPuzzle
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private char[][] wordPuzzle;

        public WordPuzzle(string rawWordPuzzle)
        {
            List<char[]> wordPuzzle = new List<char[]>();
            foreach(var line in rawWordPuzzle.Split(Environment.NewLine))
            {
                var wordPuzzleRow = new List<char>());
                foreach (var character in line.ToCharArray())
                {
                    wordPuzzleRow.Add(character);
                }
                wordPuzzle.Add(wordPuzzleRow.ToArray());
            }

            this.wordPuzzle = wordPuzzle.ToArray();
            initParameters();
        }

        private void initParameters()
        {
            Rows = wordPuzzle.GetUpperBound(1);
            Columns = wordPuzzle.GetUpperBound(2);
        }
    }
}
