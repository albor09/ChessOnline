using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chess.Models
{
    public abstract class Figure
    {
        public Figures Type;
        public FigureColor Color;
        public Cell Cell;

        public Figure(Figures type, FigureColor color, Cell cell)
        {
            this.Type = type;
            this.Color = color;
            this.Cell = cell;
        }
         
        public abstract List<Cell> CalculateMoves();

        public static string GetFigureImgPath(Figures type, FigureColor color)
        {
            string strColor = color == FigureColor.black ? "b" : "w";
            switch (type)
            {
                case Figures.pawn:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_pawn.png");
                case Figures.rook:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_rook.png");
                case Figures.bishop:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_bishop.png");
                case Figures.knight:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_knight.png");
                case Figures.king:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_king.png");
                case Figures.queen:
                    return Helpers.PathResolve(Helpers.AssetsImgFolder, strColor + "_queen.png");
            }
            return "";
        }
    }

    public enum Figures
    {
        pawn,
        rook,
        bishop,
        knight,
        king,
        queen
    }

    public enum FigureColor
    {
        white,
        black
    }
}
