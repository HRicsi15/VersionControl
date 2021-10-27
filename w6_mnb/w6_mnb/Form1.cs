using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using w6_mnb.Entities;
using w6_mnb.MnbServiceReference;

namespace w6_mnb
{
    public partial class Form1 : Form
    {
        BindingList <RateData> Rates = new BindingList<RateData>();

        public string result2;
        public Form1()
        {
            InitializeComponent();
            
            GetExchangeRates();

            dataGridView1.DataSource = Rates;

            XML();

            //Chart();
        }

        private void GetExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);//bemenetis függvény

            var result = response.GetExchangeRatesResult;//az eredmény tulajdonsága

             result2 = result;
        }

        private void XML()
        {
            //xml fgv
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result2);

            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData rateData = new RateData();

                rateData.Date = DateTime.Parse(item.GetAttribute("date"));

                var childItem = (XmlElement)item.ChildNodes[0];
                rateData.Currency = childItem.GetAttribute("curr");

                var unit = decimal.Parse(childItem.GetAttribute("unit"));
                var value = decimal.Parse(childItem.InnerText);
                if (unit != 0) rateData.Value = value / unit;

                Rates.Add(rateData);
            }

        }

        /*private void Chart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }*/
    }
}
