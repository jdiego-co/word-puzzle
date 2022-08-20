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


            string wordToSearch;
            do
            {

                Console.WriteLine(Environment.NewLine + "Please Write a word to search");
                wordToSearch = Console.ReadLine();
                if(!string.IsNullOrEmpty(wordToSearch))
                    puzzle.SearchWord(wordToSearch.ToUpper());

            } while (!string.IsNullOrEmpty(wordToSearch));

            Console.Clear();
            Console.WriteLine("Game ended. Press enter to exit");
            Console.ReadLine();
        }
    }
}
