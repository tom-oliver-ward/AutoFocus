namespace GUI_V2._0
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
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_EQMeas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_EQStatus = new System.Windows.Forms.TextBox();
            this.buttonDoAll = new System.Windows.Forms.Button();
            this.buttonSaveEQ = new System.Windows.Forms.Button();
            this.progressBarEQ = new System.Windows.Forms.ProgressBar();
            this.buttonFindEQ = new System.Windows.Forms.Button();
            this.Textbox_EQCALC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_EQ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TestBox_StagePosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressInitLaser = new System.Windows.Forms.ProgressBar();
            this.progressInitSpec = new System.Windows.Forms.ProgressBar();
            this.progressBarStageInit = new System.Windows.Forms.ProgressBar();
            this.buttonInitLaser = new System.Windows.Forms.Button();
            this.buttonInitSpec = new System.Windows.Forms.Button();
            this.buttonInitStage = new System.Windows.Forms.Button();
            this.buttonInitAll = new System.Windows.Forms.Button();
            this.buttonLaserOn = new System.Windows.Forms.Button();
            this.buttonLaserOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDisconnect.Location = new System.Drawing.Point(12, 265);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(649, 42);
            this.buttonDisconnect.TabIndex = 56;
            this.buttonDisconnect.Text = "Disconnect && Reset";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(503, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Additional EQ measurements";
            // 
            // textBox_EQMeas
            // 
            this.textBox_EQMeas.Location = new System.Drawing.Point(506, 115);
            this.textBox_EQMeas.Name = "textBox_EQMeas";
            this.textBox_EQMeas.ReadOnly = true;
            this.textBox_EQMeas.Size = new System.Drawing.Size(165, 20);
            this.textBox_EQMeas.TabIndex = 54;
            this.textBox_EQMeas.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Equilibrium Status";
            // 
            // textBox_EQStatus
            // 
            this.textBox_EQStatus.Location = new System.Drawing.Point(506, 55);
            this.textBox_EQStatus.Name = "textBox_EQStatus";
            this.textBox_EQStatus.ReadOnly = true;
            this.textBox_EQStatus.Size = new System.Drawing.Size(165, 20);
            this.textBox_EQStatus.TabIndex = 52;
            this.textBox_EQStatus.Text = "EQ Find not started";
            // 
            // buttonDoAll
            // 
            this.buttonDoAll.Location = new System.Drawing.Point(12, 217);
            this.buttonDoAll.Name = "buttonDoAll";
            this.buttonDoAll.Size = new System.Drawing.Size(649, 42);
            this.buttonDoAll.TabIndex = 51;
            this.buttonDoAll.Text = "Do Everything Automatically";
            this.buttonDoAll.UseVisualStyleBackColor = true;
            this.buttonDoAll.Click += new System.EventHandler(this.buttonDoAll_Click);
            // 
            // buttonSaveEQ
            // 
            this.buttonSaveEQ.Location = new System.Drawing.Point(401, 118);
            this.buttonSaveEQ.Name = "buttonSaveEQ";
            this.buttonSaveEQ.Size = new System.Drawing.Size(85, 43);
            this.buttonSaveEQ.TabIndex = 50;
            this.buttonSaveEQ.Text = "Save Equilibrium";
            this.buttonSaveEQ.UseVisualStyleBackColor = true;
            this.buttonSaveEQ.Click += new System.EventHandler(this.buttonSaveEQ_Click);
            // 
            // progressBarEQ
            // 
            this.progressBarEQ.ForeColor = System.Drawing.Color.LawnGreen;
            this.progressBarEQ.Location = new System.Drawing.Point(401, 77);
            this.progressBarEQ.Name = "progressBarEQ";
            this.progressBarEQ.Size = new System.Drawing.Size(85, 23);
            this.progressBarEQ.TabIndex = 49;
            // 
            // buttonFindEQ
            // 
            this.buttonFindEQ.Location = new System.Drawing.Point(401, 20);
            this.buttonFindEQ.Name = "buttonFindEQ";
            this.buttonFindEQ.Size = new System.Drawing.Size(85, 43);
            this.buttonFindEQ.TabIndex = 48;
            this.buttonFindEQ.Text = "Find Equilibrium";
            this.buttonFindEQ.UseVisualStyleBackColor = true;
            this.buttonFindEQ.Click += new System.EventHandler(this.buttonFindEQ_Click);
            // 
            // Textbox_EQCALC
            // 
            this.Textbox_EQCALC.Location = new System.Drawing.Point(217, 134);
            this.Textbox_EQCALC.Name = "Textbox_EQCALC";
            this.Textbox_EQCALC.ReadOnly = true;
            this.Textbox_EQCALC.Size = new System.Drawing.Size(100, 20);
            this.Textbox_EQCALC.TabIndex = 47;
            this.Textbox_EQCALC.Text = "Not Calculated";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Calculated EQ Position";
            // 
            // textBox_EQ
            // 
            this.textBox_EQ.Location = new System.Drawing.Point(217, 80);
            this.textBox_EQ.Name = "textBox_EQ";
            this.textBox_EQ.ReadOnly = true;
            this.textBox_EQ.Size = new System.Drawing.Size(100, 20);
            this.textBox_EQ.TabIndex = 45;
            this.textBox_EQ.Text = "Not Set";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Equilibrium Input Position";
            // 
            // TestBox_StagePosition
            // 
            this.TestBox_StagePosition.Location = new System.Drawing.Point(217, 32);
            this.TestBox_StagePosition.Name = "TestBox_StagePosition";
            this.TestBox_StagePosition.ReadOnly = true;
            this.TestBox_StagePosition.Size = new System.Drawing.Size(100, 20);
            this.TestBox_StagePosition.TabIndex = 43;
            this.TestBox_StagePosition.Text = "Not Initialised";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Stage Postition";
            // 
            // progressInitLaser
            // 
            this.progressInitLaser.Location = new System.Drawing.Point(103, 128);
            this.progressInitLaser.Name = "progressInitLaser";
            this.progressInitLaser.Size = new System.Drawing.Size(85, 23);
            this.progressInitLaser.TabIndex = 41;
            // 
            // progressInitSpec
            // 
            this.progressInitSpec.Location = new System.Drawing.Point(103, 99);
            this.progressInitSpec.Name = "progressInitSpec";
            this.progressInitSpec.Size = new System.Drawing.Size(85, 23);
            this.progressInitSpec.TabIndex = 40;
            // 
            // progressBarStageInit
            // 
            this.progressBarStageInit.ForeColor = System.Drawing.Color.LawnGreen;
            this.progressBarStageInit.Location = new System.Drawing.Point(103, 70);
            this.progressBarStageInit.Name = "progressBarStageInit";
            this.progressBarStageInit.Size = new System.Drawing.Size(85, 23);
            this.progressBarStageInit.TabIndex = 39;
            // 
            // buttonInitLaser
            // 
            this.buttonInitLaser.Location = new System.Drawing.Point(12, 128);
            this.buttonInitLaser.Name = "buttonInitLaser";
            this.buttonInitLaser.Size = new System.Drawing.Size(85, 23);
            this.buttonInitLaser.TabIndex = 38;
            this.buttonInitLaser.Text = "Initialise Laser";
            this.buttonInitLaser.UseVisualStyleBackColor = true;
            this.buttonInitLaser.Click += new System.EventHandler(this.buttonInitLaser_Click);
            // 
            // buttonInitSpec
            // 
            this.buttonInitSpec.Location = new System.Drawing.Point(12, 99);
            this.buttonInitSpec.Name = "buttonInitSpec";
            this.buttonInitSpec.Size = new System.Drawing.Size(85, 23);
            this.buttonInitSpec.TabIndex = 37;
            this.buttonInitSpec.Text = "Initialise Spec";
            this.buttonInitSpec.UseVisualStyleBackColor = true;
            this.buttonInitSpec.Click += new System.EventHandler(this.buttonInitSpec_Click);
            // 
            // buttonInitStage
            // 
            this.buttonInitStage.Location = new System.Drawing.Point(12, 70);
            this.buttonInitStage.Name = "buttonInitStage";
            this.buttonInitStage.Size = new System.Drawing.Size(85, 23);
            this.buttonInitStage.TabIndex = 36;
            this.buttonInitStage.Text = "Initialise Stage";
            this.buttonInitStage.UseVisualStyleBackColor = true;
            this.buttonInitStage.Click += new System.EventHandler(this.buttonInitStage_Click);
            // 
            // buttonInitAll
            // 
            this.buttonInitAll.Location = new System.Drawing.Point(12, 30);
            this.buttonInitAll.Name = "buttonInitAll";
            this.buttonInitAll.Size = new System.Drawing.Size(85, 23);
            this.buttonInitAll.TabIndex = 35;
            this.buttonInitAll.Text = "Initialise All";
            this.buttonInitAll.UseVisualStyleBackColor = true;
            this.buttonInitAll.Click += new System.EventHandler(this.buttonInitAll_Click);
            // 
            // buttonLaserOn
            // 
            this.buttonLaserOn.Location = new System.Drawing.Point(12, 157);
            this.buttonLaserOn.Name = "buttonLaserOn";
            this.buttonLaserOn.Size = new System.Drawing.Size(85, 23);
            this.buttonLaserOn.TabIndex = 57;
            this.buttonLaserOn.Text = "Laser On";
            this.buttonLaserOn.UseVisualStyleBackColor = true;
            this.buttonLaserOn.Click += new System.EventHandler(this.buttonLaserOn_Click);
            // 
            // buttonLaserOff
            // 
            this.buttonLaserOff.Location = new System.Drawing.Point(103, 157);
            this.buttonLaserOff.Name = "buttonLaserOff";
            this.buttonLaserOff.Size = new System.Drawing.Size(85, 23);
            this.buttonLaserOff.TabIndex = 58;
            this.buttonLaserOff.Text = "Laser Off";
            this.buttonLaserOff.UseVisualStyleBackColor = true;
            this.buttonLaserOff.Click += new System.EventHandler(this.buttonLaserOff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 332);
            this.Controls.Add(this.buttonLaserOff);
            this.Controls.Add(this.buttonLaserOn);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_EQMeas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_EQStatus);
            this.Controls.Add(this.buttonDoAll);
            this.Controls.Add(this.buttonSaveEQ);
            this.Controls.Add(this.progressBarEQ);
            this.Controls.Add(this.buttonFindEQ);
            this.Controls.Add(this.Textbox_EQCALC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_EQ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TestBox_StagePosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressInitLaser);
            this.Controls.Add(this.progressInitSpec);
            this.Controls.Add(this.progressBarStageInit);
            this.Controls.Add(this.buttonInitLaser);
            this.Controls.Add(this.buttonInitSpec);
            this.Controls.Add(this.buttonInitStage);
            this.Controls.Add(this.buttonInitAll);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDoAll;
        private System.Windows.Forms.Button buttonSaveEQ;
        private System.Windows.Forms.Button buttonFindEQ;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox_EQ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressInitLaser;
        private System.Windows.Forms.ProgressBar progressInitSpec;
        private System.Windows.Forms.ProgressBar progressBarStageInit;
        private System.Windows.Forms.Button buttonInitLaser;
        private System.Windows.Forms.Button buttonInitSpec;
        private System.Windows.Forms.Button buttonInitStage;
        private System.Windows.Forms.Button buttonInitAll;
        public System.Windows.Forms.TextBox TestBox_StagePosition;
        public System.Windows.Forms.TextBox textBox_EQStatus;
        public System.Windows.Forms.ProgressBar progressBarEQ;
        public System.Windows.Forms.TextBox Textbox_EQCALC;
        public System.Windows.Forms.TextBox textBox_EQMeas;
        private System.Windows.Forms.Button buttonLaserOn;
        private System.Windows.Forms.Button buttonLaserOff;

    }
}

