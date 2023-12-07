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
    // The UserForm1 class represents a form for managing user information.
    public partial class UserForm1 : Form
    {
        // SqlConnection object to connect to the database.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        // SqlCommand object for executing SQL queries.
        SqlCommand cm = new SqlCommand();
        // SqlDataReader object for reading data from the database.
        SqlDataReader dr;

        // Constructor for the UserForm1.

        public UserForm1()
        {
            // Initialize the form components and load user information.
            InitializeComponent();
            LoadUser();
        }

        // Method to load users into the DataGridView.
        public void LoadUser()
        {
            int i = 0;
            // Clear the DataGridView rows.
            dgvUser.Rows.Clear();
            // SQL query to select users based on search criteria.
            cm = new SqlCommand("SELECT * FROM tbUser WHERE CONCAT(username,fullname,password,phone) LIKE '%"+txtSearchUser.Text+"%'", con);
            // Open the database connection.
            con.Open();
            // Execute the query and read the results.
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                // Add user information to the DataGridView.
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            // Close the SqlDataReader and the database connection.
            dr.Close();
            con.Close();
        }

        // Event handler for the "Add" button's Click event.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open the UserModuleForm for adding a new user.
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
            userModule.ShowDialog();
            // Reload users after adding a new one.
            LoadUser();
        }

        // Event handler for the DataGridView's CellContentClick event.
        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                // Open the UserModuleForm for editing a user.
                UserModuleForm userModule = new UserModuleForm();
                userModule.txtUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled=false;
                userModule.btnUpdate.Enabled=true;
                userModule.txtUserName.Enabled = false;
                userModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                // Confirm and delete the selected user.
                if (MessageBox.Show("Are you sure want to delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            // Reload users after editing or deleting.
            LoadUser();
        }

        // Event handler for the search TextBox's TextChanged event.
        private void txtSearchUser_TextChanged(object sender, EventArgs e)
        {
            // Reload users based on the updated search criteria.
            LoadUser();
        }
    }
}
