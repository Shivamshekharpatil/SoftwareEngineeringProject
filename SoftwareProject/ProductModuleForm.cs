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
    // The ProductModuleForm class represents a form for managing product details.
    public partial class ProductModuleForm : Form
    {
        // SqlConnection object to connect to the database.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        // SqlCommand object for executing SQL queries.
        SqlCommand cm = new SqlCommand();
        // SqlDataReader object for reading data from the database.
        SqlDataReader dr;

        // Constructor for the ProductModuleForm.
        public ProductModuleForm()
        {
            // Initialize the form components and load categories.
            InitializeComponent();
            LoadCategory();
        }

        // Method to load product categories into the ComboBox.
        public void LoadCategory()
        {
            // Clear existing items in the ComboBox.
            comboCat.Items.Clear();

            // SQL query to select category names from the tbCategory table.
            cm = new SqlCommand("SELECT catname FROM tbCategory", con);
            // Open the database connection.
            con.Open();

            // Execute the query and read the results.
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                // Add category names to the ComboBox.
                comboCat.Items.Add(dr[0].ToString());
            }
            // Close the SqlDataReader and the database connection.
            dr.Close();
            con.Close();
        }
        // Event handler for the form's Close event.
        private void productModuleFormClose(object sender, EventArgs e)
        {
            // Dispose of the form.
            this.Dispose();
        }

        private void comboQty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct(pname,psname,pcategory,pdescription,pAddress,pphone)VALUES(@pname,@psname,@pcategory,@pdescription,@pAddress,@pphone)", con);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@psname", txtPComp.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    cm.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cm.Parameters.AddWithValue("@pAddress", txtPAdd.Text);
                    cm.Parameters.AddWithValue("@pphone", pphone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtPName.Clear();
            txtPComp.Clear();
            txtPAdd.Clear();
            txtPDes.Clear();
            pphone.Clear();
            comboCat.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this Product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET pname=@pname,psname=@psname,pcategory=@pcategory,pdescription=@pdescription,pAddress=@pAddress,pphone=@pphone WHERE pid LIKE '" + lblPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@psname", txtPComp.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    cm.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cm.Parameters.AddWithValue("@pAddress", txtPAdd.Text);
                    cm.Parameters.AddWithValue("@pphone", pphone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully Updated.");
                    this.Dispose();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProductModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void txtEid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
