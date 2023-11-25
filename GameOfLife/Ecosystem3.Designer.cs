namespace Ecosystem
{
    partial class Ecosystem3
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
            this.resultTOP = new System.Windows.Forms.Label();
            this.RabbitTOP = new System.Windows.Forms.Label();
            this.WolfTOP = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultTOP
            // 
            this.resultTOP.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultTOP.Location = new System.Drawing.Point(0, 22);
            this.resultTOP.Name = "resultTOP";
            this.resultTOP.Size = new System.Drawing.Size(338, 37);
            this.resultTOP.TabIndex = 0;
            this.resultTOP.Text = "Хто переміг?";
            this.resultTOP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RabbitTOP
            // 
            this.RabbitTOP.AutoSize = true;
            this.RabbitTOP.Location = new System.Drawing.Point(39, 70);
            this.RabbitTOP.Name = "RabbitTOP";
            this.RabbitTOP.Size = new System.Drawing.Size(90, 25);
            this.RabbitTOP.TabIndex = 1;
            this.RabbitTOP.Text = "Кролики";
            // 
            // WolfTOP
            // 
            this.WolfTOP.AutoSize = true;
            this.WolfTOP.Location = new System.Drawing.Point(205, 70);
            this.WolfTOP.Name = "WolfTOP";
            this.WolfTOP.Size = new System.Drawing.Size(66, 25);
            this.WolfTOP.TabIndex = 1;
            this.WolfTOP.Text = "Вовки";
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(12, 112);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(168, 34);
            this.Reset.TabIndex = 2;
            this.Reset.Text = "Нова гра";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(209, 112);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(117, 34);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Вихід";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Ecosystem3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(338, 158);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.WolfTOP);
            this.Controls.Add(this.RabbitTOP);
            this.Controls.Add(this.resultTOP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Ecosystem3";
            this.Text = "Ecosystem3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label resultTOP;
        private System.Windows.Forms.Label RabbitTOP;
        private System.Windows.Forms.Label WolfTOP;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Button Exit;
    }
}