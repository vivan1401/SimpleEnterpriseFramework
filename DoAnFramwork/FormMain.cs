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
    public partial class FormMain : BaseForm
    {
        public List<Dictionary<string, string>> dataTable = new List<Dictionary<string, string>>();
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> row1 = new Dictionary<string, string>();
            row1.Add("Tên", "Văn");
            row1.Add("Id", "123");
            row1.Add("Ngày sinh", "11/12/1913");
            Dictionary<string, string> row2 = new Dictionary<string, string>();
            row2.Add("Ngày sinh", "12/10/1914");
            row2.Add("Tên", "Tin");
            row2.Add("Id", "456");

            dataTable.Add(row1);
            dataTable.Add(row2);

            //Load cột
            foreach (KeyValuePair<string, string> feild in feilds)
            {
                listView1.Columns.Add(feild.Key);// + " (" + feild.Value + ")");
            }

            //Load dòng
            for(int i = 0; i < dataTable.Count(); i++)
            {
                List<string> row = new List<string>();
                foreach (ColumnHeader header in listView1.Columns)
                {
                    row.Add(dataTable[i][header.Text]);
                }
                ListViewItem item = new ListViewItem(row.ToArray());

                listView1.Items.Add(item);
            }

            //Auto resize
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        //Xóa
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            dataTable.Remove(dataTable[listView1.SelectedIndices[0]]);
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }

        //Sửa
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            FormUpdate formUpdate = new FormUpdate();
            formUpdate.DataUpdate(dataTable[listView1.SelectedIndices[0]]);
            formUpdate.ShowDialog();
        }

        //Thêm
        private void button3_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new FormAdd();
            formAdd.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form owner = this.Owner;
            owner.RemoveOwnedForm(this);
            owner.Close();
        }
    }
}
