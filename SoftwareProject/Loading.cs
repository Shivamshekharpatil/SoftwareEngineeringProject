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
    // The Loading class represents a form that displays a progress bar while loading.
    public partial class Loading : Form
    {
        // Constructor for the Loading form.
        public Loading()
        {
            // Initialize the form components.
            InitializeComponent();
            timer1.Start();
        }
        // Variable to track the progress bar's starting point.
        int startPoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Increment the starting point for the progress bar
            startPoint += 2;

            // Set the progress bar value to the updated starting point.
            progressBar1.Value = startPoint;
            // Check if the progress bar has reached 100%.
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();

                // Create an instance of the LoginForm.
                LoginForm login = new LoginForm();
                this.Hide();
                // Show the login form.
                login.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
