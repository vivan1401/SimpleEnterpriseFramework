using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemberShip;
using Framework;

namespace DoAnFramwork
{
    public partial class FormUpdate : BaseForm
    {
        private Dictionary<string, TextBox> listTextBox = new Dictionary<string, TextBox>();
        private Dictionary<string, string> dataDraw = new Dictionary<string, string>();
        private List<Control> listControl = new List<Control>();
        private int currentTable;

        public FormUpdate(FormType formType, UserMemberShipWithRole member, String formTitle, Size formSize, DatabaseConnection databaseConnection) : base(formType, member, formTitle, formSize, databaseConnection)
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
            foreach (Control control in listControl)
            {
                this.Controls.Remove(control);
            }

            listControl.Clear();
            listTextBox.Clear();

            int i = 0;
            foreach (KeyValuePair<string, Type> feild in feilds)
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

                listControl.Add(label);
                listControl.Add(textBox);
                if (!listTextBox.ContainsKey(feild.Key))
                    listTextBox.Add(feild.Key, textBox);

                i++;
            }
        }

        protected override void BtnUpdate_Click(object sender, EventArgs e)
        {
            List<string> text = new List<string>();
            foreach (KeyValuePair<string, Type> feild in feilds)
            {
                text.Add(listTextBox[feild.Key].Text);
            }

            if (db.update(tables[currentTable], dataDraw.Values.ToArray(),text.ToArray()) == 0)
            {
                throw new Exception("Cannot update data");
            }
            else
            {
                this.Close();
            }
            //MessageBox.Show(test);
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

        public void SetCurrentTable(int _currentTable)
        {
            this.currentTable = _currentTable;
            feilds = db.getFields(tables[currentTable]);
        }
    }
}
