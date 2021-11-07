
namespace w8_fejlesztesi_mintak
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
            this.createTimer = new System.Windows.Forms.Timer(this.components);
            this.mainPanel = new System.Windows.Forms.Panel();
            this.conveyorTimer = new System.Windows.Forms.Timer(this.components);
            this.carButton = new System.Windows.Forms.Button();
            this.ballButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.presentButton = new System.Windows.Forms.Button();
            this.ribbonButton = new System.Windows.Forms.Button();
            this.boxButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createTimer
            // 
            this.createTimer.Enabled = true;
            this.createTimer.Interval = 3000;
            this.createTimer.Tick += new System.EventHandler(this.createTimer_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(-8, 136);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(818, 302);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Click += new System.EventHandler(this.button3_Click);
            // 
            // conveyorTimer
            // 
            this.conveyorTimer.Enabled = true;
            this.conveyorTimer.Interval = 10;
            this.conveyorTimer.Tick += new System.EventHandler(this.conveyorTimer_Tick);
            // 
            // carButton
            // 
            this.carButton.Location = new System.Drawing.Point(3, 0);
            this.carButton.Name = "carButton";
            this.carButton.Size = new System.Drawing.Size(109, 35);
            this.carButton.TabIndex = 0;
            this.carButton.Text = "CAR";
            this.carButton.UseVisualStyleBackColor = true;
            this.carButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ballButton
            // 
            this.ballButton.Location = new System.Drawing.Point(128, 0);
            this.ballButton.Name = "ballButton";
            this.ballButton.Size = new System.Drawing.Size(109, 35);
            this.ballButton.TabIndex = 1;
            this.ballButton.Text = "BALL";
            this.ballButton.UseVisualStyleBackColor = true;
            this.ballButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(401, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Coming next:";
            // 
            // colorButton
            // 
            this.colorButton.BackColor = System.Drawing.Color.Firebrick;
            this.colorButton.Location = new System.Drawing.Point(3, 41);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(108, 33);
            this.colorButton.TabIndex = 3;
            this.colorButton.UseVisualStyleBackColor = false;
            this.colorButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // presentButton
            // 
            this.presentButton.Location = new System.Drawing.Point(243, 0);
            this.presentButton.Name = "presentButton";
            this.presentButton.Size = new System.Drawing.Size(109, 35);
            this.presentButton.TabIndex = 4;
            this.presentButton.Text = "Present";
            this.presentButton.UseVisualStyleBackColor = true;
            this.presentButton.Click += new System.EventHandler(this.presentButton_Click);
            // 
            // ribbonButton
            // 
            this.ribbonButton.BackColor = System.Drawing.Color.Red;
            this.ribbonButton.Location = new System.Drawing.Point(243, 41);
            this.ribbonButton.Name = "ribbonButton";
            this.ribbonButton.Size = new System.Drawing.Size(109, 35);
            this.ribbonButton.TabIndex = 5;
            this.ribbonButton.Text = "Ribbon";
            this.ribbonButton.UseVisualStyleBackColor = false;
            this.ribbonButton.Click += new System.EventHandler(this.ribbonButton_Click);
            // 
            // boxButton
            // 
            this.boxButton.BackColor = System.Drawing.Color.Lime;
            this.boxButton.Location = new System.Drawing.Point(243, 82);
            this.boxButton.Name = "boxButton";
            this.boxButton.Size = new System.Drawing.Size(109, 35);
            this.boxButton.TabIndex = 6;
            this.boxButton.Text = "Box";
            this.boxButton.UseVisualStyleBackColor = false;
            this.boxButton.Click += new System.EventHandler(this.boxButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.boxButton);
            this.Controls.Add(this.ribbonButton);
            this.Controls.Add(this.presentButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ballButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.carButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer createTimer;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer conveyorTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ballButton;
        private System.Windows.Forms.Button carButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button presentButton;
        private System.Windows.Forms.Button ribbonButton;
        private System.Windows.Forms.Button boxButton;
    }
}

