namespace BallDodgeTemplate
{
    partial class GameScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.liveLabel = new System.Windows.Forms.Label();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // liveLabel
            // 
            this.liveLabel.AutoSize = true;
            this.liveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liveLabel.Location = new System.Drawing.Point(22, 15);
            this.liveLabel.Name = "liveLabel";
            this.liveLabel.Size = new System.Drawing.Size(93, 24);
            this.liveLabel.TabIndex = 0;
            this.liveLabel.Text = "livesLabel";
            // 
            // pointsLabel
            // 
            this.pointsLabel.AutoSize = true;
            this.pointsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointsLabel.Location = new System.Drawing.Point(456, 15);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(106, 24);
            this.pointsLabel.TabIndex = 1;
            this.pointsLabel.Text = "pointsLabel";
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.liveLabel);
            this.DoubleBuffered = true;
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(600, 500);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameScreen_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GameScreen_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label liveLabel;
        private System.Windows.Forms.Label pointsLabel;
    }
}
