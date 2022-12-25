using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Queen : Figure
    {
        public Queen(FigureColor color, Cell cell) : base(Figures.queen, color, cell) { }

        public override List<Cell> CalculateMoves()
        {
            List<Cell> moves = new List<Cell>();
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Forward));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardLeft));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardRight));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Backward));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardLeft));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardRight));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Left));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Right));
            return moves;
        }
    }
}
