using Chess.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Board
    {
        public static Board Instance;

        public List<List<Cell>> cells;
        public FigureColor turn;

        private Figure WhiteKing;
        private Figure BlackKing;

        public Board(List<List<Cell>> cells)
        {
            this.cells = cells;
            if (Instance == null)
                Instance = this;
        }


        public void SetDefaultPreset()
        {
            for (int i = 0; i < 8; i++)
            {
                cells[i][ 1].SetFigure(Figures.pawn, FigureColor.black);
                cells[i][6].SetFigure(Figures.pawn, FigureColor.white);
            }
            cells[0][ 0].SetFigure(Figures.rook, FigureColor.black);
            cells[1][ 0].SetFigure(Figures.knight, FigureColor.black);
            cells[2][ 0].SetFigure(Figures.bishop, FigureColor.black);
            cells[3][ 0].SetFigure(Figures.queen, FigureColor.black);
            cells[4][ 0].SetFigure(Figures.king, FigureColor.black);
            BlackKing = cells[4][ 0].figure;
            cells[5][ 0].SetFigure(Figures.bishop, FigureColor.black);
            cells[6][ 0].SetFigure(Figures.knight, FigureColor.black);
            cells[7][0].SetFigure(Figures.rook, FigureColor.black);

            cells[0][7].SetFigure(Figures.rook, FigureColor.white);
            cells[1][ 7].SetFigure(Figures.knight, FigureColor.white);
            cells[2][ 7].SetFigure(Figures.bishop, FigureColor.white);
            cells[3][7].SetFigure(Figures.queen, FigureColor.white);
            cells[4][ 7].SetFigure(Figures.king, FigureColor.white);
            WhiteKing = cells[4][ 7].figure;
            cells[5][7].SetFigure(Figures.bishop, FigureColor.white);
            cells[6][7].SetFigure(Figures.knight, FigureColor.white);
            cells[7][ 7].SetFigure(Figures.rook, FigureColor.white);
        }

        public List<Cell> CalculateMoves(Cell from, FigureColor color, MovementDirection direction, int movementLength = int.MaxValue, bool canKill = true)
        {
            List<Cell> movementCells = new List<Cell>();
            Point currentPoint = StepInDirection(from.position, direction);
            int step = 0;
            while (IsValidPoint(currentPoint) && step < movementLength)
            {
                if (cells[currentPoint.X][currentPoint.Y].figure != null)
                {
                    if (cells[currentPoint.X][ currentPoint.Y].figure.Color == color) break;
                    else
                    {
                        if (canKill)
                            movementCells.Add(this.cells[currentPoint.X][currentPoint.Y]);
                        break;
                    }
                }
                movementCells.Add(this.cells[currentPoint.X][currentPoint.Y]);
                currentPoint = StepInDirection(currentPoint, direction);
                step++;
                
            }
            return movementCells;
        }

        public List<Cell> CalculateKnightMoves(Cell from, FigureColor color)
        {
            List<Cell> movementCells = new List<Cell>();
            foreach (var i in new int[2] { -1, 1})
            {
                foreach (var j in new int[2] { -2, 2 })
                {
                    Point nPoint = new Point(from.position.X + i, from.position.Y + j);
                    if (IsValidPoint(nPoint) && (cells[nPoint.X][nPoint.Y].figure == null || cells[nPoint.X][nPoint.Y].figure.Color != color))
                        movementCells.Add(cells[nPoint.X][nPoint.Y]);
                }
            }
            foreach (var i in new int[2] { -2, 2 })
            {
                foreach (var j in new int[2] { -1, 1 })
                {
                    Point nPoint = new Point(from.position.X + i, from.position.Y + j);
                    if (IsValidPoint(nPoint) && (cells[nPoint.X][nPoint.Y].figure == null || cells[nPoint.X][nPoint.Y].figure.Color != color))
                        movementCells.Add(cells[nPoint.X][nPoint.Y]);
                }
            }
            return movementCells;
        }

        public List<Cell> PawnKillCells(Cell from, FigureColor color)
        {
            List<Cell> movementCells = new List<Cell>();
            int yModif = color == FigureColor.white ? -1 : 1;
            Point l = new Point(from.position.X - 1, from.position.Y + yModif);
            if (IsValidPoint(l) && !cells[l.X][l.Y].IsEmpty && cells[l.X][l.Y].figure.Color != color)
                movementCells.Add(cells[l.X][l.Y]);
            Point r = new Point(from.position.X + 1, from.position.Y + yModif);
            if (IsValidPoint(r) && !cells[r.X][r.Y].IsEmpty && cells[r.X][r.Y].figure.Color != color)
                movementCells.Add(cells[r.X][r.Y]);
            return movementCells;
        }

        public Point StepInDirection(Point from, MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Forward:
                    return new Point(from.X, from.Y+1);
                case MovementDirection.Left:
                    return new Point(from.X-1, from.Y);
                case MovementDirection.Right:
                    return new Point(from.X+1, from.Y);
                case MovementDirection.Backward:
                    return new Point(from.X, from.Y - 1);
                case MovementDirection.ForwardRight:
                    return new Point(from.X+1, from.Y + 1); 
                case MovementDirection.ForwardLeft:
                    return new Point(from.X-1, from.Y + 1);
                case MovementDirection.BackwardRight:
                    return new Point(from.X+1, from.Y - 1);
                case MovementDirection.BackwardLeft:
                    return new Point(from.X-1, from.Y - 1);
            }
            return new Point(0, 0);
        }

        public bool IsValidPoint(Point point)
        {
            return point.X >= 0 && point.X < 8 && point.Y >= 0 && point.Y < 8;
        }

        public void SetDefaultColorCells()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i][j].SetDefaultColor();
                }
            }
        }

        public void Move(Cell from, Cell target)
        {
            if (from.figure.Type == Figures.pawn)
                ((Pawn)from.figure).canDoubleMove = false;

            target.SetFigure(from.figure);
            from.RemoveFigure();
            turn = turn == FigureColor.white ? FigureColor.black : FigureColor.white;

            SoundManager.Instance.PlayMoveSound();

            if (CanBeKilled(BlackKing) && !CanEscape(BlackKing))
                Debug.WriteLine("GAME OVER WHITE WIN");
            if (CanBeKilled(WhiteKing) && !CanEscape(WhiteKing))
                Debug.WriteLine("GAME OVER BLACK WIN");
            
            
        }

        public bool CanBeKilled(Figure figure, bool firstCheck = true)
        {
            
            if (firstCheck)
                return cells.Any(x => x.Any(y => y.figure != null &&
                y.figure.Color != figure.Color &&
                y.figure.CalculateMoves().Contains(figure.Cell) &&
                !CanBeKilled(y.figure, false))) ;
            else
                return cells.Any(x => x.Any(y => y.figure != null &&
                y.figure.Color != figure.Color &&
                y.figure.CalculateMoves().Contains(figure.Cell)));
        }

        public bool CanEscape(Figure figure)
        {
            foreach (var item in figure.CalculateMoves())
            {
                if (!CanBeKilled(item, figure.Color))
                    return true;
            }
            return false;
        }

        public bool CanBeKilled(Cell cell, FigureColor color)
        {
            return cells.Any(x => x.Any(y => y.figure != null && y.figure.Color != color && y.figure.CalculateMoves().Contains(cell)));
        }
    }

    public enum MovementDirection
    {
        Forward,
        Left,
        Right,
        Backward,
        ForwardRight,
        ForwardLeft,
        BackwardRight,
        BackwardLeft
    }
}
