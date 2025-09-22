using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int panjang = 3;
            int lebar = 3;

            numericUpDown1.Minimum = 3;
            numericUpDown2.Minimum = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 3 || numericUpDown2.Value <3)
            {
                MessageBox.Show("Panjang dan lebar harus lebih dari 2","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            } else
            {
                int panjang = (int)numericUpDown1.Value;
                int lebar = (int)numericUpDown2.Value;
                TicTacToe ttt = new TicTacToe(panjang, lebar);
                this.Hide();
                ttt.ShowDialog();
                this.Show();
            }
        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
