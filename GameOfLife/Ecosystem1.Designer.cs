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
            this.lblTitolo = new System.Windows.Forms.Label();
            this.nudRabbit = new System.Windows.Forms.NumericUpDown();
            this.lblNumConigli = new System.Windows.Forms.Label();
            this.lblNumLupi = new System.Windows.Forms.Label();
            this.nudWolf = new System.Windows.Forms.NumericUpDown();
            this.lblNumCarote = new System.Windows.Forms.Label();
            this.nudCarrot = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.intervallSpawn = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.nudXpos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudYpos = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudRabbit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCarrot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallSpawn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYpos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitolo
            // 
            this.lblTitolo.AutoSize = true;
            this.lblTitolo.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitolo.Location = new System.Drawing.Point(28, 33);
            this.lblTitolo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitolo.Name = "lblTitolo";
            this.lblTitolo.Size = new System.Drawing.Size(496, 93);
            this.lblTitolo.TabIndex = 0;
            this.lblTitolo.Text = "Game of life";
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
            // lblNumConigli
            // 
            this.lblNumConigli.AutoSize = true;
            this.lblNumConigli.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumConigli.Location = new System.Drawing.Point(59, 168);
            this.lblNumConigli.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumConigli.Name = "lblNumConigli";
            this.lblNumConigli.Size = new System.Drawing.Size(86, 27);
            this.lblNumConigli.TabIndex = 2;
            this.lblNumConigli.Text = "Conigli";
            // 
            // lblNumLupi
            // 
            this.lblNumLupi.AutoSize = true;
            this.lblNumLupi.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumLupi.Location = new System.Drawing.Point(59, 207);
            this.lblNumLupi.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumLupi.Name = "lblNumLupi";
            this.lblNumLupi.Size = new System.Drawing.Size(58, 27);
            this.lblNumLupi.TabIndex = 4;
            this.lblNumLupi.Text = "Lupi";
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
            // lblNumCarote
            // 
            this.lblNumCarote.AutoSize = true;
            this.lblNumCarote.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumCarote.Location = new System.Drawing.Point(59, 246);
            this.lblNumCarote.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumCarote.Name = "lblNumCarote";
            this.lblNumCarote.Size = new System.Drawing.Size(83, 27);
            this.lblNumCarote.TabIndex = 6;
            this.lblNumCarote.Text = "Carote";
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
            this.label1.Location = new System.Drawing.Point(333, 251);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nuova carota ogni ";
            // 
            // nudIntervalloCarote
            // 
            this.intervallSpawn.Location = new System.Drawing.Point(496, 249);
            this.intervallSpawn.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.intervallSpawn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervallSpawn.Name = "nudIntervalloCarote";
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
            this.label2.Location = new System.Drawing.Point(567, 251);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = "secondi";
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(556, 300);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(191, 32);
            this.Play.TabIndex = 10;
            this.Play.Text = "Avvia simulazione";
            this.Play.UseVisualStyleBackColor = true;
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
            this.label3.Size = new System.Drawing.Size(202, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dimensioni griglia";
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
            // Ecosystem1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 350);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intervallSpawn);
            this.Controls.Add(this.lblNumCarote);
            this.Controls.Add(this.nudCarrot);
            this.Controls.Add(this.lblNumLupi);
            this.Controls.Add(this.nudWolf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudYpos);
            this.Controls.Add(this.nudXpos);
            this.Controls.Add(this.lblNumConigli);
            this.Controls.Add(this.nudRabbit);
            this.Controls.Add(this.lblTitolo);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Ecosystem1";
            this.Text = "Ecosystem1";
            ((System.ComponentModel.ISupportInitialize)(this.nudRabbit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCarrot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallSpawn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYpos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitolo;
        private System.Windows.Forms.NumericUpDown nudRabbit;
        private System.Windows.Forms.Label lblNumConigli;
        private System.Windows.Forms.Label lblNumLupi;
        private System.Windows.Forms.NumericUpDown nudWolf;
        private System.Windows.Forms.Label lblNumCarote;
        private System.Windows.Forms.NumericUpDown nudCarrot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown intervallSpawn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.NumericUpDown nudXpos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudYpos;
    }
}