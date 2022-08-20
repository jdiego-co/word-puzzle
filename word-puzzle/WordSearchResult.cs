using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_puzzle
{
    public class WordSearchResult
    {
        public bool Found { get; set; }
        public Coordinate StartPosition { get; set; }
        public Coordinate EndPosition { get; set; }
    }
}
