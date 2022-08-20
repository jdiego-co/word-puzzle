using System;
using System.IO;

namespace word_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileRead = string.Empty;
            FileStream fileStream = new FileStream("wordpuzzle.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileRead = reader.ReadToEnd();
            }
            var puzzle = new WordPuzzleResolver(fileRead);
            puzzle.Print();
            Console.ReadLine();

        }
    }
}
