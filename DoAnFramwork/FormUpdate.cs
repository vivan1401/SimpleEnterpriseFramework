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
    public partial class FormUpdate : BaseForm
    {
        private Dictionary<string, TextBox> listTextBox = new Dictionary<string, TextBox>();
        private Dictionary<string, string> dataDraw = new Dictionary<string, string>();

        public FormUpdate(FormType formType, String formTitle, Size formSize, String databaseConnection) : base(formType, formTitle, formSize, databaseConnection)
        {
            InitializeComponent();
        }

        protected override void LoadTitle()
        {
            this.Text = "Màn hình cập nhật";
        }

        protected override void LoadButtonsText()
        {
            this.btnUpdate.Text = this.m_buttonsName[1];
        }


        //Load dataDraw từ formMain
        public void DataUpdate(Dictionary<string,string> _data)
        {
            dataDraw = _data;
        }

        //Tạo các ô nhập dựa theo feilds
        public void CreateLabel()
        {
            int i = 0;
            foreach (KeyValuePair<string, string> feild in feilds)
            {
                Label label = new Label();
                label.Text = feild.Key;
                label.Size = new Size(100, 20);
                label.Location = new Point(20, 20 + i * 40);
                label.Parent = this;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                //Load dữ liệu từ dataDraw, bên formAdd ko có dòng này
                if(dataDraw.ContainsKey(feild.Key))
                    textBox.Text = dataDraw[feild.Key];
                textBox.Size = new Size(300, 20);
                textBox.Location = new Point(230, 20 + i * 40);
                textBox.Parent = this;
                this.Controls.Add(textBox);

                listTextBox.Add(feild.Key, textBox);

                i++;
            }
        }

        protected override void BtnUpdate_Click(object sender, EventArgs e)
        {
            string test = "";
            foreach (KeyValuePair<string, string> feild in feilds)
            {
                test += listTextBox[feild.Key].Text + " ; ";
            }
            MessageBox.Show(test);
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            CreateLabel();
        }

        protected override void addBtnUpdate()
        {
            base.addBtnUpdate();
            this.btnUpdate.Location = new System.Drawing.Point(463, 269);
        }
    }
}
