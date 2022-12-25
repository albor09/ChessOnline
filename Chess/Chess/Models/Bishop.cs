using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Bishop : Figure
    {
        public Bishop(FigureColor color, Cell cell) : base(Figures.bishop, color, cell) { }

        public override List<Cell> CalculateMoves()
        {
            List<Cell> moves = new List<Cell>();
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardLeft));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.ForwardRight));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardLeft));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.BackwardRight));
            return moves;
        }
    }
}
