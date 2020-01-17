using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DataCompressionGraphics.Logic;
using DataCompressionGraphics.Model;

namespace DataCompression.Logic
{
    public class HuffmanLogic : BaseLogic
    {
        public static void Compress(ref List<Node> nodes, List<Character> characters)
        {
            List<Node> workingNodes = characters.Select(x => x.MapToNode()).ToList();

            while (workingNodes.Count != 1)
            {
                workingNodes = workingNodes.OrderBy(x => x.Probability).ToList();
                workingNodes.Add(MergeNodes(workingNodes[0], workingNodes[1]));
                workingNodes.RemoveRange(0,2);
            }

            AssignCodes(workingNodes[0], String.Empty);
            ExtractNodes(ref nodes, workingNodes[0]);
        }

        private static Node MergeNodes(Node firstNode, Node secondNode)
        {
            Node node = new Node()
            {
                Char = '\\',
                Probability = firstNode.Probability + secondNode.Probability,
                RightChild = firstNode,
                LeftChild = secondNode,
                Index = Counter++
            };
            return node;
        }

        private static void AssignCodes(Node node, string code)
        {
            node.Code = code;
            if (node.LeftChild != null)
            {
                AssignCodes(node.LeftChild, $"{code}0");
            }

            if (node.RightChild != null)
            {
                AssignCodes(node.RightChild, $"{code}1");
            }
        }

        private static void ExtractNodes(ref List<Node> nodes, Node node)
        {
            nodes.Add(node);
            if (node.LeftChild != null)
            {
                ExtractNodes(ref nodes, node.LeftChild);
            }

            if (node.RightChild != null)
            {
                ExtractNodes(ref nodes, node.RightChild);
            }
        }
    }
}
