using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
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
            try
            {
                DatabaseConnection databaseConnection = new DatabaseMSSQLConnection("SimpleDatabase", @"VIVAN\SQLEXPRESS");
                ReadRole readRole = new DefaultReadRoleSeparate();
                FormAdd myFormAdd = new FormAdd(FormType.Add,"My Form Add", new Size(570, 345), databaseConnection);
                FormUpdate myFormUpdate = new FormUpdate(FormType.Update, "My Form Update", new Size(570, 345), databaseConnection);
                myFormAdd.BackColor = Color.Aqua;
                myFormUpdate.BackColor = Color.Green;

                FormMain myFormMain = new FormMain(FormType.Main, "My Form Main", new Size(570, 345), databaseConnection);
                myFormAdd.SetupForm();
                myFormUpdate.SetupForm();
                myFormMain.SetFormAdd(myFormAdd);
                myFormMain.SetFormUpdate(myFormUpdate);
                FormLogin formLogin = new FormLogin(databaseConnection, readRole, myFormMain);
                Application.Run(formLogin);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
