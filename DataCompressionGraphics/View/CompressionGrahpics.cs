using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataCompression.Logic;
using DataCompressionGraphics.Logic;
using DataCompressionGraphics.Model;

namespace DataCompressionGraphics
{
    public partial class CompressionGrahpics : Form
    {
        private CompressionMethod SelectedCompressionMethod { get; set; }
        private List<Node> Nodes { get; set; }
        private List<Character> Characters { get; set; }
        private Graphics Graphics { get; set; }
        private Pen Pen { get; set; }
        private int Counter { get; set; }
        public CompressionGrahpics()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            List<string> compressionMethods = new List<string>()
            {
                CompressionMethod.Huffman.ToString(),
                CompressionMethod.ShanonFano.ToString()
            };
            compressionMethodSelect.DataSource = compressionMethods;
            SelectedCompressionMethod = CompressionMethod.Huffman;
            Nodes = new List<Node>();
            Characters = new List<Character>();
            Graphics = this.CreateGraphics();
            Pen = new Pen(Color.Black, 3);
        }

        private void CompressButton_Click(object sender, EventArgs e)
        {
            ResetData();
            string input = inputTextBox.Text.ToString();
            Characters = BaseLogic.GetProbabilityDictionary(input);
            Nodes = BaseLogic.Compress(Nodes, Characters, SelectedCompressionMethod);
            DrawingUtility.InitializePositions(Nodes, 600, 20);
            AssignRootCharacters(Nodes);
            DrawNodesToAStep(0);
            Dictionary<char, string> ResultingCodes = new Dictionary<char, string>();
            foreach (Node node in Nodes)
            {
                if (node.Char != '\\')
                {
                    ResultingCodes[node.Char] = node.Code;
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (char character in input)
            {
                sb.Append(ResultingCodes[character]);
            }

            richTextBox1.Text = sb.ToString();
            CompressionRatio.Text = Math.Round(CalculateCompressionRatio(input, sb.ToString()),3).ToString();
            ResultLabel.Text = GetCompressionCodes(Nodes);
        }

        private static void AssignRootCharacters(List<Node> workingNodes)
        {
            foreach (Node node in workingNodes.Where(node => node.Char == '\\'))
            {
                node.Char = (char)('0' + node.Index + 1);
            }
        }
        
        private void DrawNodesToAStep(int i)
        {
            DrawingUtility.DrawTreeNodes(Nodes, Graphics, Pen, i);
        }

        private double CalculateCompressionRatio(string input, string output)
        {
            return input.Length * 8 / (double)output.Length;
        }

        private string GetCompressionCodes(List<Node> nodes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node node in nodes)
            {
                if (node.Char != '\\')
                {
                    sb.Append($"{node.Char} - {node.Code},    ");
                    sb.AppendFormat("\t");
                }
            }

            return sb.ToString();
        }

        private void ResetData()
        {
            Nodes = new List<Node>();
            Characters = new List<Character>();
            Graphics.Clear(this.BackColor);
        }

        private void CompressionMethodSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCompressionMethod = (CompressionMethod)compressionMethodSelect.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Counter > 0)
            {
                Counter--;
                labelCounter.Text = Counter.ToString();
            }
            Graphics.Clear(this.BackColor);
            DrawNodesToAStep(Counter);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Counter < (Nodes == null ? 0 : Nodes.Max(x => x.Index) + 1))
            {
                Counter++;
                labelCounter.Text = Counter.ToString();
            }
            DrawNodesToAStep(Counter);
        }
    }
}
