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
                    l.Click += OnCellSelect;
                    Controls.Add(l);
                    cells[i].Add(new Cell(new Point(i, j), l));
                }
            }
            board = new Board(cells);
            //board.SetDefaultPreset();
        }

        public void OnCellSelect(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            Tuple<int, int> pos = (Tuple<int, int>)label.Tag;
            Cell s = board.cells[pos.Item1][ pos.Item2];

            if (board.turn != myColor)
                return;


            if (selected != null)
            {
                if (selected.figure.Color == myColor && availableMoves.Contains(s))
                {
                    board.Move(selected, s);
                    chessClient.SendMessageToServer($"Move {selected.position.X};{selected.position.Y} {s.position.X};{s.position.Y}");
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
                if (s.figure == null || s.figure.Color != myColor)
                {
                    UnselectAll();
                    return;
                }
            }
            selected = s;
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
