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

namespace SoftwareProject
{
    // The ProductForm class represents a form for managing products.
    public partial class ProductForm : Form
    {
        // SqlConnection object to connect to the database.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        // SqlCommand object for executing SQL queries.
        SqlCommand cm = new SqlCommand();
        // SqlDataReader object for reading data from the database.
        SqlDataReader dr;

        // Constructor for the ProductForm.
        public ProductForm()
        {
            // Initialize the form components and load products.
            InitializeComponent();
            LoadProduct();
        }

        
        // Method to load products into the DataGridView.
        public void LoadProduct()
        {
            int i = 0;
            // Clear the DataGridView rows.
            dgvProduct.Rows.Clear();
            // SQL query to select products based on search criteria.
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, psname, pcategory, pdescription,pAddress,pphone) LIKE '%"+txtSearch.Text+"%'", con);
            // Open the database connection.
            con.Open();
            // Execute the query and read the results.
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                // Add product information to the DataGridView.
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            // Close the SqlDataReader and the database connection.
            dr.Close();
            con.Close();
        }
        // Event handler for the "Add" button's Click event.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open the ProductModuleForm for adding a new product.
            ProductModuleForm formModule = new ProductModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            // Reload products after adding a new one.
            LoadProduct();
        }

        // Event handler for the DataGridView's CellContentClick event.
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                // Open the ProductModuleForm for editing a product.
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.txtPComp.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.comboCat.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.txtPDes.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.txtPAdd.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                productModule.pphone.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();


                productModule.btnSave.Enabled = false;
                productModule.btnUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                // Confirm and delete the selected product.
                if (MessageBox.Show("Are you sure want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            // Reload products after editing or deleting.
            LoadProduct();
        }
        // Event handler for the search TextBox's TextChanged event.
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Reload products based on the updated search criteria.
            LoadProduct();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
