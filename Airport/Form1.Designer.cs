namespace Airport
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
            this.RunwayPanel = new System.Windows.Forms.Panel();
            this.WeatherPicture = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WeatherPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RunwayPanel
            // 
            this.RunwayPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.RunwayPanel.BackgroundImage = global::Airport.Properties.Resources.passtart;
            this.RunwayPanel.Location = new System.Drawing.Point(23, 123);
            this.RunwayPanel.Name = "RunwayPanel";
            this.RunwayPanel.Size = new System.Drawing.Size(491, 100);
            this.RunwayPanel.TabIndex = 0;
            // 
            // WeatherPicture
            // 
            this.WeatherPicture.BackColor = System.Drawing.Color.Transparent;
            this.WeatherPicture.Image = global::Airport.Properties.Resources.sun;
            this.WeatherPicture.Location = new System.Drawing.Point(466, 69);
            this.WeatherPicture.Name = "WeatherPicture";
            this.WeatherPicture.Size = new System.Drawing.Size(48, 48);
            this.WeatherPicture.TabIndex = 0;
            this.WeatherPicture.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(573, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(755, 443);
            this.Controls.Add(this.WeatherPicture);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RunwayPanel);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.WeatherPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel RunwayPanel;
        private Button button1;
        private PictureBox WeatherPicture;
    }
}