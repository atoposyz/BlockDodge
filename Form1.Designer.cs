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
            button4 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            button3 = new Button();
            label1 = new Label();
            nightwalklimit = new Label();
            quicklimit = new Label();
            fearlesslimit = new Label();
            timeslacklimit = new Label();
            invincibilitylimit = new Label();
            sprintlimit = new Label();
            nightwalklabel = new Label();
            quicklabel = new Label();
            fearlesslabel = new Label();
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
            tableLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Size = new Size(1232, 721);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 652);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1039, 65);
            panel1.TabIndex = 0;
            // 
            // button4
            // 
            button4.Location = new Point(359, 12);
            button4.Margin = new Padding(4, 4, 4, 4);
            button4.Name = "button4";
            button4.Size = new Size(123, 50);
            button4.TabIndex = 2;
            button4.Text = "重置";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Location = new Point(688, 12);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(126, 50);
            button2.TabIndex = 1;
            button2.Text = "排行榜";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(71, 12);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(123, 50);
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
            panel2.Location = new Point(4, 4);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1039, 640);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.Controls.Add(button3);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(nightwalklimit);
            panel3.Controls.Add(quicklimit);
            panel3.Controls.Add(fearlesslimit);
            panel3.Controls.Add(timeslacklimit);
            panel3.Controls.Add(invincibilitylimit);
            panel3.Controls.Add(sprintlimit);
            panel3.Controls.Add(nightwalklabel);
            panel3.Controls.Add(quicklabel);
            panel3.Controls.Add(fearlesslabel);
            panel3.Controls.Add(sprintlabel);
            panel3.Controls.Add(invincibilitylabel);
            panel3.Controls.Add(timeslacklabel);
            panel3.Controls.Add(magnetlabel);
            panel3.Controls.Add(shieldtext);
            panel3.Controls.Add(magnetlimit);
            panel3.Controls.Add(shieldlabel);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(1051, 4);
            panel3.Margin = new Padding(4, 4, 4, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(177, 640);
            panel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Location = new Point(33, 550);
            button3.Margin = new Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new Size(108, 53);
            button3.TabIndex = 19;
            button3.Text = "切换模式";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.Location = new Point(45, 511);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(99, 35);
            label1.TabIndex = 0;
            label1.Text = "闯关模式";
            // 
            // nightwalklimit
            // 
            nightwalklimit.AutoSize = true;
            nightwalklimit.Location = new Point(137, 311);
            nightwalklimit.Margin = new Padding(4, 0, 4, 0);
            nightwalklimit.Name = "nightwalklimit";
            nightwalklimit.Size = new Size(29, 24);
            nightwalklimit.TabIndex = 18;
            nightwalklimit.Text = "0s";
            // 
            // quicklimit
            // 
            quicklimit.AutoSize = true;
            quicklimit.Location = new Point(137, 272);
            quicklimit.Margin = new Padding(4, 0, 4, 0);
            quicklimit.Name = "quicklimit";
            quicklimit.Size = new Size(29, 24);
            quicklimit.TabIndex = 17;
            quicklimit.Text = "0s";
            // 
            // fearlesslimit
            // 
            fearlesslimit.AutoSize = true;
            fearlesslimit.Location = new Point(137, 234);
            fearlesslimit.Margin = new Padding(4, 0, 4, 0);
            fearlesslimit.Name = "fearlesslimit";
            fearlesslimit.Size = new Size(29, 24);
            fearlesslimit.TabIndex = 16;
            fearlesslimit.Text = "0s";
            // 
            // timeslacklimit
            // 
            timeslacklimit.AutoSize = true;
            timeslacklimit.Location = new Point(137, 120);
            timeslacklimit.Margin = new Padding(4, 0, 4, 0);
            timeslacklimit.Name = "timeslacklimit";
            timeslacklimit.Size = new Size(29, 24);
            timeslacklimit.TabIndex = 15;
            timeslacklimit.Text = "0s";
            // 
            // invincibilitylimit
            // 
            invincibilitylimit.AutoSize = true;
            invincibilitylimit.Location = new Point(137, 158);
            invincibilitylimit.Margin = new Padding(4, 0, 4, 0);
            invincibilitylimit.Name = "invincibilitylimit";
            invincibilitylimit.Size = new Size(29, 24);
            invincibilitylimit.TabIndex = 14;
            invincibilitylimit.Text = "0s";
            // 
            // sprintlimit
            // 
            sprintlimit.AutoSize = true;
            sprintlimit.Location = new Point(137, 197);
            sprintlimit.Margin = new Padding(4, 0, 4, 0);
            sprintlimit.Name = "sprintlimit";
            sprintlimit.Size = new Size(29, 24);
            sprintlimit.TabIndex = 13;
            sprintlimit.Text = "0s";
            // 
            // nightwalklabel
            // 
            nightwalklabel.AutoSize = true;
            nightwalklabel.Location = new Point(4, 311);
            nightwalklabel.Margin = new Padding(4, 0, 4, 0);
            nightwalklabel.Name = "nightwalklabel";
            nightwalklabel.Size = new Size(64, 24);
            nightwalklabel.TabIndex = 12;
            nightwalklabel.Text = "夜行：";
            // 
            // quicklabel
            // 
            quicklabel.AutoSize = true;
            quicklabel.Location = new Point(4, 272);
            quicklabel.Margin = new Padding(4, 0, 4, 0);
            quicklabel.Name = "quicklabel";
            quicklabel.Size = new Size(64, 24);
            quicklabel.TabIndex = 11;
            quicklabel.Text = "迅捷：";
            // 
            // fearlesslabel
            // 
            fearlesslabel.AutoSize = true;
            fearlesslabel.Location = new Point(4, 234);
            fearlesslabel.Margin = new Padding(4, 0, 4, 0);
            fearlesslabel.Name = "fearlesslabel";
            fearlesslabel.Size = new Size(64, 24);
            fearlesslabel.TabIndex = 10;
            fearlesslabel.Text = "无畏：";
            // 
            // sprintlabel
            // 
            sprintlabel.AutoSize = true;
            sprintlabel.Location = new Point(4, 197);
            sprintlabel.Margin = new Padding(4, 0, 4, 0);
            sprintlabel.Name = "sprintlabel";
            sprintlabel.Size = new Size(64, 24);
            sprintlabel.TabIndex = 9;
            sprintlabel.Text = "冲刺：";
            // 
            // invincibilitylabel
            // 
            invincibilitylabel.AutoSize = true;
            invincibilitylabel.Location = new Point(4, 158);
            invincibilitylabel.Margin = new Padding(4, 0, 4, 0);
            invincibilitylabel.Name = "invincibilitylabel";
            invincibilitylabel.Size = new Size(64, 24);
            invincibilitylabel.TabIndex = 8;
            invincibilitylabel.Text = "无敌：";
            // 
            // timeslacklabel
            // 
            timeslacklabel.AutoSize = true;
            timeslacklabel.Location = new Point(4, 120);
            timeslacklabel.Margin = new Padding(4, 0, 4, 0);
            timeslacklabel.Name = "timeslacklabel";
            timeslacklabel.Size = new Size(64, 24);
            timeslacklabel.TabIndex = 7;
            timeslacklabel.Text = "缓时：";
            // 
            // magnetlabel
            // 
            magnetlabel.AutoSize = true;
            magnetlabel.Location = new Point(4, 80);
            magnetlabel.Margin = new Padding(4, 0, 4, 0);
            magnetlabel.Name = "magnetlabel";
            magnetlabel.Size = new Size(64, 24);
            magnetlabel.TabIndex = 6;
            magnetlabel.Text = "磁铁：";
            // 
            // shieldtext
            // 
            shieldtext.AutoSize = true;
            shieldtext.Location = new Point(137, 42);
            shieldtext.Margin = new Padding(4, 0, 4, 0);
            shieldtext.Name = "shieldtext";
            shieldtext.Size = new Size(21, 24);
            shieldtext.TabIndex = 4;
            shieldtext.Text = "0";
            // 
            // magnetlimit
            // 
            magnetlimit.AutoSize = true;
            magnetlimit.Location = new Point(137, 80);
            magnetlimit.Margin = new Padding(4, 0, 4, 0);
            magnetlimit.Name = "magnetlimit";
            magnetlimit.Size = new Size(29, 24);
            magnetlimit.TabIndex = 5;
            magnetlimit.Text = "0s";
            // 
            // shieldlabel
            // 
            shieldlabel.AutoSize = true;
            shieldlabel.Location = new Point(4, 42);
            shieldlabel.Margin = new Padding(4, 0, 4, 0);
            shieldlabel.Name = "shieldlabel";
            shieldlabel.Size = new Size(64, 24);
            shieldlabel.TabIndex = 3;
            shieldlabel.Text = "护盾：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 721);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
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
        private Label nightwalklabel;
        private Label quicklabel;
        private Label fearlesslabel;
        private Label nightwalklimit;
        private Label quicklimit;
        private Label fearlesslimit;
        private Label timeslacklimit;
        private Label invincibilitylimit;
        private Label sprintlimit;
        private Button button3;
        private Button button4;
    }
}