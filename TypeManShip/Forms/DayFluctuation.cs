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

namespace TypeManShip
{
    public partial class DayFluctuation : Form
    {
        MainController controller;
        public DayFluctuation()
        {
            InitializeComponent();
        }
        public DayFluctuation(ref MainController m_controller)
        {
            InitializeComponent();
            controller = m_controller;
        }


        private void DayFluctuation_Load(object sender, EventArgs e)
        {
            Draw_Excpected_Values();
            Draw_Dispersion_Values();
        }
        private void Draw_Excpected_Values()
        {
            chart1.Series.Clear();
            Series series = new Series();
            Dictionary<string, float> bars = controller.PrepareDayExpChart();
            series.ChartType = SeriesChartType.Column;
            series.Name = "Мат. Ожидание";
            chart1.Series.Add(series);
            chart1.Series["Мат. Ожидание"].Points.Clear();
            foreach (KeyValuePair<string, float> bar in bars)
                chart1.Series["Мат. Ожидание"].Points.AddXY(bar.Key, bar.Value);
        }
        private void Draw_Dispersion_Values()
        {
            chart2.Series.Clear();
            Series series = new Series();
            Dictionary<string, float> bars = controller.PrepareDayDisChart();
            series.ChartType = SeriesChartType.Column;
            series.Name = "Дисперсия";
            chart2.Series.Add(series);
            chart2.Series["Дисперсия"].Points.Clear();
            foreach (KeyValuePair<string, float> bar in bars)
                chart2.Series["Дисперсия"].Points.AddXY(bar.Key, bar.Value);
        }
    }
}
