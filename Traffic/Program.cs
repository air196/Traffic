using System;
using System.Windows.Forms;

namespace Traffic
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        // Пример входного файла
        /*
        2
        4
        10:00 12:00
        10 20
        2 1 3 5 7
        3 1 2 4 10 5 20
        */

        /*
        3
        6
        10:00 12:00 11:00
        10 20 10
        2 1 3 5 7
        3 1 2 4 10 5 20
        2 5 6 11 11
        */

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
