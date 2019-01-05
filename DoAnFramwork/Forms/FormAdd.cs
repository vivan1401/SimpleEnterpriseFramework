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
    public partial class FormAdd : BaseForm
    {
        private Dictionary<string,TextBox> listTextBox = new Dictionary<string,TextBox>();
        private List<Control> listControl = new List<Control>();
        private int currentTable;

        public FormAdd(FormType formType, UserMemberShipWithRole member, String formTitle, Size formSize, DatabaseConnection databaseConnection) : base(formType, member, formTitle, formSize, databaseConnection)
        {
            InitializeComponent();
        }

        protected override void LoadTitle()
        {
            this.Text = "Màn hình thêm";
        }

        protected override void LoadButtonsText()
        {
            this.btnAdd.Text = this.m_buttonsName[0];
        }


        private void FormAdd_Load(object sender, EventArgs e)
        {
            CreateLabel();
        }

        //Tạo các ô nhập dựa theo feilds
        private void CreateLabel()
        {
            foreach(Control control in listControl)
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
                label.Size = new Size(100,20);
                label.Location = new Point(20, 20 + i * 40);
                label.Parent = this;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Size = new Size(300, 20);
                textBox.Location = new Point(230, 20 + i * 40);
                textBox.Parent = this;
                this.Controls.Add(textBox);

                listControl.Add(label);
                listControl.Add(textBox);
                if(!listTextBox.ContainsKey(feild.Key))
                    listTextBox.Add(feild.Key,textBox);

                i++;
            }
        }

        protected override void BtnAdd_Click(object sender, EventArgs e)
        {
            List<string> text = new List<string>();
            
            foreach (KeyValuePair<string, Type> feild in feilds)
            {
                text.Add(listTextBox[feild.Key].Text);
            }

            if(db.insert(tables[currentTable], text.ToArray()) == 0)
            {
                throw new Exception("Cannot insert data");
            }
            else
            {
                this.Close();
            }
            //MessageBox.Show(test);
        }

        protected override void addBtnAdd()
        {
            base.addBtnAdd();
            this.btnAdd.Location = new System.Drawing.Point(463, 269);
        }

        public void SetCurrentTable(int _currentTable)
        {
            this.currentTable = _currentTable;
            feilds = db.getFields(tables[currentTable]);
        }
    }
}
