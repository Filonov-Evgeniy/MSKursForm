using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSKursForms
{
    public partial class Settings : Form
    {
        Form1 form;
        Config config = Config.getInstance();
        public Settings(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            this.config = config;

            lambdaTextBox.Text = config.lambda.ToString();
            dtTextBox.Text = config.dt.ToString();
            u1TextBox.Text = config.u1.ToString();
            u2TextBox.Text = config.u2.ToString();
            u2FinalTextBox.Text = config.u2Final.ToString();
            stepTextBox.Text = config.step.ToString();
            systemRunTextBox.Text = config.workTime.ToString();
            stepSysTextBox.Text = config.updateTime.ToString();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            form.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            config.lambda = Convert.ToDouble(lambdaTextBox.Text.ToString());
            config.dt = Convert.ToDouble(dtTextBox.Text.ToString());
            config.u1 = Convert.ToDouble(u1TextBox.Text.ToString());
            config.u2 = Convert.ToDouble(u2TextBox.Text.ToString());
            config.u2Final = Convert.ToDouble(u2FinalTextBox.Text.ToString());
            config.step = Convert.ToDouble(stepTextBox.Text.ToString());
            config.workTime = Convert.ToDouble(systemRunTextBox.Text.ToString());
            config.updateTime = Convert.ToDouble(stepSysTextBox.Text.ToString());
        }
    }
}
