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
using w10_mikroszimulacio.Entities;

namespace w10_mikroszimulacio
{
    public partial class Form1 : Form
    {
        List<Person> Population = null; //new List<Person>();
        List<BirthProbability> BirthProbabilities = null; // new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = null; // new List<DeathProbability>();

        //List<int> men = new List<int>(); //enyém
        //List<int> women = new List<int>(); //enyém

        Random rng = new Random(1234);

        

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> pb = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    pb.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return pb;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> db = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    db.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return db;
        }



        public Form1()
        {
            InitializeComponent();


            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
            Start((int)numericUpDown1.Value, txtFolder.Text);
        }

        public void Start(int endYear, string csvPath)
        {
            Population = GetPopulation(csvPath);

            // Végigmegyünk a vizsgált éveken
            for (int year = 2005; year <= numericUpDown1.Value; year++) //2024 helyett
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                    SimStep(year, Population[i]);

                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                richTextBox1.Text += (
                    string.Format("Év:{0} \n Fiúk:{1} \n Lányok:{2} \n", year, nbrOfMales, nbrOfFemales));
               // "Szimulációs év: " + year + "\n" + "\t" + men[valtozo] + "\n" + "\t" + women[valtozo] + "\t" + "\t";
            }
        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();//headert eldobom
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }



        private void StartBtn_Click(object sender, EventArgs e)//nem kell ez már
        {
                Start((int)numericUpDown1.Value, txtFolder.Text);

               /* richTextBox1.Clear(); //listák üritése
            men.Clear();
            women.Clear();

            //

                men.Add(nbrOfMales);
                women.Add(nbrOfFemales);//enyém
                DisplayResults(); //ide kell???*/
            
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = txtFolder.Text;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            txtFolder.Text = ofd.FileName;
        }

        private void DisplayResults()
        {/*
            int valtozo = 0;
            for (int year = 2005; year <= numericUpDown1.Value; year++) //2024 helyett
            {

                
                    richTextBox1.Text += "Szimulációs év: " + year + "\n" + "\t" + men[valtozo]+ "\n" + "\t" + women[valtozo]+ "\t"+ "\t";

                valtozo++;
              
            }*/
        }

        private void StartBtn_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear(); //listák üritése
            Start((int)numericUpDown1.Value, txtFolder.Text);
        }
    }
}
