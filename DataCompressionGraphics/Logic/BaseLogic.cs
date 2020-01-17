using DataCompression.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DataCompressionGraphics.Model;

namespace DataCompressionGraphics.Logic
{
    public class BaseLogic
    {
        protected static int Counter { get; set; } = 0;

        public static List<Node> Compress(List<Node> nodes, List<Character> characters, CompressionMethod compressionMethod)
        {
            switch (compressionMethod)
            {
                case CompressionMethod.Huffman:
                    HuffmanLogic.Compress(ref nodes, characters);
                    break;
                case CompressionMethod.ShanonFano:
                default:
                    ShanonFanoLogic.Compress(ref nodes, characters);
                    break;
            }

            return nodes;
        }

        public static List<Character> GetProbabilityDictionary(string word)
        {
            Counter = 0;
            List<Character> characters = new List<Character>();

            foreach (char c in word)
            {
                if (characters.Any(x => x.Char == c))
                {
                    characters.Single(x => x.Char == c).Probability += 1f / word.Length;
                }
                else
                {
                    characters.Add(new Character()
                    {
                        Char = c,
                        Probability = 1f / word.Length
                    });
                }
            }

            return characters.OrderByDescending(x => x.Probability).ToList();
        }

    }
}
