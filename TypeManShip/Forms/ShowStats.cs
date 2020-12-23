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
    public partial class ShowStats : Form
    {
        MainController controller;
        public ShowStats()
        {
            InitializeComponent();
        }
        public ShowStats(ref MainController m_controller)
        {
            controller = m_controller;
            InitializeComponent();
        }
        private void Draw_Hist()
        {
            chart1.Series.Clear();
            Series series = new Series();
            List<HistogramBar> bars = controller.PrepareForChart();
            series.ChartType = SeriesChartType.Column;
            series.Name = "Скорость\nввода";
            chart1.Series.Add(series);
            chart1.Series["Скорость\nввода"].Points.Clear();
            for (int i = 0; i < bars.Count; i++)
            {
                chart1.Series["Скорость\nввода"].Points.AddXY(bars[i].speed, bars[i].number);
            }
        }
        private void Draw_Chart()
        {
            chart2.Series.Clear();
            Series series = new Series();
            List<long> bars = controller.PreapareDynType();
            series.ChartType = SeriesChartType.Line;
            series.Name = "Динамика\nввода";
            chart2.Series.Add(series);
            for (int i = 0; i < bars.Count; i++)
            {
                chart2.Series["Динамика\nввода"].Points.AddXY(i, bars[i]);
            }
            chart2.ResetAutoValues();
        }

        private void Draw_Hold_Chart()
        {
            chart3.Series.Clear();
            Series series = new Series();
            List<long> bars = controller.PreapareDynHold();
            series.ChartType = SeriesChartType.Line;
            series.Name = "Время\nудержания";
            chart3.Series.Add(series);
            for (int i = 0; i < bars.Count; i++)
            {
                chart3.Series["Время\nудержания"].Points.AddXY(i, bars[i]);
            }
        }

        private void ShowStats_Load(object sender, EventArgs e)
        {
            Draw_Hist();
            Draw_Chart();
            Draw_Hold_Chart();
            label1.Text = string.Format("exp speed: {0:f3} keys /sec.", controller.Expected_speed);
            label2.Text = string.Format("dispersion: {0:f3} keys/sec.", controller.Dispersion);
        }
    }
}
