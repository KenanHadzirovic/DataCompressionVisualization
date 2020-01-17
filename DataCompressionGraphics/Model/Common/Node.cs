using DataCompressionGraphics.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompressionGraphics.Model
{
    public class Node
    {
        public char Char { get; set; }
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Rectangle Rectangle { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public string Code { get; set; }
        public bool IsRoot { get; set; } = false;
        public double Probability { get; set; }
        public int Index { get; set; }

        public Character MapToCharacter()
        {
            return new Character()
            {
                Char =  Char,
                Probability =  Probability
            };
        }
    }
}
