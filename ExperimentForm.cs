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
            double u1p = Convert.ToDouble(u1TextBox.Text.ToString()) + Convert.ToDouble(deltaU1TextBox.Text.ToString());
            double u1m = Convert.ToDouble(u1TextBox.Text.ToString()) - Convert.ToDouble(deltaU1TextBox.Text.ToString());
            double u2p = Convert.ToDouble(u2TextBox.Text.ToString()) + Convert.ToDouble(deltaU2TextBox.Text.ToString());
            double u2m = Convert.ToDouble(u2TextBox.Text.ToString()) - Convert.ToDouble(deltaU2TextBox.Text.ToString());

            (double, double)[] experimentsKeys = 
            {
                (u2m, u1p),
                (u2m, u1m),
                (u2p, u1p),
                (u2p, u1m),
            };

            Dictionary<(double, double), (double, double)> experiment = doExperiments(Convert.ToDouble(u1TextBox.Text.ToString()), Convert.ToDouble(deltaU1TextBox.Text.ToString()),
                Convert.ToDouble(u2TextBox.Text.ToString()), Convert.ToDouble(deltaU2TextBox.Text.ToString()));

            double[] s = varianceEstimation(experiment, u1p, u1m, u2p, u2m);
            
            double kohrenCoef = calculateKohrenCoef(s);
            if (kohrenCoef > Convert.ToDouble(KohrenTableValTextBox.Text.ToString()))
            {
                while (kohrenCoef > Convert.ToDouble(KohrenTableValTextBox.Text.ToString()))
                {
                    Dictionary<(double, double), (double, double)> experimentRepeat = doExperiments(Convert.ToDouble(u1TextBox.Text.ToString()), Convert.ToDouble(deltaU1TextBox.Text.ToString()),
                Convert.ToDouble(u2TextBox.Text.ToString()), Convert.ToDouble(deltaU2TextBox.Text.ToString()));
                    double[] sRepeat = varianceEstimation(experiment, u1p, u1m, u2p, u2m);

                    int indexOfMaxS = Array.IndexOf(s, s.Max());
                    s[indexOfMaxS] = sRepeat[indexOfMaxS];
                    kohrenCoef = calculateKohrenCoef(s);
                }
            }
            calculatedKohrenTextBox.Text = kohrenCoef.ToString();
            KohrenStatus.Text = "Пройдена успешно";
            showS(s);

            double[] a = calculateRegressionCoeff(experiment, experimentsKeys);
            showRegressionCoeff(a);

            RegressionEquation.Text = $"Pоб = {a[0].ToString("F3")} + {a[1].ToString("F3")}Xi + {a[2].ToString("F3")}XiXj + {a[3].ToString("F3")}Xj^2";
        }

        private void showRegressionCoeff(double[] a)
        {
            a0TextBox.Text = (a[0]).ToString("F3");
            a1TextBox.Text = (a[1]).ToString("F3");
            a2TextBox.Text = (a[2]).ToString("F3");
            a3TextBox.Text = (a[3]).ToString("F3");
        }

        private double[] calculateRegressionCoeff(Dictionary<(double, double), (double, double)> experiment, (double, double)[] keys)
        {
            double[] a = new double[4];
            double[] expMiddle = new double[4];

            for (int i = 0; i < expMiddle.Length; i++)
            {
                expMiddle[i] = (experiment[keys[i]].Item1 + experiment[keys[i]].Item2) / 2;
            }

            a[0] = expMiddle.Sum() / expMiddle.Length;

            a[1] = (keys[0].Item2 * expMiddle[0] + keys[1].Item2 * expMiddle[1] + keys[2].Item2 * expMiddle[2] + keys[3].Item2 * expMiddle[3]) / 4;

            a[2] = (keys[0].Item1 * expMiddle[0] + keys[1].Item1 * expMiddle[1] + keys[2].Item1 * expMiddle[2] + keys[3].Item1 * expMiddle[3]) / 4;
            a[3] = (keys[0].Item2 * keys[0].Item1 * expMiddle[0] + keys[1].Item2 * keys[1].Item1 * expMiddle[1] + keys[2].Item2 * keys[2].Item1 * expMiddle[2] + keys[3].Item2 * keys[3].Item1 * expMiddle[3]) / 4;

            return a;
        }

        public double calculateKohrenCoef(double[] s)
        {
            double kohrenCoef = s.Max()/s.Sum();
            return kohrenCoef;
        }

        private Dictionary<(double, double), (double, double)> doExperiments(double u1, double dU1, double u2, double dU2)
        {
            Dictionary<(double, double), (double, double)> experiment = new Dictionary<(double, double), (double, double)>();

            double u1p = u1 + dU1;
            double u1m = u1 - dU1;
            double u2p = u2 + dU2;
            double u2m = u2 - dU2;

            EngineStart engine = new EngineStart(u2m, u1p);
            experiment.Add((u2m, u1p), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2 - dU2, u1 - dU1);
            experiment.Add((u2m, u1m), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2p, u1p);
            experiment.Add((u2p, u1p), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            engine = new EngineStart(u2p, u1m);
            experiment.Add((u2p, u1m), (engine.start().chanceOfSuccess, engine.start().chanceOfSuccess));

            return experiment;
        }

        private double[] varianceEstimation(Dictionary<(double, double), (double, double)> experiment, double u1p, double u1m, double u2p, double u2m)
        {
            double[] s = new double[4];

            double exp0Middle = (experiment[(u2m, u1p)].Item1 + experiment[(u2m, u1p)].Item2) / 2;
            double exp1Middle = (experiment[(u2m, u1m)].Item1 + experiment[(u2m, u1m)].Item2) / 2;
            double exp2Middle = (experiment[(u2p, u1p)].Item1 + experiment[(u2p, u1p)].Item2) / 2;
            double exp3Middle = (experiment[(u2p, u1m)].Item1 + experiment[(u2p, u1m)].Item2) / 2;

            s[0] = Math.Pow(experiment[(u2m, u1p)].Item1 - exp0Middle, 2) + Math.Pow(experiment[(u2m, u1p)].Item2 - exp0Middle, 2);
            s[1] = Math.Pow(experiment[(u2m, u1m)].Item1 - exp0Middle, 2) + Math.Pow(experiment[(u2m, u1p)].Item2 - exp0Middle, 2);
            s[2] = Math.Pow(experiment[(u2p, u1p)].Item1 - exp0Middle, 2) + Math.Pow(experiment[(u2p, u1p)].Item2 - exp0Middle, 2);
            s[3] = Math.Pow(experiment[(u2p, u1m)].Item1 - exp0Middle, 2) + Math.Pow(experiment[(u2p, u1m)].Item2 - exp0Middle, 2);

            return s;
        }

        private void showS(double[] s)
        {
            S1TextBox.Text = s[0].ToString();
            S2TextBox.Text = s[1].ToString();
            S3TextBox.Text = s[2].ToString();
            S4TextBox.Text = s[3].ToString();
        }
    }
}
