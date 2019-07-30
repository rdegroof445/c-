using System.Drawing;
using System.Windows.Forms;

using System.Threading;
using System;

namespace BFSGraphical
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
            instance = this;

            this.test = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(12, 9);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(100, 23);
            this.test.TabIndex = 0;
            this.test.Text = "This is a test!!!!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(99, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 394);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;

            this.button1.Click += Form1.eventHandler;
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.test);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        class MethodData
        {

            public Panel panel1;

            public Graphics graphics;

        }

        class TestThreadStart
        {


            delegate void testDelegate(Graphics panelGraphics);

            public static void setColor(Object data)
            {

                MethodData methodData = (MethodData)data;

                methodData.graphics.FillRectangle(System.Drawing.Brushes.White, new Rectangle(0, 0, methodData.panel1.Width, methodData.panel1.Height));

                Console.WriteLine("Test Thread Has Executed!");

            }

            public static void testMethod()
            {

            }


        }

        
        #endregion

        private Label test;
        private Label label1;
        private Panel panel1;
        private static Form1 instance;

        public static Form1 getInstance()
        {

            return instance;

        }

        public static void eventHandler(object sender, System.EventArgs args)
        {

            Console.WriteLine("Button Has been clicked!");

            instance.runThread();

        }

        public void runThread()
        {

            ThreadStart threadStart = new ThreadStart(TestThreadStart.testMethod);

            MethodData methodData = new MethodData();

            methodData.panel1 = this.panel1;

            Graphics graphics = this.panel1.CreateGraphics();

            methodData.graphics = graphics;

            Thread testThread = new Thread(TestThreadStart.setColor);

            testThread.Start(methodData);

        }

        private Button button1;
    }

}

