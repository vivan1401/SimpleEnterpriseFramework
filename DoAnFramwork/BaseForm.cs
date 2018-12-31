using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnFramwork
{
    public partial class BaseForm : Form
    {
        public Dictionary<string, string> feilds;
        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            //Data test
            feilds = new Dictionary<string, string>();
            feilds.Add("Tên", "string");
            feilds.Add("Id", "number");
            feilds.Add("Ngày sinh", "Date");
        }
    }
}
