using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SoftwareProject
{
    // The UserModuleForm class represents a form for managing user details.
    public partial class UserModuleForm : Form
    {
        // SqlConnection object to connect to the database.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        // SqlCommand object for executing SQL queries.
        SqlCommand cm = new SqlCommand();

        // Constructor for the UserModuleForm.
        public UserModuleForm()
        {
            // Initialize the form components.
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Dispose of the form.
            this.Dispose();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the passwords match
                if (txtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // If passwords match, ask for confirmation
                if (MessageBox.Show("Are you sure want to save this user?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Create a SQL INSERT command
                    cm = new SqlCommand("INSERT INTO tbUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)", con);
                    cm.Parameters.AddWithValue("@username", txtUserName.Text);
                    cm.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    con.Open();
                    // Execute the INSERT command
                    cm.ExecuteNonQuery();
                    con.Close();
                    // Display success message
                    MessageBox.Show("User has been successfully saved.");
                    Clear();
                }
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        // Method to clear text fields
        public void Clear()
        {
            txtUserName.Clear();
            txtFullName.Clear();
            txtPass.Clear();
            txtRepass.Clear();
            txtPhone.Clear();
        }

        // Event handler for the update button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the passwords match
                if (txtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // If passwords match, ask for confirmation
                if (MessageBox.Show("Are you sure want to update this user?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Create a SQL UPDATE command
                    cm = new SqlCommand("UPDATE tbUser SET username=@username,fullname=@fullname,password=@password,phone=@phone WHERE username LIKE '" + txtUserName.Text + "' ", con);
                    cm.Parameters.AddWithValue("@username", txtUserName.Text);
                    cm.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    con.Open();
                    // Execute the UPDATE command
                    cm.ExecuteNonQuery();
                    con.Close();
                    // Display success message
                    MessageBox.Show("User has been successfully Updated.");
                    this.Dispose();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
