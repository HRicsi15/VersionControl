using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Excel_generalasa_w04
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        
        public Form1()
        {
            InitializeComponent();
            LoadData();

          
        }

        private void LoadData()
        {
            List<Flat> Flats = context.Flats.ToList();//memoryba másoljuk a szerverről
        }
    }
}
