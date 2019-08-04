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

            Console.WriteLine("Before Run Application");

            Thread testThread = new Thread(TestThreadStart.runApplication);

            Application.Run(Form2.getInstance());

            Console.WriteLine("Past Run Application");

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
            


        }

    }
    
}
