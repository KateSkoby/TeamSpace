using System;
using System.Windows.Forms;
using BirthsOutOfWedlockApp.Forms;

namespace BirthsOutOfWedlockApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}