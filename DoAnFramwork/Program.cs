using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemberShip;
using Framework;

namespace DoAnFramwork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin(new DatabaseMSSQLConnection("SimpleDatabase", @"VIVAN\SQLEXPRESS"), new DefaultReadRole()));
        }
    }
}
