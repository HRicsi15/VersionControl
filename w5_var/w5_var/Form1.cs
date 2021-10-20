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

            //a. Portfóliónk elemszáma:
            int elemszám = Portfolio.Count();
            //A Count() bálrmilyen megszámlálható listára alkalmazható.

            //b. A portfólióban szereplő részvények darabszáma: 
            decimal részvényekSzáma = (from x in Portfolio select x.Volume).Sum();
            MessageBox.Show(string.Format("Részvények száma: {0}", részvényekSzáma));
            //Először egy listába kigyűjtjük csak a darabszámokat, majd az egész bezárójlezett listát summázzuk. 
            //(A zárójelben lévő LINQ egy int-ekből álló listát ad, mert a Count tulajdonság int típusú.)
            //Működik a Min(), Max(), Average(), stb. is.

            //c. A legrégebbi kereskedési nap:
            DateTime minDátum = (from x in ticks select x.TradingDay).Min();

            //d. A legutolsó kereskedési nap:
            DateTime maxDátum = (from x in ticks select x.TradingDay).Max();

            //e. A két dátum közt eltelt idő napokban -- két DateTime típusú objektum különbsége TimeSpan típusú eredményt ad.
            //A TimeSpan Day tulajdonsága megadja az időtartam napjainak számát. (Nem kell vacakolni a szökőévekkel stb.)
            int elteltNapokSzáma = (maxDátum - minDátum).Days;

            //f. Az OTP legrégebbi kereskedési napja: 
            DateTime optMinDátum = (from x in ticks where x.Index == "OTP" select x.TradingDay).Min();

            //g. Össze is lehet kapcsolni dolgokat, ez már bonyolultabb:
            var kapcsolt =
                from
                    x in ticks
                join
                    y in Portfolio on x.Index equals y.Index//csak azokat amelyek benne vannak a Portfolioban
                select new
                {
                    Index = x.Index,
                    Date = x.TradingDay,
                    Value = x.Price,
                    Volume = y.Volume
                };
            dataGridView1.DataSource = kapcsolt.ToList();
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

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in ticks //helyi lista 
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
