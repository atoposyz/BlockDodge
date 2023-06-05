namespace demo
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            button3 = new Button();
            label1 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            timeslacklimit = new Label();
            invincibilitylimit = new Label();
            sprintlimit = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            sprintlabel = new Label();
            invincibilitylabel = new Label();
            timeslacklabel = new Label();
            magnetlabel = new Label();
            shieldtext = new Label();
            magnetlimit = new Label();
            shieldlabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Size = new Size(1008, 601);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 543);
            panel1.Name = "panel1";
            panel1.Size = new Size(850, 55);
            panel1.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(266, 10);
            button2.Name = "button2";
            button2.Size = new Size(103, 42);
            button2.TabIndex = 1;
            button2.Text = "排行榜";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(58, 10);
            button1.Name = "button1";
            button1.Size = new Size(101, 42);
            button1.TabIndex = 0;
            button1.Text = "开始游戏";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(850, 534);
            panel2.TabIndex = 1;
            //panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.Controls.Add(button3);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(timeslacklimit);
            panel3.Controls.Add(invincibilitylimit);
            panel3.Controls.Add(sprintlimit);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(sprintlabel);
            panel3.Controls.Add(invincibilitylabel);
            panel3.Controls.Add(timeslacklabel);
            panel3.Controls.Add(magnetlabel);
            panel3.Controls.Add(shieldtext);
            panel3.Controls.Add(magnetlimit);
            panel3.Controls.Add(shieldlabel);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(859, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(146, 534);
            panel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Location = new Point(27, 458);
            button3.Name = "button3";
            button3.Size = new Size(88, 44);
            button3.TabIndex = 19;
            button3.Text = "切换模式";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.Location = new Point(37, 426);
            label1.Name = "label1";
            label1.Size = new Size(81, 29);
            label1.TabIndex = 0;
            label1.Text = "闯关模式";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(93, 371);
            label10.Name = "label10";
            label10.Size = new Size(25, 20);
            label10.TabIndex = 18;
            label10.Text = "0s";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(93, 332);
            label9.Name = "label9";
            label9.Size = new Size(25, 20);
            label9.TabIndex = 17;
            label9.Text = "0s";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(100, 278);
            label8.Name = "label8";
            label8.Size = new Size(25, 20);
            label8.TabIndex = 16;
            label8.Text = "0s";
            // 
            // timeslacklimit
            // 
            timeslacklimit.AutoSize = true;
            timeslacklimit.Location = new Point(100, 140);
            timeslacklimit.Name = "timeslacklimit";
            timeslacklimit.Size = new Size(25, 20);
            timeslacklimit.TabIndex = 15;
            timeslacklimit.Text = "0s";
            // 
            // invincibilitylimit
            // 
            invincibilitylimit.AutoSize = true;
            invincibilitylimit.Location = new Point(100, 181);
            invincibilitylimit.Name = "invincibilitylimit";
            invincibilitylimit.Size = new Size(25, 20);
            invincibilitylimit.TabIndex = 14;
            invincibilitylimit.Text = "0s";
            // 
            // sprintlimit
            // 
            sprintlimit.AutoSize = true;
            sprintlimit.Location = new Point(100, 230);
            sprintlimit.Name = "sprintlimit";
            sprintlimit.Size = new Size(25, 20);
            sprintlimit.TabIndex = 13;
            sprintlimit.Text = "0s";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 371);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 12;
            label4.Text = "夜行：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 332);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 11;
            label3.Text = "迅捷：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 278);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 10;
            label2.Text = "无畏：";
            // 
            // sprintlabel
            // 
            sprintlabel.AutoSize = true;
            sprintlabel.Location = new Point(3, 230);
            sprintlabel.Name = "sprintlabel";
            sprintlabel.Size = new Size(54, 20);
            sprintlabel.TabIndex = 9;
            sprintlabel.Text = "冲刺：";
            // 
            // invincibilitylabel
            // 
            invincibilitylabel.AutoSize = true;
            invincibilitylabel.Location = new Point(3, 181);
            invincibilitylabel.Name = "invincibilitylabel";
            invincibilitylabel.Size = new Size(54, 20);
            invincibilitylabel.TabIndex = 8;
            invincibilitylabel.Text = "无敌：";
            // 
            // timeslacklabel
            // 
            timeslacklabel.AutoSize = true;
            timeslacklabel.Location = new Point(3, 140);
            timeslacklabel.Name = "timeslacklabel";
            timeslacklabel.Size = new Size(54, 20);
            timeslacklabel.TabIndex = 7;
            timeslacklabel.Text = "缓时：";
            // 
            // magnetlabel
            // 
            magnetlabel.AutoSize = true;
            magnetlabel.Location = new Point(3, 88);
            magnetlabel.Name = "magnetlabel";
            magnetlabel.Size = new Size(54, 20);
            magnetlabel.TabIndex = 6;
            magnetlabel.Text = "磁铁：";
            // 
            // shieldtext
            // 
            shieldtext.AutoSize = true;
            shieldtext.Location = new Point(112, 35);
            shieldtext.Name = "shieldtext";
            shieldtext.Size = new Size(18, 20);
            shieldtext.TabIndex = 4;
            shieldtext.Text = "0";
            // 
            // magnetlimit
            // 
            magnetlimit.AutoSize = true;
            magnetlimit.Location = new Point(112, 88);
            magnetlimit.Name = "magnetlimit";
            magnetlimit.Size = new Size(25, 20);
            magnetlimit.TabIndex = 5;
            magnetlimit.Text = "0s";
            // 
            // shieldlabel
            // 
            shieldlabel.AutoSize = true;
            shieldlabel.Location = new Point(3, 35);
            shieldlabel.Name = "shieldlabel";
            shieldlabel.Size = new Size(54, 20);
            shieldlabel.TabIndex = 3;
            shieldlabel.Text = "护盾：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 601);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button button2;
        private Button button1;
        private Panel panel2;
        private Label label1;
        private Label shieldlabel;
        private Label shieldtext;
        private Label magnetlimit;
        private Panel panel3;
        private Label timeslacklabel;
        private Label magnetlabel;
        private Label sprintlabel;
        private Label invincibilitylabel;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label timeslacklimit;
        private Label invincibilitylimit;
        private Label sprintlimit;
        private Button button3;
    }
}