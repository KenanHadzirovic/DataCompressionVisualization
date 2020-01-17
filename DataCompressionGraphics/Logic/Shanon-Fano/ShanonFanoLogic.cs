using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCompressionGraphics.Logic;
using DataCompressionGraphics.Model;

namespace DataCompression.Logic
{
    public class ShanonFanoLogic : BaseLogic
    {
        public static void Compress(ref List<Node> nodes, List<Character> characters)
        {
            Node rootNode = new Node() { IsRoot = true };
            Compress(ref nodes, ref rootNode, characters, String.Empty, Orientation.Root);
        }

        private static void Compress(ref List<Node> nodes, ref Node parentNode, List<Character> characters, string code, Orientation orientation)
        {
            Node node = new Node()
            {
                Char = '\\',
                Code = code,
                LeftChild = null,
                RightChild = null,
                Index = Counter++
            };
            AssignNode(parentNode, node, orientation);
            nodes.Add(node);
            if (characters.Count == 1)
            {
                node.Char = characters[0].Char;
                return;
            }
            int splittingIndex = SplitArray(characters);
            Compress(ref nodes, ref node, characters.Take(splittingIndex).ToList(), $"{code}0", Orientation.Left);
            Compress(ref nodes, ref node, characters.Skip(splittingIndex).ToList(), $"{code}1", Orientation.Right);
        }

        private static void AssignNode(Node parentNode, Node childNode, Orientation orientation)
        {
            if (orientation == Orientation.Left)
            {
                parentNode.LeftChild = childNode;
            }
            else
            {
                parentNode.RightChild = childNode;
            }
        }

        private static int SplitArray(List<Character> characters)
        {
            int index = 1;

            double upperArrayProbability = characters[0].Probability;
            double lowerArrayProbability = characters.Skip(index).Sum(x => x.Probability);
            double difference = Math.Abs(upperArrayProbability - lowerArrayProbability);

            for (; index < characters.Count; index++)
            {
                upperArrayProbability += characters[index].Probability;
                lowerArrayProbability -= characters[index].Probability;
                double newDifference = Math.Abs(upperArrayProbability - lowerArrayProbability);

                if (newDifference > difference)
                {
                    return index;
                }
                else
                {
                    difference = newDifference;
                }
            }
            return index;
        }
    }
}
