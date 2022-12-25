using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Pawn : Figure
    {
        public bool canDoubleMove = true;
        public Pawn(FigureColor color, Cell cell) : base(Figures.pawn, color, cell) 
        {

        }

        public override List<Cell> CalculateMoves()
        {
            List<Cell> moves = new List<Cell>();
            if (Color == FigureColor.white)
            {
                moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Backward, canDoubleMove ? 2 : 1, false));
            }
            else
            {
                moves.AddRange(Board.Instance.CalculateMoves(Cell, Color, MovementDirection.Forward, canDoubleMove ? 2 : 1, false));
            }
            moves.AddRange(Board.Instance.PawnKillCells(Cell, Color));
            return moves;
        }


    }
}
