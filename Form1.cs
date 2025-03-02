using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MSKursForms
{
    public partial class Form1 : Form
    {
        Config config = Config.getInstance();
        public Form1()
        {
            InitializeComponent();
        }

        private void StartModelling_Click(object sender, EventArgs e)
        {
            Dictionary<double, Statistic> graphic = new Dictionary<double, Statistic>();

            double atThisTime = config.u2;

            while (atThisTime <= config.u2Final)
            {
                EngineStart model = new EngineStart(atThisTime, config.u1);
                graphic.Add(atThisTime, model.start());
                atThisTime += config.step;
            }

            List<DataPoint> points = new List<DataPoint>();
            foreach (var point in graphic)
            {
                double chance = (point.Value.orderGenerate - point.Value.orderDone) / point.Value.orderGenerate;
                points.Add(new DataPoint(point.Key, chance));
            }

            var plotModel = new PlotModel
            {
                Title = "Model"
            };

            var lineSeries = new LineSeries
            {
                Title = "LineSeries",
                ItemsSource = points
            };

            plotModel.Series.Add(lineSeries);
            plotModel.Axes.Add(getAxe("Вероятность отказа", AxisPosition.Left));
            plotModel.Axes.Add(getAxe("Значение потока обслуживания u2", AxisPosition.Bottom));

            plotView1.Model = plotModel;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this);
            settings.Show();
            this.Hide();
        }

        private LinearAxis getAxe(string title, AxisPosition pos)
        {
            LinearAxis axe = new LinearAxis
            {
                Title = title,
                Position = pos,
            };

            return axe;
        }

        private void Experiment_Click(object sender, EventArgs e)
        {
            ExperimentForm experimentForm = new ExperimentForm(this);
            this.Hide();
            experimentForm.Show();
        }
    }
}
