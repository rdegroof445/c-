namespace TestWindowed2
{
    partial class Form1
    {

        private string instructiveText = "1. This is a simple paint application with zoom and pan functionality.\r\n" +
                "2. Right clicking on the screen will open a context menu.\r\n" +
                "3. The user may load an image stored in the project directory and then rotate that image.\r\n" +
                "4. Left mouse click on the main screen to draw in black.\r\n" +
                "5. Mouse wheeling will zoom in/out of the drawing surface.\r\n" +
                "6. Holding down the \"w\" key and left clicking off-center will pan in that direction.\r\n" +
                "7. This info window will reopen from the context menu via the \"help\" menu item.\r\n";

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
            this.textBox1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Text = instructiveText;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 426);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.textBox1.Font = new System.Drawing.Font("Arial", 20.0f);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textBox1;
    }
}