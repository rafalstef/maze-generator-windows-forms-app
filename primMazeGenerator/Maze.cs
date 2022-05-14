using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primMazeGenerator
{
    internal class Maze
    {
        private int mazeHeight;
        private int mazeWidth;
        private Cell[,] grid;
        Random random;

        public int MazeHeight { get { return mazeHeight; } }
        public int MazeWidth { get { return mazeWidth; } }

        public Cell[,] Grid { get { return grid; } }



        public Maze(int width, int height)
        {
            mazeWidth = width;
            mazeHeight = height;
            
            grid = new Cell[width, height];
            
            random = new Random();
            InitializeAdjacents();
        }

        private void InitializeAdjacents()
        {
            for (int i = 0; i < mazeWidth; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    grid[i, j] = new Cell(i, j);
                }
            }

            foreach(Cell cell in grid)
            {
                SetCellAdjacents(cell);
            }
        }

        public void SetCellAdjacents(Cell cell)
        {
            Dictionary<Cell, int> adjacents = new Dictionary<Cell, int>();
            Cell[,] grid = this.grid;

            int row = cell.RowNum;
            int col = cell.ColNum;  

            Cell top = row != 0 ? grid[row-1, col] : null;
            Cell right = col != MazeHeight - 1 ? grid[row, col+1] : null;
            Cell bottom = row != MazeWidth - 1 ? grid[row + 1, col] : null;
            Cell left = col != 0 ? grid[row, col - 1] : null;

            if (top != null) adjacents.Add(top, random.Next());
            if (right != null) adjacents.Add(right, random.Next());
            if (bottom != null) adjacents.Add(bottom, random.Next());
            if (left != null) adjacents.Add(left, random.Next());

            cell.Adjacents = adjacents;
        }

        public void RemoveWalls(Cell cell1, Cell cell2)
        {
            int x = cell1.RowNum - cell2.RowNum;
            if (x == 1)
            {
                cell1.Walls[Wall.left] = false;
                cell2.Walls[Wall.right] = false;
            }
            if (x == -1)
            {
                cell1.Walls[Wall.right] = false;
                cell2.Walls[Wall.left] = false;
            }

            int y = cell1.ColNum - cell2.ColNum;
            if (y == 1)
            {
                cell1.Walls[Wall.top] = false;
                cell2.Walls[Wall.bottom] = false;
            }
            if (y == -1)
            {
                cell1.Walls[Wall.bottom] = false;
                cell2.Walls[Wall.top] = false;
            }
        }

    }
}
