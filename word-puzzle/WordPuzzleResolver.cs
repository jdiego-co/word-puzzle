using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_puzzle
{
    public class WordPuzzleResolver
    {
        public WordPuzzle WordPuzzle { get; private set; }

        public WordPuzzleResolver(string rawWordPuzzle)
        {
            WordPuzzle = new WordPuzzle();
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

            WordPuzzle.Puzzle = wordPuzzle.ToArray();
            WordPuzzle.Rows = WordPuzzle.Puzzle.GetLength(0);
            if (WordPuzzle.Rows > 0)
                WordPuzzle.Columns = WordPuzzle.Puzzle[0].Length;
        }

        public void Print()
        {
            for(int x = 0; x < WordPuzzle.Rows; x++)
            {
                string row = string.Empty;
                for(int y = 0; y < WordPuzzle.Columns; y++)
                {
                    row += $"[{WordPuzzle.Puzzle[x][y]}]";
                }
                Console.WriteLine(row);
            }
        }

        public void SearchWord(string word)
        {
            WordSearchResult searchResult = new WordSearchResult();
            Console.Clear();
            Print();
            var possibleCoordinates = FindPossibleCoordinates(word);
            foreach(var coordinate in possibleCoordinates)
            {
                searchResult = ConfirmCoordinates(coordinate, word);
                if (searchResult.Found)
                {
                    Console.WriteLine($"Word Found in: { coordinate.X }, { coordinate.Y} to " +
                        $"{searchResult.EndPosition.X}, {searchResult.EndPosition.Y}");
                    break;
                }
            }

            if(!searchResult.Found)
                Console.WriteLine($"Word not found ");
        }

        private List<Coordinate> FindPossibleCoordinates(string word)
        {
            var coordinates = new List<Coordinate>();
            char firstCharacter = word[0];
            for(int x = 0; x < WordPuzzle.Rows; x++)
            {
                for(int y = 0; y < WordPuzzle.Columns; y++)
                {
                    if (WordPuzzle.Puzzle[x][y].Equals(firstCharacter))
                        coordinates.Add(new Coordinate { X = x + 1, Y = y + 1 });
                }
            }

            return coordinates;
        }
        private WordSearchResult ConfirmCoordinates(Coordinate coordinates, string word)
        {
            WordSearchResult searchResult = FindWord(coordinates, new Coordinate { X = 1, Y = 0 }, word);
            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = -1, Y = 0 }, word);

            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = 0, Y = -1 }, word);
            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = 0, Y = 1 }, word);

            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = 1, Y = 1 }, word);
            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = 1, Y = -1 }, word);

            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = -1, Y = 1 }, word);
            if (!searchResult.Found)
                searchResult = FindWord(coordinates, new Coordinate { X = -1, Y = -1 }, word);

            return searchResult;
        }
        private WordSearchResult FindWord(Coordinate startCoordinates, Coordinate displacementCoordinates, string word)
        {
            bool isValid = true;
            int totalCharacters = word.Length;
            int currentCharacter = 0;
            int x, y;
            for (x = startCoordinates.X - 1, y = startCoordinates.Y - 1;
               isValid && x < WordPuzzle.Rows && y < WordPuzzle.Columns
               && currentCharacter < totalCharacters 
               && x >= 0 && y >= 0; currentCharacter++)
            {
                if (word[currentCharacter] != WordPuzzle.Puzzle[x][y])
                    isValid = false;
                else
                {
                    x += displacementCoordinates.X;
                    y += displacementCoordinates.Y;
                }
            }

            isValid = isValid && currentCharacter == totalCharacters;

            var EndPosition = new Coordinate
            {
                X = x + 1,
                Y = y + 1
            };

            if (displacementCoordinates.X > 0)
                EndPosition.X = EndPosition.X - 1;

            if (displacementCoordinates.X < 0)
                EndPosition.X = EndPosition.X + 1;

            if (displacementCoordinates.Y > 0)
                EndPosition.Y = EndPosition.Y - 1;

            if (displacementCoordinates.Y < 0)
                EndPosition.Y = EndPosition.Y + 1;

            var result = new WordSearchResult
            {
                Found = isValid,
                StartPosition = startCoordinates,
                EndPosition = EndPosition
            };

            return result;
        }
    }
}
