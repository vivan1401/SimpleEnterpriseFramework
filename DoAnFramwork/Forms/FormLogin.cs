using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework;
using MemberShip;

namespace DoAnFramwork
{
    public partial class FormLogin : Form
    {
        private DatabaseConnection db;
        private ReadRole readRole;

        public FormLogin(DatabaseConnection _db, ReadRole _readRole)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.db = _db;
            this.readRole = _readRole;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserMemberShipWithRole member = new UserMemberShipWithRole(textBoxUsername.Text, textBoxPassword.Text, this.readRole);

            if (member.validUser())
            {
                try
                {
                    FormMain formMain = new FormMain(FormType.Main, member, "main123", new Size(570, 345), db);
                    formMain.SetupForm();
                    formMain.Show();
                    formMain.Owner = this;
                    this.Hide();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
    }
}
