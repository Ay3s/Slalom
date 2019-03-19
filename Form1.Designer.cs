namespace Slalom
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ButtonSlopeSetting = new System.Windows.Forms.Button();
            this.SkiSlope = new System.Windows.Forms.Panel();
            this.ButtonStarter = new System.Windows.Forms.Button();
            this.FinishLine = new System.Windows.Forms.Panel();
            this.TimerRacer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.ButtonStartRacer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonSlopeSetting
            // 
            this.ButtonSlopeSetting.BackColor = System.Drawing.Color.Thistle;
            this.ButtonSlopeSetting.Location = new System.Drawing.Point(337, 341);
            this.ButtonSlopeSetting.Name = "ButtonSlopeSetting";
            this.ButtonSlopeSetting.Size = new System.Drawing.Size(101, 47);
            this.ButtonSlopeSetting.TabIndex = 0;
            this.ButtonSlopeSetting.Text = "Začít kreslení dráhy";
            this.ButtonSlopeSetting.UseVisualStyleBackColor = false;
            this.ButtonSlopeSetting.Click += new System.EventHandler(this.ButtonSlopeSetting_Click);
            // 
            // SkiSlope
            // 
            this.SkiSlope.BackColor = System.Drawing.Color.MintCream;
            this.SkiSlope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SkiSlope.Location = new System.Drawing.Point(-1, -2);
            this.SkiSlope.Name = "SkiSlope";
            this.SkiSlope.Size = new System.Drawing.Size(439, 344);
            this.SkiSlope.TabIndex = 1;
            this.SkiSlope.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SkiSlope_MouseClick);
            this.SkiSlope.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SkiSlope_PreviewKeyDown);
            // 
            // ButtonStarter
            // 
            this.ButtonStarter.BackColor = System.Drawing.Color.Thistle;
            this.ButtonStarter.Location = new System.Drawing.Point(-1, 341);
            this.ButtonStarter.Name = "ButtonStarter";
            this.ButtonStarter.Size = new System.Drawing.Size(100, 47);
            this.ButtonStarter.TabIndex = 2;
            this.ButtonStarter.Text = "Začít závod";
            this.ButtonStarter.UseVisualStyleBackColor = false;
            this.ButtonStarter.Click += new System.EventHandler(this.ButtonStarter_Click);
            // 
            // FinishLine
            // 
            this.FinishLine.BackgroundImage = global::Slalom.Properties.Resources.finish_line1;
            this.FinishLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FinishLine.Location = new System.Drawing.Point(98, 341);
            this.FinishLine.Margin = new System.Windows.Forms.Padding(2);
            this.FinishLine.Name = "FinishLine";
            this.FinishLine.Size = new System.Drawing.Size(240, 47);
            this.FinishLine.TabIndex = 3;
            // 
            // TimerRacer
            // 
            this.TimerRacer.Interval = 500;
            this.TimerRacer.Tick += new System.EventHandler(this.TimerRacer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Thistle;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(98, 388);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(241, 82);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DodgerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(54, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Modrou branku objet zprava";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(52, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Červenou branku objet zleva";
            // 
            // ButtonReset
            // 
            this.ButtonReset.BackColor = System.Drawing.Color.LavenderBlush;
            this.ButtonReset.Location = new System.Drawing.Point(337, 388);
            this.ButtonReset.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(101, 82);
            this.ButtonReset.TabIndex = 7;
            this.ButtonReset.Text = "Reset branek";
            this.ButtonReset.UseVisualStyleBackColor = false;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // ButtonStartRacer
            // 
            this.ButtonStartRacer.BackColor = System.Drawing.Color.Azure;
            this.ButtonStartRacer.Location = new System.Drawing.Point(-2, 388);
            this.ButtonStartRacer.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonStartRacer.Name = "ButtonStartRacer";
            this.ButtonStartRacer.Size = new System.Drawing.Size(101, 82);
            this.ButtonStartRacer.TabIndex = 8;
            this.ButtonStartRacer.Text = "Start závodníka";
            this.ButtonStartRacer.UseVisualStyleBackColor = false;
            this.ButtonStartRacer.Click += new System.EventHandler(this.ButtonStartRacer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(436, 470);
            this.Controls.Add(this.ButtonStartRacer);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FinishLine);
            this.Controls.Add(this.ButtonStarter);
            this.Controls.Add(this.ButtonSlopeSetting);
            this.Controls.Add(this.SkiSlope);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSlopeSetting;
        private System.Windows.Forms.Panel SkiSlope;
        private System.Windows.Forms.Button ButtonStarter;
        private System.Windows.Forms.Panel FinishLine;
        private System.Windows.Forms.Timer TimerRacer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.Button ButtonStartRacer;
    }
}

