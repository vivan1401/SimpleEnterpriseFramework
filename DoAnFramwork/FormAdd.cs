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
    public partial class FormAdd : BaseForm
    {
        Dictionary<string,TextBox> listTextBox = new Dictionary<string,TextBox>();

        public FormAdd()
        {
            InitializeComponent();
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            CreateLabel();
        }

        //Tạo các ô nhập dựa theo feilds
        private void CreateLabel()
        {
            int i = 0;
            foreach (KeyValuePair<string, string> feild in feilds)
            {
                Label label = new Label();
                label.Text = feild.Key;
                label.Size = new Size(100,20);
                label.Location = new Point(20, 20 + i * 40);
                label.Parent = this;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Size = new Size(300, 20);
                textBox.Location = new Point(230, 20 + i * 40);
                textBox.Parent = this;
                this.Controls.Add(textBox);

                listTextBox.Add(feild.Key,textBox);

                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string test = "";
            foreach (KeyValuePair<string, string> feild in feilds)
            {
                test += listTextBox[feild.Key].Text+ " ; ";
            }
            MessageBox.Show(test);
        }
    }
}
