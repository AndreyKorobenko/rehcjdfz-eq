using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач
{
    public partial class Form1 : Form
    {

        public string sql = "SELECT Products.Id, Products.NameProducts, Types.NameType, Products.ProductCharacteristics, Products.PriceProduct FROM Products INNER JOIN Types ON Products.TypeProductsID = Types.Id;";
        DataSet ds;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProducts addProducts = new AddProducts();
            addProducts.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ds = Tables.updateData(sql, dataGridView1);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ds = Tables.updateData(sql, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tables.delRow(dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tables.saveTable(sql, ds, dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchProduct searchProduct = new SearchProduct();
            searchProduct.ShowDialog();
            string sqlSearch = searchProduct.sql;
            Tables.updateData(sqlSearch, dataGridView1);
            
        }
    }

    public static class Tables
    {

        static SqlDataAdapter adapter;
        static SqlCommandBuilder commandBuilder;
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        public static int userId;

        public static DataSet updateData(String text, DataGridView dataGridView) //Обнова датагрид
        {
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(text, connection);

                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView.Columns["Id"].ReadOnly = true;
                connection.Close();
                return ds;
            }
        }

        public static DataSet getDataSet(String text) //Получить чисто датасет без обновы
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(text, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        public static void addRow(DataSet ds) //Добавить строчку в датасет!
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        public static void delRow(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                dataGridView.Rows.Remove(row);
            }
        }

        public static void saveTable(String sqlThis, DataSet ds, DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                adapter = new SqlDataAdapter(sqlThis, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                ds.Clear();

            }
            updateData(sqlThis, dataGridView);
        }

        public static void saveTable(String sqlThis, DataSet ds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                adapter = new SqlDataAdapter(sqlThis, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                ds.Clear();
            }
        }
    }
}


