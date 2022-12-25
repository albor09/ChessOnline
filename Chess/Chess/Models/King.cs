using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class King : Figure
    {
        public King(FigureColor color, Cell cell) : base(Figures.king, color, cell) { }

        public override List<Cell> CalculateMoves()
        {
            List<Cell> moves = new List<Cell>();
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Forward, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardLeft, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardRight, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Backward, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardLeft, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardRight, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Left, 1));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Right, 1));
            return moves;
        }
    }
}
