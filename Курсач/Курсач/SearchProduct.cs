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
    public partial class SearchProduct : Form
    {

        public string sql = "";
 
        public SearchProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlthis = $"SELECT  Products.NameProducts FROM Products WHERE(Products.NameProducts LIKE  \"{ textBox1.Text}\")";

            sql = sqlthis;
            this.Close();
            
        }
    }
}
