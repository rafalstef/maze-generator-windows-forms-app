using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primMazeGenerator
{
    enum Wall { top, bottom, left, right }
    internal class Cell
    {
        private int rowNum;
        private int colNum;
        private bool visited;
        private int weight;
        private Cell parent;

        public Dictionary<Wall, bool> walls;
        private Dictionary<Cell, int> adjacents;

        public int RowNum { get => rowNum; set => rowNum = value; }
        public int ColNum { get => colNum; set => colNum = value; }
        public bool Visited { get => visited; set => visited = value; }
        public int Weight { get => weight; set => weight = value; }
        internal Cell Parent { get => parent; set => parent = value; }
        internal Dictionary<Cell, int> Adjacents { get => adjacents; set => adjacents = value; }
        internal Dictionary<Wall, bool> Walls { get => walls; set => walls = value; }

        public Cell(int row, int column)
        {
            rowNum = row;
            colNum = column;
            visited = false;
            weight = Int32.MaxValue;
            parent = null;

            walls = new Dictionary<Wall, bool>()
            { 
                {Wall.top, true},
                {Wall.bottom, true },
                {Wall.left, true },
                {Wall.right, true }
            };
        }
        public void drawTopWall(Graphics g, int x, int y, int size)
        {
            g.DrawLine(new Pen(Color.Black, 3), size*x, size*y, size * x+size, size * y);
        }

        public void drawBottomWall(Graphics g, int x, int y, int size)
        {
            g.DrawLine(new Pen(Color.Black, 3), size * x, size * y+size, size * x+size, size * y+size);
        }

        public void drawLeftWall(Graphics g, int x, int y, int size)
        {
            g.DrawLine(new Pen(Color.Black, 3), size * x, size * y, size * x, size * y + size);
        }
        public void drawRightWall(Graphics g, int x, int y, int size)
        {
            g.DrawLine(new Pen(Color.Black, 3), size * x + size, size * y, size * x + size, size * y + size);
        }
    }
}
