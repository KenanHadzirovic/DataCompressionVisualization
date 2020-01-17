using DataCompressionGraphics.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DataCompressionGraphics
{
    public static class DrawingUtility
    {
        public static int Size { get; set; } = 30;
        public static int DistanceX { get; set; } = 200;
        public static int DistanceY { get; set; } = 70;
        private static Font Font { get; set; }
        private static Brush Brush { get; set; }
        private static Brush CodeBrush { get; set; }
        public static void InitializePositions(List<Node> nodes, int startingPositionX, int startingPoistionY)
        {
            InitializePosition(nodes[0], startingPositionX, startingPoistionY, DistanceX);
        }

        public static void InitializePosition(Node node, int positionX, int positionY, int distance)
        {
            node.Point = new Point(positionX, positionY);
            node.Size = new Size(Size,Size);
            distance = (int)(distance / 1.8f);
            if (node.LeftChild != null)
            {
                InitializePosition(node.LeftChild, node.Point.X - distance, node.Point.Y + DistanceY, distance);
            }

            if (node.RightChild != null)
            {
                InitializePosition(node.RightChild, node.Point.X + distance, node.Point.Y + DistanceY, distance);
            }
        }

        public static void DrawTreeNodes(List<Node> nodes, Graphics graphics, Pen pen, int i)
        {
            Font = new Font("Arial", 15);
            Brush = new SolidBrush(Color.Black);
            CodeBrush = new SolidBrush(Color.Red);
            foreach (Node node in nodes)
            {
                if(node.Index < i)
                {
                    DrawTreeNode(graphics, pen, node);
                    ConnectNodes(graphics, pen, node, node.LeftChild);
                    ConnectNodes(graphics, pen, node, node.RightChild);
                }
            }
        }

        public static void DrawTreeNode(Graphics graphics, Pen pen, Node node)
        {
            node.Rectangle = new Rectangle(node.Point, node.Size);
            graphics.DrawEllipse(pen, node.Rectangle);
            graphics.DrawString(node.Char.ToString(), Font, Brush, node.Point.X + Size/3.5f, node.Point.Y);
        }

        private static void ConnectNodes(Graphics graphics, Pen pen, Node parentNode, Node childNode)
        {
            if (parentNode != null && childNode != null)
            {
                Point startingPoint = new Point(parentNode.Point.X + Size / 2, parentNode.Point.Y + Size);
                Point endPoint = new Point(childNode.Point.X + Size / 2, childNode.Point.Y);
                Point midPoint = new Point((startingPoint.X + endPoint.X) / 2 - 5, (startingPoint.Y + endPoint.Y) / 2);

                graphics.DrawLine(pen, startingPoint, endPoint);
                graphics.DrawString(childNode.Code.Last().ToString(), Font, CodeBrush, midPoint);
            }
        }
    }
}
