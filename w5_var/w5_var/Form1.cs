using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace w5_var
{
    public partial class Form1 : Form
    {
        List<Tick> ticks;
       // PortfolioEntities context = new PortfolioEntities();

        public Form1()
        {
            InitializeComponent();
            /*context.Ticks.Load();
            dataGridView1.DataSource = context.Ticks.Local;*/ //talán lehet így is, lehet ideiglenes 
          //  ticks = context.Ticks.ToList();
           // dataGridView1.DataSource = ticks;
        }
    }
}
