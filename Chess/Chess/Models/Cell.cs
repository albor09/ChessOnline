using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess.Models
{
    public class Cell
    {
        public static Color BLACK = Color.FromArgb(118, 150, 86);
        public static Color WHITE = Color.FromArgb(238, 238, 210);
        public static Color BLACKSELECTED = Color.FromArgb(141, 179, 104);
        public static Color WHITESELECTED = Color.FromArgb(240, 240, 223);

        public readonly Point position;
        public readonly Label label;
        public readonly Color defaultColor;
        public Figure figure;

        public bool IsEmpty { get { return figure == null; } }

        public Cell(Point position, Label label, Figure figure = null)
        {
            this.position = position;
            this.label = label;
            this.figure = figure;
            this.defaultColor = label.BackColor;
        }

        public void SetFigure(Figures type, FigureColor color)
        {
            string imgPath = Figure.GetFigureImgPath(type,color);
            label.Image = Image.FromFile(imgPath);
            switch (type)
            {
                case Figures.pawn:
                    figure = new Pawn(color, this);
                    break;
                case Figures.rook:
                    figure = new Rook(color, this);
                    break;
                case Figures.bishop:
                    figure = new Bishop(color, this);
                    break;
                case Figures.knight:
                    figure = new Knight(color, this);
                    break;
                case Figures.king:
                    figure = new King(color, this);
                    break;
                case Figures.queen:
                    figure = new Queen(color, this);
                    break;
            }
        }

        public void SetFigure(Figure figure)
        {
            string imgPath = Figure.GetFigureImgPath(figure.Type, figure.Color);
            label.Image = Image.FromFile(imgPath);
            this.figure = figure;
            figure.Cell = this;
        }

        public void RemoveFigure()
        {
            label.Image = null;
            figure = null;
        }

        public void SetSelectedColor()
        {
            label.BackColor = defaultColor == WHITE ? WHITESELECTED : BLACKSELECTED;
        }

        public void SetDefaultColor()
        {
            label.BackColor = defaultColor;
        } 
    }
}
