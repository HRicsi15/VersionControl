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
        PortfolioEntities context = new PortfolioEntities();

        List<Entities.PortfolioItem> Portfolio = new List<Entities.PortfolioItem>();

        public Form1()
        {
            InitializeComponent();
            /*context.Ticks.Load();
            dataGridView1.DataSource = context.Ticks.Local;*/ //talán lehet így is, lehet ideiglenes 
            ticks = context.Ticks.ToList();
            dataGridView1.DataSource = ticks;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new Entities.PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new Entities.PortfolioItem() {Index="ZWACK", Volume = 10 });
            Portfolio.Add(new Entities.PortfolioItem() { Index = "ELMU", Volume = 10 });
            
            /*ugyanaz a kettő
            Entities.PortfolioItem p = new Entities.PortfolioItem();
            p.Index = "OTP";
            p.Volume = 10;
            Portfolio.Add(p);*/


            dataGridView2.DataSource = Portfolio;
        }
       
    }
}
