namespace RetroTicker {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.connectButton = new System.Windows.Forms.Button();
            this.botStatusHeaderLabel = new System.Windows.Forms.Label();
            this.botStatusLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTickerButton = new System.Windows.Forms.Button();
            this.testTickerButton = new System.Windows.Forms.Button();
            this.startReadingButton = new System.Windows.Forms.Button();
            this.stopReadingButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 96);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // botStatusHeaderLabel
            // 
            this.botStatusHeaderLabel.AutoSize = true;
            this.botStatusHeaderLabel.Location = new System.Drawing.Point(12, 154);
            this.botStatusHeaderLabel.Name = "botStatusHeaderLabel";
            this.botStatusHeaderLabel.Size = new System.Drawing.Size(57, 13);
            this.botStatusHeaderLabel.TabIndex = 1;
            this.botStatusHeaderLabel.Text = "Bot status:";
            // 
            // botStatusLabel
            // 
            this.botStatusLabel.AutoSize = true;
            this.botStatusLabel.Location = new System.Drawing.Point(76, 154);
            this.botStatusLabel.Name = "botStatusLabel";
            this.botStatusLabel.Size = new System.Drawing.Size(58, 13);
            this.botStatusLabel.TabIndex = 2;
            this.botStatusLabel.Text = "STOPPED";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(184, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.controlsToolStripMenuItem.Text = "File";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.configToolStripMenuItem.Text = "config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // openTickerButton
            // 
            this.openTickerButton.Location = new System.Drawing.Point(12, 48);
            this.openTickerButton.Name = "openTickerButton";
            this.openTickerButton.Size = new System.Drawing.Size(75, 23);
            this.openTickerButton.TabIndex = 4;
            this.openTickerButton.Text = "open ticker";
            this.openTickerButton.UseVisualStyleBackColor = true;
            this.openTickerButton.Click += new System.EventHandler(this.openTickerButton_Click);
            // 
            // testTickerButton
            // 
            this.testTickerButton.Location = new System.Drawing.Point(93, 48);
            this.testTickerButton.Name = "testTickerButton";
            this.testTickerButton.Size = new System.Drawing.Size(75, 23);
            this.testTickerButton.TabIndex = 5;
            this.testTickerButton.Text = "test ticker";
            this.testTickerButton.UseVisualStyleBackColor = true;
            this.testTickerButton.Click += new System.EventHandler(this.testTickerButton_Click);
            // 
            // startReadingButton
            // 
            this.startReadingButton.Location = new System.Drawing.Point(93, 96);
            this.startReadingButton.Name = "startReadingButton";
            this.startReadingButton.Size = new System.Drawing.Size(75, 23);
            this.startReadingButton.TabIndex = 6;
            this.startReadingButton.Text = "start reading";
            this.startReadingButton.UseVisualStyleBackColor = true;
            this.startReadingButton.Click += new System.EventHandler(this.startReadingButton_Click);
            // 
            // stopReadingButton
            // 
            this.stopReadingButton.Location = new System.Drawing.Point(93, 125);
            this.stopReadingButton.Name = "stopReadingButton";
            this.stopReadingButton.Size = new System.Drawing.Size(75, 23);
            this.stopReadingButton.TabIndex = 7;
            this.stopReadingButton.Text = "stop reading";
            this.stopReadingButton.UseVisualStyleBackColor = true;
            this.stopReadingButton.Click += new System.EventHandler(this.stopReadingButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(12, 125);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 8;
            this.disconnectButton.Text = "disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 181);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.stopReadingButton);
            this.Controls.Add(this.startReadingButton);
            this.Controls.Add(this.testTickerButton);
            this.Controls.Add(this.openTickerButton);
            this.Controls.Add(this.botStatusLabel);
            this.Controls.Add(this.botStatusHeaderLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RetroTicker v1.2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label botStatusHeaderLabel;
        private System.Windows.Forms.Label botStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.Button openTickerButton;
        private System.Windows.Forms.Button testTickerButton;
        private System.Windows.Forms.Button startReadingButton;
        private System.Windows.Forms.Button stopReadingButton;
        private System.Windows.Forms.Button disconnectButton;
    }
}

