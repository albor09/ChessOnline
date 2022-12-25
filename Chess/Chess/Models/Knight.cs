using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Knight : Figure
    {
        public Knight(FigureColor color, Cell cell) : base(Figures.knight, color, cell) { }

        public override List<Cell> CalculateMoves()
        {
            return Board.Instance.CalculateKnightMoves(Cell, Color);
        }
    }
}
