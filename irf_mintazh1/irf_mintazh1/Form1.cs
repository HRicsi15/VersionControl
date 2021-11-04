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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace irf_mintazh1
{
    public partial class Form1 : Form
    {
        public List<NewFolder1.OlympicResult> results = new List<NewFolder1.OlympicResult>();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();

            LoadData("Summer_olympic_Medals.csv");
            CreateYearFilter();
            CalculateOrder();
        }

        private void LoadData(string filename)
        {
            results.Clear();
            using (StreamReader sr = new StreamReader(filename, Encoding.Default))
            {
                sr.ReadLine();//remove headers
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');

                    NewFolder1.OlympicResult olympic = new NewFolder1.OlympicResult();
                    olympic.Year = int.Parse(line[0]);
                    olympic.Country = line[3];
                    olympic.Medals = new int[]
                    {
                       int.Parse(line[5]),
                       int.Parse(line[6]),
                       int.Parse(line[7])
                    };
                    /* var medals = new int[3];
                     medals[0] = int.Parse(line[5]);
                     medals[1] = int.Parse(line[6]);
                     medals[2] = int.Parse(line[7]);
                     olympic.Medals = medals;*/
                    results.Add(olympic);
                }
            }

        }
        private void CreateYearFilter()
        {
            var years = (from x in results
                         orderby x.Year
                         select x.Year).Distinct();

            comboBox1.DataSource = years.ToList();
        }

        private int CalculatePosition(NewFolder1.OlympicResult olympicResult)//fgv bemenettel és kimenettel
        {
            int betterCountryCount = 0;

            var eredmeny2 = from x in results
                            where x.Year == olympicResult.Year && x.Country != olympicResult.Country
                            select x;

            foreach (var item in eredmeny2)
            {
                if (item.Medals[0] > olympicResult.Medals[0]) betterCountryCount++;
                if (item.Medals[0] == olympicResult.Medals[0] && item.Medals[1] > olympicResult.Medals[1]) betterCountryCount++;
                if (item.Medals[0] == olympicResult.Medals[0] && item.Medals[1] == olympicResult.Medals[1] && item.Medals[2] > olympicResult.Medals[2]) betterCountryCount++;
            }

            return betterCountryCount + 1;
        }

        private void CalculateOrder()
        {
            foreach (var item in results)
            {
                item.Position = CalculatePosition(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xlApp = new Excel.Application();
            xlWB = xlApp.Workbooks.Add(Missing.Value);
            xlSheet = xlWB.ActiveSheet;

            CreateTable();
            xlApp.Visible = true;
            xlApp.UserControl=true;
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Helyezés", "Ország", "Arany", "Ezüst", "Bronz"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            var eredmeny3 = from x in results
                            where x.Year == (int)comboBox1.SelectedItem
                            orderby x.Position
                            select x;

            var counter = 2;
            foreach (var item in eredmeny3)
            {
                xlSheet.Cells[counter, 1] = item.Position;
                xlSheet.Cells[counter, 2] = item.Country;
                for (int i = 0; i < 3; i++)
                {
                    xlSheet.Cells[counter, i + 3] = item.Medals[i];
                }
                counter++;
            }
        }
    }
}
