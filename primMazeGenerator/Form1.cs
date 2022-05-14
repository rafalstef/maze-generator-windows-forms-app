using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Priority_Queue;

namespace primMazeGenerator
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Maze maze;
        private int cellSize;

        public Form1()
        {
            InitializeComponent();
            SetNewMaze();
        }

        private void SetNewMaze()
        {
            cellSize = 20;
            maze = new Maze((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            DrawMaze();
        }

        private void DrawMaze()
        {
            Dictionary<Cell, Dictionary<Cell, int>> allAdjacents = GetAllAdjacents();

            pictureBoxMaze.Image = new Bitmap(pictureBoxMaze.Width, pictureBoxMaze.Height);
            g = Graphics.FromImage(pictureBoxMaze.Image);

            for (int x = 0; x < maze.MazeWidth; x++)
            {
                for (int y = 0; y < maze.MazeHeight; y++)
                {
                    if (maze.Grid[x,y].Walls[Wall.top] == true) 
                        maze.Grid[x, y].drawTopWall(g, x, y, cellSize);
                    if (maze.Grid[x, y].Walls[Wall.bottom] == true)
                         maze.Grid[x, y].drawBottomWall(g, x, y, cellSize);
                    if (maze.Grid[x, y].Walls[Wall.left] == true)
                        maze.Grid[x, y].drawLeftWall(g, x, y, cellSize);
                    if (maze.Grid[x, y].Walls[Wall.right] == true)
                        maze.Grid[x, y].drawRightWall(g, x, y, cellSize);
                }
            }
            pictureBoxMaze.Refresh();
        }

        private Dictionary<Cell, Dictionary<Cell, int>> GetAllAdjacents()
        {
            Dictionary < Cell, Dictionary<Cell, int>> allAdjacents =
                new Dictionary<Cell, Dictionary<Cell, int>>();  
            
            foreach (Cell cell in maze.Grid)
            {
                allAdjacents.Add(cell, cell.Adjacents);
            }

            return allAdjacents;
        }

        private void GenerateMaze()
        {
            SimplePriorityQueue<Cell> heap = new SimplePriorityQueue<Cell>();
            Cell initVertex = maze.Grid[0,0];

            heap.Enqueue(initVertex, 0);
            initVertex.Weight = 0;

            while (heap.Count > 0)
            {
                Cell minVertex = heap.First();
                heap.Dequeue();

                if (!minVertex.Visited)
                {
                    minVertex.Visited = true;
                    foreach (var adjacent in minVertex.Adjacents)
                    {
                        Cell neighbour = adjacent.Key;
                        int neighbourWeight = adjacent.Value;

                        if (!neighbour.Visited && neighbour.Weight > neighbourWeight)
                        {
                            neighbour.Weight = neighbourWeight;
                            heap.Enqueue(neighbour, neighbour.Weight);
                            neighbour.Parent = minVertex;
                        }
                    }
                }

                if (minVertex.RowNum == 0 && minVertex.ColNum == 0)
                    continue;

                maze.RemoveWalls(minVertex, minVertex.Parent);
                DrawMaze();
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateMaze();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetNewMaze();
        }
    }
}
