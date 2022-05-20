using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач
{
    public partial class AddProducts : Form
    {



        string sql = "SELECT * FROM Products";
        DataSet ds;
        public AddProducts()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ds = Tables.getDataSet(sql);
            Tables.addRow(ds);
            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][1] = textBox1.Text;
            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2] = comboBox1.Text;
            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][3] = richTextBox1.Text;
            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][4] = textBox2.Text;
            Tables.saveTable(sql, ds);
            this.Close();


        }

        private void AddProducts_Load(object sender, EventArgs e)
        {
        }
    }
}
