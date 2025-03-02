namespace MSKursForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Settings = new Button();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            StartModelling = new Button();
            Experiment = new Button();
            SuspendLayout();
            // 
            // Settings
            // 
            Settings.Location = new Point(725, 573);
            Settings.Name = "Settings";
            Settings.Size = new Size(75, 23);
            Settings.TabIndex = 0;
            Settings.Text = "Настройки";
            Settings.UseVisualStyleBackColor = true;
            Settings.Click += Settings_Click;
            // 
            // plotView1
            // 
            plotView1.BackColor = SystemColors.ControlLight;
            plotView1.Location = new Point(12, 12);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(897, 538);
            plotView1.TabIndex = 1;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // StartModelling
            // 
            StartModelling.Location = new Point(806, 573);
            StartModelling.Name = "StartModelling";
            StartModelling.Size = new Size(103, 23);
            StartModelling.TabIndex = 2;
            StartModelling.Text = "Моделировать";
            StartModelling.UseVisualStyleBackColor = true;
            StartModelling.Click += StartModelling_Click;
            // 
            // Experiment
            // 
            Experiment.Location = new Point(12, 573);
            Experiment.Name = "Experiment";
            Experiment.Size = new Size(97, 23);
            Experiment.TabIndex = 3;
            Experiment.Text = "Эксперимент";
            Experiment.UseVisualStyleBackColor = true;
            Experiment.Click += Experiment_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(920, 604);
            Controls.Add(Experiment);
            Controls.Add(StartModelling);
            Controls.Add(plotView1);
            Controls.Add(Settings);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button Settings;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private Button StartModelling;
        private Button Experiment;
    }
}
