using Chess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Chess
{
    public partial class Form1 : Form
    {
        private Board board;
        private Cell selected;

        private FigureColor myColor = FigureColor.white;

        private List<Cell> availableMoves;

        private ChessClient chessClient;
        public Form1()
        {
            InitializeComponent();
            InitBoard();
        }


        private void InitBoard()
        {
            List<List<Cell>> cells = new List<List<Cell>>();
            for (int i = 0; i < 8; i++)
            {
                cells.Add(new List<Cell>());
                for (int j = 0; j < 8; j++)
                {
                    Label l = new Label();
                    l.Tag = new Tuple<int,int>(i, j);
                    if ((i + j) % 2 != 0)
                        l.BackColor = Cell.WHITE;
                    else
                        l.BackColor = Cell.BLACK;
                    l.Size = new Size(64, 64);
                    l.Location = new Point(i * 64, 24 + j * 64);
                    l.ImageAlign = ContentAlignment.MiddleCenter;
                    int ii = i;
                    int jj = j;
                    l.Click += (Object sender, EventArgs e) => OnCellSelect(board.cells[ii][jj]);
                    Controls.Add(l);
                    cells[i].Add(new Cell(new Point(i, j), l));
                }
            }
            board = new Board(cells);
            //board.SetDefaultPreset();
        }

        public void OnCellSelect(Cell cell)
        {
            if (board.turn != myColor)
                return;


            if (selected != null)
            {
                if (selected.figure.Color == myColor && availableMoves.Contains(cell))
                {
                    board.Move(selected, cell);
                    chessClient.SendMessageToServer($"Move {Helpers.CoordinatesToNotation(selected.position.X, selected.position.Y)}&{Helpers.CoordinatesToNotation(cell.position.X, cell.position.Y)}");
                    UnselectAll();
                    return;
                }
                else
                {
                    UnselectAll();
                    return;
                }
            }
            else
            {
                if (cell.figure == null || cell.figure.Color != myColor)
                {
                    UnselectAll();
                    return;
                }
            }
            selected = cell;
            selected.SetSelectedColor();
            availableMoves = selected.figure.CalculateMoves();
            for (int i = 0; i < availableMoves.Count; i++)
                availableMoves[i].SetSelectedColor();
            
        }

        public void UnselectAll()
        {
            if (selected != null)
                selected.SetDefaultColor();
            if (availableMoves != null)
                board.SetDefaultColorCells();
            selected = null;
            availableMoves = null;

        }

        private void multiplayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new MultiplayerForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) // User creat lobby
                {
                    chessClient = form.client;
                    myColor = FigureColor.white;
                }
                if (result == DialogResult.Yes) // User connect lobby 
                {
                    chessClient = form.client;
                    myColor = FigureColor.black;
                }
            }
        }
    }
}
