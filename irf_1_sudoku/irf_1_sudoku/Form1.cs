using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace irf_1_sudoku
{
    public partial class Form1 : Form
    {
        int lineWidth = 5;

        private Random _rng = new Random();

        private Sudoku _currentQuiz = null;


        public Form1()
        {
            InitializeComponent();
            CreatePlayField();
            LoadSudokus();
           // _currentQuiz = GetRandomQuiz();
            NewGame();
        }

        //private List<Sudoku> _sudokus = new List<Sudoku>();

        private void CreatePlayField()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    SudokuField sf = new SudokuField();
                    sf.Left = col * sf.Width + (int)(Math.Floor((double)(col / 3))) * lineWidth;
                    sf.Top = row * sf.Height + (int)(Math.Floor((double)(row / 3))) * lineWidth;
                    mainPanel1.Controls.Add(sf);

                    MouseDown += Form1_MouseDown;


                }
            }
        }

       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            string valtozo = "";

            foreach (var sf in mainPanel1.Controls.OfType<SudokuField>())
            {
                valtozo += sf.Value.ToString();

                if (valtozo.Equals(_currentQuiz))
                {  //////////////
                    MessageBox.Show("Győztél");

                    foreach (var item in mainPanel1.Controls.OfType<SudokuField>())
                    {
                        sf.Active = false;  ///
                    }
                    MessageBox.Show("Győztél");
                }
                /*if (valtozo.Equals(Sudoku.Solution))
                {  //////////////
                    MessageBox.Show("Győztél");

                    foreach (var item in mainPanel1.Controls.OfType<SudokuField>())
                    {
                        sf.Active = false;  ///
                    }*/
                
            }
        }


        private List<Sudoku> _sudokus = new List<Sudoku>();

        //public object Solution { get; private set; } ///?

        private void LoadSudokus()
        {
            _sudokus.Clear();
            using (StreamReader sr = new StreamReader("sudoku.csv", Encoding.Default))
            { sr.ReadLine(); //remove headers
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');

                    Sudoku s = new Sudoku();
                    s.Quiz = line[0];
                    s.Solution = line[1];
                    _sudokus.Add(s);


                    //?
                    /*string valtozo = "";

                    foreach (var sf in mainPanel1.Controls.OfType<SudokuField>())
                    {
                        valtozo += sf.Value.ToString();

                    }
                    if (valtozo.Equals(s.Solution)) ;*/
                };


            }
        }

        private Sudoku GetRandomQuiz()
        {
            int randomNumber = _rng.Next(_sudokus.Count);
            return _sudokus[randomNumber];
        }

        private void NewGame()
        {
            _currentQuiz = GetRandomQuiz();

            int counter = 0;
            foreach (var sf in mainPanel1.Controls.OfType<SudokuField> ())
            {
                sf.Value = int.Parse(_currentQuiz.Quiz[counter].ToString());
                sf.Active = sf.Value == 0;
                counter++; 
            }
        }

        


       
    }
}
