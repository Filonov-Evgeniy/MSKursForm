namespace MSKursForms
{
    partial class ExperimentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            runExperiment = new Button();
            u1TextBox = new TextBox();
            deltaU1TextBox = new TextBox();
            u2TextBox = new TextBox();
            deltaU2TextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // runExperiment
            // 
            runExperiment.Location = new Point(12, 128);
            runExperiment.Name = "runExperiment";
            runExperiment.Size = new Size(154, 23);
            runExperiment.TabIndex = 1;
            runExperiment.Text = "Начать эксперимент";
            runExperiment.UseVisualStyleBackColor = true;
            runExperiment.Click += runExperiment_Click;
            // 
            // u1TextBox
            // 
            u1TextBox.Location = new Point(12, 12);
            u1TextBox.Name = "u1TextBox";
            u1TextBox.Size = new Size(100, 23);
            u1TextBox.TabIndex = 2;
            // 
            // deltaU1TextBox
            // 
            deltaU1TextBox.Location = new Point(12, 41);
            deltaU1TextBox.Name = "deltaU1TextBox";
            deltaU1TextBox.Size = new Size(100, 23);
            deltaU1TextBox.TabIndex = 3;
            // 
            // u2TextBox
            // 
            u2TextBox.Location = new Point(12, 70);
            u2TextBox.Name = "u2TextBox";
            u2TextBox.Size = new Size(100, 23);
            u2TextBox.TabIndex = 4;
            // 
            // deltaU2TextBox
            // 
            deltaU2TextBox.Location = new Point(12, 99);
            deltaU2TextBox.Name = "deltaU2TextBox";
            deltaU2TextBox.Size = new Size(100, 23);
            deltaU2TextBox.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 16);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 6;
            label1.Text = "u1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 44);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 7;
            label2.Text = "delta u1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(118, 73);
            label3.Name = "label3";
            label3.Size = new Size(20, 15);
            label3.TabIndex = 8;
            label3.Text = "u2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(118, 99);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 9;
            label4.Text = "delta u2";
            // 
            // ExperimentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(deltaU2TextBox);
            Controls.Add(u2TextBox);
            Controls.Add(deltaU1TextBox);
            Controls.Add(u1TextBox);
            Controls.Add(runExperiment);
            Name = "ExperimentForm";
            Text = "ExperimentForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button runExperiment;
        private TextBox u1TextBox;
        private TextBox deltaU1TextBox;
        private TextBox u2TextBox;
        private TextBox deltaU2TextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}