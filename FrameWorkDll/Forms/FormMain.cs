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
    public partial class FormMain : BaseForm
    {
        public List<Dictionary<string, string>> dataTable = new List<Dictionary<string, string>>();
        private FormAdd formAdd;
        private FormUpdate formUpdate;
        
        public FormMain(FormType formType, String formTitle, Size formSize, DatabaseConnection databaseConnection) : base(formType, formTitle, formSize, databaseConnection)
        {
            this.formAdd = new FormAdd(FormType.Add, "Màn hình thêm", new Size(570, 345), db);
            this.formAdd.SetupForm();
            this.formUpdate = new FormUpdate(FormType.Update, "Màn hình chỉnh sửa", new Size(570, 345), db);
            this.formUpdate.SetupForm();
            InitializeComponent();
        }

        protected override void LoadButtonsText()
        {
            this.btnAdd.Text = this.m_buttonsName[0];
            this.btnUpdate.Text = this.m_buttonsName[1];
            this.btnRemove.Text = this.m_buttonsName[2];
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            Load_ComboBoxTables();
            
            //Dictionary<string, string> row1 = new Dictionary<string, string>();
            //row1.Add("Tên", "Văn");
            //row1.Add("Id", "123");
            //row1.Add("Ngày sinh", "11/12/1913");
            //Dictionary<string, string> row2 = new Dictionary<string, string>();
            //row2.Add("Ngày sinh", "12/10/1914");
            //row2.Add("Tên", "Tin");
            //row2.Add("Id", "456");

            //dataTable.Add(row1);
            //dataTable.Add(row2);
            
        }

        //Xóa
        protected override void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            //dataTable.Remove(dataTable[listView1.SelectedIndices[0]]);
            //listView1.Items.Remove(listView1.SelectedItems[0]);
            if(db.delete(tables[cbChooseDataTable.SelectedIndex],dataTable[listView1.SelectedIndices[0]].Values.ToArray()) != 0)
            {
                feilds = db.getFields(cbChooseDataTable.Text);
                dataTable = db.readData(cbChooseDataTable.Text);
                LoadTable();
            }
            else
            {
                throw new Exception("Cannot remove data");
            }
        }

        //Sửa
        protected override void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            try
            {
                //FormUpdate formUpdate = new FormUpdate(FormType.Update, this.m_member, "update123", new Size(570, 345), db, cbChooseDataTable.SelectedIndex);
                formUpdate.SetCurrentTable(cbChooseDataTable.SelectedIndex);
                formUpdate.SetMemberShip(this.m_member);
                formUpdate.FormClosing += this.FormChild_FormClosing;
                //formUpdate.SetupForm();
                formUpdate.DataUpdate(dataTable[listView1.SelectedIndices[0]]);
                formUpdate.ShowDialog();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Thêm
        protected override void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //FormAdd formAdd = new FormAdd(FormType.Add, this.m_member, "add123", new Size(570, 345), db, cbChooseDataTable.SelectedIndex);
                formAdd.SetMemberShip(this.m_member);
                formAdd.SetCurrentTable(cbChooseDataTable.SelectedIndex);
                formAdd.FormClosing += this.FormChild_FormClosing;
                //formAdd.SetupForm();
                formAdd.ShowDialog();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form owner = this.Owner;
            owner.RemoveOwnedForm(this);
            //owner.Show();
            owner.Close();
        }

        private void Load_ComboBoxTables()
        {
            if (tables.Count() > 0)
            {
                for (int i = 0; i < tables.Count(); i++)
                    cbChooseDataTable.Items.Add(tables[i]);
                cbChooseDataTable.Text = tables[0];
            }
        }

        private void OnChangeTable(object sender, EventArgs e)
        {
            feilds = db.getFields(cbChooseDataTable.Text);
            dataTable = db.readData(cbChooseDataTable.Text);
            LoadTable();
        }

        private void LoadTable()
        {
            listView1.Clear();
            //Load cột
            foreach (KeyValuePair<string, Type> feild in feilds)
            {
                listView1.Columns.Add(feild.Key);// + " (" + feild.Value + ")");
            }

            //Load dòng
            for (int i = 0; i < dataTable.Count(); i++)
            {
                List<string> row = new List<string>();
                foreach (ColumnHeader header in listView1.Columns)
                {
                    if (dataTable[i].ContainsKey(header.Text))
                        row.Add(dataTable[i][header.Text]);
                    else
                        row.Add("");
                }
                ListViewItem item = new ListViewItem(row.ToArray());

                listView1.Items.Add(item);
            }

            //Auto resize
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void FormChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            feilds = db.getFields(cbChooseDataTable.Text);
            dataTable = db.readData(cbChooseDataTable.Text);
            LoadTable();
        }

        public void SetFormAdd(FormAdd _formAdd)
        {
            if(_formAdd != null)
            {
                this.formAdd = _formAdd;
                //this.formAdd.SetMemberShip(m_member);
            }
        }

        public void SetFormUpdate(FormUpdate _formUpdate)
        {
            if (_formUpdate != null)
            {
                this.formUpdate = _formUpdate;
                //this.formAdd.SetMemberShip(m_member);
            }
        }

        private void ListViewDoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            this.BtnUpdate_Click(sender, e);
        }
    }
}
