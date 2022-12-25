using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Rook : Figure
    {
        public Rook(FigureColor color, Cell cell) : base(Figures.rook, color, cell) { }

        public override List<Cell> CalculateMoves()
        {
            List<Cell> moves = new List<Cell>();
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Forward));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Backward));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Left));
            moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Right));
            return moves;
        }
    }
}
