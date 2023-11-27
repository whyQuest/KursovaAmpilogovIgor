namespace Ecosystem
{
    partial class Ecosystem1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ecosystem1));
            this.main = new System.Windows.Forms.Label();
            this.nudRabbit = new System.Windows.Forms.NumericUpDown();
            this.lblNumRabbit = new System.Windows.Forms.Label();
            this.lblNumWolf = new System.Windows.Forms.Label();
            this.nudWolf = new System.Windows.Forms.NumericUpDown();
            this.lblNumCarrot = new System.Windows.Forms.Label();
            this.nudCarrot = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.intervallSpawn = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.nudXpos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudYpos = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRabbit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCarrot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallSpawn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // main
            // 
            this.main.AutoSize = true;
            this.main.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.main.Location = new System.Drawing.Point(26, 9);
            this.main.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(499, 93);
            this.main.TabIndex = 0;
            this.main.Text = "Екосистема";
            this.main.Click += new System.EventHandler(this.main_Click);
            // 
            // nudRabbit
            // 
            this.nudRabbit.Location = new System.Drawing.Point(156, 166);
            this.nudRabbit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nudRabbit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRabbit.Name = "nudRabbit";
            this.nudRabbit.Size = new System.Drawing.Size(145, 35);
            this.nudRabbit.TabIndex = 1;
            this.nudRabbit.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudRabbit.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.nudRabbit.VisibleChanged += new System.EventHandler(this.ValueChanged);
            // 
            // lblNumRabbit
            // 
            this.lblNumRabbit.AutoSize = true;
            this.lblNumRabbit.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRabbit.Location = new System.Drawing.Point(59, 168);
            this.lblNumRabbit.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumRabbit.Name = "lblNumRabbit";
            this.lblNumRabbit.Size = new System.Drawing.Size(108, 27);
            this.lblNumRabbit.TabIndex = 2;
            this.lblNumRabbit.Text = "Кроликів";
            // 
            // lblNumWolf
            // 
            this.lblNumWolf.AutoSize = true;
            this.lblNumWolf.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumWolf.Location = new System.Drawing.Point(59, 207);
            this.lblNumWolf.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumWolf.Name = "lblNumWolf";
            this.lblNumWolf.Size = new System.Drawing.Size(83, 27);
            this.lblNumWolf.TabIndex = 4;
            this.lblNumWolf.Text = "Вовків";
            // 
            // nudWolf
            // 
            this.nudWolf.Location = new System.Drawing.Point(156, 205);
            this.nudWolf.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nudWolf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWolf.Name = "nudWolf";
            this.nudWolf.Size = new System.Drawing.Size(145, 35);
            this.nudWolf.TabIndex = 3;
            this.nudWolf.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudWolf.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.nudWolf.VisibleChanged += new System.EventHandler(this.ValueChanged);
            // 
            // lblNumCarrot
            // 
            this.lblNumCarrot.AutoSize = true;
            this.lblNumCarrot.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumCarrot.Location = new System.Drawing.Point(59, 246);
            this.lblNumCarrot.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumCarrot.Name = "lblNumCarrot";
            this.lblNumCarrot.Size = new System.Drawing.Size(108, 27);
            this.lblNumCarrot.TabIndex = 6;
            this.lblNumCarrot.Text = "Морквин";
            // 
            // nudCarrot
            // 
            this.nudCarrot.Location = new System.Drawing.Point(156, 244);
            this.nudCarrot.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nudCarrot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCarrot.Name = "nudCarrot";
            this.nudCarrot.Size = new System.Drawing.Size(145, 35);
            this.nudCarrot.TabIndex = 5;
            this.nudCarrot.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudCarrot.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.nudCarrot.VisibleChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(364, 248);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "Спавн морквин через";
            // 
            // intervallSpawn
            // 
            this.intervallSpawn.Location = new System.Drawing.Point(583, 246);
            this.intervallSpawn.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.intervallSpawn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervallSpawn.Name = "intervallSpawn";
            this.intervallSpawn.Size = new System.Drawing.Size(68, 35);
            this.intervallSpawn.TabIndex = 7;
            this.intervallSpawn.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(663, 246);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = "секунд(и)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Play.Location = new System.Drawing.Point(556, 300);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(191, 32);
            this.Play.TabIndex = 10;
            this.Play.Text = "Грати!";
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.play_Click);
            // 
            // nudXpos
            // 
            this.nudXpos.Location = new System.Drawing.Point(230, 124);
            this.nudXpos.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nudXpos.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudXpos.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudXpos.Name = "nudXpos";
            this.nudXpos.Size = new System.Drawing.Size(60, 35);
            this.nudXpos.TabIndex = 1;
            this.nudXpos.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudXpos.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.nudXpos.VisibleChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Розмір поля";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 27);
            this.label4.TabIndex = 2;
            this.label4.Text = "x";
            // 
            // nudYpos
            // 
            this.nudYpos.Location = new System.Drawing.Point(313, 124);
            this.nudYpos.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nudYpos.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudYpos.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudYpos.Name = "nudYpos";
            this.nudYpos.Size = new System.Drawing.Size(60, 35);
            this.nudYpos.TabIndex = 1;
            this.nudYpos.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudYpos.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.nudYpos.VisibleChanged += new System.EventHandler(this.ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(583, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 178);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(5, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Creator: I. Ampilogov";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Ecosystem1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 350);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intervallSpawn);
            this.Controls.Add(this.lblNumCarrot);
            this.Controls.Add(this.nudCarrot);
            this.Controls.Add(this.lblNumWolf);
            this.Controls.Add(this.nudWolf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudYpos);
            this.Controls.Add(this.nudXpos);
            this.Controls.Add(this.lblNumRabbit);
            this.Controls.Add(this.nudRabbit);
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Ecosystem1";
            this.Text = "Ecosystem1";
            this.Load += new System.EventHandler(this.Ecosystem1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRabbit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCarrot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallSpawn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label main;
        private System.Windows.Forms.NumericUpDown nudRabbit;
        private System.Windows.Forms.Label lblNumRabbit;
        private System.Windows.Forms.Label lblNumWolf;
        private System.Windows.Forms.NumericUpDown nudWolf;
        private System.Windows.Forms.Label lblNumCarrot;
        private System.Windows.Forms.NumericUpDown nudCarrot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown intervallSpawn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.NumericUpDown nudXpos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudYpos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
    }
}