using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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

            double Sy = calculateReproducibilityVariance(s);
            SyTextBox.Text = Sy.ToString();

            double Sai = calculateSai(Sy);
            SaiTextBox.Text = Sai.ToString();

            double[] studentCoeff = calculateStudentCoef(a, Sai);
            bool[] conclusionBool = new bool[4];
            showStudentCoeff(studentCoeff, ref conclusionBool);

            RegressionEquation.Text = buildRegressionEquation(a, conclusionBool);

            //double Sadk = calculateSadk(experiment, experimentsKeys);
            double Sadk = calculateSadk(a, experimentsKeys, experiment);

            SadkTextBox.Text = Sadk.ToString();

            double fisherCoeff = calculateFisher(Sy, Sadk);

            if (fisherCoeff > Convert.ToDouble(FisherTableTextBox.Text.ToString()))
            {
                runExperiment_Click(sender, e);
            }
            else
            {
                fisherExpTextBox.Text = fisherCoeff.ToString();
                fisherConclusion.Text = "Пройден";
                buildGraphicTest(experiment, experimentsKeys, a);
            }

            fillMatrix(experiment, experimentsKeys);
        }

        private void fillMatrix(Dictionary<(double, double), (double, double)> experiment, (double, double)[] keys)
        {
            u11textBox.Text = keys[0].Item2.ToString();
            u21textBox.Text = keys[0].Item1.ToString();
            u12textBox.Text = keys[1].Item2.ToString();
            u22textBox.Text = keys[1].Item1.ToString();
            u13textBox.Text = keys[2].Item2.ToString();
            u23textBox.Text = keys[2].Item1.ToString();
            u14textBox.Text = keys[3].Item2.ToString();
            u24textBox.Text = keys[3].Item1.ToString();

            y11textBox.Text = experiment[keys[0]].Item1.ToString();
            y21textBox.Text = experiment[keys[0]].Item2.ToString();
            y12textBox.Text = experiment[keys[1]].Item1.ToString();
            y22textBox.Text = experiment[keys[1]].Item2.ToString();
            y13textBox.Text = experiment[keys[2]].Item1.ToString();
            y23textBox.Text = experiment[keys[2]].Item2.ToString();
            y14textBox.Text = experiment[keys[3]].Item1.ToString();
            y24textBox.Text = experiment[keys[3]].Item2.ToString();

            Y1textBox.Text = ((experiment[keys[0]].Item1 + experiment[keys[0]].Item2) / 2).ToString();
            Y2textBox.Text = ((experiment[keys[1]].Item1 + experiment[keys[1]].Item2) / 2).ToString();
            Y3textBox.Text = ((experiment[keys[2]].Item1 + experiment[keys[2]].Item2) / 2).ToString();
            Y4textBox.Text = ((experiment[keys[3]].Item1 + experiment[keys[3]].Item2) / 2).ToString();
        }

        private void buildGraphicTest(Dictionary<(double, double), (double, double)> experiment, (double, double)[] keys, double[] a)
        {
            double[] expMiddle = new double[4];

            for (int i = 0; i < expMiddle.Length; i++)
            {
                expMiddle[i] = (experiment[keys[i]].Item1 + experiment[keys[i]].Item2) / (double)2;
            }

            double[] calcPoints = new double[4];

            for (int i = 0; i < keys.Length; i++)
            {
                calcPoints[i] = a[0] + a[1] * keys[i].Item2 + a[2] * keys[i].Item1 + a[3] * keys[i].Item2 * keys[i].Item1;
            }

            List<DataPoint> pointsMiddle = new List<DataPoint>();
            List<DataPoint> pointsExp1 = new List<DataPoint>();

            for (int i = 0; i < expMiddle.Length; i++)
            {
                pointsMiddle.Add(new DataPoint(i, expMiddle[i]));
                pointsExp1.Add(new DataPoint(i, calcPoints[i]));
            }

            var plotModel = new PlotModel
            {
                Title = "Model"
            };

            var lineSeriesMiddle = new LineSeries
            {
                Title = "Экспериментальное",
                ItemsSource = pointsMiddle
            };
            var lineSeries1 = new LineSeries
            {
                Title = "Расчитанное",
                ItemsSource = pointsExp1
            };

            plotModel.Series.Add(lineSeriesMiddle);
            plotModel.Series.Add(lineSeries1);

            plotView1.Model = plotModel;
        }

        private void buildGraphic(Dictionary<(double, double), (double, double)> experiment, (double, double)[] keys)
        {
            double[] expMiddle = new double[4];

            for (int i = 0; i < expMiddle.Length; i++)
            {
                expMiddle[i] = (experiment[keys[i]].Item1 + experiment[keys[i]].Item2) / (double)2;
            }

            double[] expGraph1 = new double[4];
            for (int i = 0; i < expGraph1.Length; i++)
            {
                expGraph1[i] = experiment[keys[i]].Item1;
            }

            double[] expGraph2 = new double[4];
            for (int i = 0; i < expGraph1.Length; i++)
            {
                expGraph2[i] = experiment[keys[i]].Item2;
            }

            List<DataPoint> pointsMiddle = new List<DataPoint>();
            List<DataPoint> pointsExp1 = new List<DataPoint>();
            List<DataPoint> pointsExp2 = new List<DataPoint>();

            for (int i = 0; i < expMiddle.Length; i++)
            {
                double chance1 = experiment[keys[i]].Item1;
                double chance2 = experiment[keys[i]].Item2;
                pointsMiddle.Add(new DataPoint(i, expMiddle[i]));
                pointsExp1.Add(new DataPoint(i, chance1));
                pointsExp2.Add(new DataPoint(i, chance2));
            }

            var plotModel = new PlotModel
            {
                Title = "Model"
            };

            var lineSeriesMiddle = new LineSeries
            {
                Title = "LineSeries",
                ItemsSource = pointsMiddle
            };
            var lineSeries1 = new LineSeries
            {
                Title = "LineSeries",
                ItemsSource = pointsExp1
            };
            var lineSeries2 = new LineSeries
            {
                Title = "LineSeries",
                ItemsSource = pointsExp2
            };

            plotModel.Series.Add(lineSeriesMiddle);
            plotModel.Series.Add(lineSeries1);
            plotModel.Series.Add(lineSeries2);

            plotView1.Model = plotModel;
        }

        private double calculateFisher(double Sy, double Sadk)
        {
            return Sy / Sadk;
        }

        private double calculateSadk(double[] a, (double, double)[] keys, Dictionary<(double, double), (double, double)> experiment)
        {
            double[] expMiddle = new double[4];

            for (int i = 0; i < expMiddle.Length; i++)
            {
                expMiddle[i] = (experiment[keys[i]].Item1 + experiment[keys[i]].Item2) / (double)2;
            }

            var Sadk = 0.0;
            var responseFunctionValues = new double[4];

            for (var i = 0; i < responseFunctionValues.Length; i++)
            {
                responseFunctionValues[i] = a[0]
                                            + a[1] * keys[i].Item2
                                            + a[2] * keys[i].Item1;

                Sadk += Math.Pow(expMiddle[i] - responseFunctionValues[i], 2);
            }

            return Sadk;
        }
        //private double calculateSadk(Dictionary<(double, double), (double, double)> experiment, (double, double)[] keys)
        //{
        //    double[] expMiddle = new double[4];

        //    for (int i = 0; i < expMiddle.Length; i++)
        //    {
        //        expMiddle[i] = (experiment[keys[i]].Item1 + experiment[keys[i]].Item2) / (double)2;
        //    }

        //    double Sadk = (Math.Pow(expMiddle[0] - experiment[keys[0]].Item2, 2)) + Math.Pow(expMiddle[1] - experiment[keys[1]].Item2, 2) + Math.Pow(expMiddle[2]
        //        - experiment[keys[2]].Item2, 2) + Math.Pow(expMiddle[3] - experiment[keys[3]].Item2, 2);

        //    return Sadk;
        //}

        private string buildRegressionEquation(double[] a, bool[] conclusion)
        {
            string equation = "Pоб = ";
            List<string> words = new List<String>();

            for (int i = 0; i < a.Length; i++)
            {
                if (conclusion[i])
                {
                    words.Add($"{a[i].ToString("F4")}");
                }
            }

            equation += string.Join("+", words);
            return equation;
        }

        private void showStudentCoeff(double[] studentCoeff, ref bool[] conclBool)
        {
            StudentExp0TextBox.Text = studentCoeff[0].ToString();
            StudentExp1TextBox.Text = studentCoeff[1].ToString();
            StudentExp2TextBox.Text = studentCoeff[2].ToString();
            StudentExp3TextBox.Text = studentCoeff[3].ToString();

            showConclusion(Conclusion0TextBox, studentCoeff[0], ref conclBool[0]);
            showConclusion(Conclusion1TextBox, studentCoeff[1], ref conclBool[2]);
            showConclusion(Conclusion2TextBox, studentCoeff[2], ref conclBool[2]);
            showConclusion(Conclusion3TextBox, studentCoeff[3], ref conclBool[3]);

            void showConclusion(TextBox conclusion, double coeff, ref bool conclBool)
            {
                if (coeff > Convert.ToDouble(StudentTableTextBox.Text))
                {
                    conclusion.Text = "Значим";
                    conclBool = true;
                }
                else
                {
                    conclusion.Text = "Не значим";
                    conclBool = false;
                }
            }
        }

        private double[] calculateStudentCoef(double[] a, double Sai)
        {
            double[] studentCoeff = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                studentCoeff[i] = Math.Abs(a[i]) / Math.Sqrt(Sai);
            }
            return studentCoeff;
        }

        //Дисперсия оценок коэфициентов дисперсии
        private double calculateSai(double Sy)
        {
            return Sy / (4 * 2);
        }

        //Дисперсия воспроизводимости
        private double calculateReproducibilityVariance(double[] s)
        {
            double Sy = s.Sum() / s.Length;
            return Sy;
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
            double kohrenCoef = s.Max() / s.Sum();
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
            EngineStart engine1 = new EngineStart(u2m, u1p);
            experiment.Add((u2m, u1p), (engine.start().chanceOfSuccess, engine1.start().chanceOfSuccess));

            engine = new EngineStart(u2m, u1m);
            engine1 = new EngineStart(u2m, u1m);
            experiment.Add((u2m, u1m), (engine.start().chanceOfSuccess, engine1.start().chanceOfSuccess));

            engine = new EngineStart(u2p, u1p);
            engine1 = new EngineStart(u2p, u1p);
            experiment.Add((u2p, u1p), (engine.start().chanceOfSuccess, engine1.start().chanceOfSuccess));

            engine = new EngineStart(u2p, u1m);
            engine1 = new EngineStart(u2p, u1m);
            experiment.Add((u2p, u1m), (engine.start().chanceOfSuccess, engine1.start().chanceOfSuccess));

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

        private void Cansel_Click(object sender, EventArgs e)
        {
            form.Show();
            this.Close();
        }
    }
}
