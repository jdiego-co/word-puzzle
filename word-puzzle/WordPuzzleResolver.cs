using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_puzzle
{
    public class WordPuzzleResolver
    {
        public WordPuzzle Puzzle { get; private set; }

        public WordPuzzleResolver(string rawWordPuzzle)
        {
            Puzzle = new WordPuzzle();
            List<char[]> wordPuzzle = new List<char[]>();
            foreach(var line in rawWordPuzzle.Split(Environment.NewLine))
            {
                var wordPuzzleRow = new List<char>();
                foreach (var character in line.ToCharArray())
                {
                    wordPuzzleRow.Add(character);
                }
                wordPuzzle.Add(wordPuzzleRow.ToArray());
            }

            Puzzle.Puzzle = wordPuzzle.ToArray();
            Puzzle.Rows = Puzzle.Puzzle.GetLength(0);
            if (Puzzle.Rows > 0)
                Puzzle.Columns = Puzzle.Puzzle[0].Length;
        }

        public void Print()
        {
            for(int x = 0; x < Puzzle.Rows; x++)
            {
                string row = string.Empty;
                for(int y = 0; y < Puzzle.Columns; y++)
                {
                    row += $"[{Puzzle.Puzzle[x][y]}]";
                }
                Console.WriteLine(row);
            }
        }
    }
}
