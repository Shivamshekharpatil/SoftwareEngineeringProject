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
    public partial class CategoryModuleForm : Form
    {
        // Database connection and command objects
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();

        // Constructor
        public CategoryModuleForm()
        {
            InitializeComponent();
        }

        // Event handler for the "Save" button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Prompt user for confirmation before saving a new category
                if (MessageBox.Show("Are you sure want to save this category?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // SQL query to insert a new category into the database
                    cm = new SqlCommand("INSERT INTO tbCategory(catName)VALUES(@catName)", con);
                    cm.Parameters.AddWithValue("@catName", txtCatName.Text);
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    // Display success message, and clear the input
                    MessageBox.Show("Category has been successfully saved.");
                    Clear();
                }



            }
            catch (Exception ex)
            {
                // Handle and show exceptions during the save process
                MessageBox.Show(ex.Message);
            }
        }
        // Method to clear input controls
        public void Clear()
        {
            txtCatName.Clear();
        }
        // Event handler for the "Update" button click
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Prompt user for confirmation before updating a category
                if (MessageBox.Show("Are you sure want to update this Category?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // SQL query to update an existing category in the database
                    cm = new SqlCommand("UPDATE tbCategory SET catname = @catname WHERE catid LIKE '" + lblCatId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@catname", txtCatName.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    // Display success message and close the form
                    MessageBox.Show("Category has been successfully Updated.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Handle and show exceptions during the update process
                MessageBox.Show(ex.Message);
            }
        }
        // Event handler for the "Cancel" button click
        private void button4_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Dispose();
        }

        // Event handler for the "Clear" button click
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear input controls and update button states
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
    }
}
