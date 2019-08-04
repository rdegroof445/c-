using System.Drawing;
using System.Windows.Forms;

using System.Threading;
using System;
using System.Collections.Generic;
using static BFSGraphical.BFS;

namespace BFSGraphical
{
    class Form2 : Form
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

        private Form2()
        {

            InitializeComponent();

        }

        BFS.DisplayTree displayTree = null;

        protected override void OnShown(EventArgs e)
        {

            Console.WriteLine("My Form Has Been Shown!!!!");

            base.OnShown(e);

            BFS bfs = new BFS(this.Width - 150, this.Height - 100, 20);

            displayTree = bfs.runSearch1();

            instance.runThread(displayTree);

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Console.WriteLine("My Form Has Been OnPainted!!!!");

            base.OnPaint(e);

            instance.runThread(displayTree);

        }

        
        protected override void OnResizeEnd(EventArgs e)
        {

            Console.WriteLine("My Form Has Been Resized!!!!");

            base.OnResize(e);

            int testNewWidth = this.Width - 150;

            int testNewHeight = this.Height - 100;

            instance.ClearCanvas();

            if (this.panel1.Width != testNewWidth || this.panel1.Height != testNewHeight)
            {

                this.panel1.Width = testNewWidth;

                this.panel1.Height = testNewHeight;

                BFS bfs = new BFS(testNewWidth, testNewHeight, 20);

                displayTree = bfs.runSearch1();

                instance.runThread(displayTree);

            }
            else
            {

                instance.runThread(displayTree);

            }

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

            this.button1.Click += Form2.eventHandler;

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

            Console.WriteLine("Finished Initializing Component!!!");

        }

        class MethodData
        {

            public Panel panel1;

            public BFS.DisplayTree displayTree;

            public MethodData(BFS.DisplayTree newDisplayTree, Panel newPanel1)
            {

                displayTree = newDisplayTree;

                panel1 = newPanel1;

            }

            public MethodData(Panel newPanel1)
            {

                panel1 = newPanel1;

            }

        }

        class TestThreadStart
        {


            delegate void testDelegate(Graphics panelGraphics);

            public static void setColor(Object data)
            {

                
                MethodData methodData = (MethodData)data;

                Graphics graphics = methodData.panel1.CreateGraphics();

                graphics.FillRectangle(System.Drawing.Brushes.White, new Rectangle(0, 0, methodData.panel1.Width, methodData.panel1.Height));

                // graphics.DrawEllipse(new Pen(Color.Black), new Rectangle(50, 50, 20, 20));

                BFS.DisplayTree displayTree = (BFS.DisplayTree)methodData.displayTree;

                if(displayTree != null)
                {

                    int displayTreeNodeCount = displayTree.GetDisplayNodeCount();

                    for (int nodeCounter = 0; nodeCounter < displayTreeNodeCount; nodeCounter++)
                    {

                        // Console.WriteLine("Display Tree First Node X Value: " + displayTree.getDisplayNodeAt(nodeCounter ).GetDisplayStr());

                        graphics.DrawEllipse(new Pen(Color.Black), new Rectangle(displayTree.getDisplayNodeAt(nodeCounter).GetX(), displayTree.getDisplayNodeAt(nodeCounter).GetY(), 20, 20));

                        if (displayTree.getDisplayNodeAt(nodeCounter).GetParent() != null)
                        {

                            graphics.DrawLine(new Pen(Color.Black), displayTree.getDisplayNodeAt(nodeCounter).GetX(), displayTree.getDisplayNodeAt(nodeCounter).GetY(), displayTree.getDisplayNodeAt(nodeCounter).GetParent().GetX(), displayTree.getDisplayNodeAt(nodeCounter).GetParent().GetY());

                        }

                    }

                }

                // Console.WriteLine("Display Tree First Node X Value: " + displayTree.getDisplayNodeAt(0).GetDisplayStr() );

                // Console.WriteLine("Test Thread Has Executed!");

            }

            public static void ClearCanvas(Object data)
            {

                Panel panel1 = ((MethodData)data).panel1;

                Graphics graphics = panel1.CreateGraphics();

                graphics.FillRectangle(System.Drawing.Brushes.White, new Rectangle(0, 0, panel1.Width, panel1.Height));

            }

            public static void testMethod()
            {

            }


        }


        #endregion

        private Label test;
        private Label label1;
        private Panel panel1;
        private static Form2 instance = new Form2();

        public static Form2 getInstance()
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

            MethodData methodData = new MethodData(this.panel1);

            Thread testThread = new Thread(TestThreadStart.setColor);

            testThread.Start(methodData);

        }

        public void ClearCanvas()
        {

            MethodData methodData = new MethodData(this.panel1);

            Thread testThread = new Thread(TestThreadStart.ClearCanvas);

            testThread.Start(methodData);

            testThread.Join();

        }

        public void runThread(BFS.DisplayTree displayTree)
        {

            MethodData methodData = new MethodData(displayTree, this.panel1);

            Thread testThread = new Thread(TestThreadStart.setColor);

            testThread.Start(methodData);

            testThread.Join();

        }

        private Button button1;
    }

}

