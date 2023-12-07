using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareProject
{
    public partial class MainForm : Form
    {
        // Constructor for the MainForm
        public MainForm()
        {
            InitializeComponent();
        }

        // Method to show other forms within the MainForm
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            // Close any previously active form
            if (activeForm != null)
                activeForm.Close();
            // Set the new form as the active form
            activeForm = childForm;

            // Set form properties for integration into the panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the child form to the panelMain
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;

            // Bring the child form to the front and show it
            childForm.BringToFront();
            childForm.Show();
        }

        // Event handler for the "Users" button click
        private void btnUsers_Click(object sender, EventArgs e)
        {
            // Open the UserForm1 as a child form
            openChildForm(new UserForm1());
        }

        // Event handler for the "Categories" button click
        private void btnCategories_Click(object sender, EventArgs e)
        {
            // Open the CategoryForm as a child form
            openChildForm(new CategoryForm());
        }

        // Event handler for the "Product" button click
        private void btnProduct_Click(object sender, EventArgs e)
        {
            // Open the ProductForm as a child form
            openChildForm(new ProductForm());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }


        // Event handler for the logout button (pictureBox1) click
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before logging out
            if (MessageBox.Show("Are you sure want to Logout?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Create a new instance of the LoginForm
                LoginForm main = new LoginForm();

                // Hide the current MainForm
                this.Hide();
                // Show the LoginForm
                main.ShowDialog();
            }
            
        }

        // Event handler for the home button (pictureBox2) click
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the MainForm
            MainForm main = new MainForm();
            // Hide the current MainForm
            this.Hide();
            // Show the MainForm
            main.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
