namespace MSKursForms
{
    partial class Settings
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
            lambdaTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dtTextBox = new TextBox();
            label4 = new Label();
            u1TextBox = new TextBox();
            label5 = new Label();
            u2TextBox = new TextBox();
            label6 = new Label();
            stepTextBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            Back = new Button();
            SaveButton = new Button();
            systemRunTextBox = new TextBox();
            label10 = new Label();
            stepSysTextBox = new TextBox();
            label11 = new Label();
            u2FinalTextBox = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // lambdaTextBox
            // 
            lambdaTextBox.Location = new Point(437, 19);
            lambdaTextBox.Name = "lambdaTextBox";
            lambdaTextBox.Size = new Size(100, 23);
            lambdaTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(186, 15);
            label1.TabIndex = 1;
            label1.Text = "Интенсивность входного потока";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(209, 15);
            label2.TabIndex = 3;
            label2.Text = "Время пребывания заявки в системе";
            // 
            // dtTextBox
            // 
            dtTextBox.Location = new Point(437, 51);
            dtTextBox.Name = "dtTextBox";
            dtTextBox.Size = new Size(100, 23);
            dtTextBox.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 88);
            label4.Name = "label4";
            label4.Size = new Size(257, 15);
            label4.TabIndex = 7;
            label4.Text = "Интенсивность потока обслуживания фазы 1";
            // 
            // u1TextBox
            // 
            u1TextBox.Location = new Point(437, 80);
            u1TextBox.Name = "u1TextBox";
            u1TextBox.Size = new Size(100, 23);
            u1TextBox.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 116);
            label5.Name = "label5";
            label5.Size = new Size(257, 15);
            label5.TabIndex = 9;
            label5.Text = "Интенсивность потока обслуживания фазы 2";
            // 
            // u2TextBox
            // 
            u2TextBox.Location = new Point(437, 108);
            u2TextBox.Name = "u2TextBox";
            u2TextBox.Size = new Size(100, 23);
            u2TextBox.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(348, 169);
            label6.Name = "label6";
            label6.Size = new Size(29, 15);
            label6.TabIndex = 11;
            label6.Text = "Шаг";
            // 
            // stepTextBox
            // 
            stepTextBox.Location = new Point(437, 166);
            stepTextBox.Name = "stepTextBox";
            stepTextBox.Size = new Size(100, 23);
            stepTextBox.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(348, 22);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 13;
            label7.Text = "lambda";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(348, 88);
            label8.Name = "label8";
            label8.Size = new Size(20, 15);
            label8.TabIndex = 15;
            label8.Text = "u1";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(348, 116);
            label9.Name = "label9";
            label9.Size = new Size(82, 15);
            label9.TabIndex = 16;
            label9.Text = "u2 начальное";
            // 
            // Back
            // 
            Back.Location = new Point(504, 296);
            Back.Name = "Back";
            Back.Size = new Size(75, 23);
            Back.TabIndex = 17;
            Back.Text = "Назад";
            Back.UseVisualStyleBackColor = true;
            Back.Click += Back_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(12, 296);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 18;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // systemRunTextBox
            // 
            systemRunTextBox.Location = new Point(437, 195);
            systemRunTextBox.Name = "systemRunTextBox";
            systemRunTextBox.Size = new Size(100, 23);
            systemRunTextBox.TabIndex = 19;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(294, 198);
            label10.Name = "label10";
            label10.Size = new Size(137, 15);
            label10.TabIndex = 20;
            label10.Text = "Время работы системы";
            // 
            // stepSysTextBox
            // 
            stepSysTextBox.Location = new Point(437, 224);
            stepSysTextBox.Name = "stepSysTextBox";
            stepSysTextBox.Size = new Size(100, 23);
            stepSysTextBox.TabIndex = 21;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(315, 227);
            label11.Name = "label11";
            label11.Size = new Size(80, 15);
            label11.TabIndex = 22;
            label11.Text = "Шаг системы";
            // 
            // u2FinalTextBox
            // 
            u2FinalTextBox.Location = new Point(437, 137);
            u2FinalTextBox.Name = "u2FinalTextBox";
            u2FinalTextBox.Size = new Size(100, 23);
            u2FinalTextBox.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(348, 140);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 24;
            label3.Text = "u2 конечное";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 331);
            Controls.Add(label3);
            Controls.Add(u2FinalTextBox);
            Controls.Add(label11);
            Controls.Add(stepSysTextBox);
            Controls.Add(label10);
            Controls.Add(systemRunTextBox);
            Controls.Add(SaveButton);
            Controls.Add(Back);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(stepTextBox);
            Controls.Add(label5);
            Controls.Add(u2TextBox);
            Controls.Add(label4);
            Controls.Add(u1TextBox);
            Controls.Add(label2);
            Controls.Add(dtTextBox);
            Controls.Add(label1);
            Controls.Add(lambdaTextBox);
            Name = "Settings";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox lambdaTextBox;
        private Label label1;
        private Label label2;
        private TextBox dtTextBox;
        private Label label4;
        private TextBox u1TextBox;
        private Label label5;
        private TextBox u2TextBox;
        private Label label6;
        private TextBox stepTextBox;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button Back;
        private Button SaveButton;
        private TextBox systemRunTextBox;
        private Label label10;
        private TextBox stepSysTextBox;
        private Label label11;
        private TextBox u2FinalTextBox;
        private Label label3;
    }
}