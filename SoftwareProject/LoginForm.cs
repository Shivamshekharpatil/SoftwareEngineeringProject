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
    // The LoginForm class represents a form for user authentication.
    public partial class LoginForm : Form
    {
        // SqlConnection object to connect to the database.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\patil\OneDrive\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        // SqlCommand object for executing SQL queries.
        SqlCommand cm = new SqlCommand();
        // SqlDataReader object for reading data from the database.
        SqlDataReader dr;

        // Constructor for the LoginForm.
        public LoginForm()
        {
            // Initialize the form components.
            InitializeComponent();
        }
        // Event handler for the checkbox's CheckedChanged event.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle the password visibility based on the checkbox state.
            if (checkBox1.Checked == false)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }
        // Event handler for the label's Click event.
        private void label4_Click(object sender, EventArgs e)
        {
            // Clear the contents of username and password textboxes.
            textBox1.Clear();
            textBox2.Clear();
        }
        // Event handler for the "Exit" button's Click event.
        private void button2_Click(object sender, EventArgs e)
        {
            // Display a confirmation dialog before exiting the application.
            if (MessageBox.Show("Exit Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
                //gives user an option of Yes or no before exiting the application
            {
                // Exit the application if the user chooses "Yes".
                Application.Exit();
            }
        }
        // Event handler for the "Login" button's Click event.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL query to check the entered username and password in the database.
                cm = new SqlCommand("SELECT * FROM tbUser WHERE username=@username AND password=@password", con);
                cm.Parameters.AddWithValue("@username", textBox1.Text);
                cm.Parameters.AddWithValue("@password", textBox2.Text);
                // Open the database connection.
                con.Open();
                // Execute the query and read the result.
                dr = cm.ExecuteReader();
                dr.Read();

                // Check if the query returned any rows.
                if (dr.HasRows)
                {
                    // Display a welcome message and open the main form.
                    MessageBox.Show("Welcome " + dr["fullname"].ToString() + " | ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main = new MainForm();
                    // Disable the "Users" button for non-admin users.
                    if (textBox1.Text != "admin@Citisoft.uk")
                        main.btnUsers.Enabled = false;
                    // Hide the login form and show the main form.
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    // Display an error message for invalid username or password.
                    MessageBox.Show("Invalid username or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Close the database connection.
                con.Close();

            }catch(Exception ex)
            {
                // Display an error message for any exception that occurs.
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
