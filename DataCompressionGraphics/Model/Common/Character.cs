using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompressionGraphics.Model
{
    public class Character
    {
        public char Char { get; set; }
        public double Probability { get; set; }


        public Node MapToNode()
        {
            return new Node()
            {
                Char = Char,
                Probability = Probability
            };
        }
    }
}
