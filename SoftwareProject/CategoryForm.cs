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
    public partial class CategoryForm : Form
    {
        // Database connection and command objects
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            // Load category data when the form is initialized
            InitializeComponent();
            LoadCategory();
        }

        // Method to load category data into the DataGridView
        public void LoadCategory()
        {
            int i = 0;
            // Clear existing rows in the DataGridView
            dgvCategory.Rows.Clear();
            // Construct and execute SQL query to retrieve category data
            cm = new SqlCommand("SELECT * FROM tbCategory WHERE CONCAT(catid, catName) LIKE '%"+txtcatSearch.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                // Add category data to the DataGridView
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        // Event handler for the "Add" button click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open a new instance of the CategoryModuleForm for adding a new category
            CategoryModuleForm formModule = new CategoryModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            // Reload category data after adding a new category
            LoadCategory();
        }

        // Event handler for DataGridView cell content click
        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                // Open a new instance of CategoryModuleForm for editing a category
                CategoryModuleForm formModule = new CategoryModuleForm();
                formModule.lblCatId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                formModule.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                formModule.btnSave.Enabled = false;
                formModule.btnUpdate.Enabled = true;
                formModule.ShowDialog();

            }
            else if (colName == "Delete")
            {
                // Prompt user for confirmation before deleting a category
                if (MessageBox.Show("Are you sure want to delete this Category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Execute SQL query to delete the selected category
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE catid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            // Reload category data after editing or deleting a category
            LoadCategory();
        }

        // Event handler for the search textbox text changed
        private void txtcatSearch_TextChanged(object sender, EventArgs e)
        {
            // Reload category data based on the search criteria
            LoadCategory();
        }
    }
}
