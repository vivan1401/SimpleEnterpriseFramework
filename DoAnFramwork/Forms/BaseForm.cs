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
    public enum FormType
    {
        Main   = 1,
        Add    = 2,
        Update = 3
    };
    public partial class BaseForm : Form
    {
        public Dictionary<string, string> feilds;
        protected Size m_FormSize = new Size(0, 0);
        protected String m_FormTitle = "";
        protected FormType m_FormType = FormType.Main;
        protected String[] m_buttonsName = { "Thêm", "Cập nhật", "Xóa" };
        protected int[] m_roles = { 0,0,0,0};   // 1.Xem, 2.Thêm, 3.Xóa, 4.Sửa

        protected Button btnAdd = new Button();
        protected Button btnUpdate = new Button();
        protected Button btnRemove = new Button();

        public BaseForm() {}

        public BaseForm(FormType formType, int[] roles, String formTitle, Size formSize, String databaseConnection) : this()
        {
            m_FormType = formType;
            m_FormTitle = formTitle;
            m_FormSize = formSize;
            m_roles = roles;
            InitializeComponent();
        }

        public void SetupForm()
        {
            SetupUI();
            SetupData();
        }

        private void SetupUI()
        {
            LoadSize();
            LoadTitle();
            LoadButtons();
            LoadRoles();
            //LoadButtonsText();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SetupData()
        {
            LoadDBConnection();
        }

        protected virtual void LoadSize()
        {
            this.Size = m_FormSize;
        }

        protected virtual void LoadTitle()
        {
            this.Text = m_FormTitle;
        }

        protected virtual void LoadRoles()
        {
            this.btnAdd.Enabled = m_roles[1] == 1 ? true : false;
            this.btnUpdate.Enabled = m_roles[2] == 1 ? true : false;
            this.btnRemove.Enabled = m_roles[3] == 1 ? true : false;
        }

        protected virtual void LoadButtonsText()
        {
        }

        protected virtual void LoadButtons()
        {
            switch (m_FormType) {
                case FormType.Main:
                    addBtnAdd();
                    addBtnUpdate();
                    addBtnRemove();
                    break;
                case FormType.Add:
                    addBtnAdd();
                    break;
                case FormType.Update:
                    addBtnUpdate();
                    break;
                default:
                    return;
            }
            
        }

        protected virtual void addBtnAdd()
        {
            this.btnAdd.Location = new System.Drawing.Point(301, 269);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = m_buttonsName[0];
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.Controls.Add(this.btnAdd);
        }

        protected virtual void addBtnUpdate()
        {
            this.btnUpdate.Location = new System.Drawing.Point(382, 269);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = m_buttonsName[1];
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            this.Controls.Add(this.btnUpdate);
        }

        protected virtual void addBtnRemove()
        {
            this.btnRemove.Location = new System.Drawing.Point(463, 269);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnRemove.Text = m_buttonsName[2];
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            this.Controls.Add(this.btnRemove);
        }

        protected virtual void LoadDBConnection()
        {

        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            //Data test
            feilds = new Dictionary<string, string>();
            feilds.Add("Tên", "string");
            feilds.Add("Id", "number");
            feilds.Add("Ngày sinh", "Date");
        }

        protected virtual void BtnAdd_Click(object sender, EventArgs e) {}

        protected virtual void BtnUpdate_Click(object sender, EventArgs e) {}

        protected virtual void BtnRemove_Click(object sender, EventArgs e) {}
    }
}
