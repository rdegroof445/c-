using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BFSGraphical
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            //Form2 form1 = new Form2();

            Console.WriteLine("Before Run Application");

            Thread testThread = new Thread(TestThreadStart.runApplication);

            //testThread.Start();

             Application.Run(Form2.getInstance());

            Console.WriteLine("Past Run Application");

            //Form2.getInstance().runThread();

            // BFS bfs = new BFS(500, 500, 20);

            // bfs.runSearch1();

        }

        class TestThreadStart
        {


            delegate void testDelegate();

            public static void runApplication()
            {

                Application.Run(Form2.getInstance());

                Console.WriteLine("Run Application Has Executed!");

            }

            public static void testMethod()
            {

            }


        }

        public void runThread()
        {
            /*
            ThreadStart threadStart = new ThreadStart(TestThreadStart.testMethod);

            MethodData methodData = new MethodData();

            methodData.panel1 = this.panel1;

            Graphics graphics = this.panel1.CreateGraphics();

            methodData.graphics = graphics;

            Thread testThread = new Thread(TestThreadStart.setColor);

            testThread.Start(methodData);
            */

        }

    }
    
}
