using System;
using System.Windows.Forms;

namespace common_compolet_pure
{
    // Точка входа в программу всегда в этом классе и называется Main()
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
