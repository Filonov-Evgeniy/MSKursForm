using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSKursForms
{
    public partial class ExperimentForm : Form
    {
        Form1 form;
        Config config;
        public ExperimentForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            config = Config.getInstance();

            u1TextBox.Text = config.experimentU1.ToString();
            deltaU1TextBox.Text = config.experimentDelta1.ToString();
            u2TextBox.Text = config.experimentU2.ToString();
            deltaU2TextBox.Text = config.experimentDelta2.ToString();
        }

        private void runExperiment_Click(object sender, EventArgs e)
        {
            Dictionary<(double, double), (double, double)> experiment = doExperiments(Convert.ToDouble(u1TextBox.Text.ToString()), Convert.ToDouble(deltaU1TextBox.Text.ToString()),
                Convert.ToDouble(u2TextBox.Text.ToString()), Convert.ToDouble(deltaU2TextBox.Text.ToString()));


        }

        private Dictionary<(double, double), (double, double)> doExperiments(double u1, double dU1, double u2, double dU2)
        {
            Dictionary<(double, double), (double, double)> experiment = new Dictionary<(double, double), (double, double)>();

            EngineStart engine = new EngineStart(u2 - dU2, u1 + dU1);
            experiment.Add((u2 - dU2, u1 + dU1), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2 - dU2, u1 - dU1);
            experiment.Add((u2 - dU2, u1 - dU1), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2 + dU2, u1 + dU1);
            experiment.Add((u2 + dU2, u1 + dU1), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2 - dU2, u1 - dU1);
            experiment.Add((u2 - dU2, u1 - dU1), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            return experiment;
        }

        private double[] varianceEstimation(Dictionary<(double, double), (double, double)> experiment)
        {
            double[] s = new double[4];

            return s;
        }
    }
}
